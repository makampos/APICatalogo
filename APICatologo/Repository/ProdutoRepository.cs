using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Context;
using APICatologo.Models;
using APICatologo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatologo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            //return Get()
            //    .OrderBy(on => on.Nome)
            //    .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            //    .Take(produtosParameters.PageSize)
            //    .ToList();

            return await PagedList<Produto>.ToPagedList(
                    Get().OrderBy(on => on.CategoriaId),
                    produtosParameters.PageNumber,
                    produtosParameters.PageSize
                );
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(c => c.Preco).ToListAsync();
        }
    }
}
