using Dapper;
using System.Data.SqlClient;
using WebCadTarefa.Domain;
using WebCadTarefa.Interfaces;

namespace WebCadTarefa.DAO
{
    public class TarefasDAO : ITarefas
    {
    
        private IConfiguration _config;        

        public TarefasDAO(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> CreateAsync(Tarefas tarefas)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                var retorno = await connection.ExecuteAsync(@"insert into TB_Tarefas 
                                                            (TituloTarefa
                                                            ,DescricaoTarefa
                                                            ,DataCriacao                                                             
                                                            ,Status
                                                            )
                                                        Output INSERTED.ID values
                                                            (@tituloTarefa
                                                            ,@desCricaoTarefa
                                                            ,getdate()                                                            
                                                            ,@status
                                                            )", new
                {
                    tituloTarefa    = tarefas.Titulotarefa,
                    descricaoTarefa = tarefas.Descricaotarefa,
                    dataCriacao     = tarefas.Datacriacao,
                    dataConclusao   = "", 
                    status          = tarefas.Status
                });
                return retorno > 0;

            }
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var retorno = await connection.ExecuteAsync("delete from TB_Tarefas Where ID = @ID", new { ID = ID });
                return retorno > 0;

            }
        }

        public async Task<IList<Tarefas>> GetAllAsync()
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                return (await connection.QueryAsync<Tarefas>(@"select ID
                                                                     ,TituloTarefa
                                                                     ,DescricaoTarefa
                                                                     ,format(DataCriacao, 'dd/MM/yyyy', 'pt-BR') as DataCriacao
                                                                     ,format(DataConclusao, 'dd/MM/yyyy', 'pt-BR') as DataConclusao                                                                     
                                                                     ,Status                                                                                                                                        
                                                            from TB_Tarefas").ConfigureAwait(false)).AsList();
            }

        }

        public async Task<IList<Tarefas>> GetAsync(int ID)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                return (await connection.QueryAsync<Tarefas>(@"select ID
                                                                     ,TituloTarefa
                                                                     ,DescricaoTarefa
                                                                     ,format(DataCriacao, 'dd/MM/yyyy', 'pt-BR') as DataCriacao
                                                                     ,format(DataConclusao, 'dd/MM/yyyy', 'pt-BR') as DataConclusao  
                                                                     ,Status                                                                                                                                        
                                                            from TB_Tarefas where ID = @ID", new { ID = ID }).ConfigureAwait(false)).AsList();
            }
        }

        public async Task<IList<Tarefas>> GetByDescricaotarefaAsync(string Descricaotarefa)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                return (await connection.QueryAsync<Tarefas>(@"select ID
                                                                     ,TituloTarefa
                                                                     ,DescricaoTarefa
                                                                     ,format(DataCriacao, 'dd/MM/yyyy', 'pt-BR') as DataCriacao
                                                                     ,format(DataConclusao, 'dd/MM/yyyy', 'pt-BR') as DataConclusao 
                                                                     ,Status                                                                                                                                        
                                                            from TB_Tarefas where DescricaoTarefa like @Descricaotarefa", new { Descricaotarefa = @Descricaotarefa }).ConfigureAwait(false)).AsList();


            }
        }

        public async Task<IList<Tarefas>> GetByStatusTarefaAsync(string Statustarefa) 
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                return (await connection.QueryAsync<Tarefas>(@"select ID
                                                                     ,TituloTarefa
                                                                     ,DescricaoTarefa
                                                                     ,format(DataCriacao, 'dd/MM/yyyy', 'pt-BR') as DataCriacao
                                                                     ,format(DataConclusao, 'dd/MM/yyyy', 'pt-BR') as DataConclusao 
                                                                     ,Status                                                                                                                                        
                                                            from TB_Tarefas where Status = @Statustarefa", new { Statustarefa = @Statustarefa }).ConfigureAwait(false)).AsList();


            }
        }

        public async Task<Tarefas> GetAsyncEdit(int ID)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return await connection.QueryFirstOrDefaultAsync<Tarefas>(@"select ID
                                                                                  ,TituloTarefa
                                                                                  ,DescricaoTarefa
                                                                                  ,format(DataCriacao, 'dd/MM/yyyy', 'pt-BR') as DataCriacao
                                                                                  ,format(DataConclusao, 'dd/MM/yyyy', 'pt-BR') as DataConclusao 
                                                                                  ,Status                                                                                                                                        
                                                                          from TB_Tarefas where id = @ID", new { ID = ID });
            }
        }

        public async Task<bool> UpdateAsync(Tarefas tarefas)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var retorno = await connection.ExecuteAsync(@"update TB_Tarefas
                                                            set TituloTarefa         = @tituloTarefa
                                                               ,DescricaoTarefa      = @descricaoTarefa
                                                               ,DataCriacao          = @dataCriacao
                                                               ,DataConclusao        = @dataConclusao
                                                               ,Status               = @status                                                                 
                                                            where ID                 = @ID",
                                                                new
                                                                {
                                                                    ID               = tarefas.ID,
                                                                    tituloTarefa     = tarefas.Titulotarefa,
                                                                    descricaoTarefa  = tarefas.Descricaotarefa,
                                                                    dataCriacao      = DateTime.Parse(tarefas.Datacriacao).ToString("yyyy-MM-dd HH:mm:ss").ToString(),
                                                                    dataConclusao    = DateTime.Parse(tarefas.Dataconclusao).ToString("yyyy-MM-dd HH:mm:ss").ToString(),                                                                    
                                                                    status           = tarefas.Status
                                                                });
                return retorno > 0;

            }
        }
        
    }
}
