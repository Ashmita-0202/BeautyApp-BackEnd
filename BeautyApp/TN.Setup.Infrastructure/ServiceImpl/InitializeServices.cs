
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Authentication.Application.ServiceInterface;
using TN.Authentication.Domain.Entities;
using TN.Authentication.Domain.Static.Roles;
using TN.Setup.Application.ServiceInterface;
using TN.Setup.Application.Setup.Command.Initialize;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Infrastructure.Data;
using TN.Shared.Infrastructure.DataSeed;

namespace TN.Setup.Infrastructure.ServiceImpl
{
    public class InitializeServices : IInitializeServices
    {
        private readonly ApplicationDbContext _context;
        private readonly DataSeeder _dataSeeder;
        private readonly IAuthenticationServices _authenticationService;
        public InitializeServices(ApplicationDbContext applicationDbContext, DataSeeder dataSeeder, IAuthenticationServices authenticationService)
        {
            _authenticationService = authenticationService;
            _dataSeeder = dataSeeder;
            _context = applicationDbContext;
            
        }
        public async Task<Result<InitializeCommandResponse>> InitializeAsync()
        {
            //await _context.Database.MigrateAsync();
            var successMessage = await SeedAsync();
            return Result<InitializeCommandResponse>.Success(successMessage);
        }

        private async Task<InitializeCommandResponse> SeedAsync()
        {
            //Clear data from tables dynamically
            var entityTypes = _context.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                if(tableName is not null)
                {
                    await _context.Database.ExecuteSqlRawAsync($"DELETE FROM [{tableName}]");
                }
            }

            //Insert initial data
            if(!_context.Organizations.Any())
            {
                await _dataSeeder.Seed();
                var userName = "superadminuser";
                var email = "superadmin@gmail.com";
                var password = "Admin@123";
                var user = new ApplicationUser { UserName = userName, Email = email };
                await _authenticationService.CreateUserAsync(user, password);
                await _authenticationService.AssignRoles(user, Role.SuperAdmin);

                //Seeding Developer User
                var DevUsername = "developeruser";
                var DevEmail = "developeruser@gmail.com";
                var Devpassword = "Admin@123";

                var DevUser = new ApplicationUser { UserName= DevUsername, Email = DevEmail };
                await _authenticationService.CreateUserAsync(DevUser, Devpassword);
                await _authenticationService.AssignRoles(DevUser, Role.DeveloperUser);

                return new InitializeCommandResponse(userName, password,"Initialized Sucessfull");
            }
            return new InitializeCommandResponse(null!, null!, "Already Initialized");
        }
    }
}
