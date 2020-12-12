using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Context;
using APICatologo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatologo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) : base(contexto)
        {
        }
        public IEnumerable<Categoria> GetCategoriasProduto()
        {
            return Get().Include(x => x.Produtos);
        }
    }
}