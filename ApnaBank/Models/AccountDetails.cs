using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApnaBank.Models
{
    public class AccountDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
    }

    public class AccountInfo
    {
        public int AccountNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public int TargetAccountNumber { get; set; }
    }
}
