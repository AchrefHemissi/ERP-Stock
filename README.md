
# ERP-Stock

**ERP-Stock** is a full-featured Enterprise Resource Planning (ERP) system focused on inventory and order management. Built with C# and a modern frontend stack, it helps businesses efficiently handle their stock levels, warehouse operations, and customer orders through a responsive and user-friendly web interface.

## 🌐 Live Demo

🔗 [Live Application](https://erp-stock-front-3j3i.vercel.app)

---

## 🛠️ Tech Stack

- **Frontend**: JavaScript, HTML, CSS  
- **Backend**: C# (.NET)  
- **Project Structure**:
  - `frontend/` – Frontend interface
  - `backend/` – Backend logic and APIs
  - `WebOrder/` – Additional web module (likely for order processing)

---

## 📦 Features

- **📊 Dashboard Overview**  
  Real-time dashboard summarizing stock levels, recent orders, and key metrics.

- **📦 Inventory Management**  
  Create, update, or remove product entries. Track item quantity, pricing, categories, and units.

- **📥 Stock In / 📤 Stock Out Tracking**  
  Monitor all inbound and outbound stock transactions with dates, users, and reference info.

- **📁 Multi-Warehouse Support**  
  Organize and track stock across multiple warehouses or locations.

- **🛒 Order Management**  
  Create and manage customer orders. Modify quantities, assign products, and track order fulfillment status.

- **📄 Invoices and Delivery Notes**  
  Automatically generate printable invoices and delivery slips for processed orders.

- **👥 User Authentication & Roles**  
  Secure login system with role-based access control (e.g., Admin, Manager, Staff).

- **📇 Supplier & Client Records**  
  Store and link supplier and client data with transactions.

- **📈 Analytics (Planned)**  
  View low stock alerts, best-selling products, and usage trends.

- **🌍 Responsive Web Design**  
  Mobile-first design for seamless usage on desktop, tablet, or mobile.

---

## 🚀 Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)  
- [Node.js and npm](https://nodejs.org/)

---

### Installation

1. **Clone the repository**

```bash
git clone https://github.com/Dhia9030/ERP-Stock.git
cd ERP-Stock
```

2. **Backend Setup**

```bash
cd backend
dotnet restore
dotnet build
dotnet run
```

3. **Frontend Setup**

```bash
cd ../frontend
npm install
npm start
```

4. **Access the Application**

Visit `http://localhost:3000` in your browser.

---

## 📁 Project Structure

```plaintext
ERP-Stock/
├── backend/           # C# .NET backend
├── frontend/          # Frontend interface (JS/HTML/CSS)
├── WebOrder/          # Additional module for order handling
├── ERP.sln            # Visual Studio solution file
└── README.md
```

---

## 🤝 Contributing

Contributions are welcome! If you find a bug or want to add a feature:

1. Fork the repo
2. Create a new branch (`git checkout -b feature-name`)
3. Commit your changes
4. Push to the branch (`git push origin feature-name`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---
