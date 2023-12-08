using System.ComponentModel.DataAnnotations;

namespace FreeEducation.Web.Models.Catalogs;

public class EducationCreateInput
{

        [Display(Name = "Eğitim ismi")]
        public string Name { get; set; }

        [Display(Name = "Eğitim açıklama")]
        public string Description { get; set; }

        [Display(Name = "Eğitim fiyat")]
        public decimal Price { get; set; }

        public string Picture { get; set; }

        public string UserId { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Eğitim kategori")]
        public string CategoryId { get; set; }

        [Display(Name = "Eğitim Resim")]
        public IFormFile PhotoFormFile { get; set; }
    }
    
