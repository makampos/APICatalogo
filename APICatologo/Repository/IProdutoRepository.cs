using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Models;
using APICatologo.Pagination;

namespace APICatologo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
