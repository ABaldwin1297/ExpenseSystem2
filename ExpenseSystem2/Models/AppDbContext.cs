using Microsoft.EntityFrameworkCore;
using ExpenseSystem2.Models;

namespace ExpenseSystem2.Models {
    public class AppDbContext : DbContext{

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseLine> ExpenseLines { get; set; }
        public virtual DbSet<Item > Items { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
