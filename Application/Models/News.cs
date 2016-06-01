using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class News
    {
        public int Id { get; set; }

        [Display(Name = "News Title")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Content { get; set; }
        public List<Tags> Tags { get; set; }

        public string CustomFileName { get; set; }
        public string SavedFileName { get; set; }
    }
}