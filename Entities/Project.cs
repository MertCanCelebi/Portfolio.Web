using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [MinLength(5,ErrorMessage ="Proje Adı En Az 5 Karakter Olmalıdır")]
        [MaxLength(50,ErrorMessage ="Proje Adı En Fazla 50 Karakter Olmalıdır")]
        [Required(ErrorMessage ="Proje Adı Boş Bırakılamaz")]
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string GithubUrl { get; set; }

        //navigation property
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
