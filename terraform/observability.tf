resource "google_logging_metric" "messages_published" {
  name        = "messages_published_count"
  description = "Liczba opublikowanych wiadomości"

  filter = <<EOT
resource.type="cloud_run_revision"
textPayload:"Published message"
EOT

  metric_descriptor {
    display_name = "Messages published"
    metric_kind  = "DELTA"
    value_type   = "INT64"
    unit         = "1"
  }
}

resource "google_logging_metric" "messages_received" {
  name        = "messages_received_count"
  description = "Liczba odebranych wiadomości"

  filter = <<EOT
resource.type="cloud_run_revision"
textPayload:"Received message"
EOT

  metric_descriptor {
    display_name = "Messages received"
    metric_kind  = "DELTA"
    value_type   = "INT64"
    unit         = "1"
  }
}

resource "google_logging_metric" "message_errors" {
  name        = "message_processing_error_count"
  description = "Liczba błędów przetwarzania wiadomości"

  filter = <<EOT
resource.type="cloud_run_revision"
severity>=ERROR
textPayload:"Error"
EOT

  metric_descriptor {
    display_name = "Message processing errors"
    metric_kind  = "DELTA"
    value_type   = "INT64"
    unit         = "1"
  }
}
