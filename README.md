Product Store Backend
This repository contains the backend logic for an online store. The backend is built using ASP.NET Core and leverages ASP.NET Identity for user authentication and authorization. It handles the business logic for managing users, products, and brands while providing the necessary APIs for the frontend to interact with.

Features
User Authentication:

Users can register using their email and password.
Login functionality is secured with ASP.NET Identity.
Roles and permissions are managed using the Identity Role Manager and User Manager.
Custom user claims are implemented via AppUserClaimsPrincipalFactory.
Product and Brand Management:

Products and brands are stored in a database and modeled with appropriate relationships.
Users can fetch, add, or modify product details.
API endpoints allow sorting and filtering of products.
Database Integration:

The backend interacts with the database to retrieve and store data.
Entity Framework Core is used for ORM and data manipulation.
Technologies Used
Backend Framework: ASP.NET Core
Database: Entity Framework Core with SQL Server
Authentication: ASP.NET Identity
Language: C#
Getting Started
Prerequisites
.NET 6 SDK or later installed.
SQL Server for the database.
Setup Instructions
Clone the repository:

bash
Copy code
git clone https://github.com/YourUsername/ProductStore_Backend.git  
cd ProductStore_Backend  
Update the appsettings.json file with your SQL Server connection string:

json
Copy code
"ConnectionStrings": {  
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;"  
}  
Apply migrations to set up the database:

bash
Copy code
dotnet ef database update  
Run the application:

bash
Copy code
dotnet run  
Test the API endpoints using a tool like Postman or integrate with the frontend.

Key Endpoints
User Management:

POST /api/auth/register: Register a new user.
POST /api/auth/login: Login with email and password.
Product Management:

GET /api/products: Fetch all products.
POST /api/products: Add a new product.
GET /api/products/{id}: Fetch details for a specific product.
Future Enhancements
Implement advanced search and filtering options.
Integrate payment gateway support.
Add unit and integration tests for enhanced reliability.
Frontend
The frontend is handled in a separate repository and is responsible for rendering the UI and consuming the backend APIs.

License
This project is licensed under the MIT License.
