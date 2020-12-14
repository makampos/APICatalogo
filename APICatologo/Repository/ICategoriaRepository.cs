using System.Collections.Generic;
using APICatologo.Models;
using APICatologo.Pagination;

namespace APICatologo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}
