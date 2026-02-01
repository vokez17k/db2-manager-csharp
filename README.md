# DB2 Manager – C# WinForms Database Utility

DB2 Manager is a **Windows desktop application built in C# (WinForms)** that simplifies **IBM DB2 database administration tasks** such as creating and dropping databases, restoring backups, generating DDL scripts, and executing SQL scripts directly against a DB2 instance.

It is designed for **DB2 developers, system administrators, and support engineers** who want a lightweight GUI alternative to heavy DB tools.

---

## 🚀 Features

### 🔌 Database Connection
- Connect to **IBM DB2 databases** using server, username, and password
- Auto-load available databases
- Reconnect / refresh database list

### 🗄️ Database Management
- Create databases
- Drop databases
- Restore databases from backup files
- Reload database catalog dynamically

### 📜 Script Generation
- Generate **CREATE TABLE** scripts
- Generate **VIEW DDL scripts**
- Export database schema scripts
- Clean, DB2-compatible SQL output

### ▶ Script Execution
- Execute SQL scripts directly into a selected database
- Run bulk SQL files
- Execute generated scripts instantly

### 📊 Excel to SQL
- Convert Excel data into SQL INSERT statements
- Prepare data for DB2 import

### 🖥️ User Interface
- Simple, clean WinForms UI
- One-click actions
- Status indicators for connection state

---

## 🖼️ Screenshots

> Screenshots help reviewers and employers understand the project quickly.

| Connection Screen |
|------------------|
| ![Connect](screenshots/connect.png) |

> Place images inside the `screenshots/` folder.

---

## 🛠️ Technologies Used

- **C# (.NET Framework / WinForms)**
- **IBM DB2**
- **IBM.Data.DB2 Provider**
- **ADO.NET**
- Windows Desktop UI

---

## 📁 Project Structure
db2-manager-csharp/
├── src/
│   └── DatabaseManager/
├── screenshots/
├── scripts/
├── README.md
├── LICENSE
└── .gitignore
---

## ⚙️ Requirements

- Windows OS
- .NET Framework 4.x+
- IBM DB2 installed or remote DB2 server
- IBM DB2 .NET provider (`IBM.Data.DB2`)

---

## 🧪 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/vokez17k/db2-manager-csharp.git
  Open the solution in Visual Studio
Restore NuGet packages (if any)
Build and run the project
Enter:
Server (e.g. localhost)
Username
Password
Select database
Click Connect
🔐 Security Notes
Credentials are not stored permanently
No hard-coded passwords
Intended for local or trusted environments
🎯 Use Cases
DB2 schema migration
Quick database restores
Script generation for deployment
Learning DB2 administration
Support / troubleshooting environments
📌 Future Improvements
Role-based permissions
DB2 version detection
Backup scheduling
Script preview formatting
Logging & audit trail
Dark/light theme toggle
🤝 Contributions
Contributions are welcome!
Fork the repo
Create a feature branch
Submit a pull request
📜 License
This project is licensed under the MIT License.
