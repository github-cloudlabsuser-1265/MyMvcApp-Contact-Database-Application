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
