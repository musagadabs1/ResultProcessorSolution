using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResultProcessor.Models;

namespace ResultProcessor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ResultProcessor.Models.Student> Student { get; set; }
        public DbSet<ResultProcessor.Models.Faculty> Faculty { get; set; }
        public DbSet<ResultProcessor.Models.Programme> Programme { get; set; }
        public DbSet<ResultProcessor.Models.Department> Department { get; set; }
        public DbSet<ResultProcessor.Models.Course> Course { get; set; }
        public DbSet<ResultProcessor.Models.ScoreSheet> ScoreSheet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Student>().HasMany(t => t.Programmes).WithMany()
            base.OnModelCreating(builder);  
        }
    }
}
