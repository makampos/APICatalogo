using System;
using System.Collections.Generic;
using System.Linq;
using APICatologo.DTOs;
using APICatologo.Models;
using APICatologo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatologo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            try
            {
                var categorias = _uof.CategoriaRepository.Get().ToList();
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
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
        {
            try
            {
                var categorias = _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
                var categoriasDTo = _mapper.Map<List<CategoriaDTO>>(categorias);

                return categoriasDTo;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar as categorias do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
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

        [HttpPost]
        public ActionResult Post([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _uof.CategoriaRepository.Add(categoria);
                _uof.Commit();

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaDTO);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova categoria");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                if (id != categoriaDto.CategoriaId)
                {
                    return BadRequest($"Não foi possível atualizar a categoria com o id={id}");
                }

                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _uof.CategoriaRepository.Update(categoria);
                _uof.Commit();

                return Ok($"A categoria com o id= {id} foi atualizada com sucesso");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar a categoria com o id= {id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com o id={id} não foi encontrada");
                }

                _uof.CategoriaRepository.Delete(categoria);
                _uof.Commit();
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
