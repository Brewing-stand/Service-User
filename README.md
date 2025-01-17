# User Service

## Table of Contents
1. [Description](#description)
2. [Purpose](#purpose)
3. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Clone the Repository](#clone-the-repository)
   - [Install Dependencies](#install-dependencies)
   - [Set Environment Variables](#set-environment-variables)
   - [Run the Server](#run-the-server)
   - [Access the Application](#access-the-application)
   - [Debugging (Development only)](#debugging-development-only)
4. [Endpoints](#endpoints)
   - [GET /api/user](#1-get-apiserver)
   - [PUT /api/user](#2-put-apiserver)
   - [DELETE /api/user](#3-delete-apiserver)
5. [Configuration](#configuration)

## Description

The **User Service** is a key component of the Brewing Stand platform, responsible for managing user-related operations. Built using **C#** and **.NET 9**, it provides endpoints for retrieving, updating, and deleting user information. The service integrates with a **Cosmos DB** for data storage and uses JWT-based authentication for secure access.


## Purpose

The User Service handles user-related operations within the Brewing Stand platform. Its primary purposes include:
1. **User Data Retrieval**: Allow users to fetch their profile details securely.
2. **User Profile Management**: Enable users to update their personal information.
3. **User Account Deletion**: Provide users with the ability to delete their account and associated data.


## Getting started

### Running locally
Follow these steps to set up the Project Service for the Searchable DB Project.

### Prerequisites
- **.NET 8 SDK** installed on your machine
- **MongoDB** instance running (for accessing the database)

### 1. Clone the Repository
Start by cloning the project repository to your local machine:
```bash
git clone https://github.com/Nelissen-searchable-db/API-Project
cd project-service
```

#### 2. Install Dependencies
Navigate to the project directory and restore the required NuGet packages:
```bash
dotnet restore
```

#### 3. Set environment variables

Before using this service, set the `ConnectionString` environment variable in `appsettings.json` with a valid MongoDB/MongoDB compatible URL.

##### [For Development Only]

If you are on a development build, create `appsettings.Development.json` next to the existing app settings file and add your connection string as follows:

```json
{
    "ConnectionString": "YOUR_CONNECTIONSTRING"
}
```

where `YOUR_CONNECTIONSTRING` refers to the location of a testing/development database.

> **Important!** You may need to generate dummy data to get the desired results from your development instance. To do this, use the our provided dummy generator, which you can find [here](https://github.com/Nelissen-searchable-db/DummyDataGenerator).

#### 4. Run the server

##### Production
Navigate to the project directory and run the following command
```bash
dotnet run
```

##### Development
Like production, run the following command in the project directory
```bash
dotnet run --environment Development
```

#### 5. Access the Application
The API server should be available at port `5174`.

#### 6. Debugging (Development only)

If you are running the application in development mode, you can access the swagger UI at `http://localhost:5174/swagger/index.html`.

This UI will help you debug the API endpoints.



## Endpoints

### **1. `GET /api/user`**

Retrieves the authenticated user's details.

#### **Request**
- **Method**: `GET`
- **Headers**:
    - `Authorization`: `Bearer <JWT>`
- **Body**: None

#### **Response**
- **Status Codes**:
    - `200 OK`: User details successfully retrieved.
    - `401 Unauthorized`: Authentication failed or JWT is missing/invalid.
    - `404 Not Found`: User not found.
- **Body**:
    ```json
    {
      "id": "user-id",
      "username": "username",
      "email": "user-email",
      "avatar": "avatar-url"
    }
    ```
### **2. `PUT /api/user`**

Updates the authenticated user's details.

#### **Request**
- **Method**: `PUT`
- **Headers**:
    - `Authorization`: `Bearer <JWT>`
    - `Content-Type`: `application/json`
- **Body**:
    ```json
    {
      "username": "new-username",
      "email": "new-email",
      "avatar": "new-avatar-url"
    }
    ```

#### **Response**
- **Status Codes**:
    - `200 OK`: User details successfully updated.
    - `401 Unauthorized`: Authentication failed or JWT is missing/invalid.
    - `404 Not Found`: User not found.
    - `400 Bad Request`: Invalid request body or validation error.
- **Body**:
    ```json
    {
      "id": "user-id",
      "username": "updated-username",
      "email": "updated-email",
      "avatar": "updated-avatar-url"
    }
    ```

### **3. `DELETE /api/user`**

Deletes the authenticated user's account.

#### **Request**
- **Method**: `DELETE`
- **Headers**:
    - `Authorization`: `Bearer <JWT>`
- **Body**: None

#### **Response**
- **Status Codes**:
    - `204 No Content`: User account successfully deleted.
    - `401 Unauthorized`: Authentication failed or JWT is missing/invalid.
    - `404 Not Found`: User not found.
    - `500 Internal Server Error`: An error occurred while deleting the user account.



## Configuration

Example appsettings.json
```json
{
  "AppSettings": {
    "AllowedOrigins": [""]
  },
  "ConnectionStrings": {
    "CosmosDbCluster": ""
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```
