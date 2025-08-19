using Microsoft.AspNetCore.Mvc;
using SocialApp.ViewModels;

namespace SocialApp.Interfaces
{
    public interface ITemaService
    {
        Task<IEnumerable<TemaViewModel>> GetAllTemasAsync();
        Task<TemaViewModel> GetTemaByIdAsync(int id);
        Task<TemaViewModel> CreateTemaAsync(TemaViewModel tema);
        Task UpdateTemaAsync(int id, TemaViewModel tema);
        Task<bool> DeleteTemaAsync(int id);
    }
}
