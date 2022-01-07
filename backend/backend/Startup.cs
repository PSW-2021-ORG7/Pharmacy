using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using backend.DAL;
using Microsoft.EntityFrameworkCore;
using backend.Repositories.Interfaces;
using backend.Repositories;
using backend.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static backend.Helpers.JwtMiddleware;
using System;
using Grpc.Core;
using backend.GrpcServices;
using backend.Protos;
using backend.Events.EventInventoryCheck;
using backend.Events.LogEvent;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //JSON Serializer           
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
        .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //EntityFramework 
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_P");
            if (connectionString == null) connectionString = "Server=localhost;Port=5432;Database=drugstore;User Id=postgres;Password=1234;";
            services.AddDbContext<DrugStoreContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //Authentication
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });

            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Pharmacy API",
                        Description = "Demo API for showing Pharmacy",
                        Version = "v1"
                    });
            });

            //Dependency injection
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IMedicineInventoryRepository, MedicineInventoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IMedicineCombinationRepository, MedicineCombinationRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IShoppingCartsRepository, ShoppingCartsRepository>();
            services.AddScoped<IAdRepository, AdRepository>();
            //Services
            services.AddTransient<Services.UserService>();
            services.AddTransient<Services.ShoppingCartService>();
            services.AddTransient<Services.TenderingService>();
            services.AddTransient<JwtMiddleware>();

            //events
            services.AddTransient<IInventoryCheckRepository, InventoryCheckDatabase>();
            services.AddTransient<ILogEventService<InventoryCheckEventParams>, Events.EventInventoryCheck.InventoryCheckEventService>();

            //gRPC
            services.AddGrpc();
        }

        private Server server;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {

            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            var scopeeee = app.ApplicationServices.CreateScope();

            app.UseMiddleware<JWTMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<MedicineGrpcService>().RequireHost("*:5001");

            });


            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmacy API");
            });

            server = new Server
            {
                Services = { NetGrpcService.BindService(new MedicineGrpcService()) },
                Ports = { new ServerPort("localhost", 5001, ServerCredentials.Insecure) }
            };
            server.Start();

            applicationLifetime.ApplicationStopping.Register(OnShutdown);

        }

        private void OnShutdown()
        {
            if (server != null)
            {
                server.ShutdownAsync().Wait();
            }

        }
    }
}
