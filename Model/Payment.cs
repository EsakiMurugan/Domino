﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domino.Model
{
    public class Payment
    {
        [Key]
        [Display(Name = "Payment ID")]
        public int PaymentID { get; set; }
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer? customer { get; set; }
        [Display(Name = "Card Number")]
        public int? CardNumber { get; set; }
        [Display(Name = "Total Amount")]
        public float? TotalAmount { get; set; }  
      

    }
}
