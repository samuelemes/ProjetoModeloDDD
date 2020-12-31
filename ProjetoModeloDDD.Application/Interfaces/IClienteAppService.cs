using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interfaces
{
    public interface IClienteAppService : IAppServiceBase<Cliente>
    {
        IEnumerable<Cliente> ObterClientesEspeciais();
    }
}
