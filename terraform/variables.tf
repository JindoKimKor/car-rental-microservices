# ============================================================
# Input Variables
# ============================================================
# All configurable values are defined here.
# Actual values are set in terraform.tfvars (gitignored)
# or passed via CLI: terraform plan -var="name=value"
# ============================================================

variable "subscription_id" {
  description = "Azure subscription ID (Azure for Students)"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the Azure Resource Group"
  type        = string
  default     = "carrental-rg"
}

variable "location" {
  description = "Azure region for all resources"
  type        = string
  default     = "canadacentral"
}

variable "acr_name" {
  description = "Name of the Azure Container Registry (must be globally unique, alphanumeric only)"
  type        = string
  default     = "jindobcarentalacr"
}

variable "aks_cluster_name" {
  description = "Name of the AKS cluster"
  type        = string
  default     = "carrental-aks"
}

variable "aks_node_count" {
  description = "Number of nodes in the AKS cluster (1 for Azure for Students to save credits)"
  type        = number
  default     = 1
}
