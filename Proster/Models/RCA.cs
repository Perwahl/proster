using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Collections;

namespace Proster.Models
{
    public class RCA
    {
        public int RcaId { get; set; }
        public Case Case { get; set; }
        public User User { get; set; }

        [Display(Name = "Product")]
        public Product product { get; set; }
        [Display(Name = "Component")]
        public Component component { get; set; }
        [Display(Name = "Cause")]
        public Cause cause { get; set; }

    }

    // IClientValidatable for client side Validation 
    public class MustBeSelectedAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null || (int)value == 0)
                return false;
            else return true;
        }

        // Implement IClientValidatable for client side Validation 
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[]
            {
 new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = this.ErrorMessage }
            };
        }     
    }

    public class Product
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Product")]
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class Component
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Component")]
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? Product { get; set; }
    }

    public class Cause
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Cause")]
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? Component { get; set; }
    }
}

