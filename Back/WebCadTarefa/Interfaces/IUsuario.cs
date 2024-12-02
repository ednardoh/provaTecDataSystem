using WebCadTarefa.Domain;

namespace WebCadTarefa.Interfaces
{
    public interface IUsuarios
    {
        Task<IList<Usuarios>> GetAllAsync();
        Task<Usuarios> GetAsync(int ID);
        Task<Usuarios> GetByUsernameAsync(string username);
        Task<bool> CreateAsync(Usuarios usuarios);
        Task<bool> UpdateAsync(Usuarios usuarios);
        Task<bool> DeleteAsync(int ID);
    }
}
