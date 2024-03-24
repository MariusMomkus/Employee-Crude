# Navigate to the EmployeeApp directory
cd ../EmployeeApp

# Set the environment variable for the database connection string
$Env:connectionString="Server=localhost; User ID=postgres; Password=guest; Port=7777; Database=uvsproject;"

# Build the .NET project
dotnet build

Write-Host "Creating and Retrieving employees one by one:"
# Create employees
dotnet run --no-build set-employee --employeeId 1 --employeeName John --employeeSalary 123
dotnet run --no-build set-employee --employeeId 2 --employeeName Steve --employeeSalary 456
dotnet run --no-build set-employee --employeeId 3 --employeeName Marcus --employeeSalary 956

# Retrieve and display the created employees
dotnet run --no-build get-employee --employeeId 1
dotnet run --no-build get-employee --employeeId 2
dotnet run --no-build get-employee --employeeId 3

Write-Host "Updating employee..."
# Update an employee's information
dotnet run --no-build update-employee --employeeId 1 --employeeName JohnEdited --employeeSalary 666

Write-Host "Updated employee:"
# Retrieve and display the updated employee
dotnet run --no-build get-employee --employeeId 1

Write-Host "Deleting an employee"
# Delete an employee
dotnet run --no-build delete-employee --employeeId 2

# List all remaining employees to confirm after deletion
Write-Host "Retrieving all remaining employees after deletion:"
dotnet run --no-build list-employees

# Attempt to retrieve the deleted employee to confirm deletion
Write-Host "Attempt to retrieve the deleted employee to confirm deletion"
dotnet run --no-build get-employee --employeeId 2

# Return to the original directory
cd ../..
