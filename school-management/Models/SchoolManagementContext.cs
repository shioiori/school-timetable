using Microsoft.EntityFrameworkCore;

namespace school_management.Models
{
    public class SchoolManagementContext : DbContext
    {
        public SchoolManagementContext(DbContextOptions dbContext) : base(dbContext) { }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
