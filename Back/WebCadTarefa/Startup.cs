using System;
using WebCadTarefa.DAO;
using WebCadTarefa.Data;
using WebCadTarefa.Interfaces;

namespace WebCadTarefa
{
    public class Startup
    {
        private WebApplicationBuilder _builder;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //colocar aqui as Unit de Interface
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });
            builder.Services.AddScoped<IUsuarios, UsuariosDAO>();  
            builder.Services.AddScoped<ITarefas, TarefasDAO>();
            var bui = builder.Build();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            bui.UseCors(options => options
                .WithOrigins(new[] {
                                     "http://localhost:4200",
                                     "http://localhost:5000",
                                     "http://localhost:5432"
                                   }
                )
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()                                               
            );

            bui.UseHttpsRedirection();
            bui.UseAuthorization();
            bui.MapControllers();
            bui.UseSwagger();
            bui.UseSwaggerUI();
            bui.Run();

        }
    }
}
