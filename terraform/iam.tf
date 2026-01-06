resource "google_service_account" "gate" {
  account_id   = "gate-sa"
  display_name = "Gate Cloud Run SA"
}

resource "google_service_account" "location" {
  account_id   = "location-sa"
  display_name = "Location Cloud Run SA"
}

resource "google_service_account" "web" {
  account_id   = "web-sa"
  display_name = "Web Cloud Run SA"
}

resource "google_project_iam_member" "gate_pubsub_pub" {
  project = var.project_id
  role   = "roles/pubsub.publisher"
  member = "serviceAccount:${google_service_account.gate.email}"
}

resource "google_project_iam_member" "location_pubsub_sub" {
  project = var.project_id
  role   = "roles/pubsub.subscriber"
  member = "serviceAccount:${google_service_account.location.email}"
}

resource "google_project_iam_member" "location_firestore" {
  project = var.project_id
  role   = "roles/datastore.user"
  member = "serviceAccount:${google_service_account.location.email}"
}
