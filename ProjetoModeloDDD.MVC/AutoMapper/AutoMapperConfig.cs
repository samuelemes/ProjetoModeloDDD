
using AutoMapper;

namespace ProjetoModeloDDD.MVC.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}