using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Category.Dto
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; } = null!;
    }
}
