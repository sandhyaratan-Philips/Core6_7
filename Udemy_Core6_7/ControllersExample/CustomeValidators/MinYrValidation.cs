using System.ComponentModel.DataAnnotations;

namespace ControllersExample.CustomeValidators
{
    public class MinYrValidation:ValidationAttribute
    {
        public int minYr { get; set; }
        public MinYrValidation() { }
        public MinYrValidation(int yr)
        {
            minYr = yr;
        }
        protected override ValidationResult? IsValid(object? value,ValidationContext validationContext)
        {
           if (value!= null)
            {
                DateTime date = new DateTime();
                if (date.Year >= minYr)
                {
                    return new ValidationResult("invalid yr");
                }
                return ValidationResult.Success;
            }
            return null;
        }
    }
}
