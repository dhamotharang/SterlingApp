using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.MPR.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.SystemCore.Entities;
using System.Configuration;
using System.Data.SqlClient;

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Data;
using Fintrak.Data.Core.Contracts;
using Fintrak.Data.MPR;

namespace Fintrak.Business.MPR.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class MPRPLManager : ManagerBase, IMPRPLService
    {
        public MPRPLManager()
        {
        }

        public MPRPLManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_MPR";
        const string SOLUTION_ALIAS = "MPR";
        const string MODULE_NAME = "FIN_MPR_PROFIT_LOSS";
        const string MODULE_ALIAS = "Profit or Loss";

        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_USER = "User";


        [OperationBehavior(TransactionScopeRequired = true)]
        public override void RegisterModule()
        {
            ExecuteFaultHandledOperation(() =>
            {
                systemCoreData.ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.ISolutionRepository>();
                systemCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IModuleRepository>();
                systemCoreData.IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IMenuRepository>();
                systemCoreData.IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IRoleRepository>();
                systemCoreData.IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == MODULE_NAME).FirstOrDefault();
                    if (module == null)
                    {
                        //check if module category exit
                        systemCoreEntities.Solution solution = solutionRepository.Get().Where(c => c.Name == SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new systemCoreEntities.Solution()
                            {
                                Name = SOLUTION_NAME,
                                Alias = SOLUTION_ALIAS,
                                Active = true,
                                Deleted = false,
                                CreatedBy = "Auto",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "Auto",
                                UpdatedOn = DateTime.Now
                            };

                            solution = solutionRepository.Add(solution);
                        }

                        //register module
                        module = new systemCoreEntities.Module()
                        {
                            Name = MODULE_NAME,
                            Alias = MODULE_ALIAS,
                            SolutionId = solution.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        module = moduleRepository.Add(module);

                        //Role
                        var adminRole = roleRepository.Get().Where(c => c.Name == GROUP_ADMINISTRATOR && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var userRole = roleRepository.Get().Where(c => c.Name == GROUP_USER && c.SolutionId == solution.SolutionId).FirstOrDefault();

                        int menuIndex = 0;

                        //register menu
                        //get the root for ProfitorLoss
                        var root = menuRepository.Get().Where(c => c.Alias == "MPR").FirstOrDefault();

                        var pl = new systemCoreEntities.Menu()
                        {
                            Name = "PROFIT_LOSS",
                            Alias = "Profit or Loss",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "profit_loss_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        pl = menuRepository.Add(pl);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = pl.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var actionMenu = new systemCoreEntities.Menu()
                          {
                              Name = "PROFITLOSS_CAPTION",
                              Alias = "Captions",
                              Action = "PROFITLOSS_CAPTION",
                              ActionUrl = "mpr-plcaption-list",
                              Image = null,
                              ImageUrl = "action_image",
                              ModuleId = module.EntityId,
                              ParentId = pl.EntityId,
                              Position = menuIndex += 1,
                              Active = true,
                              Deleted = false,
                              CreatedBy = "Auto",
                              CreatedOn = DateTime.Now,
                              UpdatedBy = "Auto",
                              UpdatedOn = DateTime.Now
                          };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "MPR_GL_MAPPING",
                            Alias = "GL Mapping",
                            Action = "MPR_GL_MAPPING",
                            ActionUrl = "mpr-mprglmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "GL_RECLASSIFICATION",
                            Alias = "GL Reclassification",
                            Action = "GL_RECLASSIFICATION",
                            ActionUrl = "mpr-glreclassification-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "GL_EXCEPTION",
                            Alias = "GL Exception",
                            Action = "GL_EXCEPTION",
                            ActionUrl = "mpr-glexception-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });


                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "GL_MIS",
                            Alias = "GL MIS",
                            Action = "GL_MIS",
                            ActionUrl = "mpr-glmis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "REVENUE_BUDGET",
                            Alias = "Revenue Budget",
                            Action = "REVENUE_BUDGET",
                            ActionUrl = "mpr-revenuebudget-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        //
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PROFIT_LOSS_REPORT",
                            Alias = "Fees And Commission",
                            Action = "PROFIT_LOSS_REPORT",
                            ActionUrl = "mpr-revenue-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PROFIT_LOSS_ADJUSTMENT",
                            Alias = "Profit or Loss Adjustment",
                            Action = "PROFIT_LOSS_ADJUSTMENT",
                            ActionUrl = "mpr-plincomereportadjustment-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "UNMAPPED_PL_GL",
                            Alias = "Un-Mapped P&L GL",
                            Action = "UNMAPPED_PL_GL",
                            ActionUrl = "mpr-unmappedgl-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        //
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "MEMO_GL_MAP",
                            Alias = "Memo GL Map",
                            Action = "MEMO_GL_MAP",
                            ActionUrl = "mpr-memoglmap-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PROCESS_DATA",
                            Alias = "Process Data",
                            Action = "PROCESS_DATA",
                            ActionUrl = "mpr-processdata-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = pl.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                    }

                    ts.Complete();
                }

            });

        }

        #region PLCaption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PLCaption UpdatePLCaption(PLCaption plCaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();

                PLCaption updatedEntity = null;

                if (plCaption.PLCaptionId == 0)
                    updatedEntity = plCaptionRepository.Add(plCaption);
                else
                    updatedEntity = plCaptionRepository.Update(plCaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePLCaption(int plCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();

                plCaptionRepository.Remove(plCaptionId);
            });
        }

        public PLCaption GetPLCaption(int plCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();

                PLCaption plCaptionEntity = plCaptionRepository.Get(plCaptionId);
                if (plCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PLCaption with ID of {0} is not in database", plCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return plCaptionEntity;
            });
        }

        public PLCaptionNewData[] GetPLCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();
                List<PLCaptionNewData> plCaption = new List<PLCaptionNewData>();
                PLCaption[] plCaptionEntity = plCaptionRepository.Get().Where(c => c.ModuleOwnerType == ModuleOwnerType.MPR && c.Active == true).ToArray();

                foreach (var plCaptionInfo in plCaptionEntity)
                {
                    plCaption.Add(
                        new PLCaptionNewData
                        {
                            PLCaptionId = plCaptionInfo.EntityId,
                            CaptionCode = plCaptionInfo.Code,
                            Position = plCaptionInfo.Position,
                            CaptionName = plCaptionInfo.Name,
                            Color = plCaptionInfo.Color,
                            AccountType = plCaptionInfo.AccountType,
                            AccountTypeName = plCaptionInfo.AccountType.ToString(),
                            Active = plCaptionInfo.Active,
                            ModuleOwnerType = plCaptionInfo.ModuleOwnerType,
                            ModuleName = plCaptionInfo.ModuleOwnerType.ToString(),
                            //ParentId = plCaptionInfo.Parent.ParentId,
                            //ParentId = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.EntityId : 0,
                            //ParentName= plCaptionInfo.Parent !=null ? plCaptionInfo.Parent.Name : string.Empty,
                            ParentCode = plCaptionInfo.ParentCode,
                            ParentName = "",//plCaptionInfo.Parent != null ? plCaptionInfo.Parent.Name : string.Empty,
                            CompanyCode = "",
                            //totalLineInfo.Parent != null ? totalLineInfo.Parent.Name : string.Empty,
                        });
                }

                return plCaption.ToArray();
            });
        }

        public PLCaptionData[] GetAllPLCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();


                List<PLCaptionData> plCaption = new List<PLCaptionData>();
                IEnumerable<PLCaptionInfo> plCaptionInfos = plCaptionRepository.GetPLCaptions().ToArray();

                foreach (var plCaptionInfo in plCaptionInfos)
                {
                    plCaption.Add(
                        new PLCaptionData
                        {
                            PLCaptionId = plCaptionInfo.PLCaption.EntityId,
                            Code = plCaptionInfo.PLCaption.Code,
                            Position = plCaptionInfo.PLCaption.Position,
                            Name = plCaptionInfo.PLCaption.Name,
                            Color = plCaptionInfo.PLCaption.Color,
                            AccountType = plCaptionInfo.PLCaption.AccountType,
                            AccountTypeName = plCaptionInfo.PLCaption.AccountType.ToString(),
                            Active = plCaptionInfo.PLCaption.Active,
                            ModuleOwnerType = plCaptionInfo.PLCaption.ModuleOwnerType,
                            ModuleName = plCaptionInfo.PLCaption.ModuleOwnerType.ToString(),
                            //ParentId = plCaptionInfo.Parent.ParentId,
                            //ParentId = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.EntityId : 0,
                            //ParentName= plCaptionInfo.Parent !=null ? plCaptionInfo.Parent.Name : string.Empty,
                            ParentCode = plCaptionInfo.PLCaption.ParentCode,
                            ParentName = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.Name : string.Empty,
                            CompanyCode = "",
                            //totalLineInfo.Parent != null ? totalLineInfo.Parent.Name : string.Empty,
                        });
                }

                return plCaption.ToArray();
            });
        }

        public PLCaptionNewData[] GetAllMPRPLCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();


                List<PLCaptionNewData> plCaption = new List<PLCaptionNewData>();
                IEnumerable<PLCaptionInfo> plCaptionInfos = plCaptionRepository.GetPLCaptions().ToArray().Where(f => f.PLCaption.ModuleOwnerType == ModuleOwnerType.MPR).Distinct();

                foreach (var plCaptionInfo in plCaptionInfos)
                {
                    plCaption.Add(
                        new PLCaptionNewData
                        {
                            PLCaptionId = plCaptionInfo.PLCaption.EntityId,
                            CaptionCode = plCaptionInfo.PLCaption.Code,
                            Position = plCaptionInfo.PLCaption.Position,
                            CaptionName = plCaptionInfo.PLCaption.Name,
                            Color = plCaptionInfo.PLCaption.Color,
                            AccountType = plCaptionInfo.PLCaption.AccountType,
                            AccountTypeName = plCaptionInfo.PLCaption.AccountType.ToString(),
                            Active = plCaptionInfo.PLCaption.Active,
                            ModuleOwnerType = plCaptionInfo.PLCaption.ModuleOwnerType,
                            ModuleName = plCaptionInfo.PLCaption.ModuleOwnerType.ToString(),
                            //ParentId = plCaptionInfo.Parent.ParentId,
                            //ParentId = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.EntityId : 0,
                            //ParentName= plCaptionInfo.Parent !=null ? plCaptionInfo.Parent.Name : string.Empty,
                            ParentCode = plCaptionInfo.PLCaption.ParentCode,
                            ParentName = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.Name : string.Empty,
                            CompanyCode = "",
                            //totalLineInfo.Parent != null ? totalLineInfo.Parent.Name : string.Empty,
                        });
                }

                return plCaption.ToArray();
            });
        }

        public PLCaptionNewData[] GetAllBudgetPLCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();


                List<PLCaptionNewData> plCaption = new List<PLCaptionNewData>();
                IEnumerable<PLCaptionInfo> plCaptionInfos = plCaptionRepository.GetPLCaptions().ToArray().Where(f => f.PLCaption.ModuleOwnerType == ModuleOwnerType.Budget);

                foreach (var plCaptionInfo in plCaptionInfos)
                {
                    plCaption.Add(
                        new PLCaptionNewData
                        {
                            PLCaptionId = plCaptionInfo.PLCaption.EntityId,
                            CaptionCode = plCaptionInfo.PLCaption.Code,
                            Position = plCaptionInfo.PLCaption.Position,
                            CaptionName = plCaptionInfo.PLCaption.Name,
                            Color = plCaptionInfo.PLCaption.Color,
                            AccountType = plCaptionInfo.PLCaption.AccountType,
                            AccountTypeName = plCaptionInfo.PLCaption.AccountType.ToString(),
                            Active = plCaptionInfo.PLCaption.Active,
                            ModuleOwnerType = plCaptionInfo.PLCaption.ModuleOwnerType,
                            ModuleName = plCaptionInfo.PLCaption.ModuleOwnerType.ToString(),
                            //ParentId = plCaptionInfo.Parent.ParentId,
                            //ParentId = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.EntityId : 0,
                            //ParentName= plCaptionInfo.Parent !=null ? plCaptionInfo.Parent.Name : string.Empty,
                            ParentCode = plCaptionInfo.PLCaption.ParentCode,
                            ParentName = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.Name : string.Empty,
                            CompanyCode = "",
                            //totalLineInfo.Parent != null ? totalLineInfo.Parent.Name : string.Empty,
                        });
                }

                return plCaption.ToArray();
            });
        }

        public PLCaptionNewData[] GetAllMPRPLCaptionsByCaptionName(string CaptionName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();


                List<PLCaptionNewData> plCaption = new List<PLCaptionNewData>();
                IEnumerable<PLCaptionInfo> plCaptionInfos = plCaptionRepository.GetPLCaptions().ToArray().Where(f => (f.PLCaption.ModuleOwnerType == ModuleOwnerType.MPR) && f.PLCaption.Name == CaptionName);

                foreach (var plCaptionInfo in plCaptionInfos)
                {
                    plCaption.Add(
                        new PLCaptionNewData
                        {
                            PLCaptionId = plCaptionInfo.PLCaption.EntityId,
                            CaptionCode = plCaptionInfo.PLCaption.Code,
                            Position = plCaptionInfo.PLCaption.Position,
                            CaptionName = plCaptionInfo.PLCaption.Name,
                            Color = plCaptionInfo.PLCaption.Color,
                            AccountType = plCaptionInfo.PLCaption.AccountType,
                            AccountTypeName = plCaptionInfo.PLCaption.AccountType.ToString(),
                            Active = plCaptionInfo.PLCaption.Active,
                            ModuleOwnerType = plCaptionInfo.PLCaption.ModuleOwnerType,
                            ModuleName = plCaptionInfo.PLCaption.ModuleOwnerType.ToString(),
                            //ParentId = plCaptionInfo.Parent.ParentId,
                            //ParentId = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.EntityId : 0,
                            //ParentName= plCaptionInfo.Parent !=null ? plCaptionInfo.Parent.Name : string.Empty,
                            ParentCode = plCaptionInfo.PLCaption.ParentCode,
                            ParentName = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.Name : string.Empty,
                            CompanyCode = "",
                            //totalLineInfo.Parent != null ? totalLineInfo.Parent.Name : string.Empty,
                        });
                }

                return plCaption.ToArray();
            });
        }

        public PLCaptionNewData[] GetAllBudgetPLCaptionsByCaptionName(string CaptionName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();


                List<PLCaptionNewData> plCaption = new List<PLCaptionNewData>();
                IEnumerable<PLCaptionInfo> plCaptionInfos = plCaptionRepository.GetPLCaptions().ToArray().Where(f => (f.PLCaption.ModuleOwnerType == ModuleOwnerType.Budget) && f.PLCaption.Name == CaptionName);

                foreach (var plCaptionInfo in plCaptionInfos)
                {
                    plCaption.Add(
                        new PLCaptionNewData
                        {
                            PLCaptionId = plCaptionInfo.PLCaption.EntityId,
                            CaptionCode = plCaptionInfo.PLCaption.Code,
                            Position = plCaptionInfo.PLCaption.Position,
                            CaptionName = plCaptionInfo.PLCaption.Name,
                            Color = plCaptionInfo.PLCaption.Color,
                            AccountType = plCaptionInfo.PLCaption.AccountType,
                            AccountTypeName = plCaptionInfo.PLCaption.AccountType.ToString(),
                            Active = plCaptionInfo.PLCaption.Active,
                            ModuleOwnerType = plCaptionInfo.PLCaption.ModuleOwnerType,
                            ModuleName = plCaptionInfo.PLCaption.ModuleOwnerType.ToString(),
                            //ParentId = plCaptionInfo.Parent.ParentId,
                            //ParentId = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.EntityId : 0,
                            //ParentName= plCaptionInfo.Parent !=null ? plCaptionInfo.Parent.Name : string.Empty,
                            ParentCode = plCaptionInfo.PLCaption.ParentCode,
                            ParentName = plCaptionInfo.Parent != null ? plCaptionInfo.Parent.Name : string.Empty,
                            CompanyCode = "",
                            //totalLineInfo.Parent != null ? totalLineInfo.Parent.Name : string.Empty,
                        });
                }

                return plCaption.ToArray();
            });
        }

        #endregion

        #region MPRGLMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRGLMapping UpdateMPRGLMapping(MPRGLMapping mprGLMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IMPRGLMappingRepository>();

                MPRGLMapping updatedEntity = null;

                if (mprGLMapping.MPRGLMappingId == 0)
                    updatedEntity = glMappingRepository.Add(mprGLMapping);
                else
                    updatedEntity = glMappingRepository.Update(mprGLMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMPRGLMapping(int glMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IMPRGLMappingRepository>();

                glMappingRepository.Remove(glMappingId);
            });
        }

        public MPRGLMapping GetMPRGLMapping(int glMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IMPRGLMappingRepository>();

                MPRGLMapping glMappingEntity = glMappingRepository.Get(glMappingId);
                if (glMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MPRGLMapping with ID of {0} is not in database", glMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glMappingEntity;
            });
        }

        public MPRGLMappingData[] GetAllMPRGLMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IMPRGLMappingRepository>();


                List<MPRGLMappingData> glMapping = new List<MPRGLMappingData>();
                IEnumerable<MPRGLMappingInfo> glMappingInfos = glMappingRepository.GetMPRGLMappings().ToArray();

                foreach (var glMappingInfo in glMappingInfos)
                {
                    glMapping.Add(
                        new MPRGLMappingData
                        {
                            MPRGLMappingId = glMappingInfo.MPRGLMapping.EntityId,
                            CaptionCode = glMappingInfo.PLCaption != null ? glMappingInfo.PLCaption.Name : string.Empty,
                            GLCode = glMappingInfo.MPRGLMapping.GLCode,
                            GLName = glMappingInfo.GLDefinition != null ? glMappingInfo.GLDefinition.Description : string.Empty,
                            CompanyCode = glMappingInfo.MPRGLMapping.CompanyCode,
                            Active = glMappingInfo.MPRGLMapping.Active
                        });
                }

                return glMapping.ToArray();
            });
        }

        public KeyValueData[] GetUnMappedPLGLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var results = new List<KeyValueData>();

                var connectionString = GetDataConnection();

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("spp_mpr_revenue_getunmappedgl", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    con.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var data = new KeyValueData();

                        if (reader["GLCode"] != DBNull.Value)
                            data.Key = reader["GLCode"].ToString();

                        if (reader["Description"] != DBNull.Value)
                            data.Value = reader["Description"].ToString();

                        results.Add(data);
                    }

                    con.Close();
                }

                return results.ToArray();
            });
        }

        #endregion

        #region GLReclassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GLReclassification UpdateGLReclassification(GLReclassification glReclassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLReclassificationRepository glReclassificationRepository = _DataRepositoryFactory.GetDataRepository<IGLReclassificationRepository>();

                GLReclassification updatedEntity = null;

                if (glReclassification.GLReclassificationId == 0)
                    updatedEntity = glReclassificationRepository.Add(glReclassification);
                else
                    updatedEntity = glReclassificationRepository.Update(glReclassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLReclassification(int glReclassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLReclassificationRepository glReclassificationRepository = _DataRepositoryFactory.GetDataRepository<IGLReclassificationRepository>();

                glReclassificationRepository.Remove(glReclassificationId);
            });
        }

        public GLReclassification GetGLReclassification(int glReclassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLReclassificationRepository glReclassificationRepository = _DataRepositoryFactory.GetDataRepository<IGLReclassificationRepository>();

                GLReclassification glReclassificationEntity = glReclassificationRepository.Get(glReclassificationId);
                if (glReclassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GLReclassification with ID of {0} is not in database", glReclassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glReclassificationEntity;
            });
        }

        //public GLReclassification[] GetAllGLReclassifications()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IGLReclassificationRepository glReclassificationRepository = _DataRepositoryFactory.GetDataRepository<IGLReclassificationRepository>();

        //        IEnumerable<GLReclassification> glReclassifications = glReclassificationRepository.Get().ToArray();

        //        return glReclassifications.ToArray();
        //    });
        //}

        public GLReclassificationData[] GetAllGLReclassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLReclassificationRepository glReclassificationRepository = _DataRepositoryFactory.GetDataRepository<IGLReclassificationRepository>();


                List<GLReclassificationData> glReclassification = new List<GLReclassificationData>();
                IEnumerable<GLReclassificationInfo> glReclassificationInfos = glReclassificationRepository.GetGLReclassifications().ToArray();

                foreach (var glReclassificationInfo in glReclassificationInfos)
                {
                    glReclassification.Add(
                        new GLReclassificationData
                        {
                            GLReclassificationId = glReclassificationInfo.GLReclassification.EntityId,
                            GLAccount = glReclassificationInfo.GLReclassification.GLAccount,
                            CaptionName = glReclassificationInfo.PLCaption.Name,
                            CaptionCode = glReclassificationInfo.PLCaption.Code,
                            CompanyCode = "",
                            Active = glReclassificationInfo.GLReclassification.Active
                        });
                }

                return glReclassification.ToArray();
            });
        }

        #endregion

        #region GLException operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GLException UpdateGLException(GLException glException)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLExceptionRepository glExceptionRepository = _DataRepositoryFactory.GetDataRepository<IGLExceptionRepository>();

                GLException updatedEntity = null;

                if (glException.GLExceptionId == 0)
                    updatedEntity = glExceptionRepository.Add(glException);
                else
                    updatedEntity = glExceptionRepository.Update(glException);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLException(int glExceptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLExceptionRepository glExceptionRepository = _DataRepositoryFactory.GetDataRepository<IGLExceptionRepository>();

                glExceptionRepository.Remove(glExceptionId);
            });
        }

        public GLException GetGLException(int glExceptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLExceptionRepository glExceptionRepository = _DataRepositoryFactory.GetDataRepository<IGLExceptionRepository>();

                GLException glExceptionEntity = glExceptionRepository.Get(glExceptionId);
                if (glExceptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GLException with ID of {0} is not in database", glExceptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glExceptionEntity;
            });
        }

        //public GLException[] GetAllGLExceptions()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IGLExceptionRepository glExceptionRepository = _DataRepositoryFactory.GetDataRepository<IGLExceptionRepository>();

        //        IEnumerable<GLException> glException = glExceptionRepository.Get().ToArray();

        //        return glException.ToArray();
        //    });
        //}

        public GLExceptionData[] GetAllGLExceptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLExceptionRepository glExceptionRepository = _DataRepositoryFactory.GetDataRepository<IGLExceptionRepository>();


                List<GLExceptionData> glException = new List<GLExceptionData>();
                IEnumerable<GLExceptionInfo> glExceptionInfos = glExceptionRepository.GetGLExceptions().ToArray();

                foreach (var glExceptionInfo in glExceptionInfos)
                {
                    glException.Add(
                        new GLExceptionData
                        {
                            glExceptionId = glExceptionInfo.GLException.EntityId,
                            GLAccount = glExceptionInfo.GLException.GLAccount,
                            CompanyCode = "",
                            Active = glExceptionInfo.GLException.Active
                        });
                }

                return glException.ToArray();
            });
        }
        #endregion

        #region MPRTotalLine operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRTotalLine UpdateMPRTotalLine(MPRTotalLine mprTotalLine)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineRepository>();

                MPRTotalLine updatedEntity = null;

                if (mprTotalLine.TotallineId == 0)
                    updatedEntity = totalLineRepository.Add(mprTotalLine);
                else
                    updatedEntity = totalLineRepository.Update(mprTotalLine);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMPRTotalLine(int totalLineId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineRepository>();

                totalLineRepository.Remove(totalLineId);
            });
        }

        public MPRTotalLine GetMPRTotalLine(int totalLineId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineRepository>();

                MPRTotalLine totalLineEntity = totalLineRepository.Get(totalLineId);
                if (totalLineEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MPRTotalLine with ID of {0} is not in database", totalLineId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return totalLineEntity;
            });
        }

        public MPRTotalLine[] GetAllMPRTotalLines()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineRepository>();

                IEnumerable<MPRTotalLine> totalLines = totalLineRepository.Get().ToArray();

                return totalLines.ToArray();
            });
        }

        public MPRTotalLineData[] GetMPRTotalLines()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineRepository>();


                List<MPRTotalLineData> totalLine = new List<MPRTotalLineData>();
                IEnumerable<MPRTotalLineInfo> totalLineInfos = totalLineRepository.GetMPRTotalLines().ToArray();

                foreach (var totalLineInfo in totalLineInfos)
                {
                    totalLine.Add(
                        new MPRTotalLineData
                        {
                            TotalLineIdId = totalLineInfo.MPRTotalLine.EntityId,
                            Name = totalLineInfo.MPRTotalLine.Name,
                            Position = totalLineInfo.MPRTotalLine.Position,
                            Color = totalLineInfo.MPRTotalLine.Color,
                            ParentId = totalLineInfo.MPRTotalLine.ParentId,
                            ParentName = totalLineInfo.Parent != null ? totalLineInfo.Parent.Name : string.Empty,
                            Active = totalLineInfo.MPRTotalLine.Active
                        });
                }

                return totalLine.ToArray();
            });
        }

        #endregion

        #region MPRTotalLineMakeUp operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRTotalLineMakeUp UpdateMPRTotalLineMakeUp(MPRTotalLineMakeUp mprTotalLineMakeUp)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineMakeUpRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineMakeUpRepository>();

                MPRTotalLineMakeUp updatedEntity = null;

                if (mprTotalLineMakeUp.TotalLineMakeUpId == 0)
                    updatedEntity = totalLineRepository.Add(mprTotalLineMakeUp);
                else
                    updatedEntity = totalLineRepository.Update(mprTotalLineMakeUp);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMPRTotalLineMakeUp(int totalLineMakeUpId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineMakeUpRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineMakeUpRepository>();

                totalLineRepository.Remove(totalLineMakeUpId);
            });
        }

        public MPRTotalLineMakeUp GetMPRTotalLineMakeUp(int totalLineMakeUpId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineMakeUpRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineMakeUpRepository>();

                MPRTotalLineMakeUp totalLineEntity = totalLineRepository.Get(totalLineMakeUpId);
                if (totalLineEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MPRTotalLineMakeUp with ID of {0} is not in database", totalLineMakeUpId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return totalLineEntity;
            });
        }

        public MPRTotalLineMakeUp[] GetAllMPRTotalLineMakeUps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineMakeUpRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineMakeUpRepository>();

                IEnumerable<MPRTotalLineMakeUp> totalLines = totalLineRepository.Get().ToArray();

                return totalLines.ToArray();
            });
        }

        public MPRTotalLineMakeUpData[] GetMPRTotalLineMakeUps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRTotalLineMakeUpRepository totalLineRepository = _DataRepositoryFactory.GetDataRepository<IMPRTotalLineMakeUpRepository>();


                List<MPRTotalLineMakeUpData> totalLineMakeUp = new List<MPRTotalLineMakeUpData>();
                IEnumerable<MPRTotalLineMakeUpInfo> totalLineInfos = totalLineRepository.GetMPRTotalLineMakeUps().ToArray();

                foreach (var totalLineInfo in totalLineInfos)
                {
                    totalLineMakeUp.Add(
                        new MPRTotalLineMakeUpData
                        {
                            TotalLineMakeUpId = totalLineInfo.MPRTotalLineMakeUp.EntityId,
                            TotalLine = totalLineInfo.MPRTotalLineMakeUp.TotalLine,
                            CaptionCode = totalLineInfo.MPRTotalLineMakeUp.CaptionCode,
                            CaptionName = totalLineInfo.PLCaption.Name != null ? totalLineInfo.PLCaption.Name : string.Empty,
                            Active = totalLineInfo.MPRTotalLineMakeUp.Active
                        });
                }

                return totalLineMakeUp.ToArray();
            });
        }

        #endregion

        #region GLMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GLMIS UpdateGLMIS(GLMIS glMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMISRepository glMISRepository = _DataRepositoryFactory.GetDataRepository<IGLMISRepository>();

                GLMIS updatedEntity = null;

                if (glMIS.GlMisId == 0)
                    updatedEntity = glMISRepository.Add(glMIS);
                else
                    updatedEntity = glMISRepository.Update(glMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLMIS(int glMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMISRepository glMISRepository = _DataRepositoryFactory.GetDataRepository<IGLMISRepository>();

                glMISRepository.Remove(glMISId);
            });
        }

        public GLMIS GetGLMIS(int glMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMISRepository glMISRepository = _DataRepositoryFactory.GetDataRepository<IGLMISRepository>();

                GLMIS glMISEntity = glMISRepository.Get(glMISId);
                if (glMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GLMIS with ID of {0} is not in database", glMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glMISEntity;
            });
        }


        public GLMISData[] GetAllGLMISInfo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMISRepository glMISRepository = _DataRepositoryFactory.GetDataRepository<IGLMISRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                List<GLMISData> glMIS = new List<GLMISData>();
                IEnumerable<GLMISInfo> glMISInfos = glMISRepository.GetGLMIS(setUp.Year).Where(c => c.Team.Year == setUp.Year).ToArray();

                foreach (var glMISInfo in glMISInfos)
                {
                    glMIS.Add(
                        new GLMISData
                        {
                            GLMISId = glMISInfo.GLMIS.EntityId,
                            GLAccount = glMISInfo.GLMIS.GLAccount,
                            TeamDefinitionCode = glMISInfo.TeamDefinition.Code,
                            TeamDefinitionName = glMISInfo.TeamDefinition.Name,
                            AccountOfficerDefinitionCode = glMISInfo.AccountOfficerDefinition != null ? glMISInfo.AccountOfficerDefinition.Code : string.Empty,
                            AccountOfficerCode = glMISInfo.AccountOfficer != null ? glMISInfo.AccountOfficer.Code : string.Empty,
                            AccountOfficerName = glMISInfo.AccountOfficer != null ? glMISInfo.AccountOfficer.Name : string.Empty,
                            TeamCode = glMISInfo.Team.Code,
                            TeamName = glMISInfo.Team.Name,
                            CompanyCode = "",
                            Active = glMISInfo.GLMIS.Active
                        });
                }

                return glMIS.ToArray();
            });
        }


        #endregion

        #region PLIncomeReport operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PLIncomeReport UpdatePLIncomeReport(PLIncomeReport plIncomeReport)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportRepository plIncomeReportRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportRepository>();

                PLIncomeReport updatedEntity = null;

                if (plIncomeReport.Id == 0)
                    updatedEntity = plIncomeReportRepository.Add(plIncomeReport);
                else
                    updatedEntity = plIncomeReportRepository.Update(plIncomeReport);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePLIncomeReport(int plIncomeReportId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportRepository plIncomeReportRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportRepository>();

                plIncomeReportRepository.Remove(plIncomeReportId);
            });
        }

        public PLIncomeReport GetPLIncomeReport(int plIncomeReportId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportRepository plIncomeReportRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportRepository>();

                PLIncomeReport plIncomeReportEntity = plIncomeReportRepository.Get(plIncomeReportId);
                if (plIncomeReportEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PLIncomeReport with ID of {0} is not in database", plIncomeReportId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return plIncomeReportEntity;
            });
        }

        public PLIncomeReport[] GetAllPLIncomeReports()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportRepository plIncomeReportRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportRepository>();

                IEnumerable<PLIncomeReport> plIncomeReports = plIncomeReportRepository.Get().ToArray();

                return plIncomeReports.ToArray();
            });
        }

        public PLIncomeReport[] GetPLIncomeReports(string searchType, string searchValue, int number)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportRepository plIncomeReportRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportRepository>();
                List<PLIncomeReport> plIncomeReports = plIncomeReportRepository.GetPLIncomeReportBySearch(searchType, searchValue, number);


                return plIncomeReports.ToArray();
            });
        }


        #endregion

        #region PLIncomeReportAdjustment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PLIncomeReportAdjustment UpdatePLIncomeReportAdjustment(PLIncomeReportAdjustment plIncomeReportAdjustment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportAdjustmentRepository plIncomeReportRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportAdjustmentRepository>();

                PLIncomeReportAdjustment updatedEntity = null;

                if (plIncomeReportAdjustment.Id == 0)
                    updatedEntity = plIncomeReportRepository.Add(plIncomeReportAdjustment);
                else
                    updatedEntity = plIncomeReportRepository.Update(plIncomeReportAdjustment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePLIncomeReportAdjustment(int plIncomeReportAdjustmentId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportAdjustmentRepository plIncomeReportRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportAdjustmentRepository>();

                plIncomeReportRepository.Remove(plIncomeReportAdjustmentId);
            });
        }

        public PLIncomeReportAdjustment GetPLIncomeReportAdjustment(int plIncomeReportAdjustmentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportAdjustmentRepository plIncomeReportAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportAdjustmentRepository>();

                PLIncomeReportAdjustment plIncomeReportAdjustmentEntity = plIncomeReportAdjustmentRepository.Get(plIncomeReportAdjustmentId);
                if (plIncomeReportAdjustmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PLIncomeReportAdjustment with ID of {0} is not in database", plIncomeReportAdjustmentId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return plIncomeReportAdjustmentEntity;
            });
        }

        public PLIncomeReportAdjustment[] GetAllPLIncomeReportAdjustments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportAdjustmentRepository plIncomeReportAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportAdjustmentRepository>();

                IEnumerable<PLIncomeReportAdjustment> plIncomeReportAdjustments = plIncomeReportAdjustmentRepository.Get().ToArray();

                return plIncomeReportAdjustments.ToArray();
            });
        }

        public PLIncomeReportAdjustment[] GetPLIncomeReportAdjustments(string searchType, string searchValue, int number)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportAdjustmentRepository plIncomeReportAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportAdjustmentRepository>();
                List<PLIncomeReportAdjustment> plIncomeReportAdjustments = plIncomeReportAdjustmentRepository.GetPLIncomeReportAdjustmentBySearch(searchType, searchValue, number);


                return plIncomeReportAdjustments.ToArray();
            });
        }


        public PLIncomeReportAdjustment[] GetCodebyUser(string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLIncomeReportAdjustmentRepository plIncomeReportAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportAdjustmentRepository>();

                List<PLIncomeReportAdjustment> plIncomeReportAdjustments = plIncomeReportAdjustmentRepository.GetCodebyUser(userName);

                return plIncomeReportAdjustments.ToArray();
            });
        }

        public PLIncomeReportAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IPLIncomeReportAdjustmentRepository plIncomeReportAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IPLIncomeReportAdjustmentRepository>();
                List<PLIncomeReportAdjustment> plIncomeReportAdjustments = plIncomeReportAdjustmentRepository.GetBalanceSheetAdjustmentByCode(code, userName);


                return plIncomeReportAdjustments.ToArray();
            });
        }

        public void DeleteMPRBalanceSheetAdjustment(string code, string userName)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_deleteplIncomeReportAdjustmentbyCode", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "code",
                    Value = code,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "username",
                    Value = userName,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region Revenue operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Revenue UpdateRevenue(Revenue revenue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueRepository revenueRepository = _DataRepositoryFactory.GetDataRepository<IRevenueRepository>();

                Revenue updatedEntity = null;

                if (revenue.RevenueId == 0)
                    updatedEntity = revenueRepository.Add(revenue);
                else
                    updatedEntity = revenueRepository.Update(revenue);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRevenue(int revenueId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueRepository revenueRepository = _DataRepositoryFactory.GetDataRepository<IRevenueRepository>();

                revenueRepository.Remove(revenueId);
            });
        }

        public Revenue GetRevenue(int revenueId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueRepository revenueRepository = _DataRepositoryFactory.GetDataRepository<IRevenueRepository>();

                Revenue revenueEntity = revenueRepository.Get(revenueId);
                if (revenueEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Revenue with ID of {0} is not in database", revenueId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return revenueEntity;
            });
        }

        public Revenue[] GetRunDate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueRepository revenueRepository = _DataRepositoryFactory.GetDataRepository<IRevenueRepository>();
                IEnumerable<Revenue> revenues = revenueRepository.GetRunDate().ToArray();


                return revenues.ToArray();
            });
        }


        public Revenue[] GetAllRevenues(string searchType, string searchValue, int number, DateTime runDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueRepository revenueRepository = _DataRepositoryFactory.GetDataRepository<IRevenueRepository>();
                List<Revenue> revenues = revenueRepository.GetAllRevenueBySearch(searchType, searchValue, number, runDate, toDate);


                return revenues.ToArray();
            });
        }


        public Revenue[] GetRevenues(string searchType, string searchValue, int number)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueRepository revenueRepository = _DataRepositoryFactory.GetDataRepository<IRevenueRepository>();
                List<Revenue> revenues = revenueRepository.GetRevenueBySearch(searchType, searchValue, number);


                return revenues.ToArray();
            });
        }



        public Revenue[] GetAllRevenue()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueRepository revenueRepository = _DataRepositoryFactory.GetDataRepository<IRevenueRepository>();

                IEnumerable<Revenue> revenues = revenueRepository.Get().ToArray();

                return revenues.ToArray();
            });
        }

        #endregion

        #region RevenueBudget operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RevenueBudget UpdateRevenueBudget(RevenueBudget revenueBudget)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetRepository revenueBudgetRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetRepository>();

                RevenueBudget updatedEntity = null;

                if (revenueBudget.BudgetId == 0)
                    updatedEntity = revenueBudgetRepository.Add(revenueBudget);
                else
                    updatedEntity = revenueBudgetRepository.Update(revenueBudget);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRevenueBudget(int revenueBudgetId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetRepository revenueBudgetRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetRepository>();

                revenueBudgetRepository.Remove(revenueBudgetId);
            });
        }

        public RevenueBudget GetRevenueBudget(int revenueBudgetId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetRepository revenueBudgetRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetRepository>();

                RevenueBudget revenueBudgetEntity = revenueBudgetRepository.Get(revenueBudgetId);
                if (revenueBudgetEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RevenueBudget with ID of {0} is not in database", revenueBudgetId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return revenueBudgetEntity;
            });
        }


        public RevenueBudget[] GetAllRevenueBudgets(string year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetRepository revenueBudgetRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetRepository>();

                IEnumerable<RevenueBudget> revenueBudgets = revenueBudgetRepository.GetRevenueBudgets(year).ToArray();

                return revenueBudgets.ToArray();
            });
        }


        public RevenueBudget[] GetRevenueBudgets(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetRepository revenueBudgetRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetRepository>();
                List<RevenueBudget> revenueBudgets = revenueBudgetRepository.GetBalanceSheetBySearch(searchValue);


                return revenueBudgets.ToArray();
            });
        }


        #endregion

        #region ProcessData operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProcessData UpdateProcessData(ProcessData processData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessDataRepository processDataRepository = _DataRepositoryFactory.GetDataRepository<IProcessDataRepository>();
                //ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();

                //CustAccount CustAccount = CustAccountRepository.Get().Where(c => c.AccountNo == processData.RelatedAccount).FirstOrDefault();

                //if (CustAccount == null)
                //{
                //    NotFoundException ex = new NotFoundException(string.Format("Customer with Account of {0} is not in database", processData.RelatedAccount));
                //    throw new FaultException<NotFoundException>(ex, ex.Message);
                //}

                //processData.CustomerName = CustAccount.AccountName;

                ProcessData updatedEntity = null;

                if (processData.ProcessDataId == 0)
                    updatedEntity = processDataRepository.Add(processData);
                else
                    updatedEntity = processDataRepository.Update(processData);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProcessData(int processDataId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessDataRepository processDataRepository = _DataRepositoryFactory.GetDataRepository<IProcessDataRepository>();

                processDataRepository.Remove(processDataId);
            });
        }

        public ProcessData GetProcessData(int processDataId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessDataRepository processDataRepository = _DataRepositoryFactory.GetDataRepository<IProcessDataRepository>();

                ProcessData processDataEntity = processDataRepository.Get(processDataId);
                if (processDataEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProcessData with ID of {0} is not in database", processDataId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return processDataEntity;
            });
        }

        public ProcessData[] GetAllProcessData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessDataRepository processDataRepository = _DataRepositoryFactory.GetDataRepository<IProcessDataRepository>();

                IEnumerable<ProcessData> processData = processDataRepository.Get().ToArray();

                return processData.ToArray();
            });
        }




        #endregion

        #region RevenueBudgetOfficer operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RevenueBudgetOfficer UpdateRevenueBudgetOfficer(RevenueBudgetOfficer revenueBudgetOfficer)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetOfficerRepository revenueBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetOfficerRepository>();

                RevenueBudgetOfficer updatedEntity = null;

                if (revenueBudgetOfficer.BudgetId == 0)
                    updatedEntity = revenueBudgetOffRepository.Add(revenueBudgetOfficer);
                else
                    updatedEntity = revenueBudgetOffRepository.Update(revenueBudgetOfficer);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRevenueBudgetOfficer(int revenueBudgetOffId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetOfficerRepository revenueBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetOfficerRepository>();

                revenueBudgetOffRepository.Remove(revenueBudgetOffId);
            });
        }

        public RevenueBudgetOfficer GetRevenueBudgetOfficer(int revenueBudgetOffId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetOfficerRepository revenueBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetOfficerRepository>();

                RevenueBudgetOfficer revenueBudgetOffEntity = revenueBudgetOffRepository.Get(revenueBudgetOffId);
                if (revenueBudgetOffEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RevenueBudgetOfficer with ID of {0} is not in database", revenueBudgetOffId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return revenueBudgetOffEntity;
            });
        }


        public RevenueBudgetOfficer[] GetAllRevenueBudgetOfficers(string year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetOfficerRepository revenueBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetOfficerRepository>();

                IEnumerable<RevenueBudgetOfficer> revenueBudgetOffs = revenueBudgetOffRepository.GetRevenueBudgetOfficers(year).ToArray();

                return revenueBudgetOffs.ToArray();
            });
        }

        public RevenueBudgetOfficer[] GetRevenueBudgetOfficers(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueBudgetOfficerRepository revenueBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IRevenueBudgetOfficerRepository>();
                List<RevenueBudgetOfficer> revenueBudgetOffs = revenueBudgetOffRepository.GetBalanceSheetBySearch(searchValue);


                return revenueBudgetOffs.ToArray();
            });
        }


        #endregion

        #region IncomeCentralVaultSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCentralVaultSchedule UpdateIncomeCentralVaultSchedule(IncomeCentralVaultSchedule incomeCentralVaultSchedule)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCentralVaultScheduleRepository incomeCentralVaultScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCentralVaultScheduleRepository>();

                IncomeCentralVaultSchedule updatedEntity = null;

                if (incomeCentralVaultSchedule.IncomeCentralVaultScheduleId == 0)
                    updatedEntity = incomeCentralVaultScheduleRepository.Add(incomeCentralVaultSchedule);
                else
                    updatedEntity = incomeCentralVaultScheduleRepository.Update(incomeCentralVaultSchedule);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCentralVaultSchedule(int incomeCentralVaultScheduleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCentralVaultScheduleRepository incomeCentralVaultScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCentralVaultScheduleRepository>();

                incomeCentralVaultScheduleRepository.Remove(incomeCentralVaultScheduleId);
            });
        }

        public IncomeCentralVaultSchedule GetIncomeCentralVaultSchedule(int incomeCentralVaultScheduleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCentralVaultScheduleRepository incomeCentralVaultScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCentralVaultScheduleRepository>();

                IncomeCentralVaultSchedule incomeCentralVaultScheduleEntity = incomeCentralVaultScheduleRepository.Get(incomeCentralVaultScheduleId);
                if (incomeCentralVaultScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeCentralVaultSchedule with ID of {0} is not in database", incomeCentralVaultScheduleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeCentralVaultScheduleEntity;
            });
        }

        public IncomeCentralVaultScheduleData[] GetAllIncomeCentralVaultSchedule()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCentralVaultScheduleRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCentralVaultScheduleRepository>();


                List<IncomeCentralVaultScheduleData> incomeCentralVaultSchedule = new List<IncomeCentralVaultScheduleData>();
                IEnumerable<IncomeCentralVaultScheduleInfo> incomeCentralVaultScheduleInfos = plCaptionRepository.GetIncomeCentralVaultSchedule().ToArray();

                foreach (var incomeCentralVaultScheduleInfo in incomeCentralVaultScheduleInfos)
                {
                    incomeCentralVaultSchedule.Add(
                        new IncomeCentralVaultScheduleData
                        {
                            IncomeCentralVaultScheduleId = incomeCentralVaultScheduleInfo.IncomeCentralVaultSchedule.EntityId,
                            BranchCode = incomeCentralVaultScheduleInfo.Branch.Code,
                            BranchName = incomeCentralVaultScheduleInfo.Branch.Name,
                            Currency = incomeCentralVaultScheduleInfo.IncomeCentralVaultSchedule.Currency,
                            Period = incomeCentralVaultScheduleInfo.IncomeCentralVaultSchedule.Period,
                            Year = incomeCentralVaultScheduleInfo.IncomeCentralVaultSchedule.Year,
                            Volume = incomeCentralVaultScheduleInfo.IncomeCentralVaultSchedule.Volume,
                            DatePosted = incomeCentralVaultScheduleInfo.IncomeCentralVaultSchedule.DatePosted,
                            Active = incomeCentralVaultScheduleInfo.IncomeCentralVaultSchedule.Active
                        });
                }

                return incomeCentralVaultSchedule.ToArray();
            });
        }


        #endregion

        #region MPRCommFee operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRCommFee UpdateMPRCommFee(MPRCommFee MPRCommFee)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRCommFeeRepository MPRCommFeeRepository = _DataRepositoryFactory.GetDataRepository<IMPRCommFeeRepository>();

                MPRCommFee updatedEntity = null;

                if (MPRCommFee.CommFee_Id == 0)
                    updatedEntity = MPRCommFeeRepository.Add(MPRCommFee);
                else
                    updatedEntity = MPRCommFeeRepository.Update(MPRCommFee);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMPRCommFee(int CommFee_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRCommFeeRepository MPRCommFeeRepository = _DataRepositoryFactory.GetDataRepository<IMPRCommFeeRepository>();

                MPRCommFeeRepository.Remove(CommFee_Id);
            });
        }

        public MPRCommFee GetMPRCommFee(int CommFee_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRCommFeeRepository MPRCommFeeRepository = _DataRepositoryFactory.GetDataRepository<IMPRCommFeeRepository>();

                MPRCommFee MPRCommFeeEntity = MPRCommFeeRepository.Get(CommFee_Id);
                if (MPRCommFeeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MPRCommFee with ID of {0} is not in database", CommFee_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return MPRCommFeeEntity;
            });
        }


        public MPRCommFee[] GetMPRCommFees()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var _MPRCommFeeRepository = new MPRCommFeeRepository();

                IEnumerable<MPRCommFee> MPRCommFees = _MPRCommFeeRepository.Get().Take(50);

                return MPRCommFees.ToArray();
            });
        }



        public MPRCommFee[] GetMPRCommFeesBySearch(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var _MPRCommFeeRepository = new MPRCommFeeRepository();

                IEnumerable<MPRCommFee> MPRCommFees = _MPRCommFeeRepository.GetMPRCommFeesBySearch(searchValue);
                return MPRCommFees.ToArray();
            });
        }



        //public MPRCommFee[] GetMPRCommFeesBy(string searchType, string searchValue)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        var _MPRCommFeeRepository = new MPRCommFeeRepository();

        //        if (searchType == "GLCode")
        //        {
        //            IEnumerable<MPRCommFee> MPRCommFees = _MPRCommFeeRepository.GetMPRCommFeeByGlcode(searchValue);
        //            return MPRCommFees.ToArray();
        //        }
        //        else if (searchType == "CustomerName")
        //        {
        //            IEnumerable<MPRCommFee> MPRCommFees = _MPRCommFeeRepository.GetMPRCommFeeByCustomername(searchValue);
        //            return MPRCommFees.ToArray();
        //        }
        //        else if (searchType == "RelatedAccount")
        //        {
        //            IEnumerable<MPRCommFee> MPRCommFees = _MPRCommFeeRepository.GetMPRCommFeeByRelatedaccount(searchValue);
        //            return MPRCommFees.ToArray();
        //        }

        //        return null;
        //    });
        //}


        
        #endregion

        #region Helper

        protected override bool AllowAccessToOperation(string solutionName, List<string> groupNames)
        {
            if (groupNames.Count == 0)
                return true;

            systemCoreData.IUserRoleRepository accountRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
            var accountRoles = accountRoleRepository.GetUserRoleInfo(solutionName, _LoginName, groupNames);

            if (accountRoles == null || accountRoles.Count() <= 0)
            {
                AuthorizationValidationException ex = new AuthorizationValidationException(string.Format("Access denied for {0}.", _LoginName));
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }

            return true;
        }

        public string GetDataConnection()
        {
            string connectionString = "";

            if (!string.IsNullOrEmpty(DataConnector.CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                if (companydb == null)
                    throw new Exception("Unable to load company database.");

                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.ServerName, companydb.DatabaseName, companydb.UserName, companydb.Password, companydb.IntegratedSecurity);
            }

            return connectionString;
        }


        #endregion

    }
}
