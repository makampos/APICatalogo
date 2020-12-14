using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Context;
using APICatologo.Models;
using APICatologo.Pagination;

namespace APICatologo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            //return Get()
            //    .OrderBy(on => on.Nome)
            //    .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            //    .Take(produtosParameters.PageSize)
            //    .ToList();

            return PagedList<Produto>.ToPagedList(
                    Get().OrderBy(on => on.CategoriaId),
                    produtosParameters.PageNumber,
                    produtosParameters.PageSize
                );
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c => c.Preco).ToList();
        }
    }
}
