using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;
using TN.Authentication.Domain.Static.Roles;
using TN.Setup.Application.ServiceInterface;
using TN.Setup.Application.Setup.Command.AddModule;
using TN.Setup.Application.Setup.Command.AssignModulesToRole;
using TN.Setup.Application.Setup.Command.UpdateAssignModules;
using TN.Setup.Application.Setup.Command.UpdateModules;
using TN.Setup.Application.Setup.Queries.GetModulesByRoleId;
using TN.Setup.Application.Setup.Queries.Modules;
using TN.Setup.Application.Setup.Queries.ModulesById;
using TN.Setup.Application.Setup.Queries.NavigationByUser;
using TN.Setup.Domain.Entities;
using TN.Setup.Domain.Enum;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;
using TN.Shared.Domain.Static.Cache;
using TN.Shared.Infrastructure.Data;
using TN.Shared.Infrastructure.Repository;

namespace TN.Setup.Infrastructure.ServiceImpl
{
    public class ModuleService(
        IUnitOfWork _unitOfWork,
        ApplicationDbContext _applicationDbContext,
        IMapper _mapper,
        RoleManager<IdentityRole> _roleManager,
        IMemoryCacheRepository _memoryCacheRepository
    ) : IModule
    {
        public async Task<Result<AddModuleResponse>> Add(AddModuleCommand addModuleCommand)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                // Generate new module ID
                string newId = Guid.NewGuid().ToString();

                #region Role Check (Optional)
                // Uncomment if role validation is required
                // if (addModuleCommand.Role is not null && !await _roleManager.RoleExistsAsync(addModuleCommand.Role))
                // {
                //     return Result<AddModuleResponse>.Failure("Role already exists");
                // }
                #endregion

                // Create a new module instance
                var modulesData = new Modules(
                    newId,
                    addModuleCommand.Name,
                    addModuleCommand.Rank,
                    addModuleCommand.IconUrl,
                    addModuleCommand.TargetUrl,
                    addModuleCommand.isActive
                );

                // Save to the repository
                await _unitOfWork.BaseRepository<Modules>().AddAsync(modulesData);
                await _unitOfWork.SaveChangesAsync();

                // Commit the transaction
                scope.Complete();

                // Map result
                var resultDTOs = _mapper.Map<AddModuleResponse>(modulesData);
                return Result<AddModuleResponse>.Success(resultDTOs);
            }
            catch (Exception ex)
            {
                return Result<AddModuleResponse>.Failure($"An error occurred while adding modules: {ex.Message}");
            }
        }

        public async Task<Result<AssignModulesToRoleResponse>> AssignModulesToRole(AssignModulesToRoleCommand assignModulesToRoleCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var moduleId in assignModulesToRoleCommand.modulesId)
                    {
                        var rolesModulesExists = await _unitOfWork.BaseRepository<RoleModule>()
                            .AnyAsync(rm => rm.RoleId == assignModulesToRoleCommand.roleId && rm.ModuleId == moduleId);

                        if (!rolesModulesExists)
                        {
                            var roleModule = new RoleModule()
                            {
                                Id = Guid.NewGuid().ToString(),
                                RoleId = assignModulesToRoleCommand.roleId,
                                ModuleId = moduleId,
                                IsAssigned = true,
                                IsActive = assignModulesToRoleCommand.isActive
                            };
                            await _unitOfWork.BaseRepository<RoleModule>().AddAsync(roleModule);

                        }

                    }

                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = new AssignModulesToRoleResponse
                        (
                        roleId: assignModulesToRoleCommand.roleId,
                        moduleId: assignModulesToRoleCommand.modulesId.Select(id => id.ToString()).ToList(),
                        isAssigned: true,
                        isActive: assignModulesToRoleCommand.isActive
                        );

                    return Result<AssignModulesToRoleResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while assigning modules to roles in ModulesServices", ex);
                }


            }
        }

        public async Task<Result<bool>> Delete(string modulesId, CancellationToken cancellationToken)
        {
            try
            {
                //await _memoryCacheRepository.RemoveAsync(CacheKeys.Module);

                var module = await _unitOfWork.BaseRepository<Modules>().GetByGuIdAsync(modulesId);
                if (module is null)
                {
                    return Result<bool>.Failure("NotFound", "Module Cannot be Found");
                }

                _unitOfWork.BaseRepository<Modules>().Delete(module);
                await _unitOfWork.SaveChangesAsync();


                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting modules having {modulesId}", ex);
            }
        }

        public async Task<Result<PagedResult<GetAllModulesResponse>>> GetAllModule(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                //var cachekeys = CacheKeys.Module;
                //var cacheData = await _memoryCacheRepository.GetCacheKey<PagedResult<GetAllModulesResponse>>(cachekeys);
                //if(cacheData is not null)
                //{
                //    return Result<PagedResult<GetAllModulesResponse>>.Success(cacheData);
                //}

                var modules = await _unitOfWork.BaseRepository<Modules>().GetAllAsyncWithPagination();
                var modulesPagedResult = await modules.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);
                var allModulesResponse = _mapper.Map<PagedResult<GetAllModulesResponse>>(modulesPagedResult.Data);

                //await _memoryCacheRepository.SetAsync(cachekeys, allModulesResponse,new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions 
                //    {
                //       AbsoluteExpiration=DateTimeOffset.Now.AddMinutes(30)

                //    },cancellationToken); 

                return Result<PagedResult<GetAllModulesResponse>>.Success(allModulesResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the Modules", ex);

            }
        }

        public async Task<Result<GetModulesByIdResponse>> GetModulesById(string modulesId, CancellationToken cancellationToken = default)
        {
            try
            {

                var modules = await _unitOfWork.BaseRepository<Modules>().GetByGuIdAsync(modulesId);

                var modulesResponse = _mapper.Map<GetModulesByIdResponse>(modules);


                return Result<GetModulesByIdResponse>.Success(modulesResponse);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Modules  by using Id", ex);
            }
        }

        public async Task<Result<List<GetModulesByRoleIdResponse>>> GetModulesByRoleId(string roleId, CancellationToken cancellationToken)
        {
            var modulesWithRoles = await _unitOfWork.BaseRepository<RoleModule>()
                    .GetConditionalAsync(
                    x => x.RoleId == roleId,
                    query => query.Include(rm => rm.Modules).Where(x => x.IsAssigned == true)
                    );

            var resultsDTOs = modulesWithRoles
                //.Where(rm => rm.Modules is not null)
                .Select(rm => new GetModulesByRoleIdResponse
                (
                    rm.Modules.Id,
                    rm.Modules.Name,
                    rm.Modules.TargetUrl,
                    rm.IsActive
                    )).ToList();
            //await _memoryCacheRepository.SetAsync<List<GetModulesByRoleIdResponse>>(cachekeys, resultsDTOs, new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions
            //{
            //    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)

            //}, cancellationToken);

            return Result<List<GetModulesByRoleIdResponse>>.Success(resultsDTOs);
        }

        public async Task<Result<List<NavigationByUserResponse>>> GetNavigationMenuByUser(string userId)
        {
            try
            {
                var roles = await _applicationDbContext.UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Select(ur => ur.RoleId)
                    .ToListAsync();

                var modules = await _applicationDbContext.RoleModules
                    .Where(rm => roles.Contains(rm.RoleId) && rm.IsActive == true && rm.IsAssigned == true)
                    .Include(rm => rm.Modules)
                    .Select(rm => rm.Modules)
                    .Distinct()
                    .ToListAsync();


                #region Without rolesId from RoleSubModule
                //var subModules = await _applicationDbContext.RoleSubModules
                //    .Where(rm => roles.Contains(rm.RoleId) && rm.IsActive==true && rm.IsAssigned == true)
                //    .Include(rm=>rm.SubModules)
                //    .Select(rm => rm.SubModules) 
                //    .Where(s=>s.IsActive == true)
                //    .Distinct()
                //    .ToListAsync();



                //var resultDTOs = modules.Select(m => new NavigationByUserResponse(
                //    m.Id,
                //    m.Name,
                //    m.Role,
                //    m.TargetUrl,
                //    m.IsActive,
                //    subModules
                //        .Where(sm => sm.ModulesId == m.Id) // Ensures subModules belong to the correct module
                //        .Select(sm => new SubModulesResponse(
                //            sm.ModulesId,
                //            sm.Id,
                //            sm.Name,
                //            sm.IconUrl,
                //            sm.TargetUrl,
                //            sm.Role,
                //            sm.Rank,
                //            sm.IsActive
                //        ))
                //        .ToList()
                //)).ToList();

                #endregion


                var subModules = await _applicationDbContext.RoleSubModules
                        .Where(rm => roles.Contains(rm.RoleId) && rm.IsActive && rm.IsAssigned && rm.SubModules.IsActive)
                        .Select(rm => new
                        {
                            rm.SubModules,
                            rm.RoleId
                        })
                        .Distinct()
                        .ToListAsync();

                var resultDTOs = modules.Select(m => new NavigationByUserResponse(
                    m.Id,
                    m.Name,
                    m.Rank,
                    m.TargetUrl,
                    m.IsActive,
                    subModules
                        .Where(sm => sm.SubModules.ModulesId == m.Id)
                        .Select(sm => new SubModulesResponse(
                            sm.SubModules.ModulesId,
                            sm.SubModules.Id,
                            sm.SubModules.Name,
                            sm.SubModules.IconUrl,
                            sm.SubModules.TargetUrl,
                            sm.RoleId,
                            sm.SubModules.Rank,
                            sm.SubModules.IsActive
                        ))
                        .ToList()
                )).ToList();



                return Result<List<NavigationByUserResponse>>.Success(resultDTOs);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while fetching navigation menu by {userId}", ex);
            }
        }

        public async Task<Result<UpdateModulesResponse>> Update(string modulesId, UpdateModulesCommand modulesCommand)
        {
            try
            {

                //await _memoryCacheRepository.RemoveAsync(CacheKeys.Module);

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (string.IsNullOrEmpty(modulesId))
                    {
                        return Result<UpdateModulesResponse>.Failure("NotFound", "Please provide valid modulesId");
                    }

                    var modulesToBeUpdated = await _unitOfWork.BaseRepository<Modules>().GetByGuIdAsync(modulesId);
                    if (modulesToBeUpdated is null)
                    {
                        return Result<UpdateModulesResponse>.Failure("NotFound", "Modules are not Found");
                    }

                    _mapper.Map(modulesCommand, modulesToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateModulesResponse
                        (
                        modulesToBeUpdated.Name,
                        modulesToBeUpdated.Rank,
                        modulesToBeUpdated.IconUrl,
                        modulesToBeUpdated.TargetUrl,
                        modulesToBeUpdated.IsActive
                        );

                    return Result<UpdateModulesResponse>.Success(resultResponse);

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating modules {modulesId}", ex);
            }
        }

        public async Task<Result<UpdateAssignModulesResponse>> UpdateAssignModules(UpdateAssignModulesCommand assignModulesCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(assignModulesCommand.modulesId) && (string.IsNullOrEmpty(assignModulesCommand.roleId)))
                    {
                        return Result<UpdateAssignModulesResponse>.Failure("NotFound", $"Please provide valid rolesId,{assignModulesCommand.roleId} and modulesId,{assignModulesCommand.modulesId}");
                    }

                    var assignModulesToBeUpdate = await _unitOfWork.BaseRepository<RoleModule>()
                        .GetSingleAsync(x => x.ModuleId == assignModulesCommand.modulesId && x.RoleId == assignModulesCommand.roleId);

                    assignModulesToBeUpdate.IsActive = assignModulesCommand.isActive;
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateAssignModulesResponse
                      (
                      assignModulesToBeUpdate.ModuleId,
                      assignModulesToBeUpdate.RoleId,
                      assignModulesToBeUpdate.IsActive
                      );

                    return Result<UpdateAssignModulesResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception($"An error occurred while Update AssignModule by {assignModulesCommand.modulesId} and {assignModulesCommand.roleId}", ex);
                };
            }
        }
    }
}
