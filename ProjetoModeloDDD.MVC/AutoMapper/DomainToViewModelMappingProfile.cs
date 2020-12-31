
using AutoMapper;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;

namespace ProjetoModeloDDD.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMapping"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<ProdutoViewModel, Produto>();
        }
    }
}