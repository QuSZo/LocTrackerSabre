resource "google_pubsub_topic" "device_locations" {
  name = "device-locations"
}

resource "google_pubsub_subscription" "device_locations_sub" {
  name  = "device-locations-sub"
  topic = google_pubsub_topic.device_locations.name
}
