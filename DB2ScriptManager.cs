using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPPlusLicenseContext = OfficeOpenXml.LicenseContext;

namespace DatabaseManager
{
    public partial class DB2ScriptManager : Form
    {
        private DB2Manager _dbManager = new DB2Manager();
        private ScriptProcessor _scriptProcessor = new ScriptProcessor();
        //private string _currentDatabase;
        private DataTable excelDataTable;
        public DB2ScriptManager()
        {

            InitializeComponent();
            SetupWorkflowTabs();

            excelDataTable = new DataTable();
            if (!dataGridView1.IsDoubleBuffered())
            {
                dataGridView1.EnableDoubleBuffering();
            }
        }



        private void SetupWorkflowTabs()
        {
            // Don't clear existing tabs - just configure appearance
            tabControlWorkflow.Appearance = TabAppearance.Normal;
            tabControlWorkflow.SizeMode = TabSizeMode.FillToRight;

            // Just ensure the tabs are in the correct order
            if (tabControlWorkflow.TabPages.Count == 0)
            {
                // Only initialize if empty (shouldn't happen if designed properly)
                tabControlWorkflow.TabPages.AddRange(new[] 
                {
                    // Use the designer-created instances
                    tabPageConnect,  
                    tabPageData,
                    tabPageComplete,
                    tabPageExcelToSQL
                });
            }

            MoveToStep(0);
        }

        private void MoveToStep(int stepIndex)
        {
            if (stepIndex >= 0 && stepIndex < tabControlWorkflow.TabPages.Count)
            {
                tabControlWorkflow.SelectedIndex = stepIndex;
                UpdateNavigationButtons();
            }
        }

        private void UpdateNavigationButtons()
        {
            btnPrevious.Enabled = tabControlWorkflow.SelectedIndex > 0;
            btnNext.Enabled = tabControlWorkflow.SelectedIndex < tabControlWorkflow.TabPages.Count - 1;

            // Update next button text on last step
            btnNext.Text = tabControlWorkflow.SelectedIndex == tabControlWorkflow.TabPages.Count - 1
                ? "Finish" : "Next";
        }



        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string server = txtServer.Text.Trim();
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;
                string selectedDb = cmbDatabase.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(server) ||
                    string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(selectedDb))
                {
                    MessageBox.Show("Please fill in all fields, including selecting a database.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _dbManager.Connect(server, username, password, selectedDb);
                lblStatus.Text = $"Connected to {selectedDb} at {server}";
                MoveToStep(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed:\n\n{ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





       


        private void btnLoadDataScript_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtDataScript.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }


        private async void btnExecuteDataMarquee_Click(object sender, EventArgs e)
        {

            try
            {
                progressBar1.Visible = true;
                btnExecuteData.Enabled = false;

                var result = await Task.Run(() =>
                {
                    return _scriptProcessor.ExecuteScript(_dbManager, txtDataScript.Text, true);
                });

                ShowExecutionResult(result);

                if (result.IsSuccess)
                {
                    MoveToStep(5); // Move to Views step
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Execution failed: " + ex.Message);
            }
            finally
            {
                progressBar1.Visible = false;
                btnExecuteData.Enabled = true;
            }

        }


        private async void btnExecuteDatanoskip_Click(object sender, EventArgs e)
        {
            var result = await ExecuteScriptWithProgress(
                txtDataScript.Text,
                progressBar1,
                btnExecuteData,
                lblStatusData);

            ShowExecutionResult(result);
            if (result.IsSuccess)
            {
                MoveToStep(5);
            }
        }
        private async void btnExecuteData_Click(object sender, EventArgs e)
        {
            var result = await ExecuteScriptWithProgress(
                scriptContent: txtDataScript.Text,
                progressControl: progressBar1,
                executeButton: btnExecuteData,
                statusLabel: lblStatusData,
                skipDropErrors: true,
                useTransaction: true);

            if (result.IsSuccess || result.FailedStatements == 0)
            {
                MessageBox.Show($"Execution completed. Successful: {result.SuccessfulStatements}, Skipped: {result.SkippedStatements}");
                MoveToStep(2);
            }
            else
            {
                MessageBox.Show($"Execution failed after {result.SuccessfulStatements} statements");
            }
        }

        private async Task<ScriptExecutionResult> ExecuteScriptWithProgressold(
        string scriptContent,
        Control progressControl,
        Button executeButton,
        Label statusLabel = null)
        {
            try
            {
                executeButton.Enabled = false;
                progressControl.Visible = true;

                if (progressControl is ProgressBar progressBar)
                {
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = 0;
                }

                // Get statements first to set progress max
                var statements = _scriptProcessor.SplitStatements(scriptContent);

                if (progressControl is ProgressBar progressBar1)
                {
                    progressBar1.Maximum = statements.Count;
                }

                int totalStatements = statements.Count;

                var result = await Task.Run(() =>
                {
                    var executionResult = new ScriptExecutionResult();

                    try
                    {
                        if (_dbManager.IsInTransaction)
                        {
                            _dbManager.RollbackTransaction();
                        }

                        _dbManager.BeginTransaction();

                        for (int i = 0; i < statements.Count; i++)
                        {
                            string statement = statements[i];
                            int remaining = totalStatements - i - 1;

                            try
                            {
                                int rowsAffected = _dbManager.ExecuteNonQuery(statement);
                                executionResult.SuccessfulStatements++;
                                executionResult.RowsAffected += rowsAffected;
                            }
                            catch (Exception ex)
                            {
                                executionResult.FailedStatements++;
                                executionResult.Errors.Add(new ScriptError
                                {
                                    Statement = statement,
                                    ErrorMessage = ex.Message
                                });
                                _dbManager.RollbackTransaction();
                                executionResult.WasRolledBack = true;
                                executionResult.IsSuccess = false;

                                    // Update UI
                                    UpdateProgressUI(progressControl, statusLabel, i + 1, remaining, true);
                                return executionResult;
                            }

                                // Update progress and countdown
                                UpdateProgressUI(progressControl, statusLabel, i + 1, remaining);
                        }

                        _dbManager.CommitTransaction();
                        executionResult.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        if (_dbManager.IsInTransaction)
                        {
                            _dbManager.RollbackTransaction();
                            executionResult.WasRolledBack = true;
                        }
                        executionResult.IsSuccess = false;
                        executionResult.Errors.Add(new ScriptError
                        {
                            ErrorMessage = $"Fatal error: {ex.Message}"
                        });
                    }

                    return executionResult;
                });

                return result;
            }
            finally
            {
                if (progressControl != null)
                {
                    progressControl.Visible = false;
                }
                executeButton.Enabled = true;
            }
        }

        private void UpdateProgressUIold(Control progressControl, Label statusLabel, int progress, int remaining, bool failed = false)
        {
            if (progressControl.InvokeRequired || (statusLabel?.InvokeRequired ?? false))
            {
                Invoke((MethodInvoker)delegate
                {
                    UpdateProgressUI(progressControl, statusLabel, progress, remaining, failed);
                });
                return;
            }

            if (progressControl is ProgressBar progressBar)
            {
                progressBar.Value = progress;
            }

            if (statusLabel != null)
            {
                statusLabel.Text = failed
                    ? $"{remaining} statements remaining (failed)"
                    : $"{remaining} statements remaining";
            }
        }



        private async Task<ScriptExecutionResult> ExecuteScriptWithProgress(
        string scriptContent,
        Control progressControl,
        Button executeButton,
        Label statusLabel = null,
        bool skipDropErrors = true,
        bool useTransaction = true)
        {
            try
            {
                executeButton.Enabled = false;
                progressControl.Visible = true;

                if (progressControl is ProgressBar progressBar)
                {
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = 0;
                }

                // Split and prepare statements
                var statements = _scriptProcessor.SplitStatements(scriptContent);
                int totalStatements = statements.Count;

                if (progressControl is ProgressBar progressBar1)
                {
                    progressBar1.Maximum = totalStatements;
                }

                var executionResult = new ScriptExecutionResult
                {
                    TotalStatements = totalStatements
                };

                await Task.Run(() =>
                {
                    try
                    {
                            // Clean up any existing transaction
                            if (_dbManager.IsInTransaction)
                        {
                            _dbManager.RollbackTransaction();
                        }

                            // Begin new transaction if requested
                            if (useTransaction)
                        {
                            _dbManager.BeginTransaction();
                        }

                        for (int i = 0; i < statements.Count; i++)
                        {
                            string statement = statements[i];
                            int remaining = totalStatements - i - 1;
                            bool isDropStatement = IsDropStatement(statement);

                            try
                            {
                                int rowsAffected = _dbManager.ExecuteNonQuery(statement);
                                executionResult.SuccessfulStatements++;
                                executionResult.RowsAffected += rowsAffected;
                            }
                            catch (Exception ex)
                            {
                                if (skipDropErrors && isDropStatement)
                                {
                                        // Skip failed DROP statements
                                        executionResult.SkippedStatements++;
                                    executionResult.Errors.Add(new ScriptError
                                    {
                                        Statement = statement,
                                        ErrorMessage = $"Skipped (object did not exist): {ex.Message}"
                                    });
                                    continue;
                                }

                                    // Real error case
                                    executionResult.FailedStatements++;
                                executionResult.Errors.Add(new ScriptError
                                {
                                    Statement = statement,
                                    ErrorMessage = ex.Message
                                });

                                if (useTransaction)
                                {
                                    _dbManager.RollbackTransaction();
                                    executionResult.WasRolledBack = true;
                                }

                                UpdateProgressUI(progressControl, statusLabel, i + 1, remaining, true);
                                return;
                            }

                                // Update progress after successful execution
                                UpdateProgressUI(progressControl, statusLabel, i + 1, remaining);
                        }

                            // Commit if we got this far
                            if (useTransaction)
                        {
                            _dbManager.CommitTransaction();
                        }
                        executionResult.IsSuccess = executionResult.FailedStatements == 0;
                    }
                    catch (Exception ex)
                    {
                            // Handle unexpected fatal errors
                            if (useTransaction && _dbManager.IsInTransaction)
                        {
                            _dbManager.RollbackTransaction();
                            executionResult.WasRolledBack = true;
                        }

                        executionResult.Errors.Add(new ScriptError
                        {
                            ErrorMessage = $"Fatal execution error: {ex.Message}"
                        });
                        executionResult.IsSuccess = false;
                    }
                });

                return executionResult;
            }
            finally
            {
                if (progressControl != null)
                {
                    progressControl.Visible = false;
                }
                executeButton.Enabled = true;
            }
        }

        // Helper method to identify DROP statements
        private bool IsDropStatement(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                return false;

            var trimmed = sql.Trim();
            return trimmed.StartsWith("DROP TABLE", StringComparison.OrdinalIgnoreCase) ||
                   trimmed.StartsWith("DROP VIEW", StringComparison.OrdinalIgnoreCase) ||
                   trimmed.StartsWith("DROP PROCEDURE", StringComparison.OrdinalIgnoreCase) ||
                   trimmed.StartsWith("DROP FUNCTION", StringComparison.OrdinalIgnoreCase);
        }

        // Thread-safe UI update method
        private void UpdateProgressUI(Control progressControl, Label statusLabel, int progress, int remaining, bool failed = false)
        {
            if (progressControl.InvokeRequired || (statusLabel?.InvokeRequired ?? false))
            {
                Invoke((MethodInvoker)delegate
                {
                    UpdateProgressUI(progressControl, statusLabel, progress, remaining, failed);
                });
                return;
            }

            if (progressControl is ProgressBar progressBar)
            {
                progressBar.Value = progress;
            }

            if (statusLabel != null)
            {
                statusLabel.Text = failed
                    ? $"{remaining} statements remaining (failed)"
                    : $"{remaining} statements remaining";

                if (failed) statusLabel.ForeColor = Color.Red;
            }
        }


        private void ShowExecutionResult(ScriptExecutionResult result)
        {
            if (result.IsSuccess)
            {
                MessageBox.Show($"Execution successful!\n{result.SuccessfulStatements} statements executed.\n{result.RowsAffected} rows affected.");
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Execution failed after {result.SuccessfulStatements} statements.");
                sb.AppendLine(result.WasRolledBack ? "Transaction was rolled back." : "No transaction was active.");

                foreach (var error in result.Errors)
                {
                    sb.AppendLine($"\nError: {error.ErrorMessage}");
                    if (!string.IsNullOrEmpty(error.Statement))
                    {
                        sb.AppendLine($"Statement: {error.Statement.Trim().Substring(0, Math.Min(100, error.Statement.Length))}...");
                    }
                }

                MessageBox.Show(sb.ToString(), "Execution Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            MoveToStep(tabControlWorkflow.SelectedIndex - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (tabControlWorkflow.SelectedIndex == tabControlWorkflow.TabPages.Count - 1)
            {
                Close();
            }
            else
            {
                MoveToStep(tabControlWorkflow.SelectedIndex + 1);
            }
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _dbManager.Dispose();
            base.OnFormClosing(e);
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            LoadLocalDatabases();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadLocalDatabases();
        }
        private void LoadLocalDatabases()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "db2cmd",
                        Arguments = "/i /c db2 list db directory",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                process.WaitForExit();

                File.WriteAllText("db2_output.txt", output); // 👈 Optional debug file
                File.WriteAllText("db2_errors.txt", errors);

                // Regex to extract database names
                var matches = Regex.Matches(output, @"Database name\s+=\s+([^\s]+)", RegexOptions.IgnoreCase);

                cmbDatabase.Items.Clear();
                foreach (Match match in matches)
                {
                    cmbDatabase.Items.Add(match.Groups[1].Value.Trim());
                }

                if (cmbDatabase.Items.Count > 0)
                    cmbDatabase.SelectedIndex = 0;
                else
                    MessageBox.Show("No databases found.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to list databases: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDataScript.Text = "";
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Select an Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Reset state
                        excelDataTable = new DataTable();
                        dataGridView1.DataSource = null;
                        txtSqlOutput.Clear();
                        clbSkipColumns.Items.Clear();

                        // Load file
                        string filePath = openFileDialog.FileName;
                        LoadExcelToDataTable(filePath);

                        // Bind to DataGridView
                        dataGridView1.DataSource = excelDataTable;
                        lblStatus2.Text = $"Loaded: {Path.GetFileName(filePath)} - {excelDataTable.Rows.Count} rows";

                        // Populate skip column options
                        foreach (DataColumn col in excelDataTable.Columns)
                        {
                            clbSkipColumns.Items.Add(col.ColumnName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Excel file: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadExcelToDataTable(string filePath)
        {

            ExcelPackage.LicenseContext = EPPlusLicenseContext.NonCommercial;



            FileInfo excelFile = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    throw new Exception("Excel file contains no worksheets");
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int startRow = worksheet.Dimension.Start.Row;
                int startCol = worksheet.Dimension.Start.Column;
                int endRow = worksheet.Dimension.End.Row;
                int endCol = worksheet.Dimension.End.Column;

                //excelDataTable.Columns.Clear();
                excelDataTable = new DataTable();  // Reinitialize instead of clearing rows only



                for (int col = startCol; col <= endCol; col++)
                {
                    string columnName = worksheet.Cells[startRow, col].Text;
                    if (string.IsNullOrEmpty(columnName))
                    {
                        columnName = $"Column{col}";
                    }
                    excelDataTable.Columns.Add(columnName);
                }

                for (int row = startRow + 1; row <= endRow; row++)
                {
                    DataRow dataRow = excelDataTable.NewRow();
                    for (int col = startCol; col <= endCol; col++)
                    {
                        dataRow[col - startCol] = worksheet.Cells[row, col].Text;
                    }
                    excelDataTable.Rows.Add(dataRow);
                }

            }
        }


        private void btnGenerateSql_Click(object sender, EventArgs e)
        {
            if (excelDataTable == null || excelDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Please load an Excel file first.", "No Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTableName.Text))
            {
                MessageBox.Show("Please enter a table name.", "Table Name Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string tableName = txtTableName.Text.Trim();
                StringBuilder sqlBuilder = new StringBuilder();

                // Get selected columns to skip
                HashSet<string> skipColumns = new HashSet<string>(
                    clbSkipColumns.CheckedItems.Cast<string>()
                );

                // Build list of column names to include
                List<string> includedColumns = new List<string>();
                foreach (DataColumn col in excelDataTable.Columns)
                {
                    if (!skipColumns.Contains(col.ColumnName))
                    {
                        includedColumns.Add(col.ColumnName);
                    }
                }

                // Generate INSERT statements
                foreach (DataRow row in excelDataTable.Rows)
                {
                    StringBuilder valuesBuilder = new StringBuilder();
                    bool first = true;

                    foreach (DataColumn col in excelDataTable.Columns)
                    {
                        if (skipColumns.Contains(col.ColumnName))
                            continue;

                        if (!first)
                            valuesBuilder.Append(", ");
                        else
                            first = false;

                        object value = row[col];
                        if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            valuesBuilder.Append("NULL");
                        }
                        else
                        {
                            string stringValue = value.ToString().Replace("'", "''");

                            if (decimal.TryParse(stringValue, out _))
                            {
                                valuesBuilder.Append(stringValue);
                            }
                            else if (DateTime.TryParse(stringValue, out DateTime dateValue))
                            {
                                valuesBuilder.Append($"'{dateValue:yyyy-MM-dd HH:mm:ss}'");
                            }
                            else
                            {
                                valuesBuilder.Append($"'{stringValue}'");
                            }
                        }
                    }

                    sqlBuilder.AppendLine($"INSERT INTO {tableName} ({string.Join(", ", includedColumns)})");
                    sqlBuilder.AppendLine($"VALUES ({valuesBuilder});");
                    sqlBuilder.AppendLine();
                }

                txtSqlOutput.Text = sqlBuilder.ToString();
                lblStatus2.Text = $"Generated {excelDataTable.Rows.Count} SQL INSERT statements (skipping {skipColumns.Count} column(s))";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating SQL: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSqlOutput.Text))
            {
                Clipboard.SetText(txtSqlOutput.Text);
                lblStatus2.Text = "SQL copied to clipboard!";
            }
        }

        private void btnSaveSql_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSqlOutput.Text))
            {
                MessageBox.Show("No SQL to save. Please generate SQL first.", "No Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "SQL Files|*.sql|Text Files|*.txt|All Files|*.*";
                saveFileDialog.Title = "Save SQL Output";
                saveFileDialog.DefaultExt = "sql";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, txtSqlOutput.Text);
                    lblStatus2.Text = $"SQL saved to: {saveFileDialog.FileName}";
                }
            }
        }


    }


}


