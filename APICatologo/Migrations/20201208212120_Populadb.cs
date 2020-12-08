using Microsoft.EntityFrameworkCore.Migrations;

namespace APICatologo.Migrations
{
    public partial class Populadb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categorias(Nome,ImagemUrl)" +
                "Values('Bebidas','http://wwww.faker.net/Imagens/bebidas.jpg')");
            migrationBuilder.Sql("insert into Categorias(Nome,ImagemUrl)" +
                "Values('Lanches','http://wwww.faker.net/Imagens/lanches.jpg')");
            migrationBuilder.Sql("insert into Categorias(Nome,ImagemUrl)" +
                "Values('Sobremesas','http://wwww.faker.net/Imagens/sobremesas.jpg')");


            migrationBuilder.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                "Values('Coca-Cola Diet','Refrigerante de Cola','5.45','http://www.faker.net/Imagens/coca.jpg',50,now(),(Select CategoriaId from Categorias where Nome='Bebidas'))");
            migrationBuilder.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                 "Values('Atum','Lanche natural de atum','8.45','http://www.faker.net/Imagens/atum.jpg',20,now(),(Select CategoriaId from Categorias where Nome='Lanches'))");
            migrationBuilder.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                "Values('Pudim','Pudim com leite condensado','3.15','http://www.faker.net/Imagens/pudim.jpg',6,now(),(Select CategoriaId from Categorias where Nome='Sobremesas'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("Delete from Categorias");
            migrationBuilder.Sql("Delete from Produtos");

        }
    }
}
