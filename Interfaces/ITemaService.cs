using Microsoft.AspNetCore.Mvc;
using SocialApp.ViewModels;

namespace SocialApp.Interfaces
{
    public interface ITemaService
    {
        Task<ActionResult<IEnumerable<TemaViewModel>>> GetAllTemasAsync();
        Task<ActionResult<TemaViewModel>> GetTemaByIdAsync(int id);
        Task<ActionResult<TemaViewModel>> CreateTemaAsync(TemaViewModel tema);
        Task UpdateTemaAsync(int id, TemaViewModel tema);
        Task DeleteTemaAsync(int id);
    }
}
