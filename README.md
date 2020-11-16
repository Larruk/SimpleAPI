SimpleAPI
==================================================================================
This API runs via .NET 5.0 and utilizes swagger to create documentation that can also be used to test the end points.

Requirements
==================================================================================
Visual Studio 16.8.1 (Support for .NET 5.0)

.NET 5.0 SDK: https://dotnet.microsoft.com/download/visual-studio-sdks


Steps to Run
==================================================================================
1. Navigate to SimpleWebAPI/InitialSetup.ps1
2. Right click and run via PowerShell
	WARNING: This script will do the following:
		- Install dotnet-ef (failure is expected if you already have this)
		- Attempt to remove this projects migrations (in case of reruns, failure is ok here on first time)
		- Create migrations and update localdb
3. Open SimpleWebAPI.sln via Visual Studio
4. Build / Run the project
5. Project will launch a web page to Swagger UI showing documentation on the two created endpoints
6. Here you can click Try It Out to test the endpoints

Troubleshooting
==================================================================================
The only issues I've ran into involve running the set up script more than once or deleting the db
