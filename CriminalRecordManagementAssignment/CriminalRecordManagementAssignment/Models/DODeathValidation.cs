using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CriminalRecordManagementAssignment.Models
{
    public class DODeathValidation : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime CurrentDate = DateTime.Now;
            DateTime dodvalue = Convert.ToDateTime(value);
           // DateTime dodvalue = Convert.ToDateTime(value1);

            string message = "";

            if (dodvalue > CurrentDate )
            {

                message = "DODeath should not be greater than current Date";
                return new ValidationResult(message);
            }
            return ValidationResult.Success;
        }
    }
}