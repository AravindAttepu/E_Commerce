# 🛒 E-Commerce Demo

This **E-Commerce Demo** is a comprehensive **ASP.NET Core MVC** application designed to simulate the functionalities of a modern online store. It serves as an ideal platform for **demonstrations, testing, and prototyping**, offering insights into the architecture and features of a full-fledged e-commerce system.

## 🚀 Features

- **🔑 User Authentication** – Secure user registration and login using **JWT** (for users) and **session-based authentication** (for admins).
- **📦 Product Management** – Browse, filter, and manage products and categories.
- **🛍️ Shopping Cart** – Add products, update quantities, and calculate totals.
- **📑 Order Processing** – Simulated order placement and status tracking.
- **🛠️ Admin Panel** – Manage products, categories, and orders.
- **📱 Responsive Design** – Optimized for seamless experience across all devices.

## 🛠 Technologies Used

- **Backend**: ASP.NET Core MVC with **Entity Framework Core (EF Core)**
- **Frontend**: HTML, CSS, JavaScript
- **Database**: SQL Server
- **Authentication**: JWT for users, session-based authentication for admins

## ⚡ Getting Started

To set up and run this project locally, follow these steps:

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/AravindAttepu/E_Commerce.git

```
### 2️⃣ Navigate to the Project Directory

```bash
cd E_Commerce
```
### 3️⃣ Set Up the Database

Ensure SQL Server is installed and running.

Update the connection string in appsettings.json to match your database configuration.

Apply migrations to create the database schema:

```bash
dotnet ef database update
```

### 4️⃣ Run the Application

```bash
dotnet run
```

# 🔗 Access the Application

Open your browser and navigate to **[http://localhost:5001](http://localhost:5001)** (or the assigned port).

## 🤝 Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an **issue** or submit a **pull request**.

## ⚠️ Note  

This application is for **demonstration purposes only** and does not process real transactions.
