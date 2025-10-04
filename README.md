# 🧾 Dapper ASP.NET Core Web API

## 🚀 Overview
مشروع **Web API** مبني باستخدام **ASP.NET Core** و**Dapper** كـ Micro ORM لتنفيذ عمليات الـ CRUD على قاعدة بيانات SQL Server بطريقة خفيفة وسريعة.

---

## ⚙️ Technologies
- ASP.NET Core 8  
- Dapper  
- SQL Server (LocalDB)  
- Swagger UI  

---

## 🏗️ Features
- CRUD كامل على الشركات (Company)  
- عرض الشركات مع الموظفين (**Multiple Mapping**)  
- استعلامات متعددة النتائج (**Multiple Results**)  
- تنفيذ **Transactions** لإضافة أكثر من شركة دفعة واحدة  

---

## 🧠 Endpoints

| Method | Endpoint | Description |
|---------|-----------|-------------|
| **GET** | `/api/company` | Get all companies |
| **GET** | `/api/company/{id}` | Get company by ID |
| **POST** | `/api/company` | Create a company |
| **PUT** | `/api/company/{id}` | Update company |
| **DELETE** | `/api/company/{id}` | Delete company |
| **POST** | `/api/company/CreateMultipleCompanies` | Add multiple companies (Transaction) |

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
🧪 Run the Project
Configure your connection string in appsettings.json

Run:

bash
Copy code
dotnet run
Open Swagger:

bash
Copy code
https://localhost:7194/swagger

