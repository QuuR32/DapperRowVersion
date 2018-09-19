using DapperRowVersion.Dapper;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DapperRowVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlGen = new SqlGenerator<Employee>();

            var repo = new EmployeeRepository(new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"), sqlGen);
            /*
            Employee employee1 = repo.GetById(2).GetAwaiter().GetResult();
            Console.WriteLine($"Employee1: {employee1.FirstName}");

            Employee employee2 = new Employee
            {
                FirstName = "Laura",
                LastName = "Fleury",
                Gender = "Female"
            };
            employee2 = repo.Create(employee2).GetAwaiter().GetResult();
            Console.WriteLine($"Employee2: {employee2.Id}");

            Employee employee3 = new Employee
            {
                FirstName = "Laura2",
                LastName = "Fleury2",
                Gender = "Female"
            };
            employee3 = repo.Create(employee3).GetAwaiter().GetResult();
            Console.WriteLine($"Employee3: {employee3.Id}");

            employee1.FirstName += "s";
            repo.Update(employee1);

            var employees1 = repo.GetAll().GetAwaiter().GetResult();
            Console.WriteLine($"List count: {employees1.Count()}");

            var employees2 = repo.GetAll(employee2.version).GetAwaiter().GetResult();
            Console.WriteLine($"List count: {employees2.Count()}");
            */
            var employee = repo.FindAsync(e => e.Id == 5).GetAwaiter().GetResult();
            Console.WriteLine($"Employee: {employee.Id}");
            var employees = repo.FindAllAsync(e => e.Id > 10).GetAwaiter().GetResult();
            Console.WriteLine($"List count: {employees.Count()}");
        }
    }
}
