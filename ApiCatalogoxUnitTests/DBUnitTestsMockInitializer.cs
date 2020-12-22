﻿using System;
using System.Collections.Generic;
using System.Text;
using APICatologo.Context;
using APICatologo.Models;

namespace ApiCatalogoxUnitTests
{
    public class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        {

        }

        public void Seed(AppDbContext context)
        {
            context.Categorias.Add
                (new Categoria { CategoriaId = 999, Nome = "Bebidas999", ImagemUrl = "bebidas999.jpg" });
            context.Categorias.Add
                (new Categoria { CategoriaId = 2, Nome = "Sucos", ImagemUrl = "sucos1.jpg" });
            context.Categorias.Add
                (new Categoria { CategoriaId = 3, Nome = "Doces", ImagemUrl = "doces1.jpg" });
            context.Categorias.Add
                (new Categoria { CategoriaId = 4, Nome = "Salgados", ImagemUrl = "salgados1.jpg" });
            context.Categorias.Add
                (new Categoria { CategoriaId = 5, Nome = "Tortas", ImagemUrl = "tortas1.jpg" });
            context.Categorias.Add
                (new Categoria { CategoriaId = 6, Nome = "Bolos", ImagemUrl = "bolos1.jpg" });
            context.Categorias.Add
                (new Categoria { CategoriaId = 7, Nome = "Lanches", ImagemUrl = "salgados1.jpg" });
            context.SaveChanges();
        }
    }
}
