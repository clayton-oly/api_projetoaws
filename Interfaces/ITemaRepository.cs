using SocialApp.model;

namespace SocialApp.Interfaces
{
    public interface ITemaRepository
    {
        Task<IEnumerable<Tema>> GetAllTemasAsync();
        Task<Tema> GetTemaByIdAsync(int id);
        Task<Tema> CreateTemaAsync(Tema tema);
        Task UpdateTemaAsync(Tema tema);
        Task DeleteTemaAsync(int id);

    }
}

