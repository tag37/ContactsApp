# Contacts App

## Description

The **Contacts App** is a simple web application built using Angular for the front-end and .NET Core for the back-end. The app allows users to manage contact information, including creating, updating, and deleting contacts. It features a clean, responsive UI and uses reactive forms for handling user inputs.

This app is designed to showcase how to implement basic CRUD operations with Angular and .NET Core, along with features such as routing, form validation, and integration with REST APIs.

## Features

- **Contact List**: View a list of all contacts with their details.
- **Create Contact**: Add a new contact with details such as first name, last name, and email.
- **Edit Contact**: Update existing contact information.
- **Delete Contact**: Remove a contact from the list.
- **Reactive Forms**: Uses Angular's reactive forms to handle form inputs and validations.
- **Responsive Design**: The app is fully responsive and works on all screen sizes.
- **Routing**: Navigate between pages for creating, editing, and viewing contacts.

## Tech Stack

- **Front-End**: 
  - Angular 13
  - Bootstrap 5 (for styling)
  - Reactive Forms for form handling
- **Back-End**: 
  - .NET Core
  - Entity Framework (for data persistence)
  - JSON-based file storage
- **Testing**: 
  - xUnit (for unit testing)
  - NSubstitute (for mocking dependencies)

## Setup Instructions

### Prerequisites

Before you can run the app, make sure you have the following installed:

- **Node.js** (version 12 or higher)
- **Angular CLI** (version 13 or higher)
- **.NET SDK** (version 6 or higher)

### Front-End Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/tag37/ContactsApp.git
   
   ```

2. Run the application:
   ```bash
   cd ContactsApp\ContactsApp
   dotnet restore
   dotnet build
   dotnet run
   ```

   The app will be accessible at `http://localhost:5062`.
   Swagger documentation is accessible at `http://localhost:5062/swagger/index.html`

## Testing

The project includes unit tests for the back-end.

### Back-End Tests (.NET Core)

Run the following command to execute .NET Core unit tests using xUnit:
```bash
cd ContactsApp
dotnet test
```