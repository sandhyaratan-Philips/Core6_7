using ControllersExample.CustomeValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ControllersExample.Model
{
    public class emp
    {
        [Required(ErrorMessage ="need id")]
        [Display(Name ="ID")]
        [StringLength(4,MinimumLength =1,ErrorMessage ="enter id of max 4 and min 1 size")]
        [Range(0,9999)]
        public int id { get; set; }
        [RegularExpression("^[A-Za-z .]$",ErrorMessage ="name shoud contain alphabets dot and space")]
        public string name { get; set; }
        [ValidateNever]
        public string? address { get; set; }
        [MinYrValidation(2000)]
        [BindNever]
        public DateTime? bday { get; set; }
        public string PetName { get; set; }
    }
}
