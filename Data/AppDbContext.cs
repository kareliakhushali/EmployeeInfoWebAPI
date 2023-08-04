using EmployeeInfoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInfoWebAPI.Data
{
    public class AppDbContext:DbContext
    {
public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
