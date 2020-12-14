using System.Collections.Generic;
using System.Threading.Tasks;
using APICatologo.Models;
using APICatologo.Pagination;

namespace APICatologo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
