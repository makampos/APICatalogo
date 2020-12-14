using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/*
* Define uma classe de contexto para representar 
* uma sessão com o banco de dados afim de fornecer métodos que executão 
* operações crud
* -
* Mapeia as entidades para o banco de dados através da propriedade DbSet<>
*/
namespace APICatologo.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
