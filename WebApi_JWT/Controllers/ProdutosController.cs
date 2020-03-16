using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi_JWT.Models;

namespace WebApi_JWT.Controllers
{
    [Authorize(Policy = "UsuarioAPI")]
    public class ProdutosController : Controller
    {
        [HttpGet]
        [Route("api/ListarProdutos")]
        public IActionResult ListarProdutos()
        {
            var produtos = new List<Produto>();

            for (int i = 0; i <= 10; i++)
            {
                produtos.Add(new Produto() { IdDoProduto = i, NomeDoProduto = $"Produto {i}" });
            }

            return Ok(produtos);
        }
    }
}
