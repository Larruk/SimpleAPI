# Install dotnet-ef
dotnet tool install --global dotnet-ef

# Clean up in case rerun
dotnet ef migrations remove

# Create DB migrations for EF
dotnet ef migrations add InitialCreate
dotnet ef database update

Read-Host -Prompt "Press Enter to exit"