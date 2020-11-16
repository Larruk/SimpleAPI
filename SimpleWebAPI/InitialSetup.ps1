dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update

Read-Host -Prompt "Press Enter to exit"