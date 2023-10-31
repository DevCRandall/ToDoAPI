using Microsoft.EntityFrameworkCore;

namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Step 9a) Add Cors functionality to determine what websites can access the data in this application
            //CORS stands for Cross Origin Resource Sharing and by default browsers use this to block websites from requesting data unless that website has permission to do so. This code below determines what websites have access to CORS with this API.
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("OriginPolicy", "http://localhost:3000", "http://todo.devchristopherrandall.com").AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Step 4) Add ResourcesContext services (How the controller talks to database
            builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
                    options =>
                    {
                        options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"));
                    }
                );

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

            // Step 09b) Add useCors statement below.
            app.UseCors();

            app.Run();
        }
    }
}