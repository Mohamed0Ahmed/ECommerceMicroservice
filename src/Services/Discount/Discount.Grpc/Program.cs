using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            #region Configure Services

            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();
            builder.Services.AddDbContext<DiscountContext>(opts =>
                opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));

            #endregion



            var app = builder.Build();



            // Configure the HTTP request pipeline.

            #region Configure Middleware


            app.MapGrpcService<DiscountService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.UseMigration();
            if (app.Environment.IsDevelopment())
                app.MapGrpcReflectionService();

            #endregion



            app.Run();
        }
    }
}