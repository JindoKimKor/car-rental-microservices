# ============================================================
# Azure Container Registry (ACR)
# ============================================================
# Private Docker image registry for CarRental services.
# AKS pulls container images from here instead of Docker Hub.
# Basic SKU is the cheapest option for Azure for Students.
# admin_enabled allows docker login with username/password
# for pushing images from local machine.
# ============================================================

resource "azurerm_container_registry" "main" {
  name                = var.acr_name
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  sku                 = "Basic"
  admin_enabled       = true
}
