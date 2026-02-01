using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using IBM.Data.DB2;

namespace DatabaseManager
{
    public class GenerateTablesAndViews
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, List<string>> _dependencyGraph = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, string> _viewDefinitions = new Dictionary<string, string>();
        private string _schema;

        public GenerateTablesAndViews(string connectionString, string schema)
        {
            _connectionString = connectionString;
            _schema = schema.ToUpper();
        }

        public void Export(string outputFilePath, bool exportViews, bool exportTables)
        {
            var lines = new List<string>();

            if (exportTables)
            {
                var tableStatements = LoadTables(out List<string> indexStatements);
                lines.Add("-- DROP TABLES");
                foreach (var stmt in tableStatements.Where(s => s.StartsWith("DROP TABLE")))
                    lines.Add(stmt);

                lines.Add("-- CREATE TABLES");
                foreach (var stmt in tableStatements.Where(s => s.StartsWith("CREATE TABLE")))
                    lines.Add(stmt);

                if (indexStatements.Count > 0)
                {
                    lines.Add("-- INDEXES");
                    lines.AddRange(indexStatements);
                }
            }

            if (exportViews)
            {
                var viewStatements = LoadViews();
                lines.Add("-- DROP VIEWS");
                foreach (var stmt in viewStatements.Where(s => s.StartsWith("DROP VIEW")))
                    lines.Add(stmt);

                lines.Add("-- CREATE VIEWS");
                foreach (var stmt in viewStatements.Where(s => s.StartsWith("CREATE VIEW")))
                    lines.Add(stmt);
            }

            File.WriteAllLines(outputFilePath, lines);
        }



        public static void ShowConnectionFormAndExport()
        {
            var settings = SettingsManager.LoadSettings();
            ConnectionProfile currentProfile = settings.Profiles.FirstOrDefault(p => p.ProfileName == settings.LastUsedProfile)
                                            ?? new ConnectionProfile();

            Form inputForm = new Form
            {
                Text = "Database Connection Info",
                Width = 500,
                Height = 450,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Profile Selection
            var lblProfile = new Label { Text = "Profile:", Left = 20, Top = 20, Width = 100 };
            var cmbProfiles = new ComboBox { Left = 130, Top = 20, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            var btnNewProfile = new Button { Text = "New", Left = 340, Top = 20, Width = 60 };
            var btnDeleteProfile = new Button { Text = "Delete", Left = 410, Top = 20, Width = 60 };

            // Connection Fields
            var labels = new[] { "Server", "Port", "Database", "Username", "Password", "Schema" };
            var inputs = new List<TextBox>();

            for (int i = 0; i < labels.Length; i++)
            {
                var lbl = new Label { Text = labels[i] + ":", Left = 20, Top = 60 + i * 40, Width = 100 };
                var txt = new TextBox { Left = 130, Top = 60 + i * 40, Width = 200 };
                if (labels[i] == "Password") txt.PasswordChar = '*';

                // Set values from current profile
                switch (labels[i])
                {
                    case "Server": txt.Text = currentProfile.Server; break;
                    case "Port": txt.Text = currentProfile.Port; break;
                    case "Database": txt.Text = currentProfile.Database; break;
                    case "Username": txt.Text = currentProfile.Username; break;
                    case "Password": txt.Text = currentProfile.Password; break;
                    case "Schema": txt.Text = currentProfile.Schema; break;
                }

                inputForm.Controls.Add(lbl);
                inputForm.Controls.Add(txt);
                inputs.Add(txt);
            }

            // Action Buttons
            var btnTest = new Button { Text = "Test Connection", Left = 20, Top = 300, Width = 120 };
            var btnImport = new Button { Text = "Import Profile", Left = 150, Top = 300, Width = 120 };
            var btnExport = new Button { Text = "Export Profile", Left = 280, Top = 300, Width = 120 };
            var btnOK = new Button { Text = "Export", Left = 180, Width = 100, Top = 350, DialogResult = DialogResult.OK };

            // Populate profiles combo
            RefreshProfilesCombo(cmbProfiles, settings, currentProfile.ProfileName);

            // Event Handlers
            btnNewProfile.Click += (s, e) => {
                var profileName = Microsoft.VisualBasic.Interaction.InputBox("Enter profile name:", "New Profile");
                if (!string.IsNullOrWhiteSpace(profileName))
                {
                    if (settings.Profiles.Any(p => p.ProfileName.Equals(profileName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Profile name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var newProfile = new ConnectionProfile { ProfileName = profileName };
                    settings.Profiles.Add(newProfile);
                    SettingsManager.SaveSettings(settings);
                    RefreshProfilesCombo(cmbProfiles, settings, profileName);
                }
            };

            btnDeleteProfile.Click += (s, e) => {
                if (cmbProfiles.SelectedItem != null)
                {
                    var result = MessageBox.Show("Delete selected profile?", "Confirm",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var profileToDelete = cmbProfiles.SelectedItem.ToString();
                        settings.Profiles.RemoveAll(p => p.ProfileName == profileToDelete);
                        SettingsManager.SaveSettings(settings);
                        RefreshProfilesCombo(cmbProfiles, settings, null);
                    }
                }
            };

            cmbProfiles.SelectedIndexChanged += (s, e) => {
                if (cmbProfiles.SelectedItem != null)
                {
                    var selectedProfile = settings.Profiles.FirstOrDefault(p => p.ProfileName == cmbProfiles.SelectedItem.ToString());
                    if (selectedProfile != null)
                    {
                        inputs[0].Text = selectedProfile.Server;
                        inputs[1].Text = selectedProfile.Port;
                        inputs[2].Text = selectedProfile.Database;
                        inputs[3].Text = selectedProfile.Username;
                        inputs[4].Text = selectedProfile.Password;
                        inputs[5].Text = selectedProfile.Schema;
                    }
                }
            };

            btnTest.Click += (s, e) => {
                string connectionString = BuildConnectionString(inputs[0].Text, inputs[1].Text, inputs[2].Text, inputs[3].Text, inputs[4].Text);
                if (ValidateConnection(connectionString))
                {
                    MessageBox.Show("Connection successful!", "Test Connection",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Connection failed", "Test Connection",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnImport.Click += (s, e) => {
                using (var openFile = new OpenFileDialog())
                {
                    openFile.Filter = "XML Files|*.xml|All Files|*.*";
                    if (openFile.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            var importedProfile = SettingsManager.ImportSettings(openFile.FileName);
                            if (settings.Profiles.Any(p => p.ProfileName == importedProfile.ProfileName))
                            {
                                if (MessageBox.Show("Profile already exists. Overwrite?", "Confirm",
                                                   MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    settings.Profiles.RemoveAll(p => p.ProfileName == importedProfile.ProfileName);
                                }
                                else
                                {
                                    return;
                                }
                            }

                            settings.Profiles.Add(importedProfile);
                            SettingsManager.SaveSettings(settings);
                            RefreshProfilesCombo(cmbProfiles, settings, importedProfile.ProfileName);

                            // Load the imported profile
                            inputs[0].Text = importedProfile.Server;
                            inputs[1].Text = importedProfile.Port;
                            inputs[2].Text = importedProfile.Database;
                            inputs[3].Text = importedProfile.Username;
                            inputs[4].Text = importedProfile.Password;
                            inputs[5].Text = importedProfile.Schema;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Import failed: {ex.Message}", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };

            btnExport.Click += (s, e) => {
                if (cmbProfiles.SelectedItem == null)
                {
                    MessageBox.Show("Please select a profile to export", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var saveFile = new SaveFileDialog())
                {
                    saveFile.Filter = "XML Files|*.xml|All Files|*.*";
                    saveFile.FileName = $"{cmbProfiles.SelectedItem}.xml";
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            var profileToExport = settings.Profiles.First(p => p.ProfileName == cmbProfiles.SelectedItem.ToString());
                            SettingsManager.ExportSettings(saveFile.FileName, profileToExport);
                            MessageBox.Show("Profile exported successfully", "Success",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Export failed: {ex.Message}", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };

            // Add controls to form
            inputForm.Controls.AddRange(new Control[] {
        lblProfile, cmbProfiles, btnNewProfile, btnDeleteProfile,
        btnTest, btnImport, btnExport, btnOK
    });

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                // Save current settings as a profile
                var currentProfileName = cmbProfiles.SelectedItem?.ToString() ?? "Default";
                var existingProfile = settings.Profiles.FirstOrDefault(p => p.ProfileName == currentProfileName);

                if (existingProfile == null)
                {
                    existingProfile = new ConnectionProfile { ProfileName = currentProfileName };
                    settings.Profiles.Add(existingProfile);
                }

                existingProfile.Server = inputs[0].Text.Trim();
                existingProfile.Port = inputs[1].Text.Trim();
                existingProfile.Database = inputs[2].Text.Trim();
                existingProfile.Username = inputs[3].Text.Trim();
                existingProfile.Password = inputs[4].Text;
                existingProfile.Schema = inputs[5].Text.Trim().ToUpper();

                settings.LastUsedProfile = existingProfile.ProfileName;
                SettingsManager.SaveSettings(settings);

                string connectionString = BuildConnectionString(
                    existingProfile.Server,
                    existingProfile.Port,
                    existingProfile.Database,
                    existingProfile.Username,
                    existingProfile.Password);

                if (ValidateConnection(connectionString))
                {
                    ExportWithPromptDynamic(connectionString, existingProfile.Schema);
                }
                else
                {
                    MessageBox.Show("Connection failed. Please check your settings.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void RefreshProfilesCombo(ComboBox cmb, ConnectionSettings settings, string selectProfile)
        {
            cmb.Items.Clear();
            foreach (var profile in settings.Profiles)
            {
                cmb.Items.Add(profile.ProfileName);
            }

            if (!string.IsNullOrEmpty(selectProfile) && cmb.Items.Contains(selectProfile))
            {
                cmb.SelectedItem = selectProfile;
            }
            else if (cmb.Items.Count > 0)
            {
                cmb.SelectedIndex = 0;
            }
        }

        private static string BuildConnectionString(string server, string port, string database, string username, string password)
        {
            return $"Server={server}:{port};Database={database};UID={username};PWD={password};";
        }

        private static bool ValidateConnection(string connectionString)
        {
            try
            {
                using (var connection = new DB2Connection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection validation failed: {ex.Message}");
                return false;
            }
        }

        public static void ExportWithPromptDynamic(string connectionString, string schema)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "SQL files (*.sql)|*.sql";
                saveFileDialog.FileName = "export_script.sql";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = MessageBox.Show("Export:\nYes = Tables & Views\nNo = Views only\nCancel = Tables only",
                                                 "Export Options", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    bool exportTables = false;
                    bool exportViews = false;

                    if (result == DialogResult.Yes)
                    {
                        exportTables = true;
                        exportViews = true;
                    }
                    else if (result == DialogResult.No)
                    {
                        exportViews = true;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        exportTables = true;
                    }

                    var exporter = new GenerateTablesAndViews(connectionString, schema);
                    exporter.Export(saveFileDialog.FileName, exportViews, exportTables);

                    MessageBox.Show("Export complete.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        public static void PromptAndExport()
        {
            Form inputForm = new Form { Text = "Export Configuration", Width = 400, Height = 360 };

            var lblServer = new Label { Text = "Server:", Top = 10, Left = 20, Width = 100 };
            var txtServer = new TextBox { Top = 30, Left = 20, Width = 340 };

            var lblPort = new Label { Text = "Port:", Top = 60, Left = 20, Width = 100 };
            var txtPort = new TextBox { Top = 80, Left = 20, Width = 340, Text = "50000" };

            var lblDatabase = new Label { Text = "Database:", Top = 110, Left = 20, Width = 100 };
            var txtDatabase = new TextBox { Top = 130, Left = 20, Width = 340 };

            var lblUsername = new Label { Text = "Username:", Top = 160, Left = 20, Width = 100 };
            var txtUsername = new TextBox { Top = 180, Left = 20, Width = 340 };

            var lblPassword = new Label { Text = "Password:", Top = 210, Left = 20, Width = 100 };
            var txtPassword = new TextBox { Top = 230, Left = 20, Width = 340, UseSystemPasswordChar = true };

            var lblSchema = new Label { Text = "Schema:", Top = 260, Left = 20, Width = 100 };
            var txtSchema = new TextBox { Top = 280, Left = 20, Width = 340 };

            var btnOk = new Button { Text = "Export", Top = 310, Left = 140, Width = 100 };

            btnOk.Click += (s, e) => {
                string connectionString = $"Server={txtServer.Text}:{txtPort.Text};Database={txtDatabase.Text};UID={txtUsername.Text};PWD={txtPassword.Text};";
                string schema = txtSchema.Text.Trim();
                inputForm.Close();

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "SQL files (*.sql)|*.sql";
                    saveFileDialog.FileName = "export_script.sql";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var result = MessageBox.Show("Export:\nYes = Tables & Views\nNo = Views only\nCancel = Tables only",
                                                     "Export Options", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        bool exportTables = false;
                        bool exportViews = false;

                        if (result == DialogResult.Yes)
                        {
                            exportTables = true;
                            exportViews = true;
                        }
                        else if (result == DialogResult.No)
                        {
                            exportViews = true;
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            exportTables = true;
                        }

                        var exporter = new GenerateTablesAndViews(connectionString, schema);
                        exporter.Export(saveFileDialog.FileName, exportViews, exportTables);

                        MessageBox.Show("Export complete.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            };

            inputForm.Controls.AddRange(new Control[] {
                lblServer, txtServer,
                lblPort, txtPort,
                lblDatabase, txtDatabase,
                lblUsername, txtUsername,
                lblPassword, txtPassword,
                lblSchema, txtSchema,
                btnOk
            });

            inputForm.ShowDialog();
        }
        private List<string> LoadViews()
        {
            var viewStatements = new List<string>();

            string viewsSql = $@"
        WITH view_dependencies AS (
            SELECT 
                vd.viewschema AS view_schema,
                vd.viewname AS view_name,
                vd.bschema AS dep_schema,
                vd.bname AS dep_name,
                CASE 
                    WHEN v2.viewname IS NOT NULL THEN 1 ELSE 0 
                END AS is_view_dependency
            FROM 
                syscat.viewdep vd
            LEFT JOIN 
                syscat.views v2 
                ON vd.bschema = v2.viewschema AND vd.bname = v2.viewname
            WHERE 
                vd.viewschema = '{_schema}'
        ),
        view_dependency_levels AS (
            SELECT 
                v.viewschema,
                v.viewname,
                MAX(COALESCE(d.is_view_dependency, 0)) AS dependency_level
            FROM 
                syscat.views v
            LEFT JOIN 
                view_dependencies d 
                ON v.viewschema = d.view_schema AND v.viewname = d.view_name
            WHERE 
                v.viewschema = '{_schema}'
            GROUP BY 
                v.viewschema, v.viewname
        ),
        view_definitions AS (
            SELECT
                v.viewschema,
                v.viewname,
                v.text AS view_sql,
                lvl.dependency_level,
                'CREATE VIEW ' || v.viewname || ' AS ' || CHR(10) || v.text || ';' AS full_create
            FROM 
                syscat.views v
            JOIN 
                view_dependency_levels lvl 
                ON v.viewschema = lvl.viewschema AND v.viewname = lvl.viewname
            WHERE 
                v.viewschema = '{_schema}'
        ),
        ddl_statements AS (
            SELECT 
                'DROP VIEW ' || viewname || ';' AS ddl_statement,
                0 AS sort_group,
                -1 * dependency_level AS sort_level,
                viewname
            FROM 
                view_definitions

            UNION ALL

            SELECT 
                full_create AS ddl_statement,
                1 AS sort_group,
                dependency_level AS sort_level,
                viewname
            FROM 
                view_definitions
        )
        SELECT ddl_statement
        FROM ddl_statements
        ORDER BY 
            sort_group,
            sort_level,
            viewname;";

            using (var connection = new DB2Connection(_connectionString))
            using (var command = new DB2Command(viewsSql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var stmt = reader.GetString(0).Trim();

                        // Skip unwanted duplicates
                        if (stmt.StartsWith("-- CREATE VIEWS", StringComparison.OrdinalIgnoreCase))
                            continue;

                        // Remove duplicate "CREATE VIEW xxx AS" if repeated
                        if (stmt.StartsWith("CREATE VIEW", StringComparison.OrdinalIgnoreCase))
                        {
                            var firstLineEnd = stmt.IndexOf('\n');
                            if (firstLineEnd > 0)
                            {
                                var headerLine = stmt.Substring(0, firstLineEnd).Trim();
                                var rest = stmt.Substring(firstLineEnd).Trim();

                                if (rest.StartsWith(headerLine, StringComparison.OrdinalIgnoreCase))
                                {
                                    rest = rest.Substring(headerLine.Length).TrimStart();
                                }

                                stmt = headerLine + "\n" + rest;
                            }
                        }

                        // Add a blank line after every semicolon
                        if (stmt.EndsWith(";") && (stmt.StartsWith("CREATE VIEW", StringComparison.OrdinalIgnoreCase) ||
                           stmt.StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase)))
                        {
                            stmt += "\n";
                        }


                        viewStatements.Add(stmt);
                    }

                }
            }

            return viewStatements;
        }






        private List<string> TopologicalSort()
        {
            var result = new List<string>();
            var visited = new HashSet<string>();
            var temp = new HashSet<string>();

            foreach (var view in _dependencyGraph.Keys)
            {
                try
                {
                    Visit(view, visited, temp, result);
                }
                catch
                {
                    result.Add($"-- WARNING: Skipped view {view} due to circular dependency");
                }
            }

            return result;
        }

        private void Visit(string view, HashSet<string> visited, HashSet<string> temp, List<string> result)
        {
            if (visited.Contains(view)) return;
            if (temp.Contains(view)) throw new Exception("Circular view dependency detected: " + view);

            temp.Add(view);
            foreach (var dep in _dependencyGraph[view])
            {
                Visit(dep, visited, temp, result);
            }
            temp.Remove(view);
            visited.Add(view);
            result.Add(view);
        }

        private List<string> LoadTables(out List<string> indexStatements)
        {
            indexStatements = new List<string>();
            var allStatements = new List<string>();

            string tablesSql = $@"
                        WITH 
                        table_info AS (
                            SELECT 
                                t.tabschema, 
                                t.tabname, 
                                t.tableorg, 
                                COALESCE(t.tbspace, '') AS tbspace,
                                CASE WHEN EXISTS (
                                    SELECT 1 FROM syscat.tabconst c 
                                    WHERE c.tabschema = t.tabschema 
                                    AND c.tabname = t.tabname 
                                    AND c.type = 'P'
                                ) THEN 1 ELSE 2 END AS has_primary_key
                            FROM 
                                syscat.tables t
                            WHERE 
                                t.type = 'T' 
                                AND t.tabschema = '{_schema}'
                        ),
                        primary_keys AS (
                            SELECT 
                                c.tabschema,
                                c.tabname,
                                '    CONSTRAINT ' || c.constname || ' PRIMARY KEY (' || LISTAGG(k.colname, ', ') WITHIN GROUP (ORDER BY k.colseq) || ')' AS pk_definition
                            FROM 
                                syscat.tabconst c
                            JOIN 
                                syscat.keycoluse k ON c.tabschema = k.tabschema 
                                                  AND c.tabname = k.tabname 
                                                  AND c.constname = k.constname
                            WHERE 
                                c.tabschema = '{_schema}'
                                AND c.type = 'P'
                            GROUP BY 
                                c.tabschema, c.tabname, c.constname
                        ),
                        column_info AS (
                            SELECT 
                                c.tabschema, 
                                c.tabname,
                                c.colname,
                                c.colno,
                                '    ' || c.colname || ' ' || 
                                CASE 
                                    WHEN c.typename = 'CHARACTER' AND c.length = 1 THEN 'CHAR(1)'
                                    WHEN c.typename = 'CHARACTER' THEN 'CHAR(' || c.length || ')'
                                    WHEN c.typename IN ('VARCHAR', 'CHAR', 'GRAPHIC', 'VARGRAPHIC') THEN c.typename || '(' || c.length || ')'
                                    WHEN c.typename IN ('DECIMAL', 'NUMERIC') THEN 
                                        CASE WHEN c.scale = 0 THEN c.typename || '(' || c.length || ')'
                                             ELSE c.typename || '(' || c.length || ',' || c.scale || ')' END
                                    ELSE c.typename
                                END || 
                                CASE 
                                    WHEN c.nulls = 'N' THEN ' NOT NULL' 
                                    ELSE '' 
                                END ||
                                CASE 
                                    WHEN c.identity = 'Y' THEN 
                                        ' GENERATED BY DEFAULT AS IDENTITY (START WITH 1,INCREMENT BY 1 NO MAXVALUE NO CACHE)'
                                    WHEN c.default IS NOT NULL THEN 
                                        ' DEFAULT ' || 
                                        CASE 
                                            WHEN c.default = 'CURRENT TIMESTAMP' THEN 'current timestamp'
                                            WHEN c.default LIKE '''%''' THEN 
                                                CASE 
                                                    WHEN UPPER(c.default) = c.default THEN c.default
                                                    ELSE LOWER(c.default)
                                                END
                                            ELSE 
                                                CASE 
                                                    WHEN UPPER(c.default) = c.default THEN c.default
                                                    ELSE LOWER(c.default)
                                                END
                                        END
                                    ELSE '' 
                                END AS column_definition
                            FROM 
                                syscat.columns c
                            WHERE 
                                c.tabschema = '{_schema}'
                        ),
                        foreign_keys AS (
                            SELECT 
                                c.tabschema,
                                c.tabname,
                                k.colname,
                                '    FOREIGN KEY(' || k.colname || ') REFERENCES ' || r.reftabname || 
                                ' ON DELETE ' || 
                                CASE r.deleterule
                                    WHEN 'A' THEN 'NO ACTION'
                                    WHEN 'C' THEN 'CASCADE'
                                    WHEN 'N' THEN 'SET NULL'
                                    WHEN 'R' THEN 'RESTRICT'
                                    ELSE 'NO ACTION'
                                END AS fk_definition
                            FROM 
                                syscat.tabconst c
                            JOIN 
                                syscat.keycoluse k ON c.tabschema = k.tabschema 
                                                  AND c.tabname = k.tabname 
                                                  AND c.constname = k.constname
                            JOIN
                                syscat.references r ON c.tabschema = r.tabschema
                                                  AND c.tabname = r.tabname
                                                  AND c.constname = r.constname
                            WHERE 
                                c.tabschema = '{_schema}'
                                AND c.type = 'F'
                        ),
                        other_constraints AS (
                            SELECT 
                                c.tabschema,
                                c.tabname,
                                '    CONSTRAINT ' || c.constname || 
                                CASE c.type
                                    WHEN 'U' THEN ' UNIQUE ('
                                    ELSE ' CHECK ('
                                END ||
                                LISTAGG(k.colname, ', ') WITHIN GROUP (ORDER BY k.colseq) || ')' AS constraint_definition,
                                ROW_NUMBER() OVER (PARTITION BY c.tabname ORDER BY c.constname) AS constraint_order
                            FROM 
                                syscat.tabconst c
                            JOIN 
                                syscat.keycoluse k ON c.tabschema = k.tabschema 
                                                  AND c.tabname = k.tabname 
                                                  AND c.constname = k.constname
                            WHERE 
                                c.tabschema = '{_schema}'
                                AND c.type IN ('U')  -- Unique keys (primary and foreign keys handled separately)
                            GROUP BY 
                                c.tabschema, c.tabname, c.constname, c.type
                        ),
                        indexes AS (
                            SELECT 
                                i.tabschema, 
                                i.tabname,
                                i.indname,
                                'CREATE INDEX ' || i.indname || ' ON ' || i.tabname || 
                                ' (' || LISTAGG(k.colname, ', ') WITHIN GROUP (ORDER BY k.colseq) || ');' AS index_ddl,
                                ROW_NUMBER() OVER (PARTITION BY i.tabname ORDER BY i.indname) AS index_order
                            FROM 
                                syscat.indexes i
                            JOIN 
                                syscat.indexcoluse k ON i.indschema = k.indschema AND i.indname = k.indname
                            WHERE 
                                i.tabschema = '{_schema}' 
                                AND i.indname NOT LIKE 'SQL%'  -- Skip system-generated indexes
                                AND NOT EXISTS (
                                    SELECT 1 FROM syscat.tabconst tc
                                    WHERE tc.tabschema = i.tabschema
                                      AND tc.tabname = i.tabname
                                      AND tc.type = 'U'
                                      AND tc.constname = i.indname
                                )  -- Exclude indexes that enforce unique constraints
                            GROUP BY 
                                i.tabschema, i.tabname, i.indname
                        ),
                        table_components AS (
                            SELECT 
                                ti.tabschema,
                                ti.tabname,
                                ti.has_primary_key,
                                ci.colno,
                                CASE 
                                    WHEN fk.fk_definition IS NOT NULL THEN 
                                        ci.column_definition || ',' || CHR(10) || fk.fk_definition
                                    ELSE 
                                        ci.column_definition
                                END AS column_with_fk
                            FROM 
                                table_info ti
                            JOIN 
                                column_info ci ON ti.tabschema = ci.tabschema AND ti.tabname = ci.tabname
                            LEFT JOIN 
                                foreign_keys fk ON ti.tabschema = fk.tabschema 
                                               AND ti.tabname = fk.tabname 
                                               AND ci.colname = fk.colname
                        ),
                        table_definitions AS (
                            SELECT 
                                tc.tabschema,
                                tc.tabname,
                                tc.has_primary_key,
                                LISTAGG(tc.column_with_fk, ',' || CHR(10)) WITHIN GROUP (ORDER BY tc.colno) AS column_list,
                                (SELECT pk.pk_definition FROM primary_keys pk 
                                 WHERE pk.tabschema = tc.tabschema AND pk.tabname = tc.tabname) AS pk_definition,
                                (SELECT LISTAGG(oc.constraint_definition, ',' || CHR(10)) 
                                 WITHIN GROUP (ORDER BY oc.constraint_order)
                                 FROM other_constraints oc 
                                 WHERE oc.tabschema = tc.tabschema AND oc.tabname = tc.tabname) AS other_constraints,
                                (SELECT COUNT(*) FROM indexes i WHERE i.tabschema = tc.tabschema AND i.tabname = tc.tabname) AS index_count
                            FROM 
                                table_components tc
                            GROUP BY 
                                tc.tabschema, tc.tabname, tc.has_primary_key
                        )

                        SELECT ddl_statement FROM (
                            -- DROP statements first (group 1) - ordered by has_primary_key
                            SELECT 
                                'DROP TABLE ' || t.tabname || ';' AS ddl_statement,
                                1 AS statement_group,
                                t.tabname AS table_name,
                                t.has_primary_key AS sub_order_1,
                                0 AS sub_order_2
                            FROM 
                                table_info t

                            UNION ALL

                            -- CREATE TABLE statements (group 2)
                            SELECT 
                                'CREATE TABLE ' || td.tabname || ' (' || CHR(10) ||
                                td.column_list ||
                                CASE WHEN td.pk_definition IS NOT NULL THEN ',' || CHR(10) || td.pk_definition ELSE '' END ||
                                CASE WHEN td.other_constraints IS NOT NULL THEN ',' || CHR(10) || td.other_constraints ELSE '' END ||
                                CHR(10) || ');' AS ddl_statement,
                                2 AS statement_group,
                                td.tabname AS table_name,
                                td.has_primary_key AS sub_order_1,
                                0 AS sub_order_2
                            FROM 
                                table_definitions td
                            WHERE
                                td.column_list IS NOT NULL

                            UNION ALL

                            -- INDEX statements for each table (group 2)
                            SELECT
                                x.index_ddl AS ddl_statement,
                                2 AS statement_group,
                                x.tabname AS table_name,
                                t.has_primary_key AS sub_order_1,
                                1 + x.index_order AS sub_order_2  -- Ensure indexes come after table creation
                            FROM 
                                indexes x
                            JOIN
                                table_info t ON x.tabschema = t.tabschema AND x.tabname = t.tabname
                        ) AS ddl_output
                        ORDER BY statement_group, sub_order_1, table_name, sub_order_2;";

            using (var connection = new DB2Connection(_connectionString))
            using (var command = new DB2Command(tablesSql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string statement = reader.GetString(0);
                        if (statement.StartsWith("CREATE INDEX"))
                        {
                            indexStatements.Add(statement);
                        }
                        else
                        {
                            allStatements.Add(statement);
                        }
                    }
                }
            }

            return allStatements;
        }



    }
}

