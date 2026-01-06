resource "google_cloud_run_service" "gate" {
  name     = "gate"
  location = var.region

  template {
    spec {
      service_account_name = google_service_account.gate.email

      containers {
        image = "gcr.io/${var.project_id}/gate:latest"

        env {
          name  = "GCP_PROJECT_ID"
          value = var.project_id
        }

        env {
          name  = "PUBSUB_TOPIC_ID"
          value = google_pubsub_topic.device_locations.name
        }
      }
    }
  }

  traffic {
    percent         = 100
    latest_revision = true
  }
}

resource "google_cloud_run_service_iam_member" "gate_public" {
  service  = google_cloud_run_service.gate.name
  location = var.region
  role     = "roles/run.invoker"
  member   = "allUsers"
}

resource "google_cloud_run_service" "location" {
  name     = "location"
  location = var.region

  template {
    spec {
      service_account_name = google_service_account.location.email

      containers {
        image = "gcr.io/${var.project_id}/location:latest"

        env {
          name  = "GCP_PROJECT_ID"
          value = var.project_id
        }

        env {
          name  = "PUBSUB_SUBSCRIPTION_ID"
          value = google_pubsub_subscription.device_locations_sub.name
        }
      }
    }
  }

  traffic {
    percent         = 100
    latest_revision = true
  }
}

resource "google_cloud_run_service_iam_member" "location_public" {
  service  = google_cloud_run_service.location.name
  location = var.region
  role     = "roles/run.invoker"
  member   = "allUsers"
}

resource "google_cloud_run_service" "web" {
  name     = "web"
  location = var.region

  template {
    spec {
      service_account_name = google_service_account.web.email

      containers {
        image = "gcr.io/${var.project_id}/web:latest"
      }
    }
  }

  traffic {
    percent         = 100
    latest_revision = true
  }
}

resource "google_cloud_run_service_iam_member" "web_public" {
  service  = google_cloud_run_service.web.name
  location = var.region
  role     = "roles/run.invoker"
  member   = "allUsers"
}
