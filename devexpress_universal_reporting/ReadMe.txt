npm install

dotnet restore

nohup dotnet run --launch-profile "ReportingService-Production" --environment "Production" > /dev/null 2> /dev/null < /dev/null &


nohup dotnet run --launch-profile "ReportingService" --environment "Development" > /dev/null 2> /dev/null < /dev/null &