using EthersonGetContractAPI;
using EthersonGetContractAPI.Data;
using EthersonGetContractAPI.IRepository;
using EthersonGetContractAPI.Repository;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

        builder.Services.AddDbContext<AppDBContext>(option => {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"));
        });

        builder.Services.AddScoped<IContractRepository,ContractRepository>();
        //Add service for automapper
        builder.Services.AddAutoMapper(typeof(MappingConfig));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}