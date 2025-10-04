# 🧾 Dapper ASP.NET Core Web API

## 🚀 Overview
A **Web API** built with **ASP.NET Core** and **Dapper** (Micro ORM) to perform CRUD operations on a SQL Server database efficiently and cleanly.

---

## ⚙️ Technologies
- ASP.NET Core 8  
- Dapper  
- SQL Server  
- Swagger UI  

---

## 🏗️ Features
- Full CRUD operations for **Companies**  
- Retrieve companies with employees (**Multiple Mapping**)  
- Get multiple result sets (**Multiple Results**)  
- Add multiple companies in one transaction (**Transaction Support**)  

---

## 🧠 API Endpoints

| Method | Endpoint | Description |
|---------|-----------|-------------|
| **GET** | `/api/company` | Get all companies |
| **GET** | `/api/company/{id}` | Get a company by ID |
| **POST** | `/api/company` | Create a new company |
| **PUT** | `/api/company/{id}` | Update a company |
| **DELETE** | `/api/company/{id}` | Delete a company |
| **POST** | `/api/company/CreateMultipleCompanies` | Create multiple companies (Transaction) |

---

## 🧩 Example JSON
```json
[
  { "name": "OpenAI", "address": "San Francisco", "country": "USA" },
  { "name": "Microsoft", "address": "Redmond", "country": "USA" }
]
⚙️ Database Setup
sql
Copy code
CREATE DATABASE DapperDB;

CREATE TABLE Companies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Address NVARCHAR(100),
    Country NVARCHAR(50)
);

CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Age INT,
    Position NVARCHAR(100),
    CompanyId INT FOREIGN KEY REFERENCES Companies(Id)
);
🧪 How to Run
Update the connection string in appsettings.json

Run the project:
Open Swagger UI:

https://localhost:7194/swagger

bash
Copy code
dotnet run
