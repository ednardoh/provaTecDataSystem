using Dapper;
using System.Data.SqlClient;
using WebCadTarefa.Domain;
using WebCadTarefa.Interfaces;

namespace WebCadTarefa.Data
{
    public class UsuariosDAO : IUsuarios
    {
       
        private IConfiguration _config;

        public UsuariosDAO(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> CreateAsync(Usuarios usuarios)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                var retorno = await connection.ExecuteAsync(@"insert into TB_Usuarios 
                                                              (username
                                                               ,senha
                                                               ,bloquear                                                               
                                                               )
                                                         Output INSERTED.ID values
                                                               (@username
                                                               ,@senha
                                                               ,@bloquear                                                               
                                                                )", new {username = usuarios.Username,
                                                                        senha = usuarios.Senha,
                                                                        bloquear = usuarios.Bloquear                                                                        
                                                                });
               return retorno > 0;                

            }
        }        

        public async Task<bool> DeleteAsync(int ID)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var retorno = await connection.ExecuteAsync("delete from TB_Usuarios Where ID = @ID", new { ID = ID });
                return retorno > 0;
                
            }
        }

        public async Task<IList<Usuarios>> GetAllAsync()
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return (await connection.QueryAsync<Usuarios>(@"select ID
                                                                      ,username
                                                                      ,senha
                                                                      ,bloquear                                                                                                                                         
                                                             from TB_Usuarios").ConfigureAwait(false)).AsList();
            }
            
        }

        public async Task<Usuarios> GetAsync(int ID)                                  
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return await connection.QueryFirstOrDefaultAsync<Usuarios>("Select * from TB_Usuarios where ID = @ID", new { ID = ID });
            }
        }

        public async Task<Usuarios> GetByUsernameAsync(string username) 
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return await connection.QueryFirstOrDefaultAsync<Usuarios>("select senha from TB_Usuarios where username = @username ", new { username = username});
            }
        }


        public async Task<bool> UpdateAsync(Usuarios usuarios)
        {
            await using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var retorno = await connection.ExecuteAsync(@"update TB_Usuarios
                                                               set username = @username
                                                                  ,senha = @senha
                                                                  ,bloquear = @bloquear                                                                  
                                                             where ID = @ID",
                                                             new
                                                             {
                                                                ID = usuarios.ID,
                                                                username = usuarios.Username,
                                                                senha = usuarios.Senha,
                                                                bloquear = usuarios.Bloquear
                                                             });
                return retorno > 0;   

            }
        }

    }
}
