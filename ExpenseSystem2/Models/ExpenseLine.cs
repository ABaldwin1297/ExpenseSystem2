namespace ExpenseSystem2.Models {
    public class ExpenseLine {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ExpenseId { get; set; }
        public virtual Expense? Expense { get; set; }
        public int ItemId { get; set; }
        public virtual Item? Item { get; set; }

    }
}
