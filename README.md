# Task Management Module

A modular **ASP.NET Core Web API** for managing users, tasks, and task assignments — built with Entity Framework Core and SQL Server.

> **Developed by Vikramjit Singh**

---

## Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Database Schema](#database-schema)
- [Getting Started](#getting-started)
- [API Reference](#api-reference)
- [Frontend UI](#frontend-ui)
- [Key Design Decisions](#key-design-decisions)

---

## Overview

The Task Management Module is a RESTful API that allows you to:

- Create and manage **users**
- Create and manage **tasks**
- **Assign tasks to users** (many-to-many)
- **Track and update task status** (Pending / Completed)
- View a **combined details report** of all users, tasks, and statuses

The project is organized into sub-modules (Users, Tasks, Status) each with their own controller, service, repository, and model — following a clean, modular architecture pattern.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core (.NET 10) |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| API Docs | Swagger / OpenAPI |
| Frontend | Razor View (HTML + CSS + Vanilla JS) |
| IDE | Visual Studio 2026 |

---

## Project Structure

```
TmModule/
│
├── Application/                    # Cross-cutting use cases
│   ├── Controllers/
│   │   ├── AssignmentController.cs # Use-case endpoints (create, assign, details)
│   │   └── HomeController.cs       # Serves the frontend view
│   └── UseCases/
│       ├── CreateUserTask.cs       # Creates user + task + links them
│       ├── AssignmentService.cs    # Assigns existing tasks to existing users
│       └── DetailedView.cs         # Fetches combined user-task-status data
│
├── Modules/                        # Feature sub-modules
│   ├── Users/
│   │   ├── Controllers/UsersController.cs
│   │   ├── Models/UsersTable.cs
│   │   ├── Repositories/IRepositoryUsers.cs
│   │   └── Services/UsersService.cs
│   ├── Tasks/
│   │   ├── Controllers/TasksController.cs
│   │   ├── Models/TaskTable.cs
│   │   ├── Repositories/IRepositoryTask.cs
│   │   └── Services/TaskService.cs
│   └── Status/
│       ├── Controllers/StatusController.cs
│       ├── Models/StatusTable.cs
│       ├── Models/TypeofStatus.cs  # Enum: Pending, Completed
│       ├── Repositories/IRepositoryStatus.cs
│       └── Services/StatusUpdateService.cs
│
├── Infrastructure/
│   ├── DbContext.cs                # EF Core AppDbContext
│   └── DTOs/
│       ├── SimplifiedRequest.cs    # DTO for Quick Create
│       └── AssignmentRequest.cs    # DTO for bulk assignment
│
├── Views/
│   └── Home/
│       └── Index.cshtml            # Frontend UI (single file)
│
├── Migrations/                     # EF Core migration history
├── Program.cs                      # App setup and DI registration
└── appsettings.json                # Config and connection string
```

---

## Database Schema

Three tables connected by a composite-key join table:

```
Users               Tasks               Status (join table)
─────────────       ─────────────       ─────────────────────────
UserId   (PK)       TaskId   (PK)       UserId  (PK, FK → Users)
Username            Description         TaskId  (PK, FK → Tasks)
                                        CurrentStatus  (0=Pending, 1=Completed)
```

The `Status` table acts as both a **junction table** (many-to-many between Users and Tasks) and a **state tracker** (stores the task's current status per user).

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express edition is fine)
- [Visual Studio 2026](https://visualstudio.microsoft.com/) with the **ASP.NET and web development** workload

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/task-management-module.git
cd task-management-module
```

### 2. Configure the Database Connection

Open `appsettings.json` and update the connection string with your SQL Server details:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g. `localhost` or `.\SQLEXPRESS`).

### 3. Apply Database Migrations

Open **Package Manager Console** in Visual Studio (Tools → NuGet Package Manager → Package Manager Console) and run:

```
Update-Database
```

This creates the three tables in your SQL Server database automatically.

### 4. Run the Project

Press **F5** in Visual Studio (or `dotnet run` in the terminal).

The application starts on:
- **HTTPS:** `https://localhost:7283`
- **HTTP:** `http://localhost:5131`

Swagger UI is available at: `https://localhost:7283/swagger`

The frontend UI is available at: `https://localhost:7283/Home/Index`

---

## API Reference

### Use Case Endpoints

These are the primary, high-level endpoints designed around real workflows.

---

#### `POST /api/Assignment/create`
**Quick Create** — creates a user, a task, and links them together in a single request.

**Request Body:**
```json
{
  "userName": "alice",
  "taskDescription": "Review pull requests for sprint 3",
  "status": 0
}
```

| Field | Type | Values |
|---|---|---|
| `userName` | string | Any non-empty string |
| `taskDescription` | string | Any non-empty string |
| `status` | int | `0` = Pending, `1` = Completed |

**Response:** `200 OK` — `"User task created successfully."`

---

#### `POST /api/Assignment/assign`
**Bulk Assign** — assigns multiple existing tasks to multiple existing users (many-to-many). Duplicate assignments are skipped.

**Request Body:**
```json
{
  "userId": [1, 2],
  "taskId": [1, 2, 3]
}
```

**Response:** `200 OK` — `"Tasks assigned successfully."`

---

#### `GET /api/Assignment/details`
**Full Details View** — returns every user-task-status combination in the system.

**Response:**
```json
[
  {
    "userId": 1,
    "username": "alice",
    "taskId": 1,
    "description": "Review pull requests for sprint 3",
    "status": "Pending"
  }
]
```

---

### Users Endpoints

#### `POST /api/Users`
Create a new user.

**Request Body:**
```json
{ "username": "alice" }
```

**Response:** `201 Created` with the created user object.

---

#### `GET /api/Users`
Get all users.

**Response:** Array of user objects.

---

#### `GET /api/Users/{id}`
Get a single user by their ID.

**Response:** `200 OK` with user object, or `404 Not Found`.

---

### Tasks Endpoints

#### `POST /api/Tasks`
Create a new task.

**Request Body:**
```json
{ "description": "Set up CI/CD pipeline" }
```

**Response:** `201 Created` with the created task object.

---

#### `GET /api/Tasks`
Get all tasks.

---

#### `GET /api/Tasks/{id}`
Get a single task by ID.

---

### Status Endpoints

#### `POST /api/Status`
Manually create a status record linking a user to a task. Status defaults to Pending (`0`).

**Request Body:**
```json
{
  "userId": 1,
  "taskId": 2,
  "currentStatus": 0
}
```

---

#### `PATCH /api/Status`
Update the status of an existing user-task assignment.

**Request Body:**
```json
{
  "userId": 1,
  "taskId": 2,
  "currentStatus": 1
}
```

| `currentStatus` | Meaning |
|---|---|
| `0` | Pending |
| `1` | Completed |

---

#### `GET /api/Status/{userId}/{taskId}`
Get the current status for a specific user-task pair.

---

## Frontend UI

The project includes a single-file Razor view (`Views/Home/Index.cshtml`) that provides a clean UI for interacting with all API endpoints.

**Features:**
- Dark / Light mode toggle
- Sidebar navigation separating **Use Cases** from **Sub-Modules**
- Live connection indicator showing if the backend is reachable
- Response boxes showing HTTP status code and response time
- All 10 API endpoints accessible through forms

**To access the UI:**
Navigate to `https://localhost:7283/Home/Index` after running the project.

---

## Key Design Decisions

**Modular architecture** — Each domain (Users, Tasks, Status) is self-contained with its own controller, service, repository interface, and model. This makes it easy to extend or replace individual modules.

**Use case layer** — The `Application/UseCases/` folder contains business logic that crosses module boundaries (e.g. creating a user and task together). This keeps controllers thin and logic reusable.

**Composite primary key on Status** — The `Status` table uses `(UserId, TaskId)` as its composite key, which naturally enforces the constraint that one user can only have one status per task.

**Enum for status** — `TypeofStatus` is stored as an integer (`0` = Pending, `1` = Completed) in the database, which is efficient and easy to extend.

**CORS enabled** — The API allows requests from any origin in development, making the Razor frontend and Swagger both work without cross-origin issues.

---

## Author

**Vikramjit Singh**
ASP.NET Core · Entity Framework · SQL Server

---
