# MyMvcApp - Contact Database Application

## Overview

MyMvcApp is an ASP.NET Core MVC application for managing a contact database. It allows users to create, view, edit, and delete contact records. The app uses Entity Framework Core with a SQLite database and is designed for easy deployment to Azure Web App.

---

## Features

- Add, edit, view, and delete contacts
- MVC architecture with Razor views
- Entity Framework Core for data access
- SQLite database for local development
- Ready for deployment to Azure Web App

---

## Project Structure

- `Controllers/` - MVC controllers (e.g., `UserController.cs`)
- `Models/` - Data models (e.g., `User.cs`)
- `Views/` - Razor views for UI
- `Data/` - Database context (`AppDbContext.cs`)
- `Migrations/` - EF Core migrations
- `wwwroot/` - Static files (CSS, JS, images)
- `appsettings.json` - App configuration
- `.github/workflows/deploy-to-azure.yml` - GitHub Actions workflow for CI/CD

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQLite](https://www.sqlite.org/download.html) (for local development)
- Azure account (for deployment)

### Running Locally

1. Clone the repository:
   ```bash
   git clone <your-repo-url>
   cd MyMvcApp-Contact-Database-Application
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Apply migrations (if needed):
   ```bash
   dotnet ef database update
   ```

4. Run the app:
   ```bash
   dotnet run
   ```

5. Open your browser at `https://localhost:5001` (or the URL shown in the terminal).

---

## Deployment

### Continuous Deployment with GitHub Actions

The app is configured for CI/CD using GitHub Actions and Azure Web App. The workflow file is at `.github/workflows/deploy-to-azure.yml`.

#### Workflow Steps

- **Checkout code**: Pulls the latest code from the repository.
- **Set up .NET**: Installs the .NET 8 SDK.
- **Build**: Builds the app in Release mode.
- **Publish**: Publishes the app to the `./publish` directory.
- **Deploy to Azure Web App**: Uses `azure/webapps-deploy@v3` to deploy the published app.
- **Deploy ARM Template**: Uses `azure/arm-deploy@v1` to deploy infrastructure via ARM templates.

#### Required GitHub Secrets

- `AZURE_WEBAPP_NAME`: Name of your Azure Web App
- `AZURE_WEBAPP_PUBLISH_PROFILE`: Publish profile XML for your Azure Web App
- `AZURE_SUBSCRIPTION_ID`: Azure subscription ID
- `AZURE_RESOURCE_GROUP`: Azure resource group name

#### Manual Deployment

1. Publish the app:
   ```bash
   dotnet publish -c Release -o ./publish
   ```
2. Deploy the contents of `./publish` to your Azure Web App using the Azure Portal or Azure CLI.

---

## Database

- Uses SQLite for development (`contacts.db`).
- For production, update the connection string in `appsettings.json` as needed.

---

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Open a pull request.

---

## License

This project is licensed under the MIT License.

---

# Previous Documentation

<!--
The following section is the previous documentation for reference. You may remove or update it as needed.
-->

# MyMvcApp-Contact-Database-Application

## Application Functionality

This is a simple ASP.NET Core MVC CRUD application for managing users. The app allows you to:
- View a list of users
- Add new users
- Edit existing users
- Delete users

Currently, the app uses in-memory storage for users (no database required). All data will reset when the app restarts.

---

## Azure ARM Template Deployment Guide

This project includes an Azure Resource Manager (ARM) template for deploying the app to Azure App Service.

### Files:
- `deploy.json`: The ARM template that defines the Azure resources (App Service, App Service Plan, etc.)
- `deploy.parameters.json`: Parameters file for the ARM template (app name, region, SKU, etc.)

### How to Deploy with ARM Template:
1. Edit `deploy.parameters.json` to set a unique web app name and desired region/SKU.
2. Deploy using Azure CLI:
   ```bash
   az deployment group create \
     --resource-group <your-resource-group> \
     --template-file deploy.json \
     --parameters @deploy.parameters.json
   ```
3. This will provision the Azure App Service and related resources.

---

## GitHub Actions CI/CD Pipeline

This project uses GitHub Actions to build and deploy the app to Azure Web App automatically.

### Workflow File:
- `.github/workflows/azure-webapp-deploy.yml`

### How it Works:
- Triggers on push to the `main` branch or manually via the Actions tab.
- Steps:
  1. Checks out the code
  2. Sets up .NET 8.0
  3. Restores dependencies
  4. Builds the app
  5. Publishes the app to a folder
  6. Deploys the published app to Azure Web App using the publish profile secret

### Setup Instructions:
1. In the Azure Portal, download the publish profile for your Web App.
2. In your GitHub repo, go to Settings > Secrets and add a new secret named `PUBLISH_PROFILE` with the contents of the publish profile file.
3. Push changes to `main` or use the Actions tab to trigger a deployment.

---

## Useful Links
- [Azure ARM Templates Documentation](https://docs.microsoft.com/azure/azure-resource-manager/templates/overview)
- [GitHub Actions for Azure](https://docs.microsoft.com/azure/developer/github/)
- [ASP.NET Core MVC Documentation](https://docs.microsoft.com/aspnet/core/mvc/)
