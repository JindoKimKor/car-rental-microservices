# ============================================================
# Azure Kubernetes Service (AKS)
# ============================================================
# Managed K8s cluster for running CarRental microservices.
# Single node (node_count=1) to minimize Azure for Students
# credit consumption.
#
# default_node_pool uses Standard_B2s (2 vCPU, 4GB RAM) -
# smallest VM that can run the CarRental services.
#
# identity type = SystemAssigned creates a managed identity
# for AKS to authenticate with other Azure services (ACR).
# ============================================================

resource "azurerm_kubernetes_cluster" "main" {
  name                = var.aks_cluster_name
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  dns_prefix          = var.aks_cluster_name

  default_node_pool {
    name       = "default"
    node_count = var.aks_node_count
    vm_size    = "Standard_B2s"
  }

  identity {
    type = "SystemAssigned"
  }
}

# ============================================================
# ACR Pull Role Assignment
# ============================================================
# Grants AKS the permission to pull images from ACR.
# Without this, AKS pods would fail with ImagePullBackOff
# because they cannot authenticate to the private registry.
# AcrPull is a built-in Azure role specifically for this.
# ============================================================

resource "azurerm_role_assignment" "aks_acr_pull" {
  principal_id                     = azurerm_kubernetes_cluster.main.kubelet_identity[0].object_id
  role_definition_name             = "AcrPull"
  scope                            = azurerm_container_registry.main.id
  skip_service_principal_aad_check = true
}
