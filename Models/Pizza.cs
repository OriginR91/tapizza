using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pizza_mama.Models
{
    public class Pizza
    {
        [JsonIgnore]
        public int PizzaId { get; set; }

        public string Nom { get; set; }

        public float Prix { get; set; }

        [Display(Name = "Végétarienne")]
        public bool Vegetarienne { get; set; }

        [Display(Name = "Ingrédients")]
        [JsonIgnore]
        public string Ingredients { get; set; }

        [NotMapped]
        [JsonPropertyName("ingredients")]
        public string[] listeIngredients
        {
            get
            {
                if(Ingredients == null || Ingredients.Count() == 0)
                {
                    return null;
                }

                return Ingredients.Split(", ");
            }
        }
    }
}
