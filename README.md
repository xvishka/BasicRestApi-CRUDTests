API Testing Framework

This is a simple API testing framework built using C#, NUnit, and RestSharp. The framework tests RESTful API endpoints by performing CRUD operations: GET, POST, PUT, and DELETE.

Prerequisites
.NET SDK 8.0 or higher
An IDE or text editor, such as Visual Studio, Visual Studio Code, or Rider
Project Structure
RestfulApiTests.cs: Contains the test cases for the API.
ApiTestingDemo.csproj: Project configuration file specifying dependencies like NUnit, RestSharp, and System.Text.Json.
Dependencies
This project relies on the following NuGet packages:

NUnit: Test framework
NUnit3TestAdapter: Test adapter to run NUnit tests in IDEs
Microsoft.NET.Test.Sdk: Provides necessary infrastructure for running tests
RestSharp: HTTP client library for interacting with REST APIs
System.Text.Json: Library for JSON serialization and deserialization
Setup
Clone or download the repository containing the project files.

Install the required dependencies by navigating to the project directory and running:

dotnet restore
This command installs all dependencies specified in ApiTestingDemo.csproj.

Running the Tests
Navigate to the Project Directory: Open a terminal and navigate to the directory containing the .csproj file:

cd path/to/project/ApiTestingDemo
Run the Tests:

dotnet test
This command will:

Build the project.
Run each test in the RestfulApiTests.cs file in the specified order using NUnit.
The tests will run in the following order:

Test_GetAllObjects: Retrieves all objects.
Test_AddObject: Adds a new object and stores its ID.
Test_GetSingleObject: Retrieves the newly created object using its ID.
Test_UpdateObject: Updates the object’s information.
Test_DeleteObject: Deletes the object.
Test Order and Dependencies
The tests depend on running in a specific order, achieved using the [Order] attribute. Each test performs a different operation, with Test_AddObject storing the [objectId] required for subsequent tests. Make sure that each test completes successfully to ensure smooth progression through the CRUD operations.
