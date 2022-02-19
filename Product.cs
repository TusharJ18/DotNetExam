using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetEndExamMVC.Models
{
    public class Product
    {
        [Key]
        [Required (ErrorMessage ="Product Id can't be empty")]
        [Display(Name ="Product Id")]
        public int ProductId { get; set; }

        [Required (ErrorMessage="Product Name can't be empty")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Rate can't be empty")]
        [Display(Name = "Product Rate")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Description can't be empty")]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category Name can't be empty")]
        [Display(Name = "Product's Category")]
        public string CategoryName { get; set; }

    }
}