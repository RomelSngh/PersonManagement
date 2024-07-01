using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonManagement.Repos.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        public int AccountCode { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime Capturedate { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public Account? Account { get; set; }
    }
}
