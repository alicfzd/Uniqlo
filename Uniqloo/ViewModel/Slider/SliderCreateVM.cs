using System.ComponentModel.DataAnnotations;

namespace Uniqloo.ViewModel.Slider
{
    public class SliderCreateVM
    {
        [MaxLength(32, ErrorMessage="Basliq 32 Simvoldan cox ola bilmez"),Required]

        public string Title { get; set; }
        [Required]

        public string Subtitle { get; set; }

        public string? Link { get; set; }
        [Required(ErrorMessage="Fayl Seçin!")]

        public IFormFile File { get; set; }
    }
}
