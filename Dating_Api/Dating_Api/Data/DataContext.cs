using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dating_Api.Models;
namespace Dating_Api.Data
{
    public class DataContext : DbContext
    {
        //Constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //property --To use Value class we have declared namespace above-Dating_Api.Models;
        public DbSet<Value> Values { get; set; }
        public DbSet<User> User{ get; set; }
    }
}
