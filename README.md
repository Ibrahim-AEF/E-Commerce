# E-Commerce System

The E-Commerce System is a RESTful API built with ASP.NET Core using Onion Architecture that allows users to browse products, manage a shopping basket, place orders, and complete payments.  
The system integrates SQL Server for relational data storage, Redis for efficient basket management, and uses modern best practices like Unit of Work, Generic Repository, and Specification Pattern to ensure clean and scalable code.

---

## Technologies Used

- **ASP.NET Core 8**: Backend Framework
- **Entity Framework Core**: ORM for database interaction
- **SQL Server**: Main database
- **Redis**: Basket caching
- **ASP.NET Identity & JWT**: Authentication and Authorization
- **AutoMapper**: Object-to-Object Mapping
- **Swagger**: API documentation and testing

---

## Design Patterns and Architecture

- **Unit of Work Pattern**: Manage transactions across multiple repositories.
- **Generic Repository Pattern**: Reusable repository logic for all entities.
- **Specification Pattern**: Flexible querying of data.
- **DTOs (Data Transfer Objects)**: Secure and optimized data communication.
- **AutoMapper Mapping Profiles**: Automatic mapping between entities and DTOs.
- **Global Exception Handling Middleware**: Centralized error handling.
- **Pagination**: Efficient data loading for product lists.

---

## Features

- **Authentication**:
  - User Registration and Login using Identity and JWT tokens.
- **Product Management**:
  - Browse products by brand and type.
  - View product images and details.
- **Basket Management**:
  - Add products to basket.
  - Persist basket data using Redis.
- **Order Management**:
  - Create orders from basket contents.
  - Choose payment methods and complete payments.
- **Error Handling**:
  - All unhandled exceptions are processed through a global error middleware.
- **Pagination**:
  - API supports pagination for product listing to enhance performance.
- **Payment Methods**:
  - Make a Payment and Stripe To make Delivery.

---

## Project Structure

- **Entities**: Database Models
- **Repositories**: Data access layer with generic repository and specifications
- **Services**: Business logic implementation
- **Controllers**: API endpoints
- **DTOs**: Data structures for request and response models
- **Mapping Profiles**: AutoMapper configurations

Each feature in the project is built in step-by-step layers: **Entities → Repositories → Services → Controllers**.
