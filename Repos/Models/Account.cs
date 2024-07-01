using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonManagement.Repos.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int PersonCode { get; set; }
        public string AccountNumber { get; set; }
        public Decimal OutstandingBalance { get; set; }             
        public int StatusId { get; set; }

        public AccountStatus? Status { get; set; }
        public Person? Person { get; set; }
        public List<Transaction>?  Transactions { get; set; }
    }
}
