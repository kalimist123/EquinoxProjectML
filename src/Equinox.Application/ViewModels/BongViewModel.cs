using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class BongViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The ReferenceNo is required")]

        [DisplayName("ReferenceNo")]
        [MinLength(2)]
        [MaxLength(100)]
        public string ReferenceNo { get; set; }

        [Required(ErrorMessage = "The ArrivingInStock is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Arriving In Stock")]
        public DateTime ArrivingInStock { get; set; }
    }
}
