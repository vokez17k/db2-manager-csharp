using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBM.Data.DB2;

namespace DatabaseManager
{
    public class DB2Manager : IDisposable
    {
        private DB2Connection _connection;
        private DB2Transaction _currentTransaction;

        public bool IsConnected => _connection?.State == ConnectionState.Open;
        public bool IsInTransaction => _currentTransaction != null;


        public void BeginTransaction()
        {
            if (!IsConnected) throw new InvalidOperationException("Not connected to database");
            _currentTransaction = _connection.BeginTransaction();
        }
        public void Connect(string server, string userId, string password, string database = null)
        {
            try
            {
                string connString;

                if (string.IsNullOrEmpty(database))
                {
                    // Try without database first
                    connString = $"Server={server};UID={userId};PWD={password};";
                }
                else
                {
                    connString = $"Server={server};Database={database};UID={userId};PWD={password};";
                }

                _connection = new DB2Connection(connString);
                _connection.Open();

                // Verify connection actually worked
                using (var cmd = new DB2Command("SELECT 1 FROM SYSIBM.SYSDUMMY1", _connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result == null || result.ToString() != "1")
                        throw new Exception("Connection verification failed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to connect to DB2 server: {ex.Message}", ex);
            }
        }



        public void CommitTransaction()
        {
            _currentTransaction?.Commit();
            _currentTransaction = null;
        }

        public void RollbackTransaction()
        {
            _currentTransaction?.Rollback();
            _currentTransaction = null;
        }
        public List<string> GetLocalDatabases()
        {
            var databases = new List<string>();

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "db2cmd.exe",
                    Arguments = "/c db2 list db directory",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                using (var reader = process.StandardOutput)
                {
                    string output = reader.ReadToEnd();
                    process.WaitForExit();

                    // Parse output for database names
                    var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        if (line.Trim().StartsWith("Database alias", StringComparison.OrdinalIgnoreCase))
                        {
                            var dbName = line.Split('=').Last().Trim();
                            if (!string.IsNullOrWhiteSpace(dbName))
                                databases.Add(dbName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to list local databases:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return databases.Distinct().OrderBy(x => x).ToList();
        }

        public bool DatabaseExists(string databaseName)
        {
            try
            {
                // Method 1: Query catalog tables (most reliable)
                string sql = @"
            SELECT 1 FROM SYSIBMADM.SNAPDB 
            WHERE DB_NAME = @dbname
            FETCH FIRST 1 ROW ONLY";

                using (var cmd = new DB2Command(sql, _connection))
                {
                    cmd.Parameters.Add("@dbname", DB2Type.VarChar, 128).Value = databaseName;
                    var result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value;
                }

                /* Alternative Methods:
                // Method 2: Check database directory
                string sql = $"SELECT 1 FROM TABLE(SYSPROC.ADMIN_GET_DBPATHS()) AS PATHS " +
                            $"WHERE PATH_SCHEMA = '{databaseName.ToUpper()}' " +
                            $"FETCH FIRST 1 ROW ONLY";

                // Method 3: Try to connect (works but is slower)
                try 
                {
                    using (var testConn = new DB2Connection(
                        $"Server={_connection.DataSource};Database={databaseName};" +
                        $"UID={_connection.Credential.UserId};PWD={_connection.Credential.Password}"))
                    {
                        testConn.Open();
                        return true;
                    }
                }
                catch { return false; }
                */
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking database existence: {ex.Message}", ex);
            }
        }

        public void CreateDatabase(string dbName)
        {
            //string databaseName = txtDatabase.Text.Trim();

            if (string.IsNullOrEmpty(dbName))
            {
                MessageBox.Show("Please enter a database name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to create database '{dbName}'?",
                                                 "Confirm Creation",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string batFile = Path.Combine(Path.GetTempPath(), "create_db.bat");
                    string cmdFile = Path.Combine(Path.GetTempPath(), "create_db.sql");
                    string logFile = Path.Combine(Path.GetTempPath(), $"create_db_{dbName}.log");

                    // Create the SQL command file
                    string db2Commands = $@"
                    CREATE DATABASE {dbName} AUTOMATIC STORAGE YES ON C:\ PAGESIZE 32 K;
                    CONNECT TO {dbName};
                    CONNECT RESET;";

                    File.WriteAllText(cmdFile, db2Commands);

                    // Create the batch file
                    File.WriteAllText(batFile,
                        $@"@echo off
                        db2cmd /c /w db2 -tvf ""{cmdFile}"" > ""{logFile}"" 2>&1
                        exit %ERRORLEVEL%");

                    // Execute the creation
                    ExecuteDb2Command(batFile, $"Database '{dbName}' created successfully", $"Failed to create database '{dbName}'");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void DropDatabase(string dbName)
        {
            //string databaseName = txtDatabase.Text.Trim();

            if (string.IsNullOrEmpty(dbName))
            {
                MessageBox.Show("Please enter a database name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to DROP database '{dbName}'?\nThis action cannot be undone!",
                                                 "Confirm Drop",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string batFile = Path.Combine(Path.GetTempPath(), "drop_db.bat");
                    string cmdFile = Path.Combine(Path.GetTempPath(), "drop_db.sql");
                    string logFile = Path.Combine(Path.GetTempPath(), $"drop_db_{dbName}.log");

                    // Create the SQL command file
                    string db2Commands = $@"                  
                    DB2STOP;
                    DB2START;
                    DROP DATABASE {dbName};";

                    File.WriteAllText(cmdFile, db2Commands);

                    // Create the batch file
                    File.WriteAllText(batFile,
                        $@"@echo off
                        db2cmd /c /w db2 -tvf ""{cmdFile}"" > ""{logFile}"" 2>&1
                        exit %ERRORLEVEL%");

                    // Execute the drop
                    ExecuteDb2Command(batFile, $"Database '{dbName}' dropped successfully", $"Failed to drop database '{dbName}'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error dropping database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void CreateDatabaseNOTGOOD(string databaseName)
        {
            if (DatabaseExists(databaseName))
                throw new Exception($"Database '{databaseName}' already exists");

            string createSql = $"CREATE DATABASE {databaseName} AUTOMATIC STORAGE YES ON C:\\ PAGESIZE 32 K";
            ExecuteNonQuery(createSql);

            // ✅ Auto-catalog locally
            //ExecuteDb2Command($"CATALOG DATABASE {databaseName} ON C:\\");
        }

        public int ExecuteNonQuery(string sql)
        {
            using (var cmd = new DB2Command(sql, _connection, _currentTransaction))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sql)
        {
            using (var cmd = new DB2Command(sql, _connection, _currentTransaction))
            {
                return cmd.ExecuteScalar();
            }
        }


        private void ExecuteDb2CommandNOTFINE(string command)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "db2cmd.exe",
                Arguments = $"/c db2 {command}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                process.WaitForExit();
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


        public DataTable ExecuteQuery(string sql)
        {
            var dt = new DataTable();
            using (var cmd = new DB2Command(sql, _connection, _currentTransaction))
            {
                using (var adapter = new DB2DataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _connection?.Dispose();
        }
    }


}
