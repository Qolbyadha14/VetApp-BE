using VetApp_BE.Config;
using VetApp_BE.Feature.Pets.Models;
using VetApp_BE.GenericRepositories;

namespace VetApp_BE.Feature.Pets.Repositories
{
    public interface IPetRepository : IRepositoryBase<PetModels>
    {

    }

    public class PetRepository : RepositoryBase<PetModels>, IPetRepository
    {
        public PetRepository(DataContext context) : base(context)
        {
        }
    }
}
