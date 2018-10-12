# Intercom_CustomerRecords

This is a coding exercise so please don't take this a real life sample of code.

In the next lines I will describe the main classes and code structure. I'll also comment how a particular section differs from a real life scenario.

## Structure
The solution has two projects: Intercom_CustomerRecords which represents the solution and Intercom_CustomerRecords.Tests which contains the unit tests.

This application follows a MVC pattern as it would do if we were creating a modern REST API. Also, you will notice that I’m following a “Skinny Models, Skinny Controllers, Fat Services” model leaving all the logic to services and helpers.

Please note that most of the folders in Intercom_CustomerRecords will probably be project in the solution, for example, you would have a Data Access project and Business Logic project containing all the services.

## Dependecies
- NewtonSoft – used for Json Parsing
- SimpleInjector – used for Dependecy Injection
- xUnit – used for Unit  Testing
- Moq – used for mocking 

## Improvements
A few improvements for this code exercise that were not implemented for time constrictions:
- Parameters like location and distance could have been read from console.
- Customer file location can also be read from console.
- Use NLog or Log4Net as a logger for the exceptions.
- Improve unit tests including negative tests.
- Move the presentation method to its own class.

## Main Classes

### Program
Main entry point of the application.
Initialize the Dependency Injection container and register all dependencies.
GlobalExceptionHandler method gets assigned to manage all unhandled exceptions.
Starts the application by calling the CustomerController with headquaters location and distance requirements for filtering.
Finally provides a method to display the filtered list of users according to the exercise instructions. 

Note: The presentation layer in a real life can be a total different project.  For the sake of this exercise we are just using a method to display the data in console.

### CustomerService
Provides a method that returns the list of all customers using an ICustomerProvider class.
Provides a method that filers all customers based on distance.

### UserFileReader
Implements ICustomerProvider interface. I'm using an interface since later if we want to get customers from a database it should be as simple of using another implementation.
Uses a Json parser class to convert a json line to a model.
Has a default (hardcoded) file location that is used if the file location (full path) is not provided in the App.Settings file.

### CustomerJsonParser
Relies on NewtonSoft.Json library to convert json to a model object.
Swallows exception on purpose so if a line is malformatted we can still read the rest of the file.

### DistanceCriteria 
Follows a filter pattern implementing an interface (IFilteringCriteria), so that we can add other criterias and combine them if needed.

Uses a helper class: DistanceCalculatorHelper that implements the Harvesine distance function to calculate the distance between two points.


