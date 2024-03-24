using CommandLine;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<SetEmployeeOptions, GetEmployeeOptions, DeleteEmployeeOptions, UpdateEmployeeOptions, ListEmployeesOptions>(args)
                .MapResult(
                    (SetEmployeeOptions opts) => RunSetEmployeeAndReturnExitCode(opts),
                    (GetEmployeeOptions opts) => RunGetEmployeeAndReturnExitCode(opts),
                    (DeleteEmployeeOptions opts) => RunDeleteEmployeeAndReturnExitCode(opts),
                    (UpdateEmployeeOptions opts) => RunUpdateEmployeeAndReturnExitCode(opts),
                    (ListEmployeesOptions opts) => RunListEmployeesAndReturnExitCode(),
                    errs => 1);
        }

        private static int RunSetEmployeeAndReturnExitCode(SetEmployeeOptions opts)
        {
            using var db = new DatabaseContext();
            var employee = db.Employees.Find(opts.EmployeeId);
            if (employee == null)
            {
                employee = new Employee { EmployeeId = opts.EmployeeId };
                db.Employees.Add(employee);
            }

            employee.EmployeeName = opts.EmployeeName;
            employee.EmployeeSalary = opts.EmployeeSalary;
            db.SaveChanges();
            return 0;
        }

        private static int RunGetEmployeeAndReturnExitCode(GetEmployeeOptions opts)
        {
            using var db = new DatabaseContext();
            var employee = db.Employees.Find(opts.EmployeeId);
            if (employee != null)
            {
                Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.EmployeeName}, Salary: {employee.EmployeeSalary}");
            }
            else
            {
                Console.WriteLine("Employee could not be found.");
            }
            return 0;
        }

        private static int RunDeleteEmployeeAndReturnExitCode(DeleteEmployeeOptions opts)
        {
            using var db = new DatabaseContext();
            var employee = db.Employees.Find(opts.EmployeeId);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee could not be deleted / found.");
            }
            return 0;
        }

        private static int RunUpdateEmployeeAndReturnExitCode(UpdateEmployeeOptions opts)
        {
            using var db = new DatabaseContext();
            var employee = db.Employees.Find(opts.EmployeeId);
            if (employee != null)
            {
                if (!string.IsNullOrEmpty(opts.EmployeeName))
                {
                    employee.EmployeeName = opts.EmployeeName;
                }
                if (opts.EmployeeSalary.HasValue)
                {
                    employee.EmployeeSalary = opts.EmployeeSalary.Value;
                }

                db.SaveChanges();
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee could not be updated.");
            }
            return 0;
        }

        private static int RunListEmployeesAndReturnExitCode()
        {
            using var db = new DatabaseContext();
            var employees = db.Employees.ToList();
            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.EmployeeName}, Salary: {employee.EmployeeSalary}");
                }
            }
            else
            {
                Console.WriteLine("Could not get all employees.");
            }
            return 0;
        }
    }

    [Verb("set-employee", HelpText = "Set or update an employee's data.")]
    class SetEmployeeOptions
    {
        [Option('i', "employeeId", Required = true, HelpText = "Employee ID.")]
        public int EmployeeId { get; set; }

        [Option('n', "employeeName", Required = true, HelpText = "Employee's name.")]
        public string EmployeeName { get; set; }

        [Option('s', "employeeSalary", Required = true, HelpText = "Employee's salary.")]
        public int EmployeeSalary { get; set; }
    }

    [Verb("get-employee", HelpText = "Get an employee's data.")]
    class GetEmployeeOptions
    {
        [Option('i', "employeeId", Required = true, HelpText = "Employee ID.")]
        public int EmployeeId { get; set; }
    }
    
    
    [Verb("delete-employee", HelpText = "Delete an employee's data.")]
    class DeleteEmployeeOptions
    {
        [Option('i', "employeeId", Required = true, HelpText = "Employee ID.")]
        public int EmployeeId { get; set; }
    }
    
    [Verb("update-employee", HelpText = "Update an existing employee's data.")]
    class UpdateEmployeeOptions
    {
        [Option('i', "employeeId", Required = true, HelpText = "Employee ID.")]
        public int EmployeeId { get; set; }

        [Option('n', "employeeName", Required = false, HelpText = "Employee's name.")]
        public string EmployeeName { get; set; } = string.Empty;

        [Option('s', "employeeSalary", Required = false, HelpText = "Employee's salary.")]
        public int? EmployeeSalary { get; set; }
    }

    [Verb("list-employees", HelpText = "List all employees.")]
    class ListEmployeesOptions { }

}
