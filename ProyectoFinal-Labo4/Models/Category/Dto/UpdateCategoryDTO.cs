using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_Labo4.Models.Category.Dto
{
    public class UpdateCategoryDTO
    {
        [MaxLength(30)]
        public string? Nombre { get; set; }
    }
}
