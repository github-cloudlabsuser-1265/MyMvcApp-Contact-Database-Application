param (
    [string]$resourceGroupName,
    [string]$templateFile,
    [string]$parametersFile,
    [string]$subscriptionId
)

# Login to Azure
az login

# Set the subscription
az account set --subscription $subscriptionId

# Create the resource group if it doesn't exist
az group create --name $resourceGroupName --location "westeurope"

# Deploy the ARM template
az deployment group create --resource-group $resourceGroupName --template-file $templateFile --parameters @$parametersFile