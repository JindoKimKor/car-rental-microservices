# ============================================================
# Outputs
# ============================================================
# Values needed for the next steps:
# - ACR login server: where to push Docker images
# - AKS get-credentials command: connect kubectl to AKS
# - Resource Group name: for reference
# ============================================================

output "acr_login_server" {
  description = "ACR login server URL for docker push"
  value       = azurerm_container_registry.main.login_server
}

output "aks_get_credentials_command" {
  description = "Command to connect kubectl to AKS cluster"
  value       = "az aks get-credentials --resource-group ${azurerm_resource_group.main.name} --name ${azurerm_kubernetes_cluster.main.name}"
}

output "resource_group_name" {
  description = "Resource Group name"
  value       = azurerm_resource_group.main.name
}
