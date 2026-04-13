# ============================================================
# Terraform Configuration - CarRental AKS Deployment
# ============================================================
# This file configures the Terraform provider for Azure.
# Azure CLI authentication is used (az login).
# subscription_id is passed via variables to target the
# correct Azure subscription (Azure for Students).
# ============================================================

terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0"
    }
  }
}

# ============================================================
# Azure Provider
# ============================================================
# features {} is required by azurerm provider even if empty.
# subscription_id references var.subscription_id defined in
# variables.tf - this ensures the correct subscription is used
# when multiple subscriptions exist on the account.
# ============================================================

provider "azurerm" {
  features {}
  subscription_id = var.subscription_id
}
