using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniControl.Models;
using UniControl.Service;
using WEB.Models.DataTables;

namespace UniControl.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AplicacaoDbContext _db;

        public ContatoController(AplicacaoDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Listar(DataTablesAjaxRequest requisicao)
        {
            try
            {
                var lista = from item in ContatoService.Listar(_db)
                            select new
                            {
                                item.Id,
                                item.NomeFantasia,
                                item.Telefone,
                                item.DataCadastro
                            };

                var busca = string.IsNullOrEmpty(requisicao.Busca)
                    ? requisicao.Search == null ? string.Empty : requisicao.Search.Value
                    : requisicao.Busca;

                if (!string.IsNullOrEmpty(busca))
                {
                    busca = busca.ToLower();
                    lista = string.IsNullOrEmpty(requisicao.Busca)
                        ? lista.Where(l => l.NomeFantasia.ToLower().Contains(busca))
                        : lista.Where(busca);
                }

                var ordenacao = requisicao.Order == null
                    ? DataTablesOrder.OrderValue("NomeFantasia", "asc")
                    : DataTablesOrder.OrderValue(requisicao.Columns, requisicao.Order);

                lista = lista.OrderBy(ordenacao);

                var itensPorPagina = requisicao.Length ?? lista.Count();
                var quantidadeRegistros = lista.Count();
                var pular = requisicao.Start ?? 0;
                var listaFinal = lista.Skip(pular).Take(itensPorPagina).ToList();

                return Ok(new
                {
                    data = listaFinal,
                    draw = requisicao.Draw,
                    recordsFiltered = quantidadeRegistros,
                    recordsTotal = quantidadeRegistros
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Salvar(Contato contato)
        {
            try
            {
                string mensagem;
                if (contato.Id == 0)
                {
                    contato.DataCadastro = DateTime.Now;
                    ContatoService.Cadastrar(contato, _db);
                    mensagem = "Contato cadastrado com sucesso";
                }
                else
                {
                    ContatoService.Atualizar(contato, _db);
                    mensagem = "Contato atualizado com sucesso";
                }
                return Ok(new
                {
                    Mensagem = mensagem,
                    contato.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Pagina(int id)
        {
            try
            {
                var modelo = ContatoService.Listar(id, _db);
                if (modelo == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    modelo.Id,
                    modelo.NomeFantasia,
                    modelo.Telefone,
                    modelo.DataCadastro
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            try
            {
                ContatoService.Remover(id, _db);

                return Ok(new
                {
                    Mensagem = "Contato removido com sucesso"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}