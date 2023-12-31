using BusinessLayer.Interface;
using BusinessLayer.Service;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepoLayer.Context;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooNote
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
            services.AddDbContext<FundooContext>(opts =>
            opts.UseSqlServer(Configuration["ConnectionString:FundooDB"]));
            //User Configuration
            services.AddTransient<IUserBusiness,UserBusiness>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<INoteBusiness, NoteBusiness>();
            services.AddTransient<INotesRepo, NotesRepo>();
            services.AddTransient<ILabelBusiness, LabelBusiness>();
            services.AddTransient<ILabelRepo, Labelrepo>();
            services.AddTransient<ICollabBusiness, CollabBusiness>();
            services.AddTransient<ICollabRepo, CollabRepo>();
            
            services.AddControllers();
            //services.AddCors();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1" });
           

           
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                c.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                        securitySchema, Array.Empty<string>() }
                });
            });

            //configuration for cloudinary
            IConfigurationSection configurationSection = Configuration.GetSection("CloudinaryConnection");
            Account cloudinaryAccount = new Account(
                configurationSection["CloudName"],
                configurationSection["ApiKey"],
                configurationSection["APISecret"]
                );
            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);
            services.AddSingleton(cloudinary);

            services.AddTransient<FileService, FileService>();


            //Configuration for JWT 
            var token = Configuration.GetValue<string>("JWTConfig:key");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(y =>
            {
                y.SaveToken = true;
                y.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = Configuration["JWTConfig:Issuer"],
                    ValidAudience = Configuration["JWTConfig:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JWTConfig:key"]))

                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowLocalhost",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint
                ("/swagger/v1/swagger.json", "My APIV1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowLocalhost");
            //app.UseCors(x => x
            //               .AllowAnyMethod()
            //               .AllowAnyHeader()
            //               .SetIsOriginAllowed(origin => true) // allow any origin
            //               .AllowCredentials());

            app.UseAuthentication();

           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
