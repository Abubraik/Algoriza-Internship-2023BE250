# Algoriza-Internship-2023BE250

## Overview
Algoriza-Internship-2023BE250 is a .NET Core Web API project simulating vezeeta.com. It's designed with onion architecture, ensuring adherence to clean code principles and SOLID design. The API supports three roles: admin, doctor, and patient, each with specific functionalities. It features JWT authentication and email confirmation.

## Features

### Admin Role
- Dashboard with statistics (number of doctors, patients, requests)
- Top 5 specializations and top 10 doctors based on completed requests
- CRUD operations for doctors
- Discount code management

### Doctor Role
- Login and view bookings
- Confirm checkups
- Manage appointments (add, update, delete)

### Patient Role
- Register and login
- Search for doctors and book appointments
- Cancel appointments
- View personal booking history

### Security
- Passwords are auto-generated for both doctors and patients for enhanced security and are sent via email along with the email confirmation link.

## Technologies Used
- .NET Core
- Entity Framework
- JWT for authentication
- Onion Architecture

## Setup and Installation
Just install .NET Framework 7, clone the project and have fun.
