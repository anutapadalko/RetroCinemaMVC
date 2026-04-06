using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetroCinemaDomain.Model
{
    public partial class Genre : Entity
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        [Required(ErrorMessage = "Поле 'Назва жанру' не повинно бути порожнім")]
        [Display(Name = "Назва жанру")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}