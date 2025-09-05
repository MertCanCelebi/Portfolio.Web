using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Entities
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Comment { get; set; }
        [Range(0, 5, ErrorMessage = "Değerlendirme değeri 0 ile 5 arasında olmalıdır")]
        public int Review { get; set; }
    }
}
