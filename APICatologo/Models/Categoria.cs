using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace APICatologo.Models
{
    public class Categoria
    {
        public Categoria ()
        {            
            Produtos = new Collection<Produto>();
        }
        public int CategoriaId { get; set; }
        public string Name { get; set; }
        public string ImagemUrl { get; set; }

        // Propriedade de navegação -> 1:N
        public ICollection<Produto> Produtos{ get; set; }
    }
}
