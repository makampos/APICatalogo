using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.DTOs;
using APICatologo.Models;
using APICatologo.Pagination;
using APICatologo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace APICatologo.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public StringValues JsonConver { get; private set; }

        public CategoriasController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get( [FromQuery] CategoriasParameters categoriaParameters)
        {
            try
            {
                var categorias =  await _uof.CategoriaRepository.GetCategorias(categoriaParameters);
                var metadata = new
                {
                    categorias.TotalCount,
                    categorias.PageSize,
                    categorias.CurrentPage,
                    categorias.TotalPages,
                    categorias.HasNext,
                    categorias.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

                return categoriasDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao retornar as categorias do banco de dados");
            }
        }

        [HttpGet]
        [Route("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
        {
            try
            {
                var categorias = await _uof.CategoriaRepository.GetCategoriasProdutos();
                var categoriasDTo = _mapper.Map<List<CategoriaDTO>>(categorias);

                return categoriasDTo;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar as categorias do banco de dados");
            }
        }

        /// <summary>
        /// Obtem uma Categoria pelo seu Id
        /// </summary>
        /// <param name="id">codigo da categoria</param>
        /// <returns>Objetos Categoria</returns>
        [HttpGet("{id}", Name = "ObterCategoria")]
        [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com o id ={id} não foi encontrada");
                }

                var categoriaDTo = _mapper.Map<CategoriaDTO>(categoria);
                return categoriaDTo;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar a categoria do banco de dados");
            }
        }

        /// <summary>
        /// Inclui uma nova categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     POST api/categorias
        ///     {
        ///         "categoriaId": 1,
        ///         "nome": "categoria1",
        ///         "imagemUrl": "https//teste.net.jpg"
        ///     }
        /// </remarks>
        /// <param name="categoriaDto">Objeto categoria</param>
        /// <returns>O objeto categoria incluida</returns>
        /// <remkarks>Retorna um objeto Categoria incluída</remkarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _uof.CategoriaRepository.Add(categoria);
                await _uof.Commit();

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaDTO);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova categoria");
            }
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                if (id != categoriaDto.CategoriaId)
                {
                    return BadRequest($"Não foi possível atualizar a categoria com o id={id}");
                }

                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _uof.CategoriaRepository.Update(categoria);
                await _uof.Commit();

                return Ok($"A categoria com o id= {id} foi atualizada com sucesso");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar a categoria com o id= {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com o id={id} não foi encontrada");
                }

                 _uof.CategoriaRepository.Delete(categoria);
                await _uof.Commit();
                var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

                return categoriaDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir a categoria com id= {id}");
            }
        }
    }
}
