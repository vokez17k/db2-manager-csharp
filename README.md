## Architecture Overview

- **DB2Manager.cs**
  Core service responsible for DB2 database lifecycle operations
  (create, drop, backup, restore).

- **DB2ScriptManager.cs**
  Generates DROP and CREATE scripts for tables, views, and indexes,
  including dependency-aware view ordering.

- **GenerateTablesAndViews.cs**
  Extracts DB2 metadata and formats clean, reusable DDL scripts.

- **ScriptProcessor.cs**
  Executes SQL scripts safely with logging and error handling.

- **Manager.cs**
  WinForms UI layer that orchestrates database operations and
  provides user interaction.
