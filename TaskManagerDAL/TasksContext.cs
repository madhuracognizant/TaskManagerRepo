using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDAL
{
    public class TasksContext : DbContext
    {
        public TasksContext() : base("name=TasksContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Task> Tasks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TasksContext>(null);
            modelBuilder.Entity<Task>().ToTable("Task", "dbo");
            // Primary keys
            modelBuilder.Entity<Task>().HasKey(q => q.Task_ID);
        }
    }
}
