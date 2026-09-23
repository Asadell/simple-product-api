# simple-product-api

A simple, production-ready ASP.NET Core 9 Web API template with JWT Authentication, PostgreSQL database via Entity Framework Core, Product CRUD operations, and Swagger UI / OpenAPI support.

## 🚀 Features

- **Framework**: ASP.NET Core 9 Web API (Controllers pattern)
- **Authentication**: JWT Bearer Access Token (BCrypt password hashing)
- **Database**: PostgreSQL with Entity Framework Core Code-First Migrations
- **API Documentation**: OpenAPI + Swagger UI (`/swagger`)
- **Docker Ready**: Multi-stage `Dockerfile` + `docker-compose.yml`
- **CI/CD**: GitHub Actions → GHCR → auto-deploy via Watchtower

---

## 🌐 Production

| Resource | URL |
|---|---|
| Base URL | `https://product-api.himitpens.net` |
| Swagger UI | `https://product-api.himitpens.net/swagger` |

---

## 🛠️ Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/) (or Docker)

### 1. Configuration
Copy `.env.example` to `.env` or set environment variables / appsettings:
```bash
cp .env.example .env
```

### 2. Run Locally
```bash
cd SimpleApi
dotnet restore
dotnet ef database update
dotnet run
```
Open **`http://localhost:5250/swagger`** in your browser to interact with the API via Swagger UI.

---

## 🐳 Running with Docker

```bash
docker compose up -d
```

---

## 📌 API Endpoints

All responses follow a consistent wrapper format:
```json
{
  "success": true,
  "message": "...",
  "data": { ... }
}
```

### Authentication (`/api/auth`)
| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/auth/register` | Public | Register a new user and receive JWT access token |
| `POST` | `/api/auth/login` | Public | Login and receive JWT access token |

### Products (`/api/products`)
| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/products` | Bearer Token | Get all products |
| `GET` | `/api/products/{id}` | Bearer Token | Get product by ID |
| `POST` | `/api/products` | Bearer Token | Create a new product |
| `PUT` | `/api/products/{id}` | Bearer Token | Update a product by ID |
| `DELETE` | `/api/products/{id}` | Bearer Token | Delete a product by ID |

---

## 📄 License
MIT
