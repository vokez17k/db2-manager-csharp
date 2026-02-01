using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class ScriptProcessor
    {
        public List<string> SplitStatements(string scriptContent)
        {
            // Split by semicolon that's not within quotes or comments
            var statements = new List<string>();
            var sb = new StringBuilder();
            bool inString = false;
            bool inComment = false;

            foreach (char c in scriptContent)
            {
                if (c == '\'' && !inComment) inString = !inString;
                if (c == '-' && !inString && sb.Length > 0 && sb[sb.Length - 1] == '-')
                    inComment = true;

                sb.Append(c);

                if (c == '\n') inComment = false;

                if (c == ';' && !inString && !inComment)
                {
                    string statement = sb.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(statement))
                    {
                        statements.Add(statement);
                    }
                    sb.Clear();
                }
            }

            // Add any remaining content
            string finalStatement = sb.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(finalStatement))
            {
                statements.Add(finalStatement);
            }

            return statements;
        }

        public ScriptExecutionResult ExecuteScript(DB2Manager dbManager, string scriptContent, bool useTransaction)
        {
            var result = new ScriptExecutionResult();
            var statements = SplitStatements(scriptContent);
            result.TotalStatements = statements.Count;

            try
            {
                if (useTransaction) dbManager.BeginTransaction();

                foreach (string statement in statements)
                {
                    try
                    {
                        int rowsAffected = dbManager.ExecuteNonQuery(statement);
                        result.SuccessfulStatements++;
                        result.RowsAffected += rowsAffected;
                    }
                    catch (Exception ex)
                    {
                        result.FailedStatements++;
                        result.Errors.Add(new ScriptError
                        {
                            Statement = statement,
                            ErrorMessage = ex.Message
                        });

                        if (useTransaction)
                        {
                            dbManager.RollbackTransaction();
                            result.WasRolledBack = true;
                        }

                        result.IsSuccess = false;
                        return result;
                    }
                }

                if (useTransaction) dbManager.CommitTransaction();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                if (useTransaction && dbManager.IsInTransaction)
                {
                    dbManager.RollbackTransaction();
                    result.WasRolledBack = true;
                }

                result.IsSuccess = false;
                result.Errors.Add(new ScriptError
                {
                    ErrorMessage = $"Fatal error: {ex.Message}"
                });
            }

            return result;
        }
    }

    public class ScriptExecutionResult
    {
        public bool IsSuccess { get; set; }
        public int TotalStatements { get; set; }
        public int SuccessfulStatements { get; set; }
        public int FailedStatements { get; set; }
        public int SkippedStatements { get; set; }
        public long RowsAffected { get; set; }
        public bool WasRolledBack { get; set; }
        public List<ScriptError> Errors { get; } = new List<ScriptError>();
    }

    public class ScriptError
    {
        public string Statement { get; set; }
        public string ErrorMessage { get; set; }
    }
}
