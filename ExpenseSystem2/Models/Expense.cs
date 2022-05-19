using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseSystem2.Models {
    public class Expense {
        public int Id { get; set; }
        [StringLength(30)]
        public string Desc  { get; set; } = string.Empty;
        [StringLength(15)]
        public string Status { get; set; } = string.Empty;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }
        public int EmployeeId { get; set; } = 0;
        public virtual Employee? Employee { get; set; }
    }
}
