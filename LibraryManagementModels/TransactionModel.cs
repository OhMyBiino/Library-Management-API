using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementModels
{
    public class TransactionModel
    {
        [Key]
        [Required]
        [DisplayName("Transaction ID")]
        public Guid TransactionId { get; set; }

        [Required]
        [DisplayName("User ID")]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength =2)]
        [DisplayName("Borrower")]
        public string BorrowerName { get; set; }

        [Required]
        [DisplayName("Book Id")]
        public int BookId { get; set; }

        [Required]
        public TransactionType Type { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }

        [DisplayName("Book Status")]
        public string Status { get; set; }

        public UserModel? user { get; set; }
    }
}
