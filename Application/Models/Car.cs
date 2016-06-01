using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        [Display(Name = "Car Name")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }

        [Display(Name = "Year of creation")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int Year { get; set; }

        [Display(Name = "Car Model")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Model { get; set; }
    }
}