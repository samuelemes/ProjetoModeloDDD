using AutoMapper;
using ProjetoModeloDDD.Application.Interfaces;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoApp;
        private readonly IClienteAppService _clienteApp;

        public ProdutosController(IProdutoAppService produtoApp, IClienteAppService clienteApp)
        {
            _produtoApp = produtoApp;
            _clienteApp = clienteApp;
        }

        // GET: Produtos
        public ActionResult Index()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Produto, ProdutoViewModel>(); cfg.CreateMap<Cliente, ClienteViewModel>(); });
            var mapper = new Mapper(config);

            var list = _produtoApp.GetAll();
            var produtoViewModel = mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(list);
            mapper = null;
            return View(produtoViewModel);
        }




        // GET: Produtos/Details/5
        public ActionResult Details(int id)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Produto, ProdutoViewModel>(); cfg.CreateMap<Cliente, ClienteViewModel>(); });
            var mapper = new Mapper(config);

            var produtoViewModel = mapper.Map<Produto, ProdutoViewModel>(_produtoApp.GetById(id));
            mapper = null;

            return View(produtoViewModel);
        }




        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_clienteApp.GetAll(), "ClienteId", "Nome");

            return View();
        }





        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ProdutoViewModel, Produto>());
                var mapper = new Mapper(config);

                var produtoViewModel = mapper.Map<ProdutoViewModel, Produto>(produto);
                _produtoApp.Add(produtoViewModel);
                mapper = null;

                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(_clienteApp.GetAll(), "ClienteId", "Nome", produto.ClienteId);
            return View(produto);
        }




        public ActionResult BuscarProdutoPorNome(string nome)
        {
            var produtoViewModel = _produtoApp.BuscarPorNome(nome);
            return View(produtoViewModel);
        }




        // GET: Produtos/Edit/5
        public ActionResult Edit(int id)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Produto, ProdutoViewModel>(); cfg.CreateMap<Cliente, ClienteViewModel>(); });
            var mapper = new Mapper(config);

            var produtoViewModel = mapper.Map<Produto, ProdutoViewModel>(_produtoApp.GetById(id));
            mapper = null;

            ViewBag.ClienteId = new SelectList(_clienteApp.GetAll(), "ClienteId", "Nome", produtoViewModel.ClienteId);

            return View(produtoViewModel);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProdutoViewModel, Produto>(); cfg.CreateMap<ClienteViewModel, Cliente>();});
                var mapper = new Mapper(config);

                var produtoViewModel = mapper.Map<ProdutoViewModel, Produto>(produto);
                _produtoApp.Update(produtoViewModel);
                mapper = null;

                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(_clienteApp.GetAll(), "ClienteId", "Nome", produto.ClienteId);

            return View(produto);
        }




        // GET: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var produto = _produtoApp.GetById(id);
            _produtoApp.Remove(produto);

            return RedirectToAction("Index");
        }




        // POST: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            var produto = _produtoApp.GetById(id);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Produto, ProdutoViewModel>(); cfg.CreateMap<Cliente, ClienteViewModel>(); });
            var mapper = new Mapper(config);
            
            var produtoViewModel = mapper.Map<Produto, ProdutoViewModel>(produto);

            return View(produtoViewModel);
        }
    }
}
