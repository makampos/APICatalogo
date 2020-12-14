using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Validations;

namespace APICatologo.Models
{
    [Table("Produtos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [MaxLength(80)]
        //[PrimeiraLetraMaiuscula]
        public string Nome { get; set; }
        [Required]
        [MaxLength(300)]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }
        [Required]
        [MaxLength(500)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        // Propriedade de navegação
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        /// <summary>
        /// Implementa a interface IValidatableObject para costumizar uma validação no modelo
        /// ! não permite reuso ao contrario da implementação da ValidationAttribute na pasta Validations.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraLetra = this.Nome[0].ToString();
                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new                 
                        ValidationResult("A primeira letra do produto deve ser maiúscula",
                        new[] { nameof(this.Nome) }

                    );
                }
            }
            if (this.Estoque <= 0)
            {
                yield return new
                       ValidationResult("O estoque deve ser maior que 0",
                       new[] { nameof(this.Estoque) }

                   );
            }

         }
    }     
        
}


