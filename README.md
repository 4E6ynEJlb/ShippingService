How to set up logging:
1) Loki:
1. Open in browser http://host.docker.internal:3000
2. Go to Configuration(⚙)->Data sources->Loki
3. Set next settings:
Url: http://host.docker.internal:3100
Auth: With credentials
Alerting: Manage alerts via Alerting UI

For checking logs go to Save & test or Explore(🧭), paste query {app="Serilog.Sinks.GrafanaLoki.Sample"} and execute it

2) File:
1. Open ..\ShippingService\ShippingService\appsettings.json
2. Set value in Serilog.WriteTo[1].Args.path

For checking logs open ..\ShippingService\ShippingService\logs

How to set up and run application:
1. Download and open docker desktop (desirable)
2. Release ports 27017(mongodb), 3000(grafana), 3100(loki) on localhost (recommended)
3. Run docker-compose.yml (not necessary)
