using Dapper;
using DapperRowVersion.Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DapperRowVersion
{
    public class EmployeeRepository : DapperRepository<Employee>
    {
        public EmployeeRepository(IDbConnection connection, ISqlGenerator<Employee> sqlGenerator) : base(connection, sqlGenerator)
        {
        }
        /*
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
        */
        public async Task<Employee> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, FirstName, LastName, Gender FROM Employees WHERE Id = @id";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<Employee> Create(Employee employee)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "INSERT INTO Employees (FirstName, LastName, Gender) OUTPUT INSERTED.* VALUES (@FirstName, @LastName, @Gender)";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, new { employee.FirstName, employee.LastName, employee.Gender });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Employee>> GetAll(byte[] version = null)
        {
            if (version == null)
                version = new byte[] { 0 };

            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Employees WHERE version > @version";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, new { version });
                return result;
            }
        }
        /*
        public async Task Update(Employee employee)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender WHERE Id = @Id";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, new { employee.FirstName, employee.LastName, employee.Gender, employee.Id });
            }
        }
        */
    }
}
