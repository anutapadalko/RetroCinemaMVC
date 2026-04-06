using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetroCinemaDomain.Model
{
    public partial class Hall : Entity
    {
        public Hall()
        {
            Sessions = new HashSet<Session>();
        }

        [Required(ErrorMessage = "Поле 'Назва залу' не повинно бути порожнім")]
        [Display(Name = "Назва залу")]
        public string Name { get; set; } = null!;

        [Display(Name = "Місткість (кількість місць)")]
        public int? Capacity { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}