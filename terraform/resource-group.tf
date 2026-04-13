# ============================================================
# Resource Group
# ============================================================
# All Azure resources are created inside this Resource Group.
# Deleting the Resource Group removes everything in it -
# clean up after assignment submission to avoid
# consuming Azure for Students credits.
# ============================================================

resource "azurerm_resource_group" "main" {
  name     = var.resource_group_name
  location = var.location
}
