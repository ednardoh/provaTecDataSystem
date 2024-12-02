using WebCadTarefa.Domain;

namespace WebCadTarefa.Interfaces
{
    public interface ITarefas
    {
        Task<IList<Tarefas>> GetAllAsync();
        Task<IList<Tarefas>> GetAsync(int ID);
        Task<IList<Tarefas>> GetByDescricaotarefaAsync(string Descricaotarefa);
        Task<IList<Tarefas>> GetByStatusTarefaAsync(string Statustarefa);
        Task<Tarefas> GetAsyncEdit(int ID); //consulta para Editar a tarefa
        Task<bool> CreateAsync(Tarefas tarefas);
        Task<bool> UpdateAsync(Tarefas tarefas);
        Task<bool> DeleteAsync(int ID);
    }
}
