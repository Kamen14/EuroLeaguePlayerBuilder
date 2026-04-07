# 🏀 EuroLeague Player Builder

A web application that allows basketball enthusiasts to create and manage custom players, arenas, and games for the EuroLeague.

> ⚠️ **Setup Required:** Before running the app, you must apply the database migrations and update the connection string in `appsettings.json`. See [Getting Started](#getting-started) below.

---

## 📋 Table of Contents

- [About](#about)
- [Key Features](#key-features)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Roles & Permissions](#roles--permissions)

---

## About

**EuroLeague Player Builder** is a web application that provides an interactive way to build your own roster and manage players across six pre-existing EuroLeague teams. Beyond players, users can create custom arenas and simulate realistic basketball games — all within a role-based system that keeps data ownership clear and protected.

---

## Key Features

### 🏀 Players
- Create custom basketball players with attributes and assign them to one of the six existing EuroLeague teams
- Edit or delete only the players you have created — default and other users' players are protected

### 🏟️ Arenas
- Each user can create their own arena with relevant details
- Users can only edit or delete arenas they have created

### 🎮 Games
- Create and delete games between teams
- Simulate a game with the **Simulate** button, which generates a realistic basketball score
- Users can only see and manage games they have created

### 👥 Teams
- Six pre-existing EuroLeague teams, each with a coach, city, arena info, and a full roster view

### 🔐 Roles & Ownership
- Two roles: **Admin** and **User**
- Every newly registered user gets the **User** role by default
- Admins can change other users' roles but **not their own** (to prevent lockout)
- Admins can edit or delete any user-created entity regardless of who created it

---

## Architecture

The project is split into focused layers for separation of concerns:

| Layer | Responsibility |
|---|---|
| `Data` | EF Core DbContext, migrations, repositories, seed data |
| `Data.Models` | Entity classes (Player, Team, Arena, Game, etc.) |
| `Services` | Business logic |
| `Services.Models` | DTOs |
| `Web` | ASP.NET Core MVC — controllers, views, middleware |
| `ViewModels` | View-specific models passed to Razor views |
| `GCommon` | Shared constants, enums, utilities, etc. |

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 |
| Language | C# 12 |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Frontend | Razor Views / Bootstrap |
| Auth & Roles | ASP.NET Core Identity |
| Pattern | Repository pattern with DTOs |

---

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Update the connection string**

   Open `appsettings.json` and set your SQL Server connection:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=EuroLeaguePlayerBuilderDb;Trusted_Connection=True;"
   }
   ```

2. **Apply migrations**


3. **Run the app**

---

## Roles & Permissions

| Action | User | Admin |
|---|---|---|
| Create players / arenas / games | ✅ | ✅ |
| Edit / delete own entities | ✅ | ✅ |
| Edit / delete other users' entities | ❌ | ✅ |
| Change other users' roles | ❌ | ✅ |
| Change own role | ❌ | ❌ |


> Built with ❤️ for EuroLeague basketball fans.

