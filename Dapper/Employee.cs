using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DapperRowVersion.Dapper
{
    [Table("Employees")]
    public class Employee
    {
        [Key, Identity]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public byte[] version { get; set; }
    }
}
