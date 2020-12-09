using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Context;
using APICatologo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatologo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }

        /// <summary>
        /// Retorna uma lista de produtos
        /// </summary>
        /// <returns>Produtos</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() {
            return _context.Produtos.AsNoTracking().ToList();
        }

        /// <summary>
        /// Retorna um produto pelo seu identificador
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <returns>Produto</returns>
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        /// <summary>
        /// Cria um novo produto
        /// </summary>
        /// <param name="produto">Objeto produto</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        /// <summary>
        /// Alterar um produto
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="produto">Objeto produto</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            // Procura o recurso diretamente no banco de dados
             var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            // Procura o recurso primeiro em memória, somente se este recurso tiver a propriedade 'id' 
            // como chave primária da tabela
            //var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return produto;
        }
    }
}
