using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APICatologo.Context;
using APICatologo.Models;
using APICatologo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatologo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Exempo de injeção de depedência através de um serviço
        /// </summary>
        /// <param name="meuservico"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuservico, string nome)
        {
            return meuservico.Saudacao(nome);
        }

        /// <summary>
        /// Retorna uma lista de categorias
        /// </summary>
        /// <returns>Categorias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao retornar as categorias do banco de dados");
            }           
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retorna listas de categorias e produtos relacionados</returns>
        [HttpGet]
        [Route("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _context.Categorias.Include(x => x.Produtos).ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar as categorias do banco de dados");
            }
           
        }

        /// <summary>
        /// Retorna uma categoria pelo seu identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Categoria</returns>
        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com o id ={id} não foi encontrada");
                }
                return categoria;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar a categoria do banco de dados" );
            }
        }

        /// <summary>
        ///  Criar uma nova categoria
        /// </summary>
        /// <param name="categoria">Objeto categoria</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova categoria");
            }
        }

        /// <summary>
        /// Alterar uma categoria
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="categoria">Objeto Categoria</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest($"Não foi possível atualizar a categoria com o id={id}");
                }
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok($"A categoria com o id= {id} foi atualizada com sucesso");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar a categoria com o id= {id}");
            }
        }

        /// <summary>
        /// Deletar uma categoria
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com o id={id} não foi encontrada");
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return categoria;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir a categoria com id= {id}");
            }
        }
    }
}
