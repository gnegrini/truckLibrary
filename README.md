# TruckLibrary
A CRUD project in .NET and Angular.

## How to run the API:
The Web API is developed using .NET 8, so you need to download that SDK first.
After that:

Restore dependencies by running this command from "API\TruckLibrary":

1. dotnet restore .\TruckLibrary.sln

Apply database migrations (if not already applied) :

2. dotnet ef database update --project ../TruckLibrary.Core --startup-project ./

Run the API:

3. dotnet run .\TruckLibrary.sln

After that, the API will be running at the address and port configured on your "launchSettings.json" (ex.: https://localhost:5001)

Additionally, there is also a Swagger page that can be used as documentation and test for the API at "/swagger" endpoint.

## How to run the Unit Tests:
The Unit tests were developed using xUnit. 
To run and view the results, execute the following command:

1. dotnet test TruckLibrary.Tests

This will build the test project and execute all unit tests, showing the results in the terminal.

## How to run the UI:
The UI is a simple front-end for testing, developed with Angular 13.

On the "truck-ui" folder, run:

1. npm install;

Update the API Address (Truck Endpoint) with the real address running in your computer:

2. On the "\src\environments\environment.ts" file, update the "apiUrl" variable to match the address where the API is running on your computer (eg: http://localhost:5001/api/);

Run the Angular development server:

3. "ng serve" or "npm start"

The application should open on your browser (eg: http://localhost:4200)