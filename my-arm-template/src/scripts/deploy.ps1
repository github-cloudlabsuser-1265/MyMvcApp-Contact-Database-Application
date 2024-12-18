# PowerShell script to deploy ARM template

# Login to Azure
az login

# Set the subscription (replace with your subscription ID)
$subscriptionId = "<your-subscription-id>"
az account set --subscription $subscriptionId

# Define variables
$templateFile = "../mainTemplate.json"
$parametersFile = "../parameters.json"
$resourceGroupName = "<your-resource-group-name>"
$location = "<your-location>"

# Create resource group if it doesn't exist
az group create --name $resourceGroupName --location $location

# Deploy the ARM template
az deployment group create --resource-group $resourceGroupName --template-file $templateFile --parameters $parametersFile

# Output deployment status
Write-Host "Deployment completed."