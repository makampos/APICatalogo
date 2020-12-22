using System;
using System.Collections.Generic;
using System.Text;
using APICatologo.Context;
using APICatologo.Controllers;
using APICatologo.DTOs;
using APICatologo.DTOs.Mappings;
using APICatologo.Repository;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApiCatalogoxUnitTests
{
    public class CategoriasUnitTestController
    {
        private IMapper mapper;
        private IUnitOfWork repository;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString = "Server=localhost;Port=3306;Database=CatalogoDb;Uid=root;Pwd=root;";

        static CategoriasUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseMySql(connectionString).Options;
        }

        public CategoriasUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);

            repository = new UnitOfWork(context);
        }

        // testes unitários =========================================================================================
        // testar o método GET

        [Fact]
        public void GetCategorias_Return_OkResult()
        {
            // Arrange
            var controller = new CategoriasController(repository, mapper);

            // Act           
            var data = controller.Get();

            // Assert
            Assert.IsType<List<CategoriaDTO>>(data.Value);
        }  
        
        // GET - badrequest

        [Fact]
        public void GetCategorias_ReturnBadRequest()
        {
            // Arrange
            var controller = new CategoriasController(repository, mapper);

            // Act 
            var data = controller.Get();

            // Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }

        // GET - retornar uma lista de objetos categoria

        [Fact]
        public void GetCategorias_MatchResult()
        {
            // Arrange
            var controller = new CategoriasController(repository, mapper);

            // Act
            var data = controller.Get();

            // Assert
            Assert.IsType<List<CategoriaDTO>>(data.Value);
            var cat = data.Value.Should().BeAssignableTo<List<CategoriaDTO>>().Subject;

            Assert.Equal("Bebidas", cat[0].Nome);
            Assert.Equal("http://wwww.faker.net/Imagens/bebidas.jpg", cat[0].ImagemUrl);

            Assert.Equal("Lanches", cat[1].Nome);
            Assert.Equal("http://wwww.faker.net/Imagens/lanches.jpg", cat[1].ImagemUrl);

        }

        // GET por id // retornar a categoria pelo id
        [Fact]
        public void GetCategoriaById_Return_OkResult()
        {
            // Arrange
            var controller = new CategoriasController(repository, mapper);
            var catId = 9;

            // Act
            var data = controller.Get(catId);

            // Assert
            Assert.IsType<CategoriaDTO>(data.Value);

        }

        // POST - CreateResult

        [Fact]
        public void Post_Categoria_AddValidate_Return_CreatedResult()
        {
            // Arrange
            var controller = new CategoriasController(repository, mapper);
            var cat = new CategoriaDTO() 
            { Nome = "Teste Unitario inclusão", ImagemUrl = "http://wwww.faker.net/Imagens/testeUnitarioInclusao.jpg" };

            // Act
            var data = controller.Post(cat);

            // Assert
            Assert.IsType<CreatedAtActionResult>(data);
        }

    }
}
