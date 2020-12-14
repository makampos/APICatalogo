using System.Collections.Generic;
using System.Threading.Tasks;
using APICatologo.Models;
using APICatologo.Pagination;

namespace APICatologo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
    }
}
