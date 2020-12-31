using AutoMapper;
using ProjetoModeloDDD.Application.Interfaces;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteAppService _clienteApp;

        public ClientesController(IClienteAppService clienteApp)
        {
            _clienteApp = clienteApp;
        }

        // GET: Clientes
        public ActionResult Index()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cliente, ClienteViewModel>());
            var mapper = new Mapper(config);
            
            var clienteViewModel = mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteApp.GetAll());

            return View(clienteViewModel);
        }




        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cliente, ClienteViewModel>());
            var mapper = new Mapper(config);

            var clienteViewModel = mapper.Map<Cliente, ClienteViewModel>(_clienteApp.GetById(id));

            return View(clienteViewModel);
        }




        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }





        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ClienteViewModel, Cliente>());
                var mapper = new Mapper(config);

                var clienteViewModel = mapper.Map<ClienteViewModel, Cliente>(cliente);
                _clienteApp.Add(clienteViewModel);

                return RedirectToAction("Index");
            }

            return View(cliente);
        }




        public ActionResult Especiais()
        {
            var clienteViewModel = _clienteApp.ObterClientesEspeciais();
            return View(clienteViewModel);
        }




        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Cliente, ClienteViewModel>());
                var mapper = new Mapper(config);

                var clienteViewModel = mapper.Map<Cliente, ClienteViewModel>(_clienteApp.GetById(id));

                return View(clienteViewModel);
            }
            catch (System.Exception)
            {
                return View();
            }
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(ClienteViewModel cliente)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ClienteViewModel, Cliente>());
                var mapper = new Mapper(config);

                var clienteViewModel = mapper.Map<ClienteViewModel, Cliente>(cliente);
                _clienteApp.Update(clienteViewModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(cliente);
            }
        }




        // GET: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cliente, ClienteViewModel>());
            var mapper = new Mapper(config);

            var clienteViewModel = mapper.Map<Cliente, ClienteViewModel>(_clienteApp.GetById(id));

            return View(clienteViewModel);

            var cliente = _clienteApp.GetById(id);
            _clienteApp.Remove(cliente);

            return View("Index");
        }




        // POST: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Cliente, ClienteViewModel>());
                var mapper = new Mapper(config);

                var clienteViewModel = mapper.Map<Cliente, ClienteViewModel>(_clienteApp.GetById(id));

                return View(clienteViewModel);
            }
            catch
            {
                return View();
            }
        }
    }
}
