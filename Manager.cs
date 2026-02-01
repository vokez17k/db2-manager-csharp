using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;

namespace DatabaseManager
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
            AddVersionNumber();
        }
        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            //this.Text += $"v.{versionInfo.FileVersion}";
            this.Text = $"DBManager V{versionInfo.FileVersion}";
        }
        private void Manager_Load(object sender, EventArgs e)
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

        private void Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "DB2 Backup Files (*.DBPART*)|*.DBPART*;*.001";
                ofd.Title = "Select DB2 Backup File";
                ofd.InitialDirectory = @"D:\Digitec\"; // Default starting folder

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string fullPath = ofd.FileName;
                        string fileName = Path.GetFileName(fullPath);
                        string folderPath = Path.GetDirectoryName(fullPath);

                        // Clear previous values
                        cmbDatabase.Text = "";
                        txtDatabaseLocation.Text = "";
                        txtTimestamp.Text = "";

                        // Parse the filename with multiple format support
                        var (dbName, timestamp) = ParseDb2BackupFileName(fileName);

                        if (dbName != null)
                        {
                            //txtDatabase.Text = dbName;
                            cmbDatabase.Text = dbName;
                            txtDatabaseLocation.Text = folderPath;

                            if (timestamp != null)
                            {
                                txtTimestamp.Text = timestamp;
                            }
                            else
                            {
                                MessageBox.Show("Timestamp not found in filename.\n" +
                                               "The restore will use the most recent backup in the folder.",
                                               "Information",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid DB2 backup file format.\n" +
                                           "Supported formats:\n" +
                                           "1. DIGIPOS.0.DB2.DBPART000.20250610165418.001\n" +
                                           "2. DIGIPOS.TIMESTAMP.001",
                                           "Invalid Format",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing backup file: {ex.Message}",
                                      "Error",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                    }
                }
            }
        }

        private (string dbName, string timestamp) ParseDb2BackupFileName(string fileName)
        {
            // Remove .001 extension if present
            fileName = Path.GetFileNameWithoutExtension(fileName);
            string[] parts = fileName.Split('.');

            // Format 1: DIGIPOS.0.DB2.DBPART000.20250610165418
            if (parts.Length >= 5 && parts[4].Length == 14 && long.TryParse(parts[4], out _))
            {
                return (parts[0], parts[4]); // dbName and timestamp
            }
            // Format 2: DIGIPOS.TIMESTAMP
            else if (parts.Length >= 2 && parts[1].Length == 14 && long.TryParse(parts[1], out _))
            {
                return (parts[0], parts[1]); // dbName and timestamp
            }
            // Format 3: DIGIPOS.0.DB2 (no timestamp)
            else if (parts.Length >= 3)
            {
                return (parts[0], null); // dbName only
            }

            return (null, null); // Invalid format
        }
        private void Restore_Click(object sender, EventArgs e)
        {
           
            if (ConfirmRestoreOperation() == DialogResult.Yes)
            {

                string backupFolder = txtDatabaseLocation.Text;
                DoRestoreDatabase(backupFolder);
            }
        }
        private DialogResult ConfirmRestoreOperation()
        {
            return MessageBox.Show($"You are about to restore database '{cmbDatabase.Text}'.\n\n" +
                                 "This will overwrite the existing database.\n" +
                                 "Are you sure you want to continue?",
                                 "Confirm Database Restore",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Warning,
                                 MessageBoxDefaultButton.Button2);
        }
        private void DoRestoreDatabase(string backupFolder)
        {
            const string logFileName = "restore.log";
            string databaseName = Path.GetFileNameWithoutExtension(cmbDatabase.Text)?.Split('.')[0];
            //string databaseName = Path.GetFileNameWithoutExtension((cmbDatabase.SelectedItem?.ToString())?.Split('.')[0]);
            
            string timestamp = txtTimestamp.Text;
            string logFile = Path.Combine(backupFolder, logFileName);
            string batFile = Path.Combine(backupFolder, "restore.bat");
            string cmdFile = Path.Combine(backupFolder, "restore_commands.txt");

            // Create progress form
            var progressForm = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Text = "Database Restore in Progress",
                MaximizeBox = false,
                MinimizeBox = false
            };

            var progressBar = new ProgressBar()
            {
                Style = ProgressBarStyle.Marquee,
                Dock = DockStyle.Top,
                Height = 30,
                MarqueeAnimationSpeed = 30
            };

            var lblStatus = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Initializing restore process..."
            };

            progressForm.Controls.Add(lblStatus);
            progressForm.Controls.Add(progressBar);
            progressForm.Show();

            // Run the restore in a background thread
            var bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += (sender, e) =>
            {
                try
                {
                    // Validate inputs
                    if (string.IsNullOrEmpty(databaseName))
                    {
                        throw new ArgumentException("Database name could not be determined from backup file!");
                    }

                    if (string.IsNullOrEmpty(timestamp))
                    {
                        throw new ArgumentException("Could not determine backup timestamp!");
                    }

                    // Clean up previous files
                    CleanupFiles(batFile, cmdFile, logFile);

                    // Create DB2 restore commands
                    string db2Commands = $@"CONNECT TO {databaseName};
                    QUIESCE DATABASE IMMEDIATE FORCE CONNECTIONS;
                    CONNECT RESET;
                    DEACTIVATE DATABASE {databaseName};
                    RESTORE DATABASE {databaseName} FROM ""{backupFolder}"" TAKEN AT {timestamp} WITHOUT PROMPTING;
                    CONNECT TO {databaseName};
                    UNQUIESCE DATABASE;
                    CONNECT RESET;";

                    File.WriteAllText(cmdFile, db2Commands);

                    // Create batch file with logging
                    File.WriteAllText(batFile, $@"@echo off
                    echo [Restore Start %date% %time%] > ""{logFile}""
                    db2cmd /c /w /i db2 -tvf ""{cmdFile}"" >> ""{logFile}"" 2>&1
                    echo [Restore End %date% %time%] >> ""{logFile}""
                    exit %ERRORLEVEL%");

                    // Execute restore process
                    using (var process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = batFile,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        };

                        process.OutputDataReceived += (s, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                bgWorker.ReportProgress(0, args.Data);

                                // Optional: Parse specific progress updates
                                if (args.Data.Contains("RESTORE DATABASE"))
                                {
                                    bgWorker.ReportProgress(0, "Restoring database files...");
                                }
                                else if (args.Data.Contains("UNQUIESCE DATABASE"))
                                {
                                    bgWorker.ReportProgress(0, "Finalizing restore...");
                                }
                            }
                        };

                        process.ErrorDataReceived += (s, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                bgWorker.ReportProgress(0, "ERROR: " + args.Data);
                            }
                        };

                        process.Start();
                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        while (!process.HasExited)
                        {
                            bgWorker.ReportProgress(0, "Restore in progress...");
                            Thread.Sleep(500);
                        }

                        process.WaitForExit();
                        e.Result = process.ExitCode;
                    }
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                }
            };

            bgWorker.ProgressChanged += (sender, e) =>
            {
                lblStatus.Text = e.UserState.ToString();
            };

            bgWorker.RunWorkerCompleted += (sender, e) =>
            {
                progressForm.Close();

                if (e.Result is Exception ex)
                {
                    string errorDetails = $"Restore Error: {ex.Message}";
                    if (File.Exists(logFile))
                    {
                        errorDetails += $"\n\nLog Contents:\n{File.ReadAllText(logFile)}";
                    }
                    MessageBox.Show(errorDetails, "Restore Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int exitCode = (int)e.Result;
                    string logContent = File.ReadAllText(logFile);

                    if (exitCode == 0)
                    {
                        MessageBox.Show($"Restore completed successfully!:\n\nComplete Restore log saved to:\n{logFile}",
                                      "Restore Successful",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    else if (logContent.Contains("SQL2540W") || logContent.Contains("SQL2539W"))
                    {
                        MessageBox.Show($"Restore completed with warnings:\n\nComplete Restore log saved to:\n{logFile}",
                                      "Restore Completed with Warnings",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Restore failed (Exit Code: {exitCode})\n\n{logContent}",
                                      "Restore Failed",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                    }
                }
            };

            bgWorker.RunWorkerAsync();
        }

      

        
        private void CleanupFiles(params string[] files)
        {
            foreach (var file in files)
            {
                try { if (File.Exists(file)) File.Delete(file); }
                catch { /* Ignore deletion errors */ }
            }
        }
       
        private void Create_Click(object sender, EventArgs e)
        {
            //string databaseName = txtDatabase.Text.Trim();
            string databaseName = cmbDatabase.Text;

            if (string.IsNullOrEmpty(databaseName))
            {
                MessageBox.Show("Please enter a database name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to create database '{databaseName}'?",
                                                 "Confirm Creation",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string batFile = Path.Combine(Path.GetTempPath(), "create_db.bat");
                    string cmdFile = Path.Combine(Path.GetTempPath(), "create_db.sql");
                    string logFile = Path.Combine(Path.GetTempPath(), $"create_db_{databaseName}.log");

                    // Create the SQL command file
                    string db2Commands = $@"
                    CREATE DATABASE {databaseName} AUTOMATIC STORAGE YES ON C:\ PAGESIZE 32 K;
                    CONNECT TO {databaseName};
                    CONNECT RESET;";

                    File.WriteAllText(cmdFile, db2Commands);

                    // Create the batch file
                    File.WriteAllText(batFile,
                        $@"@echo off
                        db2cmd /c /w db2 -tvf ""{cmdFile}"" > ""{logFile}"" 2>&1
                        exit %ERRORLEVEL%");

                    // Execute the creation
                    ExecuteDb2Command(batFile, $"Database '{databaseName}' created successfully", $"Failed to create database '{databaseName}'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Drop_Click(object sender, EventArgs e)
        {
            //string databaseName = txtDatabase.Text.Trim();
            string databaseName = cmbDatabase.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(databaseName))
            {
                MessageBox.Show("Please enter a database name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to DROP database '{databaseName}'?\nThis action cannot be undone!",
                                                 "Confirm Drop",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string batFile = Path.Combine(Path.GetTempPath(), "drop_db.bat");
                    string cmdFile = Path.Combine(Path.GetTempPath(), "drop_db.sql");
                    string logFile = Path.Combine(Path.GetTempPath(), $"drop_db_{databaseName}.log");

                    // Create the SQL command file
                    string db2Commands = $@"
                    DB2STOP;
                    DB2START;
                    DEACTIVATE DATABASE {databaseName};
                    DROP DATABASE {databaseName};";

                    File.WriteAllText(cmdFile, db2Commands);

                    // Create the batch file
                    File.WriteAllText(batFile,
                        $@"@echo off
                        db2cmd /c /w db2 -tvf ""{cmdFile}"" > ""{logFile}"" 2>&1
                        exit %ERRORLEVEL%");

                    // Execute the drop
                    ExecuteDb2Command(batFile, $"Database '{databaseName}' dropped successfully", $"Failed to drop database '{databaseName}'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error dropping database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ExecuteDb2Command(string batFile, string successMessage, string failureMessage)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = batFile,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                if (!process.WaitForExit(300000)) // 5 minute timeout
                {
                    process.Kill();
                    throw new TimeoutException("Operation timed out after 5 minutes");
                }

                if (process.ExitCode != 0)
                {
                    throw new Exception($"{failureMessage}\nExit Code: {process.ExitCode}\nOutput: {output}\nErrors: {errors}");
                }

                MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BackupDatabase_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to backup the database?",
                                                "Backup Database",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Select backup folder";
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string backupFolder = folderDialog.SelectedPath;
                        DoBackupDatabase(backupFolder);
                    }
                }
            }
        }
        private void DoBackupDatabase(string backupFolder)
        {
            const string logFileName = "backup.log";
            //string databaseName = txtDatabase.Text.Trim();
            string databaseName = cmbDatabase.Text;
            string logFile = Path.Combine(backupFolder, logFileName);
            string batFile = Path.Combine(backupFolder, "backup.bat");
            string cmdFile = Path.Combine(backupFolder, "backup_commands.txt");

            // Create progress form
            var progressForm = new Form()
            {
                Width = 450,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Text = "Database Backup Progress",
                MaximizeBox = false,
                MinimizeBox = false
            };

            var progressBar = new ProgressBar()
            {
                Style = ProgressBarStyle.Continuous,
                Dock = DockStyle.Top,
                Height = 30,
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };

            var lblStatus = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Initializing backup..."
            };

            progressForm.Controls.Add(lblStatus);
            progressForm.Controls.Add(progressBar);
            progressForm.Show();

            // Background worker setup
            var bgWorker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            Process backupProcess = null;
            DateTime operationStartTime;
            bool backupPhaseStarted = false;

            bgWorker.DoWork += (sender, e) =>
            {
                try
                {
                    // Validate and setup (existing code)
                    if (string.IsNullOrEmpty(databaseName))
                        throw new ArgumentException("Database name cannot be empty");

                    CleanupFiles(batFile, cmdFile, logFile);

                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string db2Commands = $@"CONNECT TO {databaseName};
                    QUIESCE DATABASE IMMEDIATE FORCE CONNECTIONS;
                    CONNECT RESET;
                    BACKUP DATABASE {databaseName} TO ""{backupFolder}"" WITH 2 BUFFERS BUFFER 1024 COMPRESS WITHOUT PROMPTING;
                    CONNECT TO {databaseName};
                    UNQUIESCE DATABASE;
                    CONNECT RESET;";

                    File.WriteAllText(cmdFile, db2Commands);
                    File.WriteAllText(batFile, $@"@echo off
                    echo [Backup Start %date% %time%] > ""{logFile}""
                    db2cmd /c /w /i db2 -tvf ""{cmdFile}"" >> ""{logFile}"" 2>&1
                    echo [Backup End %date% %time%] >> ""{logFile}""
                    exit %ERRORLEVEL%");

                    operationStartTime = DateTime.Now;
                    TimeSpan estimatedDuration = TimeSpan.FromMinutes(5); // Adjust based on your typical backup time

                    backupProcess = new Process()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = batFile,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        }
                    };

                    backupProcess.OutputDataReceived += (s, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            // Detect backup phase start
                            if (args.Data.Contains("BACKUP DATABASE"))
                            {
                                backupPhaseStarted = true;
                                bgWorker.ReportProgress(20, "Backup phase started...");
                            }
                            // Detect specific progress markers if available
                            else if (args.Data.Contains("Processing table"))
                            {
                                // Parse progress from output if possible
                                bgWorker.ReportProgress(0, args.Data);
                            }
                        }
                    };

                    backupProcess.Start();
                    backupProcess.BeginOutputReadLine();
                    backupProcess.BeginErrorReadLine();

                    // Progress monitoring loop
                    while (!backupProcess.HasExited)
                    {
                        if (bgWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        double progress = 0;
                        string status = "Preparing...";

                        if (backupPhaseStarted)
                        {
                            // During backup phase, calculate progress based on elapsed time
                            TimeSpan elapsed = DateTime.Now - operationStartTime;
                            double fractionComplete = Math.Min(elapsed.TotalMilliseconds / estimatedDuration.TotalMilliseconds, 0.9);
                            progress = 20 + (int)(fractionComplete * 70); // 20-90% during backup
                            status = $"Backup in progress ({progress}%)...";
                        }
                        else
                        {
                            // Initial phase (0-20%)
                            TimeSpan elapsed = DateTime.Now - operationStartTime;
                            progress = Math.Min((int)(elapsed.TotalMilliseconds / 30000 * 20), 20); // First 30 seconds
                            status = "Initializing backup...";
                        }

                        bgWorker.ReportProgress((int)progress, status);
                        Thread.Sleep(300);
                    }

                    // Final completion
                    bgWorker.ReportProgress(100, "Backup completed");
                    e.Result = backupProcess.ExitCode;
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                }
            };

            bgWorker.ProgressChanged += (sender, e) =>
            {
                progressBar.Value = e.ProgressPercentage;
                lblStatus.Text = e.UserState.ToString();
            };

            bgWorker.RunWorkerCompleted += (sender, e) =>
            {
                progressForm.Close();

                if (e.Cancelled)
                {
                    MessageBox.Show("Backup was cancelled", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (e.Result is Exception ex)
                {
                    string errorDetails = $"Backup Error: {ex.Message}";
                    if (File.Exists(logFile))
                    {
                        errorDetails += $"\n\nLog Contents:\n{File.ReadAllText(logFile)}";
                    }
                    MessageBox.Show(errorDetails, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int exitCode = (int)e.Result;
                    string logContent = File.ReadAllText(logFile);

                    if (exitCode == 0)
                    {
                        MessageBox.Show($"Backup completed successfully!:Complete Backup log saved to:\n{logFile}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (IsNonCriticalWarning(logContent))
                    {
                        MessageBox.Show($"Backup completed with warnings:Complete Backup log saved to:\n{logFile}",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Backup failed (Exit Code: {exitCode})\n\n{logContent}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            bgWorker.RunWorkerAsync();
        }

        private bool IsNonCriticalWarning(string logContent)
        {
            return logContent.Contains("SQL2539W") || logContent.Contains("completed successfully");
        }
      
        // Reuse the existing CleanupFiles method from restore code
       
        private void txtDatabase_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void GenerateTablesViews_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateTablesAndViews.ShowConnectionFormAndExport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunScripts_Click(object sender, EventArgs e)
        {
            DB2ScriptManager ScriptManager = new DB2ScriptManager();
            ScriptManager.ShowDialog();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            LoadLocalDatabases();
        }
    }
}
