# Online Sales Management Web Application :  ClickNTrade 
![favicon2](https://github.com/HafsaTATA/ClickNTrade/assets/120058921/462ba02f-2f5d-4b58-810c-5ca834ee704c) 


## Overview

This web application is developed using **ASP.NET Core 8** with the **MVC** architecture. The application leverages **Entity Framework** for seamless data access and is powered by **SQL Server** as the underlying database. The front-end is designed for responsiveness using **Bootstrap**, while enhanced user interactivity is achieved through the integration of **jQuery** and **Ajax**.

## Project Objective

The primary goal of this project is to create a user-friendly and efficient web application dedicated to online sales. This platform targets both individuals and companies, aiming to provide a seamless experience for visitors and comprehensive management tools for owners and administrators. The project's ambition is to deliver a complete and innovative solution tailored to the specific needs of the online sales market.

## Functional Requirements

### Home Page
- Display statistics and special offers.
- Access to user accounts (authentication) and language change.

### Admin Profile
- Dashboard showcasing the history of all clients.
- Blacklist management and favorites list management.
- User and database management.

### Owner Profile
- Form for entering owner information (for individuals or companies).
- Access to the owner's history.

### Property Profile
- Form for entering property information and owner details.
- Update or delete property information.
- List of available items with availability after selecting a date.
- Search function for items based on criteria (brand, color, year, mileage, owner name, etc.).

### Tenant Profile
- Information form and reservation form for selecting a property and choosing a payment method.
- Access to tenant's history.

## Roles

- **Administrator:** Privileged access to the dashboard with features like user history, blacklist management, and favorites management. Responsible for managing owners, properties, and the database.

- **Owner:** Authenticated access to the platform. Can provide property information, view history, add, update, and delete property information, view a complete list of properties in a given category, and refine search criteria.

- **Guests:** Can visit the site without authentication. Can view property and owner information to contact them.

## Non-Functional Requirements

- **Availability:** The application must be continuously accessible and function without interruption.

- **Performance:** The application must be fast and responsive for an optimal user experience.

- **Security:** Protect sensitive user data (e.g., payment information) and safeguard against cyber attacks.

- **Reliability:** Handle exceptions gracefully, log incidents on the backend, and ensure a seamless user experience without displaying errors.

- **Maintenance:** The application should be easy to maintain and update for long-term sustainability.

- **Internationalization:** Adapt to different languages and cultures.

- **Response Time:** Ensure fast loading times through optimal hosting.

- **Usability:** The user interfaces should be simple, ergonomic, and user-friendly.

## Technologies Used

- **Backend:** ASP.NET Core 8, MVC architecture, Entity Framework, SQL Server.
#### 
![icons8- net-framework-48](https://github.com/HafsaTATA/ClickNTrade/assets/120058921/66b10f05-8c80-4e56-ad87-de666c4e4f99)
![MS SQL Server](https://img.icons8.com/color/48/000000/microsoft-sql-server.png)
![C#](https://img.icons8.com/color/48/000000/c-sharp-logo.png)


- **Frontend:** Bootstrap, jQuery, Ajax.
#### 
![Bootstrap](https://img.icons8.com/color/48/000000/bootstrap.png) 
![jQuery](https://img.icons8.com/ios-filled/48/1169ae/jquery.png)
![javascript](https://img.icons8.com/color/48/000000/javascript--v1.png)
![icons8-html5-48](https://github.com/HafsaTATA/ClickNTrade/assets/120058921/1f958197-b1d3-4654-8764-8c4de37f8ea3)
![icons8-css3-48](https://github.com/HafsaTATA/ClickNTrade/assets/120058921/a6f93301-87e1-48b2-9d40-beb967ba0d2f)


## Demo

here's a live demo of the application:


## Getting Started

To run this application locally, follow these steps:

1. Clone the repository.
2. Install the required dependencies using [NuGet Package Manager](https://www.nuget.org/).
3. Set up the SQL Server database using Entity Framework migrations.
4. Configure the application settings, such as database connection strings, in the `appsettings.json` file.
5. Build and run the application.
