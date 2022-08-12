using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domino.Model
{
    public class Receipt
    {
        [Key]
        public int ReceiptID { get; set; }  
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer? customer { get; set; }  
        public int CartID { get; set; }
        [ForeignKey("CartID")]
        public virtual Cart? cart { get; set; }  

    }
}
