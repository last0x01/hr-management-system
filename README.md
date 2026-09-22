# HR_MS — Human Resources Management System

![Status](https://img.shields.io/badge/Status-Completed-brightgreen)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
![C#](https://img.shields.io/badge/language-C%23-178600?logo=csharp&logoColor=white)
![UI](https://img.shields.io/badge/UI-WPF%20%2F%20MVVM-512BD4)
![Database](https://img.shields.io/badge/Database-PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-3--Tier%20%2B%20MVVM-blue)

An **HR Management System** desktop application for managing employees, departments, attendance, and absences. Built on **.NET 8** with a **WPF (MVVM)** front end and a clean **3-tier backend** (Models, Data Access, Business Logic) over **PostgreSQL**, with **FastReport** for reporting and PDF export.

---

## Architecture

```
┌───────────────────────────────────────────────────────────┐
│  Front End — HR_MS (WPF + MVVM)                           │
│  Views / ViewModels / Commands / Behaviors / Converters   │
└───────────────────────────┬───────────────────────────────┘
                            │
                            ▼
┌───────────────────────────────────────────────────────────┐
│  Back End                                                  │
│  Business Layer (Services + Interfaces + Validations)      │
│  Data Access Layer (ADO.NET + Npgsql -> PostgreSQL)        │
│  Models (domain entities)                                  │
└───────────────────────────────────────────────────────────┘
```

- **Front End** — `Front End/HR_MS` — WPF desktop app following the **MVVM** pattern
- **Back End** — `Back End/`
  - `Models/` — domain entities (`clsUser`, `clsPerson`, `clsEmployee`, `clsDepartment`, `clsAttendance`, `clsAbsence`, `clsAbsenceType`)
  - `Data Access Layer/` — PostgreSQL data-access classes per entity (Npgsql)
  - `Business Layer/` — interface-first services + input validations

---

## Features

### Users & Login
- Login screen with validation (`LoginViewModel`)
- User management (add/edit via `UsersView` / `AddEditUserView`)

### Employees
- Employee and person records with full add/edit (`EmployeesView`)
- Input validation behavior (e.g. age, phone) via `InputBehavior`

### Departments
- Department management (`DepartmentsView`, `AddEditDepartmentView`)

### Attendance
- Record and manage employee attendance (`AttendancesView`, `AddEditAttendanceView`)

### Absences
- Define absence types (`clsAbsenceType`)
- Manage employee absences (`AbsencesView`, `AddEditAbsencesView`)

### Home Dashboard
- Overview dashboard (`HomeView`) with charts (**OxyPlot**) and modern styling

### Reporting
- Reports rendered with **FastReport.OpenSource**, with **PDF export** (`FastReport.OpenSource.Export.PdfSimple`)

### UI/UX
- Modern Metro styling (**MahApps.Metro**) with Material icon packs
- Font Awesome icons (**FontAwesome.Sharp**)
- Custom message dialogs and value converters

---

## Technologies

| Area          | Technology                                    |
| ------------- | --------------------------------------------- |
| Language      | C# (.NET 8)                                   |
| UI            | WPF + MVVM                                    |
| Database      | PostgreSQL (Npgsql 10)                        |
| Reporting     | FastReport OpenSource + PDF export            |
| Styling       | MahApps.Metro v2 + Material IconPacks         |
| Charts        | OxyPlot.Wpf                                   |
| Icons         | FontAwesome.Sharp                             |

---

## Getting Started

### Requirements
- .NET 8 SDK
- Visual Studio 2022 (or Rider / VS Code with C# Dev Kit)
- PostgreSQL 14+ instance

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/last0x01/hr-management-system.git
   ```

2. Set up a PostgreSQL database (e.g. `OpenDoor`) with the schema matching the `Models` and `Data Access Layer` entities.

3. Point the **Front End** `App.config` connection string to your database:

   ```xml
   <connectionStrings>
     <add name="MyDB"
          connectionString="Host=localhost;Port=5432;Database=YOUR_DB;Username=YOUR_USER;Password=YOUR_PASSWORD;" />
   </connectionStrings>
   ```

4. Open `Front End/HR_MS/HR_MS.sln`, build, and run.

---

## Project Structure

```
HR_MS
├── Back End/
│   ├── Models/               # Domain entities
│   ├── Data Access Layer/    # Npgsql data access
│   └── Business Layer/       # Service interfaces, implementations, validations
└── Front End/
    └── HR_MS/                # WPF MVVM application
        ├── MVVM/Views/       # Home, Logins, Users, Employees, Departments, Attendances, Absences
        ├── MVVM/ViewModels/
        ├── MVVM/Commands/    # RelayCommand
        ├── MVVM/Behaviors/   # InputBehavior
        ├── MVVM/Converters/
        ├── Services/
        ├── Styles/           # MahApps.Metro themes
        └── Utilities/
```

---

> **Security note:** change the hard-coded PostgreSQL password found in `App.config` before deploying anywhere shared.