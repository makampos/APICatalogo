using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatologo.Models;
using AutoMapper;

namespace APICatologo.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
