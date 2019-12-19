using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.IFRS.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Utils;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;
using System.Data.SqlClient;
using Fintrak.Presentation.WebClient.Models;
using System.Web.Script.Serialization;
using Fintrak.Shared.Common.Services.QueryService;
using Fintrak.Shared.Common.Services;
using System.IO;

namespace Fintrak.Business.IFRS.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class IFRS9Manager : ManagerBase, IIFRS9Service
    {
        public IFRS9Manager()
        {
        }

        public IFRS9Manager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_IFRS9";
        const string MODULE_ALIAS = "IFRS9";

     //  private string a;
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
                //IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();
             
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
                        //get the root for finstat
                        var root = menuRepository.Get().Where(c => c.Alias == "IFRS9").FirstOrDefault();
                       
                       var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "IRB_EXTERNAL_MAPPING",
                            Alias = "IRB To External Rating Mapping",
                            Action = "IRB_EXTERNAL_MAPPING",
                            ActionUrl = "ifrs9-ratingmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "INTERNAL_RATING_BASED",
                            Alias = "Internal Rating Based",
                            Action = "INTERNAL_RATING_BASED",
                            ActionUrl = "ifrs9-internalratingbased-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "EXTERNAL_AGENCY_RATING",
                            Alias = "External Agency Rating",
                            Action = "EXTERNAL_AGENCY_RATING",
                            ActionUrl = "ifrs9-externalrating-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "TRANSITION_MATRIX",
                            Alias = "Transition Matrix",
                            Action = "TRANSITION_MATRIX",
                            ActionUrl = "ifrs9-transition-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "MACROECONOMIC_VARIABLE",
                            Alias = "12Months Macroeconomic Variables",
                            Action = "MACROECONOMIC_VARIABLE",
                            ActionUrl = "ifrs9-macroeconomic-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "HISTORICAL_SECTORIAL_RATING",
                            Alias = "Historical Sectorial Rating",
                            Action = "HISTORICAL_SECTORIAL_RATING",
                            ActionUrl = "ifrs9-historicalsectorrating-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "NOTCH_DIFFERENCE",
                            Alias = "Notch Difference",
                            Action = "NOTCH_DIFFERENCE",
                            ActionUrl = "ifrs9-notchdifference-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "MACRO_ECONOMIC_HISTORICAL",
                            Alias = "Macro Economic Historical",
                            Action = "MACRO_ECONOMIC_HISTORICAL",
                            ActionUrl = "ifrs9-macroeconomichistorical-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "HISTORICAL_CLASSIFICATION",
                            Alias = "Historical Classification",
                            Action = "HISTORICAL_CLASSIFICATION",
                            ActionUrl = "ifrs9-historicalclassification-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "SET_UP",
                            Alias = "SetUp",
                            Action = "SET_UP",
                            ActionUrl = "ifrs9-setup-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "SECTOR",
                            Alias = "Sector",
                            Action = "SECTOR",
                            ActionUrl = "ifrs9-sector-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "ComputedForcastedPDLGD",
                            Alias = "Forcasted Computed PD/LGD",
                            Action = "ComputedForcastedPDLGD",
                            ActionUrl = "ifrs9-computedforcastedpdlgd-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "SectorialRegressedLGD",
                            Alias = "Sectorial Regressed LGD",
                            Action = "SectorialRegressedLGD",
                            ActionUrl = "ifrs9-sectorialregressedlgd-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "SectorialRegressedPD",
                            Alias = "Sectorial Regressed PD",
                            Action = "SectorialRegressedPD",
                            ActionUrl = "ifrs9-sectorialregressedpd-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "HISTORICAL_SECTORIAL_PD",
                            Alias = "Historical Sectorial PD",
                            Action = "HISTORICAL_SECTORIAL_PD",
                            ActionUrl = "ifrs9-historicalsectorialpd-list",
                            Image = null,
                            ImageUrl = "action_image",
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



        public UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var connectionString = GetDataConnection();

                UInt64 totalRecords = 0;

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("spp_Get_Count", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TableName",
                        Value = tableName,
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "ColumnName",
                        Value = columnName,
                    });
                    if (searchParamN == null)
                    {
                        cmd.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "SearchParam",
                            Value = searchParamS,
                        });
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "SearchParam",
                            Value = searchParamN,
                        });
                    }

                    con.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader["Count"] != DBNull.Value)
                            totalRecords = UInt64.Parse(reader["Count"].ToString());
                    }

                    con.Close();
                }
                return totalRecords;
            });
        }


        #region ExternalRating operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExternalRating UpdateExternalRating(ExternalRating externalRating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                ExternalRating updatedEntity = null;

                if (externalRating.ExternalRatingId == 0)
                    updatedEntity = externalRatingRepository.Add(externalRating);
                else
                    updatedEntity = externalRatingRepository.Update(externalRating);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExternalRating(int externalRatingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                externalRatingRepository.Remove(externalRatingId);
            });
        }

        public ExternalRating GetExternalRating(int externalRatingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                ExternalRating externalRatingEntity = externalRatingRepository.Get(externalRatingId);
                if (externalRatingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExternalRating with ID of {0} is not in database", externalRatingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return externalRatingEntity;
            });
        }

        public ExternalRating[] GetAllExternalRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                IEnumerable<ExternalRating> externalRatings = externalRatingRepository.Get().ToArray();

                return externalRatings.ToArray();
            });
        }

             #endregion

        #region HistoricalSectorRating operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalSectorRating UpdateHistoricalSectorRating(HistoricalSectorRating historicalSectorRating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                HistoricalSectorRating updatedEntity = null;

                if (historicalSectorRating.HistoricalSectorRatingId == 0)
                    updatedEntity = historicalSectorRatingRepository.Add(historicalSectorRating);
                else
                    updatedEntity = historicalSectorRatingRepository.Update(historicalSectorRating);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalSectorRating(int historicalSectorRatingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                historicalSectorRatingRepository.Remove(historicalSectorRatingId);
            });
        }

        public HistoricalSectorRating GetHistoricalSectorRating(int historicalSectorRatingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                HistoricalSectorRating historicalSectorRatingEntity = historicalSectorRatingRepository.Get(historicalSectorRatingId);
                if (historicalSectorRatingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalSectorRating with ID of {0} is not in database", historicalSectorRatingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalSectorRatingEntity;
            });
        }

        public HistoricalSectorRating[] GetAllHistoricalSectorRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                IEnumerable<HistoricalSectorRating> historicalSectorRatings = historicalSectorRatingRepository.Get().ToArray();

                return historicalSectorRatings.ToArray();
            });
        }

        #endregion

        #region InternalRatingBased operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public InternalRatingBased UpdateInternalRatingBased(InternalRatingBased internalRatingBased)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                InternalRatingBased updatedEntity = null;

                if (internalRatingBased.InternalRatingBasedId == 0)
                    updatedEntity = internalRatingBasedRepository.Add(internalRatingBased);
                else
                    updatedEntity = internalRatingBasedRepository.Update(internalRatingBased);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInternalRatingBased(int internalRatingBasedId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                internalRatingBasedRepository.Remove(internalRatingBasedId);
            });
        }

        public InternalRatingBased GetInternalRatingBased(int internalRatingBasedId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                InternalRatingBased internalRatingBasedEntity = internalRatingBasedRepository.Get(internalRatingBasedId);
                if (internalRatingBasedEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InternalRatingBased with ID of {0} is not in database", internalRatingBasedId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return internalRatingBasedEntity;
            });
        }

        public InternalRatingBased[] GetAllInternalRatingBaseds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                IEnumerable<InternalRatingBased> internalRatingBaseds = internalRatingBasedRepository.Get().ToArray().OrderBy(c => c.Rank).ToList();

                return internalRatingBaseds.ToArray();
            });
        }

        #endregion

        #region MacroEconomic operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomic UpdateMacroEconomic(MacroEconomic macroEconomic)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                MacroEconomic updatedEntity = null;

                if (macroEconomic.MacroEconomicId == 0)
                    updatedEntity = macroEconomicRepository.Add(macroEconomic);
                else
                    updatedEntity = macroEconomicRepository.Update(macroEconomic);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomic(int macroEconomicId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                macroEconomicRepository.Remove(macroEconomicId);
            });
        }

        public MacroEconomic GetMacroEconomic(int macroEconomicId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                MacroEconomic macroEconomicEntity = macroEconomicRepository.Get(macroEconomicId);
                if (macroEconomicEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomic with ID of {0} is not in database", macroEconomicId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicEntity;
            });
        }

        public MacroEconomic[] GetAllMacroEconomics()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                IEnumerable<MacroEconomic> macroEconomics = macroEconomicRepository.Get().ToArray();

                return macroEconomics.ToArray();
            });
        }

        #endregion

        #region RatingMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RatingMapping UpdateRatingMapping(RatingMapping ratingMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                RatingMapping updatedEntity = null;

                if (ratingMapping.RatingMappingId == 0)
                    updatedEntity = ratingMappingRepository.Add(ratingMapping);
                else
                    updatedEntity = ratingMappingRepository.Update(ratingMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRatingMapping(int ratingMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                ratingMappingRepository.Remove(ratingMappingId);
            });
        }

        public RatingMapping GetRatingMapping(int ratingMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                RatingMapping ratingMappingEntity = ratingMappingRepository.Get(ratingMappingId);
                if (ratingMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RatingMapping with ID of {0} is not in database", ratingMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ratingMappingEntity;
            });
        }

        public RatingMapping[] GetAllRatingMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                IEnumerable<RatingMapping> ratingMappings = ratingMappingRepository.Get().ToArray();

                return ratingMappings.ToArray();
            });
        }


        public RatingMappingData[] GetRatingMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();


                List<RatingMappingData> ratingMappings = new List<RatingMappingData>();
                IEnumerable<RatingMappingInfo> ratingMappingInfos = ratingMappingRepository.GetRatingMappings().ToArray();

                foreach (var ratingMappingInfo in ratingMappingInfos)
                {
                    ratingMappings.Add(
                        new RatingMappingData
                        {
                            RatingMappingId = ratingMappingInfo.RatingMapping.EntityId,
                            Credit_Risk_Id = ratingMappingInfo.InternalRatingBased.InternalRatingBasedId,
                            CreditRiskName = ratingMappingInfo.InternalRatingBased.Code,
                            External_Rating_Id = ratingMappingInfo.ExternalRating.ExternalRatingId,
                            ExternalRatingName = ratingMappingInfo.ExternalRating.Rating,
                            Active = ratingMappingInfo.RatingMapping.Active
                        });
                }

                return ratingMappings.ToArray();
            });
        }

        #endregion

        #region Transition operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Transition UpdateTransition(Transition transition)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                Transition updatedEntity = null;

                if (transition.TransitionId == 0)
                    updatedEntity = transitionRepository.Add(transition);
                else
                    updatedEntity = transitionRepository.Update(transition);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransition(int transitionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                transitionRepository.Remove(transitionId);
            });
        }

        public Transition GetTransition(int transitionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                Transition transitionEntity = transitionRepository.Get(transitionId);
                if (transitionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Transition with ID of {0} is not in database", transitionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return transitionEntity;
            });
        }

        public Transition[] GetAllTransitions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                IEnumerable<Transition> transitions = transitionRepository.Get().ToArray();

                return transitions.ToArray();
            });
        }

        #endregion
                
        #region HistoricalClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalClassification UpdateHistoricalClassification(HistoricalClassification historicalClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                HistoricalClassification updatedEntity = null;

                if (historicalClassification.HistoricalClassificationId == 0)
                    updatedEntity = historicalClassificationRepository.Add(historicalClassification);
                else
                    updatedEntity = historicalClassificationRepository.Update(historicalClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalClassification(int historicalClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                historicalClassificationRepository.Remove(historicalClassificationId);
            });
        }

        public HistoricalClassification GetHistoricalClassification(int historicalClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                HistoricalClassification historicalClassificationEntity = historicalClassificationRepository.Get(historicalClassificationId);
                if (historicalClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalClassification with ID of {0} is not in database", historicalClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalClassificationEntity;
            });
        }

        public HistoricalClassification[] GetAllHistoricalClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                IEnumerable<HistoricalClassification> historicalClassifications = historicalClassificationRepository.Get().ToArray();

                return historicalClassifications.ToArray();
            });
        }

        #endregion

        #region MacroEconomicHistorical operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomicHistorical UpdateMacroEconomicHistorical(MacroEconomicHistorical macroEconomicHistorical)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                MacroEconomicHistorical updatedEntity = null;

                if (macroEconomicHistorical.MacroEconomicHistoricalId == 0)
                    updatedEntity = macroEconomicHistoricalRepository.Add(macroEconomicHistorical);
                else
                    updatedEntity = macroEconomicHistoricalRepository.Update(macroEconomicHistorical);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                macroEconomicHistoricalRepository.Remove(macroEconomicHistoricalId);
            });
        }

        public MacroEconomicHistorical GetMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                MacroEconomicHistorical macroEconomicHistoricalEntity = macroEconomicHistoricalRepository.Get(macroEconomicHistoricalId);
                if (macroEconomicHistoricalEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomicHistorical with ID of {0} is not in database", macroEconomicHistoricalId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicHistoricalEntity;
            });
        }

        //public MacroEconomicHistorical[] GetAllMacroEconomicHistoricals()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

        //        IEnumerable<MacroEconomicHistorical> macroEconomicHistoricals = macroEconomicHistoricalRepository.Get().ToArray();

        //        return macroEconomicHistoricals.ToArray();
        //    });
        //}


        public MacroEconomicHistoricalData[] GetAllMacroEconomicHistoricals()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                List<MacroEconomicHistoricalData> macroEconomicHistoricals = new List<MacroEconomicHistoricalData>();
                IEnumerable<MacroEconomicHistoricalInfo> macroEconomicHistoricalInfos = macroEconomicHistoricalRepository.GetMacroEconomicHistoricals().ToArray();


                foreach (var macroEconomicHistoricalInfo in macroEconomicHistoricalInfos)
                {
                    string vtype = "";
                    if (macroEconomicHistoricalInfo.MacroEconomicHistorical.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    macroEconomicHistoricals.Add(
                        new MacroEconomicHistoricalData
                        {
                            MacroEconomicHistoricalId = macroEconomicHistoricalInfo.MacroEconomicHistorical.EntityId,
                            Year = macroEconomicHistoricalInfo.MacroEconomicHistorical.Year,
                            Sector = macroEconomicHistoricalInfo.MacroEconomicHistorical.Sector_Code,
                            SectorName = macroEconomicHistoricalInfo.Sector.Description,
                            Type = macroEconomicHistoricalInfo.MacroEconomicHistorical.Type,
                            TypeName = vtype,
                            Variable = macroEconomicHistoricalInfo.MacroEconomicHistorical.Variable,
                            VariableName = macroEconomicHistoricalInfo.MacroEconomicVariable.Description,
                            Value = macroEconomicHistoricalInfo.MacroEconomicHistorical.Value,
                            Active = macroEconomicHistoricalInfo.MacroEconomicHistorical.Active
                        });
                }

                return macroEconomicHistoricals.ToArray();
            });
        }

        #endregion

        #region NotchDifference operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public NotchDifference UpdateNotchDifference(NotchDifference notchDifference)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                NotchDifference updatedEntity = null;

                if (notchDifference.NotchDifferenceId == 0)
                    updatedEntity = notchDifferenceRepository.Add(notchDifference);
                else
                    updatedEntity = notchDifferenceRepository.Update(notchDifference);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteNotchDifference(int notchDifferenceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                notchDifferenceRepository.Remove(notchDifferenceId);
            });
        }

        public NotchDifference GetNotchDifference(int notchDifferenceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                NotchDifference notchDifferenceEntity = notchDifferenceRepository.Get(notchDifferenceId);
                if (notchDifferenceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("NotchDifference with ID of {0} is not in database", notchDifferenceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return notchDifferenceEntity;
            });
        }

        public NotchDifference[] GetAllNotchDifferences()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                IEnumerable<NotchDifference> notchDifferences = notchDifferenceRepository.Get().ToArray();

                return notchDifferences.ToArray();
            });
        }

        #endregion

        #region SetUp operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SetUp UpdateSetUp(SetUp setUp)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp updatedEntity = null;

                if (setUp.SetUpId == 0)
                    updatedEntity = setUpRepository.Add(setUp);
                else
                    updatedEntity = setUpRepository.Update(setUp);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSetUp(int setUpId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                setUpRepository.Remove(setUpId);
            });
        }

        public SetUp GetSetUp(int setUpId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp setUpEntity = setUpRepository.Get(setUpId);
                if (setUpEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SetUp with ID of {0} is not in database", setUpId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return setUpEntity;
            });
        }

        public SetUp[] GetAllSetUps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                IEnumerable<SetUp> setUps = setUpRepository.Get().ToArray();

                return setUps.ToArray();
            });
        }

        #endregion

        #region HistoricalSectorialPD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalSectorialPD UpdateHistoricalSectorialPD(HistoricalSectorialPD historicalSectorialPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                HistoricalSectorialPD updatedEntity = null;

                if (historicalSectorialPD.HistoricalSectorialPDId == 0)
                    updatedEntity = historicalSectorialPDRepository.Add(historicalSectorialPD);
                else
                    updatedEntity = historicalSectorialPDRepository.Update(historicalSectorialPD);

                return updatedEntity;
                            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalSectorialPD(int historicalSectorialPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                historicalSectorialPDRepository.Remove(historicalSectorialPDId);
            });
        }

        public HistoricalSectorialPD GetHistoricalSectorialPD(int historicalSectorialPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                HistoricalSectorialPD historicalSectorialPDEntity = historicalSectorialPDRepository.Get(historicalSectorialPDId);
                if (historicalSectorialPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalSectorialPD with ID of {0} is not in database", historicalSectorialPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalSectorialPDEntity;
            });
        }

        public HistoricalSectorialPD[] GetAllHistoricalSectorialPDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                IEnumerable<HistoricalSectorialPD> historicalSectorialPDs = historicalSectorialPDRepository.Get().ToArray();

                return historicalSectorialPDs.ToArray();
            });
        }

        public string[] GetDistinctYear()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            //List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_year", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var years = new ReferenceNoModel();
                        if (reader["Year"] != DBNull.Value)
                            years.RefNo = reader["Year"].ToString();
                        yearList.Add(years.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }

        public string[] GetDistinctPeriod()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_period", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var periods = new ReferenceNoModel();
                        if (reader["Period"] != DBNull.Value)
                            periods.RefNo = reader["Period"].ToString();
                        yearList.Add(periods.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }


        public void ComputeHistoricalSectorialPD(int computationtype, int curYear, int curPeriod, int prevYear = 0, int prevPeriod = 0)
        {

            var connectionString = GetDataConnection();

            int status = 0;
            string storProc = "";
            if (computationtype == 1)
            {
                storProc = "ifrs_historical_pd_computation";
            }

            else
            {
                storProc = "ifrs_historical_lgd_computation";
            }

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(storProc, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurYear",
                    Value = curYear,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurPeriod",
                    Value = curPeriod,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevYear",
                    Value = prevYear,                
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevPeriod",
                    Value = prevPeriod,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
       
        #endregion

        #region HistoricalSectorialLGD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalSectorialLGD UpdateHistoricalSectorialLGD(HistoricalSectorialLGD historicalSectorialPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                HistoricalSectorialLGD updatedEntity = null;

                if (historicalSectorialPD.HistoricalSectorialLGDId == 0)
                    updatedEntity = historicalSectorialPDRepository.Add(historicalSectorialPD);
                else
                    updatedEntity = historicalSectorialPDRepository.Update(historicalSectorialPD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalSectorialLGD(int historicalSectorialPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                historicalSectorialPDRepository.Remove(historicalSectorialPDId);
            });
        }

        public HistoricalSectorialLGD GetHistoricalSectorialLGD(int historicalSectorialPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                HistoricalSectorialLGD historicalSectorialPDEntity = historicalSectorialPDRepository.Get(historicalSectorialPDId);
                if (historicalSectorialPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalSectorialLGD with ID of {0} is not in database", historicalSectorialPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalSectorialPDEntity;
            });
        }

        public HistoricalSectorialLGD[] GetAllHistoricalSectorialLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                IEnumerable<HistoricalSectorialLGD> historicalSectorialPDs = historicalSectorialPDRepository.Get().ToArray();

                return historicalSectorialPDs.ToArray();
            });
        }

        public string[] GetDistinctLYear()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_year", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var years = new ReferenceNoModel();
                        if (reader["Year"] != DBNull.Value)
                            years.RefNo = reader["Year"].ToString();
                        yearList.Add(years.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }

        public string[] GetDistinctLPeriod()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_period", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var periods = new ReferenceNoModel();
                        if (reader["Period"] != DBNull.Value)
                            periods.RefNo = reader["Period"].ToString();
                        yearList.Add(periods.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }


        public void ComputeHistoricalSectorialLGD(int computationtype, int curYear, int curPeriod, int prevYear = 0, int prevPeriod = 0)
        {

            var connectionString = GetDataConnection();

            int status = 0;
            string storProc = "";
            if (computationtype == 1)
            {
                storProc = "ifrs_historical_pd_computation";
            }

            else
            {
                storProc = "ifrs_historical_lgd_computation";
            }

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(storProc, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurYear",
                    Value = curYear,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurPeriod",
                    Value = curPeriod,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevYear",
                    Value = prevYear,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevPeriod",
                    Value = prevPeriod,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        //#region ComputedForcastedPDLGD operations

        //[OperationBehavior(TransactionScopeRequired = true)]
        //public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        ComputedForcastedPDLGD updatedEntity = null;

        //        if (computedForcastedPDLGD.ComputedPDId == 0)
        //            updatedEntity = computedForcastedPDLGDRepository.Add(computedForcastedPDLGD);
        //        else
        //            updatedEntity = computedForcastedPDLGDRepository.Update(computedForcastedPDLGD);

        //        return updatedEntity;
        //    });
        //}

        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void DeleteComputedForcastedPDLGD(int computedForcastedPDLGDId)
        //{
        //    ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        computedForcastedPDLGDRepository.Remove(computedForcastedPDLGDId);
        //    });
        //}

        //public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedForcastedPDLGDId)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        ComputedForcastedPDLGD computedForcastedPDLGDEntity = computedForcastedPDLGDRepository.Get(computedForcastedPDLGDId);
        //        if (computedForcastedPDLGDEntity == null)
        //        {
        //            NotFoundException ex = new NotFoundException(string.Format("ComputedForcastedPDLGD with ID of {0} is not in database", computedForcastedPDLGDId));
        //            throw new FaultException<NotFoundException>(ex, ex.Message);
        //        }

        //        return computedForcastedPDLGDEntity;
        //    });
        //}

        //public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        List<ComputedForcastedPDLGD> computedForcastedPDLGDs = computedForcastedPDLGDRepository.GetComputedForcastedPDLGD();

        //        return computedForcastedPDLGDs.ToArray();
        //    });
        //}

        //#endregion

        #region SectorialRegressedPD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorialRegressedPD UpdateSectorialRegressedPD(SectorialRegressedPD sectorialRegressedPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                SectorialRegressedPD updatedEntity = null;

                if (sectorialRegressedPD.SectorialRegressedPDId == 0)
                    updatedEntity = sectorialRegressedPDRepository.Add(sectorialRegressedPD);
                else
                    updatedEntity = sectorialRegressedPDRepository.Update(sectorialRegressedPD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorialRegressedPD(int sectorialRegressedPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                sectorialRegressedPDRepository.Remove(sectorialRegressedPDId);
            });
        }

        public SectorialRegressedPD GetSectorialRegressedPD(int sectorialRegressedPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                SectorialRegressedPD sectorialRegressedPDEntity = sectorialRegressedPDRepository.Get(sectorialRegressedPDId);
                if (sectorialRegressedPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorialRegressedPD with ID of {0} is not in database", sectorialRegressedPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialRegressedPDEntity;
            });
        }

        public SectorialRegressedPD[] GetAllSectorialRegressedPDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                IEnumerable<SectorialRegressedPD> sectorialRegressedPDs = sectorialRegressedPDRepository.Get().ToArray();

                return sectorialRegressedPDs.ToArray();
            });
        }

        #endregion

        #region SectorialRegressedLGD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorialRegressedLGD UpdateSectorialRegressedLGD(SectorialRegressedLGD sectorialRegressedLGD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                SectorialRegressedLGD updatedEntity = null;

                if (sectorialRegressedLGD.SectorialRegressedLGDId == 0)
                    updatedEntity = sectorialRegressedLGDRepository.Add(sectorialRegressedLGD);
                else
                    updatedEntity = sectorialRegressedLGDRepository.Update(sectorialRegressedLGD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                sectorialRegressedLGDRepository.Remove(sectorialRegressedLGDId);
            });
        }

        public SectorialRegressedLGD GetSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                SectorialRegressedLGD sectorialRegressedLGDEntity = sectorialRegressedLGDRepository.Get(sectorialRegressedLGDId);
                if (sectorialRegressedLGDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorialRegressedLGD with ID of {0} is not in database", sectorialRegressedLGDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialRegressedLGDEntity;
            });
        }

        public SectorialRegressedLGD[] GetAllSectorialRegressedLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                IEnumerable<SectorialRegressedLGD> sectorialRegressedLGDs = sectorialRegressedLGDRepository.Get().ToArray();

                return sectorialRegressedLGDs.ToArray();
            });
        }

        #endregion

        #region MacroEconomicVariable operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomicVariable UpdateMacroEconomicVariable(MacroEconomicVariable macroEconomicVariable)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                MacroEconomicVariable updatedEntity = null;

                if (macroEconomicVariable.MacroEconomicVariableId == 0)
                    updatedEntity = macroEconomicVariableRepository.Add(macroEconomicVariable);
                else
                    updatedEntity = macroEconomicVariableRepository.Update(macroEconomicVariable);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomicVariable(int macroEconomicVariableId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                macroEconomicVariableRepository.Remove(macroEconomicVariableId);
            });
        }

        public MacroEconomicVariable GetMacroEconomicVariable(int macroEconomicVariableId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                MacroEconomicVariable macroEconomicVariableEntity = macroEconomicVariableRepository.Get(macroEconomicVariableId);
                if (macroEconomicVariableEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomicVariable with ID of {0} is not in database", macroEconomicVariableId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicVariableEntity;
            });
        }

        public MacroEconomicVariable[] GetAllMacroEconomicVariables()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                IEnumerable<MacroEconomicVariable> macroEconomicVariables = macroEconomicVariableRepository.Get().ToArray();

                return macroEconomicVariables.ToArray();
            });
        }

        #endregion

        #region SectorVariableMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorVariableMapping UpdateSectorVariableMapping(SectorVariableMapping sectorVariableMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                SectorVariableMapping updatedEntity = null;

                if (sectorVariableMapping.SectorVariableMappingId == 0)
                    updatedEntity = sectorVariableMappingRepository.Add(sectorVariableMapping);
                else
                    updatedEntity = sectorVariableMappingRepository.Update(sectorVariableMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorVariableMapping(int sectorVariableMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                sectorVariableMappingRepository.Remove(sectorVariableMappingId);
            });
        }

        public SectorVariableMapping GetSectorVariableMapping(int sectorVariableMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                SectorVariableMapping sectorVariableMappingEntity = sectorVariableMappingRepository.Get(sectorVariableMappingId);
                if (sectorVariableMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorVariableMapping with ID of {0} is not in database", sectorVariableMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorVariableMappingEntity;
            });
        }

        public SectorVariableMappingData[] GetAllSectorVariableMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                List<SectorVariableMappingData> sectorVariableMappings = new List<SectorVariableMappingData>();
                IEnumerable<SectorVariableMappingInfo> sectorVariableMappingInfos = sectorVariableMappingRepository.GetSectorVariableMappings().ToArray();
               

                foreach (var sectorVariableMappingInfo in sectorVariableMappingInfos)
                {
                     string vtype = "";
                    if (sectorVariableMappingInfo.SectorVariableMapping.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    sectorVariableMappings.Add(
                        new SectorVariableMappingData
                        {
                            SectorVariableMappingId = sectorVariableMappingInfo.SectorVariableMapping.EntityId,
                            Year = sectorVariableMappingInfo.SectorVariableMapping.Year,
                            Sector = sectorVariableMappingInfo.SectorVariableMapping.Sector,
                            SectorName = sectorVariableMappingInfo.Sector.Description,
                            Type = sectorVariableMappingInfo.SectorVariableMapping.Type,                                          
                            TypeName = vtype,
                            Variable = sectorVariableMappingInfo.SectorVariableMapping.Variable,
                            VariableName = sectorVariableMappingInfo.MacroEconomicVariable.Description,
                            Value = sectorVariableMappingInfo.SectorVariableMapping.Value,
                            Active = sectorVariableMappingInfo.SectorVariableMapping.Active
                        });
                }

                return sectorVariableMappings.ToArray();
            });
        }

        #endregion

        #region PitFormular operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PitFormular UpdatePitFormular(PitFormular pitFormular)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                PitFormular updatedEntity = null;

                if (pitFormular.PitFormularId == 0)
                    updatedEntity = pitFormularRepository.Add(pitFormular);
                else
                    updatedEntity = pitFormularRepository.Update(pitFormular);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePitFormular(int pitFormularId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                pitFormularRepository.Remove(pitFormularId);
            });
        }

        public PitFormular GetPitFormular(int pitFormularId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                PitFormular pitFormularEntity = pitFormularRepository.Get(pitFormularId);
                if (pitFormularEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PitFormular with ID of {0} is not in database", pitFormularId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return pitFormularEntity;
            });
        }

        public PitFormular[] GetAllPitFormulars()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                List<PitFormular> pitFormulars = pitFormularRepository.GetPitFormular();

                return pitFormulars.ToArray();
            });
        }

        #endregion

        #region PortfolioExposure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PortfolioExposure UpdatePortfolioExposure(PortfolioExposure portfolioExposure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                PortfolioExposure updatedEntity = null;

                if (portfolioExposure.PortfolioId == 0)
                    updatedEntity = portfolioExposureRepository.Add(portfolioExposure);
                else
                    updatedEntity = portfolioExposureRepository.Update(portfolioExposure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePortfolioExposure(int portfolioExposureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                portfolioExposureRepository.Remove(portfolioExposureId);
            });
        }

        public PortfolioExposure GetPortfolioExposure(int portfolioExposureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                PortfolioExposure portfolioExposureEntity = portfolioExposureRepository.Get(portfolioExposureId);
                if (portfolioExposureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PortfolioExposure with ID of {0} is not in database", portfolioExposureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return portfolioExposureEntity;
            });
        }

        public PortfolioExposure[] GetAllPortfolioExposures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                IEnumerable<PortfolioExposure> portfolioExposures = portfolioExposureRepository.Get().ToArray();

              //  ShowFusionChart(); 
                return portfolioExposures.ToArray();
            });
        }

        public string GetAllPortfolioExposuresChart()
        {
            DataTable dt = new DataTable();
            dt = LoadGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Name")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Exposure")
                        row.Add("value", dr[col]);
                }
                rows.Add(row);

            }
            return serializer.Serialize(rows);
        }

        public DataTable LoadGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_portfolio_exposure";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
  
        #endregion

        #region SectorialExposure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorialExposure UpdateSectorialExposure(SectorialExposure sectorialExposure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                SectorialExposure updatedEntity = null;

                if (sectorialExposure.SectorialExposureId == 0)
                    updatedEntity = sectorialExposureRepository.Add(sectorialExposure);
                else
                    updatedEntity = sectorialExposureRepository.Update(sectorialExposure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorialExposure(int sectorialExposureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                sectorialExposureRepository.Remove(sectorialExposureId);
            });
        }

        public SectorialExposure GetSectorialExposure(int sectorialExposureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                SectorialExposure sectorialExposureEntity = sectorialExposureRepository.Get(sectorialExposureId);
                if (sectorialExposureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorialExposure with ID of {0} is not in database", sectorialExposureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialExposureEntity;
            });
        }

        public SectorialExposure[] GetAllSectorialExposures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                IEnumerable<SectorialExposure> sectorialExposures = sectorialExposureRepository.Get().ToArray();

                return sectorialExposures.ToArray();
            });
        }

        public string GetAllSectorialExposuresChart()
        {
            DataTable dt = new DataTable();
            dt = LoadSectorialGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Name")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Exposure")
                        row.Add("value", dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }


        public DataTable LoadSectorialGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_sectorial_exposure";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
        #endregion

        #region PiTPD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PiTPD UpdatePiTPD(PiTPD piTPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                PiTPD updatedEntity = null;

                if (piTPD.PiTPDId == 0)
                    updatedEntity = piTPDRepository.Add(piTPD);
                else
                    updatedEntity = piTPDRepository.Update(piTPD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePiTPD(int piTPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                piTPDRepository.Remove(piTPDId);
            });
        }

        public PiTPD GetPiTPD(int piTPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                PiTPD piTPDEntity = piTPDRepository.Get(piTPDId);
                if (piTPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PiTPD with ID of {0} is not in database", piTPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return piTPDEntity;
            });
        }

        public PiTPD[] GetAllPiTPDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                IEnumerable<PiTPD> piTPDs = piTPDRepository.Get().ToArray();

                return piTPDs.ToArray();
            });
        }


        public void RegressPD()
        {

            var connectionString = GetDataConnection();

           // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_regression_Analysis", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
        #endregion

        #region EclCalculationModel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public EclCalculationModel UpdateEclCalculationModel(EclCalculationModel eclCalculationModel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                EclCalculationModel updatedEntity = null;

                if (eclCalculationModel.EclModelId == 0)
                    updatedEntity = externalRatingRepository.Add(eclCalculationModel);
                else
                    updatedEntity = externalRatingRepository.Update(eclCalculationModel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteEclCalculationModel(int eclCalculationModelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository eclCalculationModelRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                eclCalculationModelRepository.Remove(eclCalculationModelId);
            });
        }

        public EclCalculationModel GetEclCalculationModel(int eclCalculationModelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository eclCalculationModelRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                EclCalculationModel eclCalculationModelEntity = eclCalculationModelRepository.Get(eclCalculationModelId);
                if (eclCalculationModelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("EclCalculationModel with ID of {0} is not in database", eclCalculationModelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return eclCalculationModelEntity;
            });
        }

        public EclCalculationModel[] GetAllEclCalculationModels()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository eclCalculationModelRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                IEnumerable<EclCalculationModel> eclCalculationModels = eclCalculationModelRepository.Get().ToArray();

                return eclCalculationModels.ToArray();
            });
        }

        #endregion

        #region LoanBucketDistribution operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanBucketDistribution UpdateLoanBucketDistribution(LoanBucketDistribution loanSpreadScenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                LoanBucketDistribution updatedEntity = null;

                if (loanSpreadScenario.LoanBucketDistributionId == 0)
                    updatedEntity = loanSpreadScenarioRepository.Add(loanSpreadScenario);
                else
                    updatedEntity = loanSpreadScenarioRepository.Update(loanSpreadScenario);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanBucketDistribution(int loanSpreadScenarioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                loanSpreadScenarioRepository.Remove(loanSpreadScenarioId);
            });
        }

        public LoanBucketDistribution GetLoanBucketDistribution(int loanSpreadScenarioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                LoanBucketDistribution loanSpreadScenarioEntity = loanSpreadScenarioRepository.Get(loanSpreadScenarioId);
                if (loanSpreadScenarioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanBucketDistribution with ID of {0} is not in database", loanSpreadScenarioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanSpreadScenarioEntity;
            });
        }

        public LoanBucketDistribution[] GetAllLoanBucketDistributions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                IEnumerable<LoanBucketDistribution> loanSpreadScenarios = loanSpreadScenarioRepository.Get().ToArray();

                return loanSpreadScenarios.ToArray();
            });
        }

        public void PDDistribution()
        {

            var connectionString = GetDataConnection();

            // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_loan_classification_spread_pd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public void PastDueDayDistribution()
        {

            var connectionString = GetDataConnection();

            // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_loan_classification_spread_pastduedays", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public LoanBucketDistribution[] GetLoanAssessments(string bucket)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                IEnumerable<LoanBucketDistribution> loanSpreadScenarios = loanSpreadScenarioRepository.GetLoanAssessments(bucket).ToArray();

                return loanSpreadScenarios.ToArray();
            });
        }


        #endregion

        #region MacroeconomicVDisplay operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroeconomicVDisplay UpdateMacroeconomicVDisplay(MacroeconomicVDisplay macroeconomicVDisplay)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                MacroeconomicVDisplay updatedEntity = null;

                if (macroeconomicVDisplay.MacroVariableDisplayId == 0)
                    updatedEntity = macroeconomicVDisplayRepository.Add(macroeconomicVDisplay);
                else
                    updatedEntity = macroeconomicVDisplayRepository.Update(macroeconomicVDisplay);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                macroeconomicVDisplayRepository.Remove(macroeconomicVDisplayId);
            });
        }

        public MacroeconomicVDisplay GetMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                MacroeconomicVDisplay macroeconomicVDisplayEntity = macroeconomicVDisplayRepository.Get(macroeconomicVDisplayId);
                if (macroeconomicVDisplayEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroeconomicVDisplay with ID of {0} is not in database", macroeconomicVDisplayId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroeconomicVDisplayEntity;
            });
        }

        public MacroeconomicVDisplay[] GetAllMacroeconomicVDisplays()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                IEnumerable<MacroeconomicVDisplay> macroeconomicVDisplays = macroeconomicVDisplayRepository.Get().ToArray();

                return macroeconomicVDisplays.ToArray();
            });
        }

        public string[] GetDistinctFHYear(string vType)
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_FHyear", con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "VType",
                    Value = vType,
                });
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var years = new ReferenceNoModel();
                        if (reader["Year"] != DBNull.Value)
                            years.RefNo = reader["Year"].ToString();
                        yearList.Add(years.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }

        public MacroeconomicVDisplay[] GetMacroeconomicVDisplayByYear(int yr)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                IEnumerable<MacroeconomicVDisplay> macroeconomicVDisplays = macroeconomicVDisplayRepository.GetMacroeconomicVDisplayByYear(yr).ToList();

                return macroeconomicVDisplays.ToArray();
            });
        }
        #endregion

        #region LifeTimePDClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LifeTimePDClassification UpdateLifeTimePDClassification(LifeTimePDClassification lifeTimePDClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                LifeTimePDClassification updatedEntity = null;

                if (lifeTimePDClassification.LifeTimePDClassificationId == 0)
                    updatedEntity = lifeTimePDClassificationRepository.Add(lifeTimePDClassification);
                else
                    updatedEntity = lifeTimePDClassificationRepository.Update(lifeTimePDClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                lifeTimePDClassificationRepository.Remove(lifeTimePDClassificationId);
            });
        }

        public LifeTimePDClassification GetLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                LifeTimePDClassification lifeTimePDClassificationEntity = lifeTimePDClassificationRepository.Get(lifeTimePDClassificationId);
                if (lifeTimePDClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LifeTimePDClassification with ID of {0} is not in database", lifeTimePDClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lifeTimePDClassificationEntity;
            });
        }

        public LifeTimePDClassification[] GetAllLifeTimePDClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                PopulateAssesmentTable();

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                IEnumerable<LifeTimePDClassification> lifeTimePDClassifications = lifeTimePDClassificationRepository.Get().ToArray();

                return lifeTimePDClassifications.ToArray();
            });
        }


        public void PopulateAssesmentTable()
        {

            var connectionString = GetDataConnection();

            // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_loan_assessment", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
        #endregion

        #region SummaryReport operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SummaryReport UpdateSummaryReport(SummaryReport summaryReport)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                SummaryReport updatedEntity = null;

                if (summaryReport.SummaryReportId == 0)
                    updatedEntity = summaryReportRepository.Add(summaryReport);
                else
                    updatedEntity = summaryReportRepository.Update(summaryReport);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSummaryReport(int summaryReportId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                summaryReportRepository.Remove(summaryReportId);
            });
        }

        public SummaryReport GetSummaryReport(int summaryReportId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                SummaryReport summaryReportEntity = summaryReportRepository.Get(summaryReportId);
                if (summaryReportEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SummaryReport with ID of {0} is not in database", summaryReportId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return summaryReportEntity;
            });
        }

        public SummaryReport[] GetAllSummaryReports()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                IEnumerable<SummaryReport> summaryReports = summaryReportRepository.Get().ToArray();

                //  ShowFusionChart(); 
                return summaryReports.ToArray();
            });
        }

        public string GetAllSummaryReportsChart()
        {
            DataTable dt = new DataTable();
            dt = LoadSummaryGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Bucket")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Individual")
                        row.Add("value", dr[col]);
                    if (col.ColumnName == "Collective")
                     row.Add("value2", dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public DataTable LoadSummaryGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_summary_report";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }

        #endregion

        #region FairValuationModel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FairValuationModel UpdateFairValuationModel(FairValuationModel fairValuationModel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                FairValuationModel updatedEntity = null;

                if (fairValuationModel.FairValueModelId == 0)
                    updatedEntity = externalRatingRepository.Add(fairValuationModel);
                else
                    updatedEntity = externalRatingRepository.Update(fairValuationModel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFairValuationModel(int fairValuationModelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository fairValuationModelRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                fairValuationModelRepository.Remove(fairValuationModelId);
            });
        }

        public FairValuationModel GetFairValuationModel(int fairValuationModelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository fairValuationModelRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                FairValuationModel fairValuationModelEntity = fairValuationModelRepository.Get(fairValuationModelId);
                if (fairValuationModelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FairValuationModel with ID of {0} is not in database", fairValuationModelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return fairValuationModelEntity;
            });
        }

        public FairValuationModel[] GetAllFairValuationModels()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository fairValuationModelRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                IEnumerable<FairValuationModel> fairValuationModels = fairValuationModelRepository.Get().ToArray();

                return fairValuationModels.ToArray();
            });
        }

        #endregion

        #region IfrsEquityUnqouted operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsEquityUnqouted UpdateIfrsEquityUnqouted(IfrsEquityUnqouted ifrsEquityUnqouted)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                IfrsEquityUnqouted updatedEntity = null;

                if (ifrsEquityUnqouted.IfrsEquityUnqoutedId == 0)
                    updatedEntity = ifrsEquityUnqoutedRepository.Add(ifrsEquityUnqouted);
                else
                    updatedEntity = ifrsEquityUnqoutedRepository.Update(ifrsEquityUnqouted);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                ifrsEquityUnqoutedRepository.Remove(ifrsEquityUnqoutedId);
            });
        }

        public IfrsEquityUnqouted GetIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                IfrsEquityUnqouted ifrsEquityUnqoutedEntity = ifrsEquityUnqoutedRepository.Get(ifrsEquityUnqoutedId);
                if (ifrsEquityUnqoutedEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsEquityUnqouted with ID of {0} is not in database", ifrsEquityUnqoutedId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsEquityUnqoutedEntity;
            });
        }

        public IfrsEquityUnqouted[] GetAllIfrsEquityUnqouteds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                IEnumerable<IfrsEquityUnqouted> ifrsEquityUnqouteds = ifrsEquityUnqoutedRepository.Get().ToArray();

                return ifrsEquityUnqouteds.ToArray();
            });
        }

        #endregion

        #region IfrsStocksPrimaryData operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsStocksPrimaryData UpdateIfrsStocksPrimaryData(IfrsStocksPrimaryData ifrsStocksPrimaryData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                IfrsStocksPrimaryData updatedEntity = null;

                if (ifrsStocksPrimaryData.IfrsStocksPrimaryDataId == 0)
                    updatedEntity = ifrsStocksPrimaryDataRepository.Add(ifrsStocksPrimaryData);
                else
                    updatedEntity = ifrsStocksPrimaryDataRepository.Update(ifrsStocksPrimaryData);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                ifrsStocksPrimaryDataRepository.Remove(ifrsStocksPrimaryDataId);
            });
        }

        public IfrsStocksPrimaryData GetIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                IfrsStocksPrimaryData ifrsStocksPrimaryDataEntity = ifrsStocksPrimaryDataRepository.Get(ifrsStocksPrimaryDataId);
                if (ifrsStocksPrimaryDataEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsStocksPrimaryData with ID of {0} is not in database", ifrsStocksPrimaryDataId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsStocksPrimaryDataEntity;
            });
        }

        public IfrsStocksPrimaryData[] GetAllIfrsStocksPrimaryDatas()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                IEnumerable<IfrsStocksPrimaryData> ifrsStocksPrimaryDatas = ifrsStocksPrimaryDataRepository.Get().ToArray();

                return ifrsStocksPrimaryDatas.ToArray();
            });
        }

        #endregion

        #region IfrsStocksMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsStocksMapping UpdateIfrsStocksMapping(IfrsStocksMapping ifrsStocksMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                IfrsStocksMapping updatedEntity = null;

                if (ifrsStocksMapping.IfrsStocksMappingId == 0)
                    updatedEntity = ifrsStocksMappingRepository.Add(ifrsStocksMapping);
                else
                    updatedEntity = ifrsStocksMappingRepository.Update(ifrsStocksMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsStocksMapping(int ifrsStocksMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                ifrsStocksMappingRepository.Remove(ifrsStocksMappingId);
            });
        }

        public IfrsStocksMapping GetIfrsStocksMapping(int ifrsStocksMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                IfrsStocksMapping ifrsStocksMappingEntity = ifrsStocksMappingRepository.Get(ifrsStocksMappingId);
                if (ifrsStocksMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsStocksMapping with ID of {0} is not in database", ifrsStocksMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsStocksMappingEntity;
            });
        }

        public IfrsStocksMappingData[] GetAllIfrsStocksMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                List<IfrsStocksMappingData> ifrsstocksMappings = new List<IfrsStocksMappingData>();
                IEnumerable<IfrsStocksMappingInfo> ifrsstocksMappingInfos = ifrsStocksMappingRepository.GetIfrsStocksMappings().ToArray();

                foreach (var ifrsstocksMappingInfo in ifrsstocksMappingInfos)
                {
                    ifrsstocksMappings.Add(
                        new IfrsStocksMappingData
                        {
                            IfrsStocksMappingId = ifrsstocksMappingInfo.IfrsEquityUnqouted.EntityId,
                            Qouted_stock_code = ifrsstocksMappingInfo.IfrsStocksPrimaryData.Stock_code,
                            Qouted_stock_Name = ifrsstocksMappingInfo.IfrsStocksPrimaryData.Company_name,
                            Unqouted_stock_code = ifrsstocksMappingInfo.IfrsEquityUnqouted.Stock_code,
                            Unqouted_stock_Name = ifrsstocksMappingInfo.IfrsEquityUnqouted.Stock_description,
                            Active = ifrsstocksMappingInfo.IfrsStocksMapping.Active
                        });
                }


                return ifrsstocksMappings.ToArray();
            });
        }


        #endregion

        #region Reconciliation operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Reconciliation UpdateReconciliation(Reconciliation reconciliation)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                Reconciliation updatedEntity = null;

                if (reconciliation.ReconciliationId == 0)
                    updatedEntity = reconciliationRepository.Add(reconciliation);
                else
                    updatedEntity = reconciliationRepository.Update(reconciliation);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteReconciliation(int reconciliationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                reconciliationRepository.Remove(reconciliationId);
            });
        }

        public Reconciliation GetReconciliation(int reconciliationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                Reconciliation reconciliationEntity = reconciliationRepository.Get(reconciliationId);
                if (reconciliationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Reconciliation with ID of {0} is not in database", reconciliationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return reconciliationEntity;
            });
        }

        public Reconciliation[] GetAllReconciliations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                IEnumerable<Reconciliation> reconciliations = reconciliationRepository.Get().OrderBy(c => c.ReconciliationId).ToArray();

                return reconciliations.ToArray();
            });
        }

        #endregion

        #region ForecastedMacroeconimcsSensitivity operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ForecastedMacroeconimcsSensitivity UpdateForecastedMacroeconimcsSensitivity(ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                ForecastedMacroeconimcsSensitivity updatedEntity = null;

                if (forecastedMacroeconimcsSensitivity.ForecastedMacroeconimcsSensitivityId == 0)
                    updatedEntity = forecastedMacroeconimcsSensitivityRepository.Add(forecastedMacroeconimcsSensitivity);
                else
                    updatedEntity = forecastedMacroeconimcsSensitivityRepository.Update(forecastedMacroeconimcsSensitivity);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                forecastedMacroeconimcsSensitivityRepository.Remove(forecastedMacroeconimcsSensitivityId);
            });
        }

        public ForecastedMacroeconimcsSensitivity GetForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivityEntity = forecastedMacroeconimcsSensitivityRepository.Get(forecastedMacroeconimcsSensitivityId);
                if (forecastedMacroeconimcsSensitivityEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ForecastedMacroeconimcsSensitivity with ID of {0} is not in database", forecastedMacroeconimcsSensitivityId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return forecastedMacroeconimcsSensitivityEntity;
            });
        }

        public ForecastedMacroeconimcsSensitivityData[] GetAllForecastedMacroeconimcsSensitivitys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                List<ForecastedMacroeconimcsSensitivityData> forecastedMacroeconimcsSensitivitys = new List<ForecastedMacroeconimcsSensitivityData>();
                IEnumerable<ForecastedMacroeconimcsSensitivityInfo> forecastedMacroeconimcsSensitivityInfos = forecastedMacroeconimcsSensitivityRepository.GetForecastedMacroeconimcsSensitivitys().ToArray();


                foreach (var forecastedMacroeconimcsSensitivityInfo in forecastedMacroeconimcsSensitivityInfos)
                {
                    string vtype = "";
                    if (forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    forecastedMacroeconimcsSensitivitys.Add(
                        new ForecastedMacroeconimcsSensitivityData
                        {
                            ForecastedMacroeconimcsSensitivityId = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.EntityId,
                            Year = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Year,
                            Sector = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Sector_Code,
                            SectorName = forecastedMacroeconimcsSensitivityInfo.Sector.Description,
                            Type = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Type,
                            TypeName = vtype,
                            Variable = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Variable,
                            VariableName = forecastedMacroeconimcsSensitivityInfo.MacroEconomicVariable.Description,
                            Value = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Value,
                            Active = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Active
                        });
                }

                return forecastedMacroeconimcsSensitivitys.ToArray();
            });
        }


        public void InsertSensitivityData(string microeconomic, int year, int types, float values)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_Forecasted_Macroeconimcs_Sensitivity_Add", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Microeconomic",
                    Value = microeconomic,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = year,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Type",
                    Value = types,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Value",
                    Value = values,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
        public void ComputeSensitivity()
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_regression_Analysis_Sensitivity", con);//spp_ifrs_regression_Analysis_Sensitivity
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region BucketExposure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BucketExposure UpdateBucketExposure(BucketExposure sectorialExposure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                BucketExposure updatedEntity = null;

                if (sectorialExposure.BucketExposureId == 0)
                    updatedEntity = sectorialExposureRepository.Add(sectorialExposure);
                else
                    updatedEntity = sectorialExposureRepository.Update(sectorialExposure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBucketExposure(int sectorialExposureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                sectorialExposureRepository.Remove(sectorialExposureId);
            });
        }

        public BucketExposure GetBucketExposure(int sectorialExposureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                BucketExposure sectorialExposureEntity = sectorialExposureRepository.Get(sectorialExposureId);
                if (sectorialExposureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BucketExposure with ID of {0} is not in database", sectorialExposureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialExposureEntity;
            });
        }

        public BucketExposure[] GetAllBucketExposures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                IEnumerable<BucketExposure> sectorialExposures = sectorialExposureRepository.Get().ToArray();

                return sectorialExposures.ToArray();
            });
        }

        public string GetAllBucketExposuresChart()
        {
            DataTable dt = new DataTable();
            dt = LoadBucketGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Name")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Exposure")
                        row.Add("value", dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }


        public DataTable LoadBucketGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_bucket_exposure";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
        #endregion

        #region ForecastedMacroeconimcsScenario operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ForecastedMacroeconimcsScenario UpdateForecastedMacroeconimcsScenario(ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                ForecastedMacroeconimcsScenario updatedEntity = null;

                if (forecastedMacroeconimcsScenario.ForecastedMacroeconimcsScenarioId == 0)
                    updatedEntity = forecastedMacroeconimcsScenarioRepository.Add(forecastedMacroeconimcsScenario);
                else
                    updatedEntity = forecastedMacroeconimcsScenarioRepository.Update(forecastedMacroeconimcsScenario);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                forecastedMacroeconimcsScenarioRepository.Remove(forecastedMacroeconimcsScenarioId);
            });
        }

        public ForecastedMacroeconimcsScenario GetForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenarioEntity = forecastedMacroeconimcsScenarioRepository.Get(forecastedMacroeconimcsScenarioId);
                if (forecastedMacroeconimcsScenarioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ForecastedMacroeconimcsScenario with ID of {0} is not in database", forecastedMacroeconimcsScenarioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return forecastedMacroeconimcsScenarioEntity;
            });
        }

        public ForecastedMacroeconimcsScenarioData[] GetAllForecastedMacroeconimcsScenarios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                List<ForecastedMacroeconimcsScenarioData> forecastedMacroeconimcsScenarios = new List<ForecastedMacroeconimcsScenarioData>();
                IEnumerable<ForecastedMacroeconimcsScenarioInfo> forecastedMacroeconimcsScenarioInfos = forecastedMacroeconimcsScenarioRepository.GetForecastedMacroeconimcsScenarios().ToArray();


                foreach (var forecastedMacroeconimcsScenarioInfo in forecastedMacroeconimcsScenarioInfos)
                {
                    string vtype = "";
                    if (forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    forecastedMacroeconimcsScenarios.Add(
                        new ForecastedMacroeconimcsScenarioData
                        {
                            ForecastedMacroeconimcsScenarioId = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.EntityId,
                            Year = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Year,
                            Sector = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Sector_Code,
                            SectorName = forecastedMacroeconimcsScenarioInfo.Sector.Description,
                            Type = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Type,
                            TypeName = vtype,
                            Variable = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Variable,
                            VariableName = forecastedMacroeconimcsScenarioInfo.MacroEconomicVariable.Description,
                            Value = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Value,
                            Active = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Active
                        });
                }

                return forecastedMacroeconimcsScenarios.ToArray();
            });
        }


        public void InsertScenarioData(string sector, string microeconomic, int year, int types, float values)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_Forecasted_Macroeconimcs_Scenario_Add", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Sector",
                    Value = sector,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Microeconomic",
                    Value = microeconomic,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = year,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Type",
                    Value = types,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Value",
                    Value = values,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public void ComputeScenario()
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_regression_Analysis_Scenario", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region LoanSpreadSensitivity operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanSpreadSensitivity UpdateLoanSpreadSensitivity(LoanSpreadSensitivity loanSpreadSensitivity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                LoanSpreadSensitivity updatedEntity = null;

                if (loanSpreadSensitivity.LoanSpreadSensitivityId == 0)
                    updatedEntity = loanSpreadSensitivityRepository.Add(loanSpreadSensitivity);
                else
                    updatedEntity = loanSpreadSensitivityRepository.Update(loanSpreadSensitivity);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                loanSpreadSensitivityRepository.Remove(loanSpreadSensitivityId);
            });
        }

        public LoanSpreadSensitivity GetLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                LoanSpreadSensitivity loanSpreadSensitivityEntity = loanSpreadSensitivityRepository.Get(loanSpreadSensitivityId);
                if (loanSpreadSensitivityEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanSpreadSensitivity with ID of {0} is not in database", loanSpreadSensitivityId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanSpreadSensitivityEntity;
            });
        }

        public LoanSpreadSensitivity[] GetAllLoanSpreadSensitivitys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                IEnumerable<LoanSpreadSensitivity> loanSpreadSensitivitys = loanSpreadSensitivityRepository.Get().ToArray();

                return loanSpreadSensitivitys.ToArray();
            });
        }



        #endregion

        #region LoanSpreadScenario operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanSpreadScenario UpdateLoanSpreadScenario(LoanSpreadScenario loanSpreadScenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                LoanSpreadScenario updatedEntity = null;

                if (loanSpreadScenario.LoanSpreadScenarioId == 0)
                    updatedEntity = loanSpreadScenarioRepository.Add(loanSpreadScenario);
                else
                    updatedEntity = loanSpreadScenarioRepository.Update(loanSpreadScenario);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanSpreadScenario(int loanSpreadScenarioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                loanSpreadScenarioRepository.Remove(loanSpreadScenarioId);
            });
        }

        public LoanSpreadScenario GetLoanSpreadScenario(int loanSpreadScenarioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                LoanSpreadScenario loanSpreadScenarioEntity = loanSpreadScenarioRepository.Get(loanSpreadScenarioId);
                if (loanSpreadScenarioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanSpreadScenario with ID of {0} is not in database", loanSpreadScenarioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanSpreadScenarioEntity;
            });
        }

        public LoanSpreadScenario[] GetAllLoanSpreadScenarios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                IEnumerable<LoanSpreadScenario> loanSpreadScenarios = loanSpreadScenarioRepository.Get().ToArray();

                return loanSpreadScenarios.ToArray();
            });
        }


        #endregion

        #region UnquotedEquityFairvalueResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UnquotedEquityFairvalueResult UpdateUnquotedEquityFairvalueResult(UnquotedEquityFairvalueResult unquotedEquityFairvalueResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                UnquotedEquityFairvalueResult updatedEntity = null;

                if (unquotedEquityFairvalueResult.UnquotedEquityFairvalueResultId == 0)
                    updatedEntity = unquotedEquityFairvalueResultRepository.Add(unquotedEquityFairvalueResult);
                else
                    updatedEntity = unquotedEquityFairvalueResultRepository.Update(unquotedEquityFairvalueResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                unquotedEquityFairvalueResultRepository.Remove(unquotedEquityFairvalueResultId);
            });
        }

        public UnquotedEquityFairvalueResult GetUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                UnquotedEquityFairvalueResult unquotedEquityFairvalueResultEntity = unquotedEquityFairvalueResultRepository.Get(unquotedEquityFairvalueResultId);
                if (unquotedEquityFairvalueResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UnquotedEquityFairvalueResult with ID of {0} is not in database", unquotedEquityFairvalueResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return unquotedEquityFairvalueResultEntity;
            });
        }

        public UnquotedEquityFairvalueResult[] GetAllUnquotedEquityFairvalueResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                IEnumerable<UnquotedEquityFairvalueResult> unquotedEquityFairvalueResults = unquotedEquityFairvalueResultRepository.Get().ToArray();

                return unquotedEquityFairvalueResults.ToArray();
            });
        }

        #endregion

        #region PiTPDComparism operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PiTPDComparism UpdatePiTPDComparism(PiTPDComparism piTPDComparism)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                PiTPDComparism updatedEntity = null;

                if (piTPDComparism.ComparismPDId == 0)
                    updatedEntity = piTPDComparismRepository.Add(piTPDComparism);
                else
                    updatedEntity = piTPDComparismRepository.Update(piTPDComparism);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePiTPDComparism(int piTPDComparismId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                piTPDComparismRepository.Remove(piTPDComparismId);
            });
        }

        public PiTPDComparism GetPiTPDComparism(int piTPDComparismId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                PiTPDComparism piTPDComparismEntity = piTPDComparismRepository.Get(piTPDComparismId);
                if (piTPDComparismEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PiTPDComparism with ID of {0} is not in database", piTPDComparismId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return piTPDComparismEntity;
            });
        }

        public PiTPDComparism[] GetAllPiTPDComparisms()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                IEnumerable<PiTPDComparism> piTPDComparisms = piTPDComparismRepository.Get().Where(c => c.Type == "SENSITIVITY").ToArray();

                return piTPDComparisms.ToArray();
            });
        }


        #endregion

        #region MarkovMatrix operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarkovMatrix UpdateMarkovMatrix(MarkovMatrix markovMatrix)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                MarkovMatrix updatedEntity = null;

                if (markovMatrix.MarkovMatrixId == 0)
                    updatedEntity = markovMatrixRepository.Add(markovMatrix);
                else
                    updatedEntity = markovMatrixRepository.Update(markovMatrix);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarkovMatrix(int markovMatrixId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                markovMatrixRepository.Remove(markovMatrixId);
            });
        }

        public MarkovMatrix GetMarkovMatrix(int markovMatrixId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                MarkovMatrix markovMatrixEntity = markovMatrixRepository.Get(markovMatrixId);
                if (markovMatrixEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarkovMatrix with ID of {0} is not in database", markovMatrixId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return markovMatrixEntity;
            });
        }

        public MarkovMatrix[] GetAllMarkovMatrixs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                IEnumerable<MarkovMatrix> markovMatrixs = markovMatrixRepository.Get().ToArray();

                return markovMatrixs.ToArray();
            });
        }


        public MarkovMatrixData[] GetMarkovMatrixs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();


                List<MarkovMatrixData> markovMatrixs = new List<MarkovMatrixData>();
                IEnumerable<MarkovMatrixInfo> markovMatrixInfos = markovMatrixRepository.GetMarkovMatrixs().ToArray();

                foreach (var markovMatrixInfo in markovMatrixInfos)
                {
                    markovMatrixs.Add(
                        new MarkovMatrixData
                        {
                            MarkovMatrixId = markovMatrixInfo.MarkovMatrix.EntityId,
                            Sector = markovMatrixInfo.Sector.Code,
                            SectorName = markovMatrixInfo.Sector.Description,
                            Year = markovMatrixInfo.MarkovMatrix.Year,
                            InitialPD = markovMatrixInfo.MarkovMatrix.InitialPD,
                            InitialNonPD = markovMatrixInfo.MarkovMatrix.InitialNonPD,
                            PDmatrix = markovMatrixInfo.MarkovMatrix.PDmatrix,
                            NPDmatrix = markovMatrixInfo.MarkovMatrix.NPDmatrix,
                            Active = markovMatrixInfo.MarkovMatrix.Active
                        });
                }

                return markovMatrixs.ToArray();
            });
        }

        #endregion

        #region CCFModelling operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CCFModelling UpdateCCFModelling(CCFModelling ccfModelling)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                CCFModelling updatedEntity = null;

                if (ccfModelling.CCFModellingId == 0)
                    updatedEntity = ccfModellingRepository.Add(ccfModelling);
                else
                    updatedEntity = ccfModellingRepository.Update(ccfModelling);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCCFModelling(int ccfModellingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                ccfModellingRepository.Remove(ccfModellingId);
            });
        }

        public CCFModelling GetCCFModelling(int ccfModellingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                CCFModelling ccfModellingEntity = ccfModellingRepository.Get(ccfModellingId);
                if (ccfModellingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CCFModelling with ID of {0} is not in database", ccfModellingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ccfModellingEntity;
            });
        }

        public CCFModelling[] GetAllCCFModellings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                IEnumerable<CCFModelling> ccfModellings = ccfModellingRepository.Get().ToArray();

                return ccfModellings.ToArray();
            });
        }


        #endregion

        #region ECLComparism operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ECLComparism UpdateECLComparism(ECLComparism eclComparism)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                ECLComparism updatedEntity = null;

                if (eclComparism.ECLComparismId == 0)
                    updatedEntity = eclComparismRepository.Add(eclComparism);
                else
                    updatedEntity = eclComparismRepository.Update(eclComparism);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteECLComparism(int eclComparismId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                eclComparismRepository.Remove(eclComparismId);
            });
        }

        public ECLComparism GetECLComparism(int eclComparismId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                ECLComparism eclComparismEntity = eclComparismRepository.Get(eclComparismId);
                if (eclComparismEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ECLComparism with ID of {0} is not in database", eclComparismId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return eclComparismEntity;
            });
        }

        public ECLComparism[] GetAllECLComparisms()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                IEnumerable<ECLComparism> eclComparisms = eclComparismRepository.Get().ToArray();

                return eclComparisms.ToArray();
            });
        }

        #endregion

        #region ForeignEADExchangeRate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ForeignEADExchangeRate UpdateForeignEADExchangeRate(ForeignEADExchangeRate ForeignEADExchangeRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEADExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                ForeignEADExchangeRate updatedEntity = null;

                if (ForeignEADExchangeRate.ForeignEADExchangeRateId == 0)
                    updatedEntity = foreignEADExchangeRateRepository.Add(ForeignEADExchangeRate);
                else
                    updatedEntity = foreignEADExchangeRateRepository.Update(ForeignEADExchangeRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteForeignEADExchangeRate(int foreignEadExchangeRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEadExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                foreignEadExchangeRateRepository.Remove(foreignEadExchangeRateId);
            });
        }

        public ForeignEADExchangeRate GetForeignEADExchangeRate(int foreignEADExchangeRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEadExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                ForeignEADExchangeRate foreignEadExchangeRateEntity = foreignEadExchangeRateRepository.Get(foreignEADExchangeRateId);
                if (foreignEadExchangeRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ForeignEADExchangeRate with ID of {0} is not in database", foreignEADExchangeRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return foreignEadExchangeRateEntity;
            });
        }

        public ForeignEADExchangeRate[] GetAllForeignEADExchangeRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEadExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                IEnumerable<ForeignEADExchangeRate> foreignEadExchangeRates = foreignEadExchangeRateRepository.Get().ToArray();

                return foreignEadExchangeRates.ToArray();
            });
        }



        #endregion

        //Begin Victor Segment

        #region EuroBondSpread operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public EuroBondSpread UpdateEuroBondSpread(EuroBondSpread euroBondSpread)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                EuroBondSpread updatedEntity = null;

                if (euroBondSpread.ID == 0)
                    updatedEntity = euroBondSpreadRepository.Add(euroBondSpread);
                else
                    updatedEntity = euroBondSpreadRepository.Update(euroBondSpread);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteEuroBondSpread(int euroBondSpreadId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                euroBondSpreadRepository.Remove(euroBondSpreadId);
            });
        }

        public EuroBondSpread GetEuroBondSpread(int euroBondSpreadId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                EuroBondSpread euroBondSpreadEntity = euroBondSpreadRepository.Get(euroBondSpreadId);
                if (euroBondSpreadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("EuroBondSpread with ID of {0} is not in database", euroBondSpreadId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return euroBondSpreadEntity;
            });
        }

        public EuroBondSpread[] GetAllEuroBondSpreads()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                IEnumerable<EuroBondSpread> euroBondSpreads = euroBondSpreadRepository.Get().ToArray();

                return euroBondSpreads.ToArray();
            });
        }

        #endregion

        #region PlacementComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PlacementComputationResult UpdatePlacementComputationResult(PlacementComputationResult placementComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                PlacementComputationResult updatedEntity = null;

                if (placementComputationResult.Id == 0)
                    updatedEntity = placementComputationResultRepository.Add(placementComputationResult);
                else
                    updatedEntity = placementComputationResultRepository.Update(placementComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePlacementComputationResult(int placementComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                placementComputationResultRepository.Remove(placementComputationResultId);
            });
        }

        public PlacementComputationResult GetPlacementComputationResult(int placementComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                PlacementComputationResult placementComputationResultEntity = placementComputationResultRepository.Get(placementComputationResultId);
                if (placementComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PlacementComputationResult with ID of {0} is not in database", placementComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return placementComputationResultEntity;
            });
        }

        public PlacementComputationResult[] GetAllPlacementComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                IEnumerable<PlacementComputationResult> placementComputationResults = placementComputationResultRepository.Get().ToArray();

                return placementComputationResults.ToArray();
            });
        }

        #endregion

        #region LgdComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LgdComputationResult UpdateLgdComputationResult(LgdComputationResult lgdComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                LgdComputationResult updatedEntity = null;

                if (lgdComputationResult.Id == 0)
                    updatedEntity = lgdComputationResultRepository.Add(lgdComputationResult);
                else
                    updatedEntity = lgdComputationResultRepository.Update(lgdComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLgdComputationResult(int lgdComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                lgdComputationResultRepository.Remove(lgdComputationResultId);
            });
        }

        public LgdComputationResult GetLgdComputationResult(int lgdComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                LgdComputationResult lgdComputationResultEntity = lgdComputationResultRepository.Get(lgdComputationResultId);
                if (lgdComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LgdComputationResult with ID of {0} is not in database", lgdComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lgdComputationResultEntity;
            });
        }

        public LgdComputationResult[] GetAllLgdComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                IEnumerable<LgdComputationResult> lgdComputationResults = lgdComputationResultRepository.Get().ToArray();

                return lgdComputationResults.ToArray();
            });
        }

        #endregion

        #region LgdComputationResultPlacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LgdComputationResultPlacement UpdateLgdComputationResultPlacement(LgdComputationResultPlacement lgdComputationResultPlacement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultPlacementRepository lgdComputationResultPlacementRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultPlacementRepository>();

                LgdComputationResultPlacement updatedEntity = null;

                if (lgdComputationResultPlacement.Id == 0)
                    updatedEntity = lgdComputationResultPlacementRepository.Add(lgdComputationResultPlacement);
                else
                    updatedEntity = lgdComputationResultPlacementRepository.Update(lgdComputationResultPlacement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLgdComputationResultPlacement(int lgdComputationResultPlacementId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultPlacementRepository lgdComputationResultPlacementRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultPlacementRepository>();

                lgdComputationResultPlacementRepository.Remove(lgdComputationResultPlacementId);
            });
        }

        public LgdComputationResultPlacement GetLgdComputationResultPlacement(int lgdComputationResultPlacementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultPlacementRepository lgdComputationResultPlacementRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultPlacementRepository>();

                LgdComputationResultPlacement lgdComputationResultPlacementEntity = lgdComputationResultPlacementRepository.Get(lgdComputationResultPlacementId);
                if (lgdComputationResultPlacementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LgdComputationResultPlacement with ID of {0} is not in database", lgdComputationResultPlacementId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lgdComputationResultPlacementEntity;
            });
        }

        public LgdComputationResultPlacement[] GetAllLgdComputationResultPlacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultPlacementRepository lgdComputationResultPlacementRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultPlacementRepository>();

                IEnumerable<LgdComputationResultPlacement> lgdComputationResultPlacements = lgdComputationResultPlacementRepository.Get().ToArray();

                return lgdComputationResultPlacements.ToArray();
            });
        }

        #endregion

        #region LocalBondSpread operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LocalBondSpread UpdateLocalBondSpread(LocalBondSpread localBondSpread)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                LocalBondSpread updatedEntity = null;

                if (localBondSpread.ID == 0)
                    updatedEntity = localBondSpreadRepository.Add(localBondSpread);
                else
                    updatedEntity = localBondSpreadRepository.Update(localBondSpread);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLocalBondSpread(int localBondSpreadId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                localBondSpreadRepository.Remove(localBondSpreadId);
            });
        }

        public LocalBondSpread GetLocalBondSpread(int localBondSpreadId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                LocalBondSpread localBondSpreadEntity = localBondSpreadRepository.Get(localBondSpreadId);
                if (localBondSpreadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LocalBondSpread with ID of {0} is not in database", localBondSpreadId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return localBondSpreadEntity;
            });
        }

        public LocalBondSpread[] GetAllLocalBondSpreads()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                IEnumerable<LocalBondSpread> localBondSpreads = localBondSpreadRepository.Get().ToArray();

                return localBondSpreads.ToArray();
            });
        }

        #endregion

        #region MarginalPDDistribution operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalPDDistribution UpdateMarginalPDDistribution(MarginalPDDistribution marginalPDDistribution)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                MarginalPDDistribution updatedEntity = null;

                if (marginalPDDistribution.ID == 0)
                    updatedEntity = marginalPDDistributionRepository.Add(marginalPDDistribution);
                else
                    updatedEntity = marginalPDDistributionRepository.Update(marginalPDDistribution);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalPDDistribution(int marginalPDDistributionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                marginalPDDistributionRepository.Remove(marginalPDDistributionId);
            });
        }

        public MarginalPDDistribution GetMarginalPDDistribution(int marginalPDDistributionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                MarginalPDDistribution marginalPDDistributionEntity = marginalPDDistributionRepository.Get(marginalPDDistributionId);
                if (marginalPDDistributionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalPDDistribution with ID of {0} is not in database", marginalPDDistributionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return marginalPDDistributionEntity;
            });
        }

        public MarginalPDDistribution[] GetAllMarginalPDDistributions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                IEnumerable<MarginalPDDistribution> marginalPDDistributions = marginalPDDistributionRepository.Get().ToArray();

                return marginalPDDistributions.ToArray();
            });
        }

        #endregion

        #region BondMarginalPDDistribution operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BondMarginalPDDistribution UpdateBondMarginalPDDistribution(BondMarginalPDDistribution bondMarginalPDDistribution)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                BondMarginalPDDistribution updatedEntity = null;

                if (bondMarginalPDDistribution.ID == 0)
                    updatedEntity = bondMarginalPDDistributionRepository.Add(bondMarginalPDDistribution);
                else
                    updatedEntity = bondMarginalPDDistributionRepository.Update(bondMarginalPDDistribution);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                bondMarginalPDDistributionRepository.Remove(bondMarginalPDDistributionId);
            });
        }

        public BondMarginalPDDistribution GetBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                BondMarginalPDDistribution bondMarginalPDDistributionEntity = bondMarginalPDDistributionRepository.Get(bondMarginalPDDistributionId);
                if (bondMarginalPDDistributionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondMarginalPDDistribution with ID of {0} is not in database", bondMarginalPDDistributionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondMarginalPDDistributionEntity;
            });
        }

        public BondMarginalPDDistribution[] GetAllBondMarginalPDDistributions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                IEnumerable<BondMarginalPDDistribution> bondMarginalPDDistributions = bondMarginalPDDistributionRepository.Get().ToArray();

                return bondMarginalPDDistributions.ToArray();
            });
        }

        #endregion

        #region MarginalPDDistributionPlacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalPDDistributionPlacement UpdateMarginalPDDistributionPlacement(MarginalPDDistributionPlacement marginalPDDistributionPlacement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                MarginalPDDistributionPlacement updatedEntity = null;

                if (marginalPDDistributionPlacement.ID == 0)
                    updatedEntity = marginalPDDistributionPlacementRepository.Add(marginalPDDistributionPlacement);
                else
                    updatedEntity = marginalPDDistributionPlacementRepository.Update(marginalPDDistributionPlacement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                marginalPDDistributionPlacementRepository.Remove(marginalPDDistributionPlacementId);
            });
        }

        public MarginalPDDistributionPlacement GetMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                MarginalPDDistributionPlacement marginalPDDistributionPlacementEntity = marginalPDDistributionPlacementRepository.Get(marginalPDDistributionPlacementId);
                if (marginalPDDistributionPlacementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalPDDistributionPlacement with ID of {0} is not in database", marginalPDDistributionPlacementId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return marginalPDDistributionPlacementEntity;
            });
        }

        public MarginalPDDistributionPlacement[] GetAllMarginalPDDistributionPlacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                IEnumerable<MarginalPDDistributionPlacement> marginalPDDistributionPlacements = marginalPDDistributionPlacementRepository.Get().ToArray();

                return marginalPDDistributionPlacements.ToArray();
            });
        }

        #endregion

        #region EclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public EclComputationResult UpdateEclComputationResult(EclComputationResult eclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                EclComputationResult updatedEntity = null;

                if (eclComputationResult.ID == 0)
                    updatedEntity = eclComputationResultRepository.Add(eclComputationResult);
                else
                    updatedEntity = eclComputationResultRepository.Update(eclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteEclComputationResult(int eclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                eclComputationResultRepository.Remove(eclComputationResultId);
            });
        }

        public EclComputationResult GetEclComputationResult(int eclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                EclComputationResult eclComputationResultEntity = eclComputationResultRepository.Get(eclComputationResultId);
                if (eclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("EclComputationResult with ID of {0} is not in database", eclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return eclComputationResultEntity;
            });
        }

        public EclComputationResult[] GetAllEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                IEnumerable<EclComputationResult> eclComputationResults = eclComputationResultRepository.Get().ToArray();

                return eclComputationResults.ToArray();
            });
        }

        public EclComputationResult[] GetAllEclComputationResultsByStage(int stage)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                IEnumerable<EclComputationResult> eclComputationResults = eclComputationResultRepository.GetEntityByStage(stage);

                return eclComputationResults.ToArray();
            });
        }

        #endregion

        #region BondEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BondEclComputationResult UpdateBondEclComputationResult(BondEclComputationResult bondEclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                BondEclComputationResult updatedEntity = null;

                if (bondEclComputationResult.ID == 0)
                    updatedEntity = bondEclComputationResultRepository.Add(bondEclComputationResult);
                else
                    updatedEntity = bondEclComputationResultRepository.Update(bondEclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBondEclComputationResult(int bondEclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                bondEclComputationResultRepository.Remove(bondEclComputationResultId);
            });
        }

        public BondEclComputationResult GetBondEclComputationResult(int bondEclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                BondEclComputationResult bondEclComputationResultEntity = bondEclComputationResultRepository.Get(bondEclComputationResultId);
                if (bondEclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondEclComputationResult with ID of {0} is not in database", bondEclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondEclComputationResultEntity;
            });
        }

        public BondEclComputationResult[] GetAllBondEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                IEnumerable<BondEclComputationResult> bondEclComputationResults = bondEclComputationResultRepository.Get().ToArray();

                return bondEclComputationResults.ToArray();
            });
        }

        #endregion

        #region PlacementEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PlacementEclComputationResult UpdatePlacementEclComputationResult(PlacementEclComputationResult placementEclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                PlacementEclComputationResult updatedEntity = null;

                if (placementEclComputationResult.ID == 0)
                    updatedEntity = placementEclComputationResultRepository.Add(placementEclComputationResult);
                else
                    updatedEntity = placementEclComputationResultRepository.Update(placementEclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePlacementEclComputationResult(int placementEclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                placementEclComputationResultRepository.Remove(placementEclComputationResultId);
            });
        }

        public PlacementEclComputationResult GetPlacementEclComputationResult(int placementEclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                PlacementEclComputationResult placementEclComputationResultEntity = placementEclComputationResultRepository.Get(placementEclComputationResultId);
                if (placementEclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PlacementEclComputationResult with ID of {0} is not in database", placementEclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return placementEclComputationResultEntity;
            });
        }

        public PlacementEclComputationResult[] GetAllPlacementEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                IEnumerable<PlacementEclComputationResult> placementEclComputationResults = placementEclComputationResultRepository.Get().ToArray();

                return placementEclComputationResults.ToArray();
            });
        }

        #endregion

        #region LcBgEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LcBgEclComputationResult UpdateLcBgEclComputationResult(LcBgEclComputationResult lcBgEclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                LcBgEclComputationResult updatedEntity = null;

                if (lcBgEclComputationResult.ID == 0)
                    updatedEntity = lcBgEclComputationResultRepository.Add(lcBgEclComputationResult);
                else
                    updatedEntity = lcBgEclComputationResultRepository.Update(lcBgEclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                lcBgEclComputationResultRepository.Remove(lcBgEclComputationResultId);
            });
        }

        public LcBgEclComputationResult GetLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                LcBgEclComputationResult lcBgEclComputationResultEntity = lcBgEclComputationResultRepository.Get(lcBgEclComputationResultId);
                if (lcBgEclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LcBgEclComputationResult with ID of {0} is not in database", lcBgEclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lcBgEclComputationResultEntity;
            });
        }

        public LcBgEclComputationResult[] GetAllLcBgEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                IEnumerable<LcBgEclComputationResult> lcBgEclComputationResults = lcBgEclComputationResultRepository.Get().ToArray();

                return lcBgEclComputationResults.ToArray();
            });
        }

        #endregion

        #region TBillEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TBillEclComputationResult UpdateTBillEclComputationResult(TBillEclComputationResult tBillEclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITBillEclComputationResultRepository tBillEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ITBillEclComputationResultRepository>();

                TBillEclComputationResult updatedEntity = null;

                if (tBillEclComputationResult.ID == 0)
                    updatedEntity = tBillEclComputationResultRepository.Add(tBillEclComputationResult);
                else
                    updatedEntity = tBillEclComputationResultRepository.Update(tBillEclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTBillEclComputationResult(int tBillEclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITBillEclComputationResultRepository tBillEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ITBillEclComputationResultRepository>();

                tBillEclComputationResultRepository.Remove(tBillEclComputationResultId);
            });
        }

        public TBillEclComputationResult GetTBillEclComputationResult(int tBillEclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITBillEclComputationResultRepository tBillEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ITBillEclComputationResultRepository>();

                TBillEclComputationResult tBillEclComputationResultEntity = tBillEclComputationResultRepository.Get(tBillEclComputationResultId);
                if (tBillEclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TBillEclComputationResult with ID of {0} is not in database", tBillEclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return tBillEclComputationResultEntity;
            });
        }

        public TBillEclComputationResult[] GetAllTBillEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITBillEclComputationResultRepository tBillEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ITBillEclComputationResultRepository>();

                IEnumerable<TBillEclComputationResult> tBillEclComputationResults = tBillEclComputationResultRepository.Get().ToArray();

                return tBillEclComputationResults.ToArray();
            });
        }

        #endregion

        //End Victor Segment
        #region ComputedForcastedPDLGD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                ComputedForcastedPDLGD updatedEntity = null;

                if (computedForcastedPDLGD.ComputedPDId == 0)
                    updatedEntity = computedForcastedPDLGDRepository.Add(computedForcastedPDLGD);
                else
                    updatedEntity = computedForcastedPDLGDRepository.Update(computedForcastedPDLGD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteComputedForcastedPDLGD(int computedForcastedPDLGDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                computedForcastedPDLGDRepository.Remove(computedForcastedPDLGDId);
            });
        }

        public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedForcastedPDLGDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                ComputedForcastedPDLGD computedForcastedPDLGDEntity = computedForcastedPDLGDRepository.Get(computedForcastedPDLGDId);
                if (computedForcastedPDLGDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ComputedForcastedPDLGD with ID of {0} is not in database", computedForcastedPDLGDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return computedForcastedPDLGDEntity;
            });
        }

        public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                List<ComputedForcastedPDLGD> computedForcastedPDLGDs = computedForcastedPDLGDRepository.GetComputedForcastedPDLGD();

                return computedForcastedPDLGDs.ToArray();
            });
        }

        public ComputedForcastedPDLGD[] GetListComputedForcastedPDLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                IEnumerable<ComputedForcastedPDLGD> computedForcastedPDLGDs = computedForcastedPDLGDRepository.Get().ToArray();

                return computedForcastedPDLGDs.ToArray();
            });
        }

        #endregion

        #region ComputedForcastedPDLGDForeign operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ComputedForcastedPDLGDForeign UpdateComputedForcastedPDLGDForeign(ComputedForcastedPDLGDForeign computedForcastedPDLGDForeign)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                ComputedForcastedPDLGDForeign updatedEntity = null;

                if (computedForcastedPDLGDForeign.ComputedPDId == 0)
                    updatedEntity = computedForcastedPDLGDForeignRepository.Add(computedForcastedPDLGDForeign);
                else
                    updatedEntity = computedForcastedPDLGDForeignRepository.Update(computedForcastedPDLGDForeign);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteComputedForcastedPDLGDForeign(int computedForcastedPDLGDForeignId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                computedForcastedPDLGDForeignRepository.Remove(computedForcastedPDLGDForeignId);
            });
        }

        public ComputedForcastedPDLGDForeign GetComputedForcastedPDLGDForeign(int computedForcastedPDLGDForeignId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                ComputedForcastedPDLGDForeign computedForcastedPDLGDForeignEntity = computedForcastedPDLGDForeignRepository.Get(computedForcastedPDLGDForeignId);
                if (computedForcastedPDLGDForeignEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ComputedForcastedPDLGDForeign with ID of {0} is not in database", computedForcastedPDLGDForeignId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return computedForcastedPDLGDForeignEntity;
            });
        }

        public ComputedForcastedPDLGDForeign[] GetAllComputedForcastedPDLGDForeigns()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                List<ComputedForcastedPDLGDForeign> computedForcastedPDLGDForeigns = computedForcastedPDLGDForeignRepository.GetComputedForcastedPDLGDForeign();

                return computedForcastedPDLGDForeigns.ToArray();
            });
        }

        public ComputedForcastedPDLGDForeign[] GetListComputedForcastedPDLGDForeigns()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                IEnumerable<ComputedForcastedPDLGDForeign> computedForcastedPDLGDForeigns = computedForcastedPDLGDForeignRepository.Get().ToArray();

                return computedForcastedPDLGDForeigns.ToArray();
            });
        }

        #endregion

        #region MacroEconomicsNPL operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomicsNPL UpdateMacroEconomicsNPL(MacroEconomicsNPL macroEconomicsNPL)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                MacroEconomicsNPL updatedEntity = null;

                if (macroEconomicsNPL.macroeconomicnplId == 0)
                    updatedEntity = macroEconomicsNPLRepository.Add(macroEconomicsNPL);
                else
                    updatedEntity = macroEconomicsNPLRepository.Update(macroEconomicsNPL);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomicsNPL(int macroeconomicnplId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                macroEconomicsNPLRepository.Remove(macroeconomicnplId);
            });
        }


        public MacroEconomicsNPL GetMacroEconomicsNPL(int macroeconomicnplId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                MacroEconomicsNPL macroEconomicsNPLEntity = macroEconomicsNPLRepository.Get(macroeconomicnplId);
                if (macroEconomicsNPLEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomicsNPL with ID of {0} is not in database", macroeconomicnplId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicsNPLEntity;
            });
        }

        public MacroEconomicsNPL[] GetMacroEconomicsNPLByScenario(string scenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsnplRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                IEnumerable<MacroEconomicsNPL> nplScenario = macroEconomicsnplRepository.Get().OrderBy(c => c.Year).Where(c => c.Scenerio == scenario);

                return nplScenario.ToArray();

                
            });
        }

        public MacroEconomicsNPL[] GetAllMacroEconomicsNPLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                IEnumerable<MacroEconomicsNPL> macroEconomicsNPLs = macroEconomicsNPLRepository.Get().ToArray();

                return macroEconomicsNPLs.ToArray();
            });
        }


        #endregion

        #region MonthlyDiscountFactor operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MonthlyDiscountFactor UpdateMonthlyDiscountFactor(MonthlyDiscountFactor monthlyDiscountFactor)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                MonthlyDiscountFactor updatedEntity = null;

                if (monthlyDiscountFactor.MonthlyDiscountFactor_Id == 0)
                    updatedEntity = monthlyDiscountFactorRepository.Add(monthlyDiscountFactor);
                else
                    updatedEntity = monthlyDiscountFactorRepository.Update(monthlyDiscountFactor);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                monthlyDiscountFactorRepository.Remove(MonthlyDiscountFactor_Id);
            });
        }

        public MonthlyDiscountFactor GetMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                MonthlyDiscountFactor monthlyDiscountFactorEntity = monthlyDiscountFactorRepository.Get(MonthlyDiscountFactor_Id);
                if (monthlyDiscountFactorEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MonthlyDiscountFactor with ID of {0} is not in database", MonthlyDiscountFactor_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return monthlyDiscountFactorEntity;
            });
        }

        public MonthlyDiscountFactor[] GetAllMonthlyDiscountFactors()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                IEnumerable<MonthlyDiscountFactor> monthlyDiscountFactors = monthlyDiscountFactorRepository.Get().ToArray();

                return monthlyDiscountFactors.Take(100).ToArray();
            });
        }

        public MonthlyDiscountFactor[] GetMonthlyDiscountFactorByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                return monthlyDiscountFactorRepository.GetMonthlyDiscountFactorByRefNo(RefNo).ToArray();
            });
        }



        #endregion

        #region MonthlyDiscountFactorBond operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MonthlyDiscountFactorBond UpdateMonthlyDiscountFactorBond(MonthlyDiscountFactorBond monthlyDiscountFactorBond)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                MonthlyDiscountFactorBond updatedEntity = null;

                if (monthlyDiscountFactorBond.MonthlyDiscountFactorBond_Id == 0)
                    updatedEntity = monthlyDiscountFactorBondRepository.Add(monthlyDiscountFactorBond);
                else
                    updatedEntity = monthlyDiscountFactorBondRepository.Update(monthlyDiscountFactorBond);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                monthlyDiscountFactorBondRepository.Remove(MonthlyDiscountFactorBond_Id);
            });
        }

        public MonthlyDiscountFactorBond GetMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                MonthlyDiscountFactorBond monthlyDiscountFactorBondEntity = monthlyDiscountFactorBondRepository.Get(MonthlyDiscountFactorBond_Id);
                if (monthlyDiscountFactorBondEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MonthlyDiscountFactorBond with ID of {0} is not in database", MonthlyDiscountFactorBond_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return monthlyDiscountFactorBondEntity;
            });
        }

        public MonthlyDiscountFactorBond[] GetAllMonthlyDiscountFactorBonds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                IEnumerable<MonthlyDiscountFactorBond> monthlyDiscountFactorBonds = monthlyDiscountFactorBondRepository.Get().ToArray();

                return monthlyDiscountFactorBonds.Take(100).ToArray();
            });
        }

        public MonthlyDiscountFactorBond[] GetMonthlyDiscountFactorBondByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>(); ;

                return monthlyDiscountFactorBondRepository.GetMonthlyDiscountFactorBondByRefNo(RefNo).ToArray();
            });
        }


        #endregion

        #region MonthlyDiscountFactorPlacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MonthlyDiscountFactorPlacement UpdateMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement monthlyDiscountFactorPlacement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                MonthlyDiscountFactorPlacement updatedEntity = null;

                if (monthlyDiscountFactorPlacement.MonthlyDiscountFactorPlacement_Id == 0)
                    updatedEntity = monthlyDiscountFactorPlacementRepository.Add(monthlyDiscountFactorPlacement);
                else
                    updatedEntity = monthlyDiscountFactorPlacementRepository.Update(monthlyDiscountFactorPlacement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                monthlyDiscountFactorPlacementRepository.Remove(MonthlyDiscountFactorPlacement_Id);
            });
        }

        public MonthlyDiscountFactorPlacement GetMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                MonthlyDiscountFactorPlacement monthlyDiscountFactorPlacementEntity = monthlyDiscountFactorPlacementRepository.Get(MonthlyDiscountFactorPlacement_Id);
                if (monthlyDiscountFactorPlacementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MonthlyDiscountFactorPlacement with ID of {0} is not in database", MonthlyDiscountFactorPlacement_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return monthlyDiscountFactorPlacementEntity;
            });
        }

        public MonthlyDiscountFactorPlacement[] GetAllMonthlyDiscountFactorPlacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                IEnumerable<MonthlyDiscountFactorPlacement> monthlyDiscountFactorPlacements = monthlyDiscountFactorPlacementRepository.Get().ToArray();

                return monthlyDiscountFactorPlacements.Take(100).ToArray();
            });
        }

        public MonthlyDiscountFactorPlacement[] GetMonthlyDiscountFactorPlacementByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                return monthlyDiscountFactorPlacementRepository.GetMonthlyDiscountFactorPlacementByRefNo(RefNo).ToArray();
            });
        }


        #endregion

        #region IfrsPdSeriesByRating operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsPdSeriesByRating UpdateIfrsPdSeriesByRating(IfrsPdSeriesByRating IfrsPdSeriesByRating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdSeriesByRatingRepository IfrsPdSeriesByRatingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdSeriesByRatingRepository>();

                IfrsPdSeriesByRating updatedEntity = null;

                if (IfrsPdSeriesByRating.Sno == 0)
                    updatedEntity = IfrsPdSeriesByRatingRepository.Add(IfrsPdSeriesByRating);
                else
                    updatedEntity = IfrsPdSeriesByRatingRepository.Update(IfrsPdSeriesByRating);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsPdSeriesByRating(int IfrsPdSeriesByRatingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdSeriesByRatingRepository IfrsPdSeriesByRatingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdSeriesByRatingRepository>();

                IfrsPdSeriesByRatingRepository.Remove(IfrsPdSeriesByRatingId);
            });
        }

        public IfrsPdSeriesByRating GetIfrsPdSeriesBySno(int IfrsPdSeriesByRatingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdSeriesByRatingRepository IfrsPdSeriesByRatingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdSeriesByRatingRepository>();

                IfrsPdSeriesByRating IfrsPdSeriesByRatingEntity = IfrsPdSeriesByRatingRepository.Get(IfrsPdSeriesByRatingId);
                if (IfrsPdSeriesByRatingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsPdSeriesByRating with ID of {0} is not in database", IfrsPdSeriesByRatingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsPdSeriesByRatingEntity;
            });
        }

        public IfrsPdSeriesByRating[] GetAllIfrsPdSeriesByRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdSeriesByRatingRepository IfrsPdSeriesByRatingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdSeriesByRatingRepository>();

                IEnumerable<IfrsPdSeriesByRating> IfrsPdSeriesByRatings = IfrsPdSeriesByRatingRepository.Get().ToArray().OrderBy(c => c.seq).ToList();

                return IfrsPdSeriesByRatings.ToArray();
            });
        }

        public string[] GetAllRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdSeriesByRatingRepository IfrsPdSeriesByRatingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdSeriesByRatingRepository>();

                IEnumerable<string> IfrsPdSeriesByRating = IfrsPdSeriesByRatingRepository.Get().OrderBy(c => c.Rating).Select(c => c.Rating).Distinct();

                return IfrsPdSeriesByRating.ToArray();
            });
        }

        public IfrsPdSeriesByRating[] GetIfrsPdSeriesByRating(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdSeriesByRatingRepository ifrsPdSeriesByRatingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdSeriesByRatingRepository>();

                IEnumerable<IfrsPdSeriesByRating> ifrsPdSeriesByRatingEntity = ifrsPdSeriesByRatingRepository.GetEntityByCode(code).OrderBy(c => c.seq);
                if (ifrsPdSeriesByRatingEntity.Count() == 0)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsPdSeriesByRating with rating of {0} is not in database", code));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsPdSeriesByRatingEntity.ToArray();
            });
        }

        #endregion

        #region IfrsRetailPdSeries operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsRetailPdSeries UpdateIfrsRetailPdSeries(IfrsRetailPdSeries IfrsRetailPdSeries)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsRetailPdSeriesRepository IfrsRetailPdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsRetailPdSeriesRepository>();

                IfrsRetailPdSeries updatedEntity = null;

                if (IfrsRetailPdSeries.PdSeriesId == 0)
                    updatedEntity = IfrsRetailPdSeriesRepository.Add(IfrsRetailPdSeries);
                else
                    updatedEntity = IfrsRetailPdSeriesRepository.Update(IfrsRetailPdSeries);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsRetailPdSeries(int IfrsRetailPdSeriesId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsRetailPdSeriesRepository IfrsRetailPdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsRetailPdSeriesRepository>();

                IfrsRetailPdSeriesRepository.Remove(IfrsRetailPdSeriesId);
            });
        }

        public IfrsRetailPdSeries GetIfrsRetailPdSeriesById(int PdSeriesId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsRetailPdSeriesRepository IfrsRetailPdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsRetailPdSeriesRepository>();

                IfrsRetailPdSeries IfrsRetailPdSeriesEntity = IfrsRetailPdSeriesRepository.Get(PdSeriesId);
                if (IfrsRetailPdSeriesEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsRetailPdSeries with ID of {0} is not in database", PdSeriesId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsRetailPdSeriesEntity;
            });
        }

        public IfrsRetailPdSeries[] GetAvailableIfrsRetailPdSeries(QueryOptions queryOptions)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsRetailPdSeriesRepository IfrsRetailPdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsRetailPdSeriesRepository>();

                IEnumerable<IfrsRetailPdSeries> IfrsRetailPdSeries = IfrsRetailPdSeriesRepository.GetPaginatedEntities(queryOptions);

                return IfrsRetailPdSeries.ToArray();
            });
        }

        #endregion

        #region IfrsLgdScenarioByInstrument operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsLgdScenarioByInstrument UpdateIfrsLgdScenarioByInstrument(IfrsLgdScenarioByInstrument IfrsLgdScenarioByInstrument)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsLgdScenarioByInstrumentRepository IfrsLgdScenarioByInstrumentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdScenarioByInstrumentRepository>();

                IfrsLgdScenarioByInstrument updatedEntity = null;

                if (IfrsLgdScenarioByInstrument.InstrumentId == 0)
                    updatedEntity = IfrsLgdScenarioByInstrumentRepository.Add(IfrsLgdScenarioByInstrument);
                else
                    updatedEntity = IfrsLgdScenarioByInstrumentRepository.Update(IfrsLgdScenarioByInstrument);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsLgdScenarioByInstrument(int InstrumentId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsLgdScenarioByInstrumentRepository IfrsLgdScenarioByInstrumentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdScenarioByInstrumentRepository>();

                IfrsLgdScenarioByInstrumentRepository.Remove(InstrumentId);
            });
        }

        public IfrsLgdScenarioByInstrument GetIfrsLgdScenarioByInstrumentId(int InstrumentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsLgdScenarioByInstrumentRepository IfrsLgdScenarioByInstrumentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdScenarioByInstrumentRepository>();

                IfrsLgdScenarioByInstrument IfrsLgdScenarioByInstrumentEntity = IfrsLgdScenarioByInstrumentRepository.Get(InstrumentId);
                if (IfrsLgdScenarioByInstrumentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsLgdScenarioByInstrument with ID of {0} is not in database", InstrumentId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsLgdScenarioByInstrumentEntity;
            });
        }

        public IfrsLgdScenarioByInstrument[] GetAllIfrsLgdScenarioByInstruments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsLgdScenarioByInstrumentRepository IfrsLgdScenarioByInstrumentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdScenarioByInstrumentRepository>();

                IEnumerable<IfrsLgdScenarioByInstrument> IfrsLgdScenarioByInstruments = IfrsLgdScenarioByInstrumentRepository.Get().ToArray().OrderBy(c => c.InstrumentType).ToList();

                return IfrsLgdScenarioByInstruments.ToArray();
            });
        }

        public string[] GetAllInstruments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsLgdScenarioByInstrumentRepository IfrsLgdScenarioByInstrumentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdScenarioByInstrumentRepository>();

                IEnumerable<string> IfrsLgdScenarioByInstrument = IfrsLgdScenarioByInstrumentRepository.Get().OrderBy(c => c.InstrumentType).Select(c => c.InstrumentType).Distinct();

                return IfrsLgdScenarioByInstrument.ToArray();
            });
        }

        public IfrsLgdScenarioByInstrument[] GetIfrsLgdScenarioByInstrument(string type)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsLgdScenarioByInstrumentRepository IfrsLgdScenarioByInstrumentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdScenarioByInstrumentRepository>();

                IEnumerable<IfrsLgdScenarioByInstrument> IfrsLgdScenarioByInstrumentEntity = IfrsLgdScenarioByInstrumentRepository.GetEntityByInstrumentType(type).OrderBy(c => c.InstrumentType);
                if (IfrsLgdScenarioByInstrumentEntity.Count() == 0)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsLgdScenarioByInstrument with instrument of type {0} is not in database", type));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsLgdScenarioByInstrumentEntity.ToArray();
            });
        }

        #endregion

        #region MacroVarRecoveryRates operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroVarRecoveryRates UpdateMacroVarRecoveryRates(MacroVarRecoveryRates MacroVarRecoveryRates)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroVarRecoveryRatesRepository MacroVarRecoveryRatesRepository = _DataRepositoryFactory.GetDataRepository<IMacroVarRecoveryRatesRepository>();

                MacroVarRecoveryRates updatedEntity = null;

                if (MacroVarRecoveryRates.RecoveryRatesId == 0)
                    updatedEntity = MacroVarRecoveryRatesRepository.Add(MacroVarRecoveryRates);
                else
                    updatedEntity = MacroVarRecoveryRatesRepository.Update(MacroVarRecoveryRates);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroVarRecoveryRates(int InstrumentId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroVarRecoveryRatesRepository MacroVarRecoveryRatesRepository = _DataRepositoryFactory.GetDataRepository<IMacroVarRecoveryRatesRepository>();

                MacroVarRecoveryRatesRepository.Remove(InstrumentId);
            });
        }

        public MacroVarRecoveryRates GetMacroVarRecoveryRatesById(int RecoveryRatesId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroVarRecoveryRatesRepository MacroVarRecoveryRatesRepository = _DataRepositoryFactory.GetDataRepository<IMacroVarRecoveryRatesRepository>();

                MacroVarRecoveryRates MacroVarRecoveryRatesEntity = MacroVarRecoveryRatesRepository.Get(RecoveryRatesId);
                if (MacroVarRecoveryRatesEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroVarRecoveryRates with ID of {0} is not in database", RecoveryRatesId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return MacroVarRecoveryRatesEntity;
            });
        }

        public MacroVarRecoveryRates[] GetAllMacroVarRecoveryRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroVarRecoveryRatesRepository MacroVarRecoveryRatesRepository = _DataRepositoryFactory.GetDataRepository<IMacroVarRecoveryRatesRepository>();

                IEnumerable<MacroVarRecoveryRates> MacroVarRecoveryRatess = MacroVarRecoveryRatesRepository.Get().ToArray().OrderBy(c => c.RecoveryRatesId).ToList();

                return MacroVarRecoveryRatess.ToArray();
            });
        }

        /*public MacroVarRecoveryRates[] GetMacroVarRecoveryRatesById(int RecoveryRatesId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroVarRecoveryRatesRepository MacroVarRecoveryRatesRepository = _DataRepositoryFactory.GetDataRepository<IMacroVarRecoveryRatesRepository>();

                IEnumerable<MacroVarRecoveryRates> MacroVarRecoveryRatesEntity = MacroVarRecoveryRatesRepository.GetEntityByRecoveryRatesId(RecoveryRatesId).OrderBy(c => c.RecoveryRatesId);
                if (MacroVarRecoveryRatesEntity.Count() == 0)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroVarRecoveryRates with id: {0} is not in database", RecoveryRatesId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return MacroVarRecoveryRatesEntity.ToArray();
            });
        }*/

        #endregion

        #region IfrsCorporateEcl operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsCorporateEcl UpdateIfrsCorporateEcl(IfrsCorporateEcl IfrsCorporateEcl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporateEclRepository IfrsCorporateEclRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporateEclRepository>();

                IfrsCorporateEcl updatedEntity = null;

                if (IfrsCorporateEcl.ecl_id == 0)
                    updatedEntity = IfrsCorporateEclRepository.Add(IfrsCorporateEcl);
                else
                    updatedEntity = IfrsCorporateEclRepository.Update(IfrsCorporateEcl);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsCorporateEcl(int IfrsCorporateEclId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporateEclRepository IfrsCorporateEclRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporateEclRepository>();

                IfrsCorporateEclRepository.Remove(IfrsCorporateEclId);
            });
        }

        public IfrsCorporateEcl GetIfrsCorporateEclById(int IfrsCorporateEclId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporateEclRepository IfrsCorporateEclRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporateEclRepository>();

                IfrsCorporateEcl IfrsCorporateEclEntity = IfrsCorporateEclRepository.Get(IfrsCorporateEclId);
                if (IfrsCorporateEclEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsCorporateEcl with ID of {0} is not in database", IfrsCorporateEclId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsCorporateEclEntity;
            });
        }

        public IfrsCorporateEcl[] GetAllIfrsCorporateEcls(bool export)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporateEclRepository IfrsCorporateEclRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporateEclRepository>();

                IEnumerable<IfrsCorporateEcl> IfrsCorporateEcls = IfrsCorporateEclRepository.Get().ToArray().OrderBy(c => c.refno).ToList();

                return IfrsCorporateEcls.ToArray();
            });
        }

        public IfrsCorporateEcl[] GetIfrsCorporateEclByRefNo(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporateEclRepository IfrsCorporateEclRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporateEclRepository>();

                IEnumerable<IfrsCorporateEcl> IfrsCorporateEclEntity = IfrsCorporateEclRepository.GetEntityByRefNo(refNo).OrderBy(c => c.refno);
                if (IfrsCorporateEclEntity.Count() == 0)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsCorporateEcl with refno of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsCorporateEclEntity.ToArray();
            });
        }

        #endregion

        #region InvestmentOthersECL operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public InvestmentOthersECL UpdateInvestmentOthersECL(InvestmentOthersECL investmentOthersECL)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                InvestmentOthersECL updatedEntity = null;

                if (investmentOthersECL.ecl_Id == 0)
                    updatedEntity = investmentOthersECLRepository.Add(investmentOthersECL);
                else
                    updatedEntity = investmentOthersECLRepository.Update(investmentOthersECL);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInvestmentOthersECL(int ecl_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                investmentOthersECLRepository.Remove(ecl_Id);
            });
        }

        public InvestmentOthersECL GetInvestmentOthersECL(int ecl_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                InvestmentOthersECL investmentOthersECLEntity = investmentOthersECLRepository.Get(ecl_Id);
                if (investmentOthersECLEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InvestmentOthersECL with ID of {0} is not in database", ecl_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return investmentOthersECLEntity;
            });
        }

        public InvestmentOthersECL[] GetAllInvestmentOthersECLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                IEnumerable<InvestmentOthersECL> investmentOthersECLs = investmentOthersECLRepository.Get().ToArray();

                return investmentOthersECLs.ToArray();
            });
        }

        public InvestmentOthersECL[] GetInvestmentOthersECLByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                return investmentOthersECLRepository.GetInvestmentOthersECLByRefNo(RefNo).ToArray();
            });
        }

        #endregion

        #region IfrsSectorCCF operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsSectorCCF UpdateIfrsSectorCCF(IfrsSectorCCF IfrsSectorCCF)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsSectorCCFRepository IfrsSectorCCFRepository = _DataRepositoryFactory.GetDataRepository<IIfrsSectorCCFRepository>();

                IfrsSectorCCF updatedEntity = null;

                if (IfrsSectorCCF.SectorId == 0)
                    updatedEntity = IfrsSectorCCFRepository.Add(IfrsSectorCCF);
                else
                    updatedEntity = IfrsSectorCCFRepository.Update(IfrsSectorCCF);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsSectorCCF(int SectorId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsSectorCCFRepository IfrsSectorCCFRepository = _DataRepositoryFactory.GetDataRepository<IIfrsSectorCCFRepository>();

                IfrsSectorCCFRepository.Remove(SectorId);
            });
        }

        public IfrsSectorCCF GetIfrsSectorCCFById(int SectorId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsSectorCCFRepository IfrsSectorCCFRepository = _DataRepositoryFactory.GetDataRepository<IIfrsSectorCCFRepository>();

                IfrsSectorCCF IfrsSectorCCFEntity = IfrsSectorCCFRepository.Get(SectorId);
                if (IfrsSectorCCFEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsSectorCCF with ID of {0} is not in database", SectorId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsSectorCCFEntity;
            });
        }

        public IfrsSectorCCF[] GetAllIfrsSectorCCFs(string Type)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsSectorCCFRepository IfrsSectorCCFRepository = _DataRepositoryFactory.GetDataRepository<IIfrsSectorCCFRepository>();

                IEnumerable<IfrsSectorCCF> IfrsSectorCCFs = IfrsSectorCCFRepository.Get().Where(c => c.type == Type).ToList();

                return IfrsSectorCCFs.ToArray();
            });
        }

        public Sector[] GetAllSectorsForCCF()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository SectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                IEnumerable<Sector> IfrsSectorCCF = SectorRepository.Get().OrderBy(c => c.Description).Where(c => c.Source == "CBN");

                return IfrsSectorCCF.ToArray();
            });
        }

        #endregion
        
        #region SandPRating operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SandPRating UpdateSandPRating(SandPRating sandPRating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISandPRatingRepository sandPRatingRepository = _DataRepositoryFactory.GetDataRepository<ISandPRatingRepository>();

                SandPRating updatedEntity = null;

                if (sandPRating.SandPRating_Id == 0)
                    updatedEntity = sandPRatingRepository.Add(sandPRating);
                else
                    updatedEntity = sandPRatingRepository.Update(sandPRating);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSandPRating(int sandPRatingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISandPRatingRepository sandPRatingRepository = _DataRepositoryFactory.GetDataRepository<ISandPRatingRepository>();

                sandPRatingRepository.Remove(sandPRatingId);
            });
        }

        public SandPRating GetSandPRating(int sandPRatingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISandPRatingRepository sandPRatingRepository = _DataRepositoryFactory.GetDataRepository<ISandPRatingRepository>();

                SandPRating sandPRatingEntity = sandPRatingRepository.Get(sandPRatingId);
                if (sandPRatingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SandPRating with ID of {0} is not in database", sandPRatingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sandPRatingEntity;
            });
        }

        public SandPRating[] GetAllSandPRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISandPRatingRepository sandPRatingRepository = _DataRepositoryFactory.GetDataRepository<ISandPRatingRepository>();

                IEnumerable<SandPRating> sandPRatings = sandPRatingRepository.Get().ToArray();

                return sandPRatings.ToArray();
            });
        }


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

        private decimal GetNegativeAbsolute(decimal value)
        {
            return (value > 0 ? value : (-1 * value));
        }

        protected override string GetContextConnection()
        {
            if (!string.IsNullOrEmpty(_CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
            }

            return string.Empty;
        }

        protected Solution GetSolution(string name)
        {
            ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

            var solution = solutionRepository.Get().Where(c => c.Name == name).FirstOrDefault();

            return solution;
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

        #region ProbabilityWeighted operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProbabilityWeighted UpdateProbabilityWeighted(ProbabilityWeighted probabilityWeighted)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                ProbabilityWeighted updatedEntity = null;

                if (probabilityWeighted.ProbabilityWeighted_Id == 0)
                    updatedEntity = probabilityWeightedRepository.Add(probabilityWeighted);
                else
                    updatedEntity = probabilityWeightedRepository.Update(probabilityWeighted);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                probabilityWeightedRepository.Remove(ProbabilityWeighted_Id);
            });
        }

        public ProbabilityWeighted GetProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                ProbabilityWeighted probabilityWeightedEntity = probabilityWeightedRepository.Get(ProbabilityWeighted_Id);
                if (probabilityWeightedEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProbabilityWeighted with ID of {0} is not in database", ProbabilityWeighted_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return probabilityWeightedEntity;
            });
        }

        public ProbabilityWeighted[] GetAllProbabilityWeighteds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                IEnumerable<ProbabilityWeighted> probabilityWeighteds = probabilityWeightedRepository.Get().ToArray();

                return probabilityWeighteds.ToArray();
            });
        }

        public ProbabilityWeighted[] GetProbabilityWeightedByInstrumentType(string InstrumentType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                return probabilityWeightedRepository.GetProbabilityWeightedByInstrumentType(InstrumentType).ToArray();
            });
        }



        #endregion

        #region MacrovariableEstimate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacrovariableEstimate UpdateMacrovariableEstimate(MacrovariableEstimate macrovariableEstimate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                MacrovariableEstimate updatedEntity = null;

                if (macrovariableEstimate.MacrovariableEstimate_Id == 0)
                    updatedEntity = macrovariableEstimateRepository.Add(macrovariableEstimate);
                else
                    updatedEntity = macrovariableEstimateRepository.Update(macrovariableEstimate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                macrovariableEstimateRepository.Remove(MacrovariableEstimate_Id);
            });
        }

        public MacrovariableEstimate GetMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                MacrovariableEstimate macrovariableEstimateEntity = macrovariableEstimateRepository.Get(MacrovariableEstimate_Id);
                if (macrovariableEstimateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacrovariableEstimate with ID of {0} is not in database", MacrovariableEstimate_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macrovariableEstimateEntity;
            });
        }

        public MacrovariableEstimate[] GetAllMacrovariableEstimates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                IEnumerable<MacrovariableEstimate> macrovariableEstimates = macrovariableEstimateRepository.Get().ToArray();

                return macrovariableEstimates.ToArray();
            });
        }

        public MacrovariableEstimate[] GetMacrovariableEstimateByCategory(string Category)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                return macrovariableEstimateRepository.GetMacrovariableEstimateByCategory(Category).ToArray();
            });
        }



        #endregion

        #region SectorMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorMapping UpdateSectorMapping(SectorMapping sectorMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                SectorMapping updatedEntity = null;

                if (sectorMapping.SectorMapping_Id == 0)
                    updatedEntity = sectorMappingRepository.Add(sectorMapping);
                else
                    updatedEntity = sectorMappingRepository.Update(sectorMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorMapping(int SectorMapping_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                sectorMappingRepository.Remove(SectorMapping_Id);
            });
        }

        public SectorMapping GetSectorMapping(int SectorMapping_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                SectorMapping sectorMappingEntity = sectorMappingRepository.Get(SectorMapping_Id);
                if (sectorMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorMapping with ID of {0} is not in database", SectorMapping_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorMappingEntity;
            });
        }

        public SectorMapping[] GetAllSectorMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                IEnumerable<SectorMapping> sectorMappings = sectorMappingRepository.Get().ToArray();

                return sectorMappings.ToArray();
            });
        }

        //public SectorMapping[] GetSectorMappingBySource(string Source)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

        //        return sectorMappingRepository.GetSectorMappingBySource(Source).ToArray();
        //    });
        //}

        #endregion

        #region Sector operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Sector UpdateSector(Sector sector)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                Sector updatedEntity = null;

                if (sector.SectorId == 0)
                    updatedEntity = sectorRepository.Add(sector);
                else
                    updatedEntity = sectorRepository.Update(sector);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSector(int sectorId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                sectorRepository.Remove(sectorId);
            });
        }

        public Sector GetSector(int sectorId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                Sector sectorEntity = sectorRepository.Get(sectorId);
                if (sectorEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Sector with ID of {0} is not in database", sectorId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorEntity;
            });
        }

        public Sector[] GetAllSectors()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                IEnumerable<Sector> sectors = sectorRepository.Get().ToArray();

                return sectors.ToArray();
            });
        }

        public Sector[] GetSectorBySource(string Source)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                return sectorRepository.GetSectorBySource(Source).ToArray();
            });
        }

        #endregion

        #region HistoricalDefaultedAccounts operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalDefaultedAccounts UpdateHistoricalDefaultedAccount(HistoricalDefaultedAccounts historicalDefaultedAccounts)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalDefaultedAccountsRepository historicalDefaultedAccountsRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultedAccountsRepository>();

                HistoricalDefaultedAccounts updatedEntity = null;

                if (historicalDefaultedAccounts.DefaultedAccountId == 0)
                    updatedEntity = historicalDefaultedAccountsRepository.Add(historicalDefaultedAccounts);
                else
                    updatedEntity = historicalDefaultedAccountsRepository.Update(historicalDefaultedAccounts);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalDefaultedAccount(int DefaultedAccountId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalDefaultedAccountsRepository historicalDefaultedAccountsRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultedAccountsRepository>();

                historicalDefaultedAccountsRepository.Remove(DefaultedAccountId);
            });
        }

        public HistoricalDefaultedAccounts GetHistoricalDefaultedAccount(int historicalDefaultedAccountsId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalDefaultedAccountsRepository historicalDefaultedAccountsRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultedAccountsRepository>();

                HistoricalDefaultedAccounts historicalDefaultedAccountsEntity = historicalDefaultedAccountsRepository.Get(historicalDefaultedAccountsId);
                if (historicalDefaultedAccountsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalDefaultedAccount with ID of {0} is not in database", historicalDefaultedAccountsId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalDefaultedAccountsEntity;
            });
        }

        public HistoricalDefaultedAccounts[] GetAllHistoricalDefaultedAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalDefaultedAccountsRepository historicalDefaultedAccountsRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultedAccountsRepository>();

                IEnumerable<HistoricalDefaultedAccounts> historicalDefaultedAccounts = historicalDefaultedAccountsRepository.Get().ToArray();

                return historicalDefaultedAccounts.ToArray();
            });
        }


        #endregion

        #region OffBalancesheetECL operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OffBalancesheetECL UpdateOffBalancesheetECL(OffBalancesheetECL eclComparism)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalancesheetECLRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IOffBalancesheetECLRepository>();

                OffBalancesheetECL updatedEntity = null;

                if (eclComparism.obe_Id == 0)
                    updatedEntity = eclComparismRepository.Add(eclComparism);
                else
                    updatedEntity = eclComparismRepository.Update(eclComparism);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOffBalancesheetECL(int eclComparismId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalancesheetECLRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IOffBalancesheetECLRepository>();

                eclComparismRepository.Remove(eclComparismId);
            });
        }

        public OffBalancesheetECL GetOffBalancesheetECL(int eclComparismId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalancesheetECLRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IOffBalancesheetECLRepository>();

                OffBalancesheetECL eclComparismEntity = eclComparismRepository.Get(eclComparismId);
                if (eclComparismEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OffBalancesheetECL with Account No or Customer Name of {0} is not in database", eclComparismId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return eclComparismEntity;
            });
        }

        public OffBalancesheetECL[] GetAllOffBalancesheetECLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalancesheetECLRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IOffBalancesheetECLRepository>();

                IEnumerable<OffBalancesheetECL> eclComparisms = eclComparismRepository.Get().ToArray();

                return eclComparisms.ToArray();
            });
        }

        public OffBalancesheetECL[] GetOffBalancesheetECLBySearch(string SearchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalancesheetECLRepository offBalancesheetECLRepository = _DataRepositoryFactory.GetDataRepository<IOffBalancesheetECLRepository>();

                return offBalancesheetECLRepository.GetEntityBySearchParam(SearchParam).OrderBy(c => c.Seq).ToArray();
            });
        }

        public OffBalancesheetECL[] GetOffBalancesheetECLs(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalancesheetECLRepository offBalancesheetECLRepository = _DataRepositoryFactory.GetDataRepository<IOffBalancesheetECLRepository>();

                return offBalancesheetECLRepository.GetOffBalancesheetECLs(defaultCount).OrderBy(c => c.Seq).ToArray();
            });
        }

        #endregion

        #region ImpairmentResultRetail operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ImpairmentResultRetail UpdateImpairmentResultRetail(ImpairmentResultRetail impairmentResultRetail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultRetailRepository impairmentResultRetailRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultRetailRepository>();

                ImpairmentResultRetail updatedEntity = null;

                if (impairmentResultRetail.Impairment_Id == 0)
                    updatedEntity = impairmentResultRetailRepository.Add(impairmentResultRetail);
                else
                    updatedEntity = impairmentResultRetailRepository.Update(impairmentResultRetail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteImpairmentResultRetail(int Impairment_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultRetailRepository impairmentResultRetailRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultRetailRepository>();

                impairmentResultRetailRepository.Remove(Impairment_Id);
            });
        }

        public ImpairmentResultRetail GetImpairmentResultRetail(int Impairment_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultRetailRepository impairmentResultRetailRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultRetailRepository>();

                ImpairmentResultRetail impairmentResultRetailEntity = impairmentResultRetailRepository.Get(Impairment_Id);
                if (impairmentResultRetailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ImpairmentResultRetail with Impairment_Id of {0} is not in database", Impairment_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return impairmentResultRetailEntity;
            });
        }

        public ImpairmentResultRetail[] GetAllImpairmentResultRetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultRetailRepository impairmentResultRetailRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultRetailRepository>();

                IEnumerable<ImpairmentResultRetail> impairmentResultRetails = impairmentResultRetailRepository.Get().ToArray();

                return impairmentResultRetails.ToArray();
            });
        }

        public ImpairmentResultRetail[] GetImpairmentResultRetailBySearch(string SearchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultRetailRepository impairmentResultOBERepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultRetailRepository>();

                return impairmentResultOBERepository.GetEntityBySearchParam(SearchParam).ToArray();
            });
        }

        #endregion

        #region ImpairmentResultOBE operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ImpairmentResultOBE UpdateImpairmentResultOBE(ImpairmentResultOBE impairmentResultOBE)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultOBERepository impairmentResultOBERepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultOBERepository>();

                ImpairmentResultOBE updatedEntity = null;

                if (impairmentResultOBE.Impairment_Id == 0)
                    updatedEntity = impairmentResultOBERepository.Add(impairmentResultOBE);
                else
                    updatedEntity = impairmentResultOBERepository.Update(impairmentResultOBE);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteImpairmentResultOBE(int Impairment_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultOBERepository impairmentResultOBERepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultOBERepository>();

                impairmentResultOBERepository.Remove(Impairment_Id);
            });
        }

        public ImpairmentResultOBE GetImpairmentResultOBE(int Impairment_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultOBERepository impairmentResultOBERepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultOBERepository>();

                ImpairmentResultOBE impairmentResultOBEEntity = impairmentResultOBERepository.Get(Impairment_Id);
                if (impairmentResultOBEEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ImpairmentResultOBE with Impairment_Id of {0} is not in database", Impairment_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return impairmentResultOBEEntity;
            });
        }

        public ImpairmentResultOBE[] GetAllImpairmentResultOBEs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultOBERepository impairmentResultOBERepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultOBERepository>();

                IEnumerable<ImpairmentResultOBE> impairmentResultOBEs = impairmentResultOBERepository.Get().ToArray();

                return impairmentResultOBEs.ToArray();
            });
        }

        public ImpairmentResultOBE[] GetImpairmentResultOBEBySearch(string SearchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentResultOBERepository impairmentResultOBERepository = _DataRepositoryFactory.GetDataRepository<IImpairmentResultOBERepository>();

                return impairmentResultOBERepository.GetEntityBySearchParam(SearchParam).ToArray();
            });
        }

        #endregion

        #region ImpairmentInvestment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ImpairmentInvestment UpdateImpairmentInvestment(ImpairmentInvestment impairmentInvestment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentInvestmentRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentInvestmentRepository>();

                ImpairmentInvestment updatedEntity = null;

                if (impairmentInvestment.Investment_Id == 0)
                    updatedEntity = impairmentInvestmentRepository.Add(impairmentInvestment);
                else
                    updatedEntity = impairmentInvestmentRepository.Update(impairmentInvestment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteImpairmentInvestment(int Investment_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentInvestmentRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentInvestmentRepository>();

                impairmentInvestmentRepository.Remove(Investment_Id);
            });
        }

        public ImpairmentInvestment GetImpairmentInvestment(int Investment_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentInvestmentRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentInvestmentRepository>();

                ImpairmentInvestment impairmentInvestmentEntity = impairmentInvestmentRepository.Get(Investment_Id);
                if (impairmentInvestmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ImpairmentInvestment with Investment_Id of {0} is not in database", Investment_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return impairmentInvestmentEntity;
            });
        }

        public ImpairmentInvestment[] GetAllImpairmentInvestments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentInvestmentRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentInvestmentRepository>();

                IEnumerable<ImpairmentInvestment> impairmentInvestments = impairmentInvestmentRepository.Get().ToArray();

                return impairmentInvestments.ToArray();
            });
        }

        #endregion

        #region ImpairmentCorporate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ImpairmentCorporate UpdateImpairmentCorporate(ImpairmentCorporate impairmentInvestment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentCorporateRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentCorporateRepository>();

                ImpairmentCorporate updatedEntity = null;

                if (impairmentInvestment.Corporate_Id == 0)
                    updatedEntity = impairmentInvestmentRepository.Add(impairmentInvestment);
                else
                    updatedEntity = impairmentInvestmentRepository.Update(impairmentInvestment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteImpairmentCorporate(int Corporate_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentCorporateRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentCorporateRepository>();

                impairmentInvestmentRepository.Remove(Corporate_Id);
            });
        }

        public ImpairmentCorporate GetImpairmentCorporate(int Corporate_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentCorporateRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentCorporateRepository>();

                ImpairmentCorporate impairmentInvestmentEntity = impairmentInvestmentRepository.Get(Corporate_Id);
                if (impairmentInvestmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ImpairmentCorporate with Corporate_Id of {0} is not in database", Corporate_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return impairmentInvestmentEntity;
            });
        }

        public ImpairmentCorporate[] GetAllImpairmentCorporates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentCorporateRepository impairmentInvestmentRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentCorporateRepository>();

                IEnumerable<ImpairmentCorporate> impairmentInvestments = impairmentInvestmentRepository.Get().ToArray();

                return impairmentInvestments.ToArray();
            });
        }

        #endregion

        #region IfrsFinalRetailOutput operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsFinalRetailOutput UpdateIfrsFinalRetailOutput(IfrsFinalRetailOutput IfrsFinalRetailOutput)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsFinalRetailOutputRepository IfrsFinalRetailOutputRepository = _DataRepositoryFactory.GetDataRepository<IIfrsFinalRetailOutputRepository>();

                IfrsFinalRetailOutput updatedEntity = null;

                if (IfrsFinalRetailOutput.FinalRetailId == 0)
                    updatedEntity = IfrsFinalRetailOutputRepository.Add(IfrsFinalRetailOutput);
                else
                    updatedEntity = IfrsFinalRetailOutputRepository.Update(IfrsFinalRetailOutput);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsFinalRetailOutput(int IfrsFinalRetailOutputId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsFinalRetailOutputRepository IfrsFinalRetailOutputRepository = _DataRepositoryFactory.GetDataRepository<IIfrsFinalRetailOutputRepository>();

                IfrsFinalRetailOutputRepository.Remove(IfrsFinalRetailOutputId);
            });
        }

        public IfrsFinalRetailOutput GetIfrsFinalRetailOutput(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsFinalRetailOutputRepository IfrsFinalRetailOutputRepository = _DataRepositoryFactory.GetDataRepository<IIfrsFinalRetailOutputRepository>();

                IfrsFinalRetailOutput IfrsFinalRetailOutputEntity = IfrsFinalRetailOutputRepository.Get(Id);
                if (IfrsFinalRetailOutputEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsFinalRetailOutput with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsFinalRetailOutputEntity;
            });
        }

        public IfrsFinalRetailOutput[] GetAllIfrsFinalRetailOutput()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsFinalRetailOutputRepository IfrsFinalRetailOutputRepository = _DataRepositoryFactory.GetDataRepository<IIfrsFinalRetailOutputRepository>();

                IEnumerable<IfrsFinalRetailOutput> IfrsFinalRetailOutputs = IfrsFinalRetailOutputRepository.Get().ToList();

                return IfrsFinalRetailOutputs.ToArray();
            });
        }

        public IfrsFinalRetailOutput[] GetIfrsFinalRetailOutputByAccountNo(string accountNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsFinalRetailOutputRepository IfrsFinalRetailOutputRepository = _DataRepositoryFactory.GetDataRepository<IIfrsFinalRetailOutputRepository>();

                IEnumerable<IfrsFinalRetailOutput> IfrsFinalRetailOutputEntity = IfrsFinalRetailOutputRepository.GetEntityByAccountNo(accountNo).OrderBy(c => c.Seq);
                if (IfrsFinalRetailOutputEntity.Count() == 0)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsFinalRetailOutput with Account No. of {0} is not in database", accountNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsFinalRetailOutputEntity.ToArray();
            });
        }

        #endregion

        #region IfrsCustomerPD operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsCustomerPD UpdateIfrsCustomerPD(IfrsCustomerPD IfrsCustomerPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerPDRepository IfrsCustomerPDRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerPDRepository>();

                IfrsCustomerPD updatedEntity = null;

                if (IfrsCustomerPD.CustomerPDId == 0)
                    updatedEntity = IfrsCustomerPDRepository.Add(IfrsCustomerPD);
                else
                    updatedEntity = IfrsCustomerPDRepository.Update(IfrsCustomerPD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsCustomerPD(int IfrsCustomerPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerPDRepository IfrsCustomerPDRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerPDRepository>();

                IfrsCustomerPDRepository.Remove(IfrsCustomerPDId);
            });
        }

        public IfrsCustomerPD GetIfrsCustomerPD(int IfrsCustomerPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerPDRepository IfrsCustomerPDRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerPDRepository>();

                IfrsCustomerPD IfrsCustomerPDEntity = IfrsCustomerPDRepository.Get(IfrsCustomerPDId);
                if (IfrsCustomerPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsCustomerPD with ID of {0} is not in database", IfrsCustomerPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsCustomerPDEntity;
            });
        }

        public IfrsCustomerPD[] GetAllIfrsCustomerPDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerPDRepository IfrsCustomerPDRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerPDRepository>();

                IEnumerable<IfrsCustomerPD> IfrsCustomerPDs = IfrsCustomerPDRepository.Get().ToArray().OrderBy(c => c.PD).ToList();

                return IfrsCustomerPDs.ToArray();
            });
        }

        public string[] GetAllCustomerRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerPDRepository IfrsCustomerPDRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerPDRepository>();

                IEnumerable<string> IfrsCustomerPD = IfrsCustomerPDRepository.Get().OrderBy(c => c.Rating).Select(c => c.Rating).Distinct();

                return IfrsCustomerPD.ToArray();
            });
        }

        public IfrsCustomerPD[] GetIfrsCustomerPDByRating(string rating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerPDRepository IfrsCustomerPDRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerPDRepository>();

                IEnumerable<IfrsCustomerPD> IfrsCustomerPDEntity = IfrsCustomerPDRepository.GetEntityByRating(rating).OrderBy(c => c.PD);
                if (IfrsCustomerPDEntity.Count() == 0)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsCustomerPD with rating of {0} is not in database", rating));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsCustomerPDEntity.ToArray();
            });
        }

        #endregion

        #region IfrsCorporatePdSeries operations
        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsCorporatePdSeries UpdateIfrsCorporatePdSeries(IfrsCorporatePdSeries IfrsCorporatePdSeries)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporatePdSeriesRepository IfrsCorporatePdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporatePdSeriesRepository>();

                IfrsCorporatePdSeries updatedEntity = null;

                if (IfrsCorporatePdSeries.Sno == 0)
                    updatedEntity = IfrsCorporatePdSeriesRepository.Add(IfrsCorporatePdSeries);
                else
                    updatedEntity = IfrsCorporatePdSeriesRepository.Update(IfrsCorporatePdSeries);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsCorporatePdSeries(int IfrsCorporatePdSeriesId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporatePdSeriesRepository IfrsCorporatePdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporatePdSeriesRepository>();

                IfrsCorporatePdSeriesRepository.Remove(IfrsCorporatePdSeriesId);
            });
        }

        public IfrsCorporatePdSeries GetIfrsCorporatePdSeriesById(int Sno)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporatePdSeriesRepository IfrsCorporatePdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporatePdSeriesRepository>();

                IfrsCorporatePdSeries IfrsCorporatePdSeriesEntity = IfrsCorporatePdSeriesRepository.Get(Sno);
                if (IfrsCorporatePdSeriesEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsCorporatePdSeries with ID of {0} is not in database", Sno));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsCorporatePdSeriesEntity;
            });
        }

        public IfrsCorporatePdSeries[] GetAvailableIfrsCorporatePdSeries(QueryOptions queryOptions)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporatePdSeriesRepository IfrsCorporatePdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporatePdSeriesRepository>();

                IEnumerable<IfrsCorporatePdSeries> IfrsCorporatePdSeries = IfrsCorporatePdSeriesRepository.GetPaginatedEntities(queryOptions);

                return IfrsCorporatePdSeries.ToArray();
            });
        }

        public IfrsCorporatePdSeries[] GetAllIfrsCorporatePdSeries()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCorporatePdSeriesRepository IfrsCorporatePdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporatePdSeriesRepository>();
                IEnumerable<IfrsCorporatePdSeries> IfrsCorporatePdSeries = null;
                IfrsCorporatePdSeries = IfrsCorporatePdSeriesRepository.Get().ToArray().ToList();

                return IfrsCorporatePdSeries.ToArray();
            });
        }

        public string GetExportIfrsCorporatePdSeries(string Path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                try
                {
                    IIfrsCorporatePdSeriesRepository IfrsCorporatePdSeriesRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCorporatePdSeriesRepository>();

                    string file = IfrsCorporatePdSeriesRepository.GetForExport(Path);

                    return file;
                }
                catch (Exception ex)
                {
                    throw new FaultException<Exception>(ex, ex.Message);
                }
            });
        }

        #endregion

        #region EclInputRetail operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ECLInputRetail UpdatEclInputRetail(ECLInputRetail eclInputRetail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLInputRetailRepository ECLInputRetailRepository = _DataRepositoryFactory.GetDataRepository<IECLInputRetailRepository>();

                ECLInputRetail updatedEntity = null;

                if (eclInputRetail.EclInputRetailId == 0)
                    updatedEntity = ECLInputRetailRepository.Add(eclInputRetail);
                else
                    updatedEntity = ECLInputRetailRepository.Update(eclInputRetail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteEclInputRetail(int eclInputRetailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLInputRetailRepository ECLInputRetailRepository = _DataRepositoryFactory.GetDataRepository<IECLInputRetailRepository>();

                ECLInputRetailRepository.Remove(eclInputRetailId);
            });
        }

        public ECLInputRetail GetEclInputRetail(int eclInputRetailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLInputRetailRepository ECLInputRetailRepository = _DataRepositoryFactory.GetDataRepository<IECLInputRetailRepository>();

                ECLInputRetail ECLInputRetailEntity = ECLInputRetailRepository.Get(eclInputRetailId);
                if (ECLInputRetailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("EclInputRetail with ID of {0} is not in database", eclInputRetailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ECLInputRetailEntity;
            });
        }

        public ECLInputRetail[] GetAllEclInputRetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLInputRetailRepository ECLInputRetailRepository = _DataRepositoryFactory.GetDataRepository<IECLInputRetailRepository>();

                IEnumerable<ECLInputRetail> ECLInputRetails = ECLInputRetailRepository.Get().Where(c => c.SeriesValue == 1).ToArray().Take(100).OrderBy(c => c.account_number);

                return ECLInputRetails.ToArray();
            });
        }

        public ECLInputRetail[] GetAllEclInputRetailsByRefno(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLInputRetailRepository eclInputRetailRepository = _DataRepositoryFactory.GetDataRepository<IECLInputRetailRepository>();

                IEnumerable<ECLInputRetail> eclInputRetails = eclInputRetailRepository.GetAllEclInputRetailsByRefno(refNo).ToArray();

                return eclInputRetails.ToArray();
            });
        }
        #endregion

        #region StaffLoansComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public StaffLoansComputationResult UpdateStaffLoansComputationResult(StaffLoansComputationResult staffLoansComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffLoansComputationResultRepository staffLoansComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IStaffLoansComputationResultRepository>();

                StaffLoansComputationResult updatedEntity = null;

                if (staffLoansComputationResult.StaffLoan_Id == 0)
                    updatedEntity = staffLoansComputationResultRepository.Add(staffLoansComputationResult);
                else
                    updatedEntity = staffLoansComputationResultRepository.Update(staffLoansComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteStaffLoansComputationResult(int StaffLoan_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffLoansComputationResultRepository staffLoansComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IStaffLoansComputationResultRepository>();

                staffLoansComputationResultRepository.Remove(StaffLoan_Id);
            });
        }

        public StaffLoansComputationResult GetStaffLoansComputationResult(int StaffLoan_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffLoansComputationResultRepository staffLoansComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IStaffLoansComputationResultRepository>();

                StaffLoansComputationResult staffLoansComputationResultEntity = staffLoansComputationResultRepository.Get(StaffLoan_Id);
                if (staffLoansComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("StaffLoansComputationResult with StaffLoan_Id of {0} is not in database", StaffLoan_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return staffLoansComputationResultEntity;
            });
        }

        public StaffLoansComputationResult[] GetAllStaffLoansComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffLoansComputationResultRepository staffLoansComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IStaffLoansComputationResultRepository>();

                IEnumerable<StaffLoansComputationResult> staffLoansComputationResults = staffLoansComputationResultRepository.Get().ToArray();

                return staffLoansComputationResults.ToArray();
            });
        }

        public StaffLoansComputationResult[] GetStaffLoansComputationResultBySearch(string SearchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffLoansComputationResultRepository staffLoansComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IStaffLoansComputationResultRepository>();

                return staffLoansComputationResultRepository.GetEntityBySearchParam(SearchParam).ToArray();
            });
        }

        public StaffLoansComputationResult[] GetStaffLoans(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffLoansComputationResultRepository staffLoansComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IStaffLoansComputationResultRepository>();

                return staffLoansComputationResultRepository.GetStaffLoans(defaultCount).ToArray();
            });
        }

        #endregion

        #region SPCumulativePD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SPCumulativePD UpdateSPCumulativePD(SPCumulativePD sPCumulativePD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISPCumulativePDRepository sPCumulativePDRepository = _DataRepositoryFactory.GetDataRepository<ISPCumulativePDRepository>();

                SPCumulativePD updatedEntity = null;

                if (sPCumulativePD.SPCumulative_Id == 0)
                    updatedEntity = sPCumulativePDRepository.Add(sPCumulativePD);
                else
                    updatedEntity = sPCumulativePDRepository.Update(sPCumulativePD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSPCumulativePD(int sPCumulativePDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISPCumulativePDRepository sPCumulativePDRepository = _DataRepositoryFactory.GetDataRepository<ISPCumulativePDRepository>();

                sPCumulativePDRepository.Remove(sPCumulativePDId);
            });
        }

        public SPCumulativePD GetSPCumulativePD(int sPCumulativePDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISPCumulativePDRepository sPCumulativePDRepository = _DataRepositoryFactory.GetDataRepository<ISPCumulativePDRepository>();

                SPCumulativePD sPCumulativePDEntity = sPCumulativePDRepository.Get(sPCumulativePDId);
                if (sPCumulativePDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SPCumulativePD with ID of {0} is not in database", sPCumulativePDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sPCumulativePDEntity;
            });
        }

        public SPCumulativePD[] GetAllSPCumulativePDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISPCumulativePDRepository sPCumulativePDRepository = _DataRepositoryFactory.GetDataRepository<ISPCumulativePDRepository>();

                IEnumerable<SPCumulativePD> sPCumulativePDs = sPCumulativePDRepository.Get().ToArray();

                return sPCumulativePDs.ToArray();
            });
        }


        #endregion

        #region Assumption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Assumption UpdateAssumption(Assumption assumption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssumptionRepository assumptionRepository = _DataRepositoryFactory.GetDataRepository<IAssumptionRepository>();

                Assumption updatedEntity = null;

                if (assumption.InstrumentID == 0)
                    updatedEntity = assumptionRepository.Add(assumption);
                else
                    updatedEntity = assumptionRepository.Update(assumption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAssumption(int assumptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssumptionRepository assumptionRepository = _DataRepositoryFactory.GetDataRepository<IAssumptionRepository>();

                assumptionRepository.Remove(assumptionId);
            });
        }

        public Assumption GetAssumption(int assumptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssumptionRepository assumptionRepository = _DataRepositoryFactory.GetDataRepository<IAssumptionRepository>();

                Assumption assumptionEntity = assumptionRepository.Get(assumptionId);
                if (assumptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Assumption with ID of {0} is not in database", assumptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return assumptionEntity;
            });
        }

        public Assumption[] GetAllAssumptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssumptionRepository assumptionRepository = _DataRepositoryFactory.GetDataRepository<IAssumptionRepository>();

                IEnumerable<Assumption> assumptions = assumptionRepository.Get().ToArray();

                return assumptions.ToArray();
            });
        }


        #endregion


        #region BondsECLComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BondsECLComputationResult UpdateBondsECLComputationResult(BondsECLComputationResult bondseclcomputationresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondsECLComputationResultRepository bondseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLComputationResultRepository>();

                BondsECLComputationResult updatedEntity = null;

                if (bondseclcomputationresult.ID == 0)
                    updatedEntity = bondseclcomputationresultRepository.Add(bondseclcomputationresult);
                else
                    updatedEntity = bondseclcomputationresultRepository.Update(bondseclcomputationresult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBondsECLComputationResult(int bondseclcomputationresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondsECLComputationResultRepository bondseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLComputationResultRepository>();

                bondseclcomputationresultRepository.Remove(bondseclcomputationresultId);
            });
        }

        public BondsECLComputationResult GetBondsECLComputationResult(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondsECLComputationResultRepository bondseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLComputationResultRepository>();

                BondsECLComputationResult bondseclcomputationresultEntity = bondseclcomputationresultRepository.Get(ID);
                if (bondseclcomputationresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondsECLComputationResult with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondseclcomputationresultEntity;
            });
        }

        public BondsECLComputationResult[] GetAllBondsECLComputationResults()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IBondsECLComputationResultRepository bondseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLComputationResultRepository>();
                IEnumerable<BondsECLComputationResult> bondseclcomputationresults = bondseclcomputationresultRepository.Get().ToArray();
                return bondseclcomputationresults.ToArray();
            });
        }

        public BondsECLComputationResult[] GetBondsECLComputationResultBySearch(string searchParam, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IBondsECLComputationResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLComputationResultRepository>();
                IEnumerable<BondsECLComputationResult> marginalpddstrlbs = marginalpddstrlbRepository.GetBondsECLComputationResultBySearch(searchParam, path);
                return marginalpddstrlbs.ToArray();
            });
        }

        public BondsECLComputationResult[] GetBondsECLComputationResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IBondsECLComputationResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLComputationResultRepository>();
                IEnumerable<BondsECLComputationResult> marginalpddstrlbs = marginalpddstrlbRepository.GetBondsECLComputationResults(defaultCount, path);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region MonthlyObeEadSTRLB operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MonthlyObeEadSTRLB UpdateMonthlyObeEadSTRLB(MonthlyObeEadSTRLB monthlyobeeadstrlb)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyObeEadSTRLBRepository monthlyobeeadstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyObeEadSTRLBRepository>();

                MonthlyObeEadSTRLB updatedEntity = null;

                if (monthlyobeeadstrlb.ID == 0)
                    updatedEntity = monthlyobeeadstrlbRepository.Add(monthlyobeeadstrlb);
                else
                    updatedEntity = monthlyobeeadstrlbRepository.Update(monthlyobeeadstrlb);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMonthlyObeEadSTRLB(int monthlyobeeadstrlbId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyObeEadSTRLBRepository monthlyobeeadstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyObeEadSTRLBRepository>();

                monthlyobeeadstrlbRepository.Remove(monthlyobeeadstrlbId);
            });
        }

        public MonthlyObeEadSTRLB GetMonthlyObeEadSTRLB(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyObeEadSTRLBRepository monthlyobeeadstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyObeEadSTRLBRepository>();

                MonthlyObeEadSTRLB monthlyobeeadstrlbEntity = monthlyobeeadstrlbRepository.Get(ID);
                if (monthlyobeeadstrlbEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MonthlyObeEadSTRLB with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return monthlyobeeadstrlbEntity;
            });
        }

        public MonthlyObeEadSTRLB[] GetAllMonthlyObeEadSTRLBs()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMonthlyObeEadSTRLBRepository monthlyobeeadstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyObeEadSTRLBRepository>();
                IEnumerable<MonthlyObeEadSTRLB> monthlyobeeadstrlbs = monthlyobeeadstrlbRepository.Get().ToArray();
                return monthlyobeeadstrlbs.ToArray();
            });
        }

        public MonthlyObeEadSTRLB[] GetMonthlyObeEadSTRLBBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMonthlyObeEadSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyObeEadSTRLBRepository>();
                IEnumerable<MonthlyObeEadSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetMonthlyObeEadSTRLBBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public MonthlyObeEadSTRLB[] GetMonthlyObeEadSTRLBs(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMonthlyObeEadSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyObeEadSTRLBRepository>();
                IEnumerable<MonthlyObeEadSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetMonthlyObeEadSTRLBs(defaultCount);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion


        #region CollateralInput operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CollateralInput UpdateCollateralInput(CollateralInput collateralInput)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInputRepository collateralInputRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInputRepository>();

                CollateralInput updatedEntity = null;

                if (collateralInput.Collateral_Id == 0)
                    updatedEntity = collateralInputRepository.Add(collateralInput);
                else
                    updatedEntity = collateralInputRepository.Update(collateralInput);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCollateralInput(int collateralInputId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInputRepository collateralInputRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInputRepository>();

                collateralInputRepository.Remove(collateralInputId);
            });
        }

        public CollateralInput GetCollateralInput(int collateralInputId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInputRepository collateralInputRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInputRepository>();

                CollateralInput collateralInputEntity = collateralInputRepository.Get(collateralInputId);
                if (collateralInputEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CollateralInput with ID of {0} is not in database", collateralInputId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return collateralInputEntity;
            });
        }

        public CollateralInput[] GetAllCollateralInputs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInputRepository collateralInputRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInputRepository>();

                IEnumerable<CollateralInput> collateralInputs = collateralInputRepository.Get().ToArray();

                return collateralInputs.ToArray();
            });
        }


        #endregion

        #region LoanCommitmentComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanCommitmentComputationResult UpdateLoanCommitmentComputationResult(LoanCommitmentComputationResult loanCommitmentComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanCommitmentComputationResultRepository loanCommitmentComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILoanCommitmentComputationResultRepository>();

                LoanCommitmentComputationResult updatedEntity = null;

                if (loanCommitmentComputationResult.LoanCommitmentComputationResult_Id == 0)
                    updatedEntity = loanCommitmentComputationResultRepository.Add(loanCommitmentComputationResult);
                else
                    updatedEntity = loanCommitmentComputationResultRepository.Update(loanCommitmentComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanCommitmentComputationResult(int loanCommitmentComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanCommitmentComputationResultRepository loanCommitmentComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILoanCommitmentComputationResultRepository>();

                loanCommitmentComputationResultRepository.Remove(loanCommitmentComputationResultId);
            });
        }

        public LoanCommitmentComputationResult GetLoanCommitmentComputationResult(int loanCommitmentComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanCommitmentComputationResultRepository loanCommitmentComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILoanCommitmentComputationResultRepository>();

                LoanCommitmentComputationResult loanCommitmentComputationResultEntity = loanCommitmentComputationResultRepository.Get(loanCommitmentComputationResultId);
                if (loanCommitmentComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanCommitmentComputationResult with LoanCommitmentComputationResult_Id of {0} is not in database", loanCommitmentComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanCommitmentComputationResultEntity;
            });
        }

        public LoanCommitmentComputationResult[] GetAllLoanCommitmentComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanCommitmentComputationResultRepository loanCommitmentComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILoanCommitmentComputationResultRepository>();

                IEnumerable<LoanCommitmentComputationResult> loanCommitmentComputationResults = loanCommitmentComputationResultRepository.Get().ToArray();

                return loanCommitmentComputationResults.ToArray();
            });
        }

        #endregion

        #region LgdInputFactor operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LgdInputFactor UpdateLgdInputFactor(LgdInputFactor lgdInputFactor)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdInputFactorRepository lgdInputFactorRepository = _DataRepositoryFactory.GetDataRepository<ILgdInputFactorRepository>();

                LgdInputFactor updatedEntity = null;

                if (lgdInputFactor.LgdInputFactorId == 0)
                    updatedEntity = lgdInputFactorRepository.Add(lgdInputFactor);
                else
                    updatedEntity = lgdInputFactorRepository.Update(lgdInputFactor);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLgdInputFactor(int lgdInputFactorId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdInputFactorRepository lgdInputFactorRepository = _DataRepositoryFactory.GetDataRepository<ILgdInputFactorRepository>();

                lgdInputFactorRepository.Remove(lgdInputFactorId);
            });
        }

        public LgdInputFactor GetLgdInputFactor(int lgdInputFactorId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdInputFactorRepository lgdInputFactorRepository = _DataRepositoryFactory.GetDataRepository<ILgdInputFactorRepository>();

                LgdInputFactor lgdInputFactorEntity = lgdInputFactorRepository.Get(lgdInputFactorId);
                if (lgdInputFactorEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LgdInputFactor with ID of {0} is not in database", lgdInputFactorId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lgdInputFactorEntity;
            });
        }

        public LgdInputFactor[] GetAllLgdInputFactors()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdInputFactorRepository lgdInputFactorRepository = _DataRepositoryFactory.GetDataRepository<ILgdInputFactorRepository>();

                IEnumerable<LgdInputFactor> lgdInputFactors = lgdInputFactorRepository.Get().ToArray();

                return lgdInputFactors.ToArray();
            });
        }


        #endregion

        #region RegressionOutput operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RegressionOutput UpdateRegressionOutput(RegressionOutput regressionOutput)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRegressionOutputRepository regressionOutputRepository = _DataRepositoryFactory.GetDataRepository<IRegressionOutputRepository>();

                RegressionOutput updatedEntity = null;

                if (regressionOutput.RegressionOutputId == 0)
                    updatedEntity = regressionOutputRepository.Add(regressionOutput);
                else
                    updatedEntity = regressionOutputRepository.Update(regressionOutput);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRegressionOutput(int regressionOutputId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRegressionOutputRepository regressionOutputRepository = _DataRepositoryFactory.GetDataRepository<IRegressionOutputRepository>();

                regressionOutputRepository.Remove(regressionOutputId);
            });
        }

        public RegressionOutput GetRegressionOutput(int regressionOutputId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRegressionOutputRepository regressionOutputRepository = _DataRepositoryFactory.GetDataRepository<IRegressionOutputRepository>();

                RegressionOutput regressionOutputEntity = regressionOutputRepository.Get(regressionOutputId);
                if (regressionOutputEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RegressionOutput with ID of {0} is not in database", regressionOutputId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return regressionOutputEntity;
            });
        }

        public RegressionOutput[] GetAllRegressionOutputs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRegressionOutputRepository regressionOutputRepository = _DataRepositoryFactory.GetDataRepository<IRegressionOutputRepository>();

                IEnumerable<RegressionOutput> regressionOutputs = regressionOutputRepository.Get().ToArray();

                return regressionOutputs.ToArray();
            });
        }


        #endregion

        #region MacroeconomicsVariableScenario operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroeconomicsVariableScenario UpdateMacroeconomicsVariableScenario(MacroeconomicsVariableScenario macroeconomicsVariableScenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicsVariableScenarioRepository macroeconomicsVariableScenarioRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicsVariableScenarioRepository>();

                MacroeconomicsVariableScenario updatedEntity = null;

                if (macroeconomicsVariableScenario.MacroeconomicsId == 0)
                    updatedEntity = macroeconomicsVariableScenarioRepository.Add(macroeconomicsVariableScenario);
                else
                    updatedEntity = macroeconomicsVariableScenarioRepository.Update(macroeconomicsVariableScenario);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroeconomicsVariableScenario(int macroeconomicsVariableScenarioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicsVariableScenarioRepository macroeconomicsVariableScenarioRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicsVariableScenarioRepository>();

                macroeconomicsVariableScenarioRepository.Remove(macroeconomicsVariableScenarioId);
            });
        }

        public MacroeconomicsVariableScenario GetMacroeconomicsVariableScenario(int macroeconomicsVariableScenarioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicsVariableScenarioRepository macroeconomicsVariableScenarioRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicsVariableScenarioRepository>();

                MacroeconomicsVariableScenario macroeconomicsVariableScenarioEntity = macroeconomicsVariableScenarioRepository.Get(macroeconomicsVariableScenarioId);
                if (macroeconomicsVariableScenarioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroeconomicsVariableScenario with ID of {0} is not in database", macroeconomicsVariableScenarioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroeconomicsVariableScenarioEntity;
            });
        }

        public MacroeconomicsVariableScenario[] GetAllMacroeconomicsVariableScenarios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicsVariableScenarioRepository macroeconomicsVariableScenarioRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicsVariableScenarioRepository>();

                IEnumerable<MacroeconomicsVariableScenario> macroeconomicsVariableScenarios = macroeconomicsVariableScenarioRepository.Get().ToArray();

                return macroeconomicsVariableScenarios.ToArray();
            });
        }

        public MacroeconomicsVariableScenario[] GetMacroeconomicsVariableScenariosByFlag(int flag)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicsVariableScenarioRepository macroeconomicsVariableScenarioRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicsVariableScenarioRepository>();

                IEnumerable<MacroeconomicsVariableScenario> macroeconomics = macroeconomicsVariableScenarioRepository.GetEntitiesByFlag(flag).ToArray();

                return macroeconomics.ToArray();
            });
        }


        #endregion

        #region FacilityStaging operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FacilityStaging UpdateFacilityStaging(FacilityStaging facilityStaging)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityStagingRepository facilityStagingRepository = _DataRepositoryFactory.GetDataRepository<IFacilityStagingRepository>();

                FacilityStaging updatedEntity = null;

                if (facilityStaging.facId == 0)
                    updatedEntity = facilityStagingRepository.Add(facilityStaging);
                else
                    updatedEntity = facilityStagingRepository.Update(facilityStaging);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFacilityStaging(int facId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityStagingRepository facilityStagingRepository = _DataRepositoryFactory.GetDataRepository<IFacilityStagingRepository>();

                facilityStagingRepository.Remove(facId);
            });
        }

        public FacilityStaging GetFacilityStaging(int facId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityStagingRepository facilityStagingRepository = _DataRepositoryFactory.GetDataRepository<IFacilityStagingRepository>();

                FacilityStaging facilityStagingEntity = facilityStagingRepository.Get(facId);
                if (facilityStagingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FacilityStaging with ID of {0} is not in database", facId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return facilityStagingEntity;
            });
        }

        public FacilityStaging[] GetAllFacilityStagings(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityStagingRepository facilityStagingRepository = _DataRepositoryFactory.GetDataRepository<IFacilityStagingRepository>();

                IEnumerable<FacilityStaging> facilityStagings = facilityStagingRepository.GetAllFacilityStagings(defaultCount, path);

                return facilityStagings.ToArray();
            });
        }


        public FacilityStaging[] GetEntityByParam(string param)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityStagingRepository facilityStagingRepository = _DataRepositoryFactory.GetDataRepository<IFacilityStagingRepository>();

                IEnumerable<FacilityStaging> facilityStagings = facilityStagingRepository.GetEntityByParam(param).OrderBy(c=> c.Refno).OrderByDescending(c=>c.ReportDate).ToArray();

                return facilityStagings.ToArray();
            });
        }
        #endregion

        #region FacilityClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FacilityClassification UpdateFacilityClassification(FacilityClassification facilityClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityClassificationRepository facilityClassificationRepository = _DataRepositoryFactory.GetDataRepository<IFacilityClassificationRepository>();

                FacilityClassification updatedEntity = null;

                if (facilityClassification.Id == 0)
                    updatedEntity = facilityClassificationRepository.Add(facilityClassification);
                else
                    updatedEntity = facilityClassificationRepository.Update(facilityClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFacilityClassification(int facClassId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityClassificationRepository facClassRepository = _DataRepositoryFactory.GetDataRepository<IFacilityClassificationRepository>();

                facClassRepository.Remove(facClassId);
            });
        }


        public FacilityClassification[] GetFacilityClassificationBySearch(string type, string searchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityClassificationRepository facilityClassificationRepository = _DataRepositoryFactory.GetDataRepository<IFacilityClassificationRepository>();

                IEnumerable<FacilityClassification> facClassRepository = facilityClassificationRepository.GetFacilityClassificationBySearch(type, searchParam);

                return facClassRepository.ToArray();
            });
        }

        public FacilityClassification[] GetFacilityClassification(int defaultcount,string type)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityClassificationRepository facilityClassificationRepository = _DataRepositoryFactory.GetDataRepository<IFacilityClassificationRepository>();

                IEnumerable<FacilityClassification> facClassRepository = facilityClassificationRepository.GetFacilityClassification(defaultcount, type);

                return facClassRepository.ToArray();
            });
        }

        public FacilityClassification GetFacilityClassificationbyId(int facClassId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFacilityClassificationRepository facClassRepository = _DataRepositoryFactory.GetDataRepository<IFacilityClassificationRepository>();

                FacilityClassification facClassEntity = facClassRepository.Get(facClassId);
                if (facClassEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OBE Exposure with ID of {0} is not in database", facClassId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return facClassEntity;
            });
        }
        public string[] GetProductTypes()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            // List<string> pTypes;
            var pTypesList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_get_ProductTypes", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var prodType = new KeyValueModel();
                        if (reader["ProductType"] != DBNull.Value)
                            prodType.Value = reader["ProductType"].ToString();
                        pTypesList.Add(prodType.Value);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return pTypesList.ToArray();
        }

        public string[] GetsubTypes(string productType)
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            // List<string> pTypes;
            var subTypesList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_get_subTypes_by_product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "productType",
                    Value = productType,
                });

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var prodType = new KeyValueModel();
                        if (reader["SubType"] != DBNull.Value)
                            prodType.Value = reader["SubType"].ToString();
                        subTypesList.Add(prodType.Value);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return subTypesList.ToArray();
        }

        #endregion

        #region SICRParameters operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SICRParameters UpdateSICRParameters(SICRParameters sicrParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISICRParametersRepository sicrParametersRepository = _DataRepositoryFactory.GetDataRepository<ISICRParametersRepository>();

                SICRParameters updatedEntity = null;

                if (sicrParam.ID == 0)
                    updatedEntity = sicrParametersRepository.Add(sicrParam);
                else
                    updatedEntity = sicrParametersRepository.Update(sicrParam);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSICRParameters(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISICRParametersRepository sicrParametersRepository = _DataRepositoryFactory.GetDataRepository<ISICRParametersRepository>();

                sicrParametersRepository.Remove(ID);
            });
        }

        public SICRParameters GetSICRParameters(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISICRParametersRepository sicrParametersRepository = _DataRepositoryFactory.GetDataRepository<ISICRParametersRepository>();

                SICRParameters sicrParametersEntity = sicrParametersRepository.Get(ID);
                if (sicrParametersEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SICRParameters with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sicrParametersEntity;
            });
        }

        public SICRParameters[] GetAllSICRParameters()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISICRParametersRepository sicrParametersRepository = _DataRepositoryFactory.GetDataRepository<ISICRParametersRepository>();

                IEnumerable<SICRParameters> sicrParameterss = sicrParametersRepository.Get().ToArray();

                return sicrParameterss.ToArray();
            });
        }


        #endregion

        #region ObeEclComputation operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ObeEclComputation UpdateObeEclComputation(ObeEclComputation obeeclcomputation)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeEclComputationRepository obeeclcomputationRepository = _DataRepositoryFactory.GetDataRepository<IObeEclComputationRepository>();
                ObeEclComputation updatedEntity = null;
                if (obeeclcomputation.ID == 0)
                    updatedEntity = obeeclcomputationRepository.Add(obeeclcomputation);
                else
                    updatedEntity = obeeclcomputationRepository.Update(obeeclcomputation);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteObeEclComputation(int obeeclcomputationId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeEclComputationRepository obeeclcomputationRepository = _DataRepositoryFactory.GetDataRepository<IObeEclComputationRepository>();
                obeeclcomputationRepository.Remove(obeeclcomputationId);
            });
        }

        public ObeEclComputation GetObeEclComputation(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeEclComputationRepository obeeclcomputationRepository = _DataRepositoryFactory.GetDataRepository<IObeEclComputationRepository>();
                ObeEclComputation obeeclcomputationEntity = obeeclcomputationRepository.Get(Id);
                if (obeeclcomputationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ObeEclComputation with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return obeeclcomputationEntity;
            });
        }

        public ObeEclComputation[] GetAllObeEclComputation()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeEclComputationRepository obeeclcomputationRepository = _DataRepositoryFactory.GetDataRepository<IObeEclComputationRepository>();
                IEnumerable<ObeEclComputation> obeeclcomputations = obeeclcomputationRepository.Get().ToArray();
                return obeeclcomputations.ToArray();
            });
        }


        public ObeEclComputation[] GetObeEclComputationBySearch(string searchParam, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeEclComputationRepository obeeclcomputationRepository = _DataRepositoryFactory.GetDataRepository<IObeEclComputationRepository>();
                IEnumerable<ObeEclComputation> obeeclcomputations = obeeclcomputationRepository.GetObeEclComputationBySearch(searchParam, path);
                return obeeclcomputations.ToArray();
            });
        }

        public ObeEclComputation[] GetObeEclComputations(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeEclComputationRepository obeeclcomputationRepository = _DataRepositoryFactory.GetDataRepository<IObeEclComputationRepository>();
                IEnumerable<ObeEclComputation> obeeclcomputations = obeeclcomputationRepository.GetObeEclComputations(defaultCount, path);
                return obeeclcomputations.ToArray();
            });
        }

        #endregion

        #region LoansECLComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoansECLComputationResult UpdateLoansECLComputationResult(LoansECLComputationResult loanseclcomputationresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoansECLComputationResultRepository loanseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<ILoansECLComputationResultRepository>();
                LoansECLComputationResult updatedEntity = null;
                if (loanseclcomputationresult.ID == 0)
                    updatedEntity = loanseclcomputationresultRepository.Add(loanseclcomputationresult);
                else
                    updatedEntity = loanseclcomputationresultRepository.Update(loanseclcomputationresult);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoansECLComputationResult(int loanseclcomputationresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoansECLComputationResultRepository loanseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<ILoansECLComputationResultRepository>();
                loanseclcomputationresultRepository.Remove(loanseclcomputationresultId);
            });
        }

        public LoansECLComputationResult GetLoansECLComputationResult(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoansECLComputationResultRepository loanseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<ILoansECLComputationResultRepository>();
                LoansECLComputationResult loanseclcomputationresultEntity = loanseclcomputationresultRepository.Get(Id);
                if (loanseclcomputationresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoansECLComputationResult with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return loanseclcomputationresultEntity;
            });
        }

        public LoansECLComputationResult[] GetAllLoansECLComputationResult()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoansECLComputationResultRepository loanseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<ILoansECLComputationResultRepository>();
                IEnumerable<LoansECLComputationResult> loanseclcomputationresults = loanseclcomputationresultRepository.Get().ToArray();
                return loanseclcomputationresults.ToArray();
            });
        }


        public LoansECLComputationResult[] GetLoansECLComputationResultBySearch(string searchParam, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoansECLComputationResultRepository loanseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<ILoansECLComputationResultRepository>();
                IEnumerable<LoansECLComputationResult> loanseclcomputationresults = loanseclcomputationresultRepository.GetLoansECLComputationResultBySearch(searchParam, path);
                return loanseclcomputationresults.ToArray();
            });
        }

        public LoansECLComputationResult[] GetLoansECLComputationResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoansECLComputationResultRepository loanseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<ILoansECLComputationResultRepository>();
                IEnumerable<LoansECLComputationResult> loanseclcomputationresults = loanseclcomputationresultRepository.GetLoansECLComputationResults(defaultCount, path);
                return loanseclcomputationresults.ToArray();
            });
        }

        #endregion

        #region MarginalCCFStr operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalCCFStr UpdateMarginalCCFStr(MarginalCCFStr marginalccfstr)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFStrRepository marginalccfstrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFStrRepository>();
                MarginalCCFStr updatedEntity = null;
                if (marginalccfstr.Id == 0)
                    updatedEntity = marginalccfstrRepository.Add(marginalccfstr);
                else
                    updatedEntity = marginalccfstrRepository.Update(marginalccfstr);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalCCFStr(int marginalccfstrId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFStrRepository marginalccfstrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFStrRepository>();
                marginalccfstrRepository.Remove(marginalccfstrId);
            });
        }

        public MarginalCCFStr GetMarginalCCFStr(int marginalccfstrId)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFStrRepository marginalccfstrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFStrRepository>();
                MarginalCCFStr marginalccfstrEntity = marginalccfstrRepository.Get(marginalccfstrId);
                if (marginalccfstrEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalCCFStr with ID of {0} is not in database", marginalccfstrId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return marginalccfstrEntity;
            });
        }

        public MarginalCCFStr[] GetAllMarginalCCFStr()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFStrRepository marginalccfstrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFStrRepository>();
                IEnumerable<MarginalCCFStr> marginalccfstrs = marginalccfstrRepository.Get().ToArray();
                return marginalccfstrs.ToArray();
            });
        }


        public MarginalCCFStr[] GetMarginalCCFStrBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFStrRepository marginalccfstrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFStrRepository>();
                //IEnumerable<MarginalCCFStr> marginalccfstrs = marginalccfstrRepository.GetMarginalCCFStrBySearch(searchParam);
                IEnumerable<MarginalCCFStr> marginalccfstrs = marginalccfstrRepository.Get().Where(c=>c.OBEType == searchParam).OrderBy(c => c.OBEType).ThenBy(c => c.seq).ToArray();
                return marginalccfstrs.ToArray();
            });
        }

        public MarginalCCFStr[] GetMarginalCCFStrs(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFStrRepository marginalccfstrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFStrRepository>();
                IEnumerable<MarginalCCFStr> marginalccfstr = marginalccfstrRepository.GetMarginalCCFStrs(defaultCount).OrderBy(c=> c.OBEType).ThenBy(c=>c.seq).ToArray();
                return marginalccfstr.ToArray();
            });
        }

        #endregion

        #region LoanSignificantFlag operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanSignificantFlag UpdateLoanSignificantFlag(LoanSignificantFlag loansignificantflag)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanSignificantFlagRepository loansignificantflagsRepository = _DataRepositoryFactory.GetDataRepository<ILoanSignificantFlagRepository>();
                LoanSignificantFlag updatedEntity = null;
                if (loansignificantflag.Id == 0)
                    updatedEntity = loansignificantflagsRepository.Add(loansignificantflag);
                else
                    updatedEntity = loansignificantflagsRepository.Update(loansignificantflag);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanSignificantFlag(int Id)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanSignificantFlagRepository loansignificantflagsRepository = _DataRepositoryFactory.GetDataRepository<ILoanSignificantFlagRepository>();
                loansignificantflagsRepository.Remove(Id);
            });
        }

        public LoanSignificantFlag GetLoanSignificantFlag(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanSignificantFlagRepository loansignificantflagsRepository = _DataRepositoryFactory.GetDataRepository<ILoanSignificantFlagRepository>();
                LoanSignificantFlag cashflowEntity = loansignificantflagsRepository.Get(Id);
                if (cashflowEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanSignificantFlag with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return cashflowEntity;
            });
        }

        public LoanSignificantFlag[] GetAllLoanSignificantFlag()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanSignificantFlagRepository loansignificantflagsRepository = _DataRepositoryFactory.GetDataRepository<ILoanSignificantFlagRepository>();
                IEnumerable<LoanSignificantFlag> loansignificantflags = loansignificantflagsRepository.Get().ToArray();
                return loansignificantflags.ToArray();
            });
        }


        public LoanSignificantFlag[] GetLoanSignificantFlagBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanSignificantFlagRepository loansignificantflagsRepository = _DataRepositoryFactory.GetDataRepository<ILoanSignificantFlagRepository>();
                IEnumerable<LoanSignificantFlag> loansignificantflags = loansignificantflagsRepository.GetLoanSignificantFlagBySearch(searchParam);
                return loansignificantflags.ToArray();
            });
        }

        public LoanSignificantFlag[] GetLoanSignificantFlags(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanSignificantFlagRepository loansignificantflagsRepository = _DataRepositoryFactory.GetDataRepository<ILoanSignificantFlagRepository>();
                IEnumerable<LoanSignificantFlag> loansignificantflags = loansignificantflagsRepository.GetLoanSignificantFlags(defaultCount);
                return loansignificantflags.ToArray();
            });
        }

        #endregion

        #region IfrsInvestment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsInvestment UpdateIfrsInvestment(IfrsInvestment ifrsinvestment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsInvestmentRepository ifrsinvestmentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsInvestmentRepository>();

                IfrsInvestment updatedEntity = null;

                if (ifrsinvestment.Id == 0)
                    updatedEntity = ifrsinvestmentRepository.Add(ifrsinvestment);
                else
                    updatedEntity = ifrsinvestmentRepository.Update(ifrsinvestment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsInvestment(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsInvestmentRepository ifrsinvestmentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsInvestmentRepository>();

                ifrsinvestmentRepository.Remove(Id);
            });
        }

        public IfrsInvestment GetIfrsInvestment(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsInvestmentRepository ifrsinvestmentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsInvestmentRepository>();

                IfrsInvestment ifrsinvestmentEntity = ifrsinvestmentRepository.Get(Id);
                if (ifrsinvestmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsInvestment with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsinvestmentEntity;
            });
        }

        public IfrsInvestment[] GetAllIfrsInvestments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsInvestmentRepository ifrsinvestmentRepository = _DataRepositoryFactory.GetDataRepository<IIfrsInvestmentRepository>();

                IEnumerable<IfrsInvestment> ifrsinvestments = ifrsinvestmentRepository.Get().ToArray();

                return ifrsinvestments.ToArray();
            });
        }




        #endregion

        #region IfrsBondLGD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsBondLGD UpdateIfrsBondLGD(IfrsBondLGD ifrsbondlgd)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondLGDRepository ifrsbondlgdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondLGDRepository>();

                IfrsBondLGD updatedEntity = null;

                if (ifrsbondlgd.Id == 0)
                    updatedEntity = ifrsbondlgdRepository.Add(ifrsbondlgd);
                else
                    updatedEntity = ifrsbondlgdRepository.Update(ifrsbondlgd);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsBondLGD(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondLGDRepository ifrsbondlgdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondLGDRepository>();

                ifrsbondlgdRepository.Remove(Id);
            });
        }

        public IfrsBondLGD GetIfrsBondLGD(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondLGDRepository ifrsbondlgdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondLGDRepository>();

                IfrsBondLGD ifrsbondlgdEntity = ifrsbondlgdRepository.Get(Id);
                if (ifrsbondlgdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsBondLGD with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsbondlgdEntity;
            });
        }


        public IfrsBondLGD[] GetRecordByRefNo(string searchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondLGDRepository ifrsbondlgdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondLGDRepository>();

                IfrsBondLGD[] ifrsbondlgdEntity = ifrsbondlgdRepository.GetRecordByRefNo(searchParam).ToArray();
                if (ifrsbondlgdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsBondLGD with RefNo of {0} is not in database", searchParam));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsbondlgdEntity;
            });
        }

        public IfrsBondLGD[] GetAllIfrsBondLGD(int defaultcount, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondLGDRepository ifrsbondlgdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondLGDRepository>();

                IEnumerable<IfrsBondLGD> ifrsbondlgds = ifrsbondlgdRepository.GetAllIfrsBondLGD(defaultcount, path).OrderBy(c => c.RefNo).ThenBy(c => c.date_pmt);

                return ifrsbondlgds.ToArray();
            });
        }




        #endregion

        #region MarginalPddSTRLB operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalPddSTRLB UpdateMarginalPddSTRLB(MarginalPddSTRLB marginalpddstrlb)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPddSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPddSTRLBRepository>();
                MarginalPddSTRLB updatedEntity = null;
                if (marginalpddstrlb.ID == 0)
                    updatedEntity = marginalpddstrlbRepository.Add(marginalpddstrlb);
                else
                    updatedEntity = marginalpddstrlbRepository.Update(marginalpddstrlb);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalPddSTRLB(int marginalpddstrlbId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPddSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPddSTRLBRepository>();
                marginalpddstrlbRepository.Remove(marginalpddstrlbId);
            });
        }

        public MarginalPddSTRLB GetMarginalPddSTRLB(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPddSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPddSTRLBRepository>();
                MarginalPddSTRLB marginalpddstrlbEntity = marginalpddstrlbRepository.Get(Id);
                if (marginalpddstrlbEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalPddSTRLB with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return marginalpddstrlbEntity;
            });
        }

        public MarginalPddSTRLB[] GetAllMarginalPddSTRLB()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPddSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPddSTRLBRepository>();
                IEnumerable<MarginalPddSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.Get().ToArray();
                return marginalpddstrlbs.ToArray();
            });
        }


        public MarginalPddSTRLB[] GetMarginalPddSTRLBBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPddSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPddSTRLBRepository>();
                IEnumerable<MarginalPddSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetMarginalPddSTRLBBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public MarginalPddSTRLB[] GetMarginalPddSTRLBs(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPddSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPddSTRLBRepository>();
                IEnumerable<MarginalPddSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetMarginalPddSTRLBs(defaultCount, path);
                return marginalpddstrlbs.ToArray();
            });
        }

        #endregion

        #region ODEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ODEclComputationResult UpdateODEclComputationResult(ODEclComputationResult odeclcomputationresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IODEclComputationResultRepository odeclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IODEclComputationResultRepository>();
                ODEclComputationResult updatedEntity = null;
                if (odeclcomputationresult.ID == 0)
                    updatedEntity = odeclcomputationresultRepository.Add(odeclcomputationresult);
                else
                    updatedEntity = odeclcomputationresultRepository.Update(odeclcomputationresult);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteODEclComputationResult(int odeclcomputationresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IODEclComputationResultRepository odeclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IODEclComputationResultRepository>();
                odeclcomputationresultRepository.Remove(odeclcomputationresultId);
            });
        }

        public ODEclComputationResult GetODEclComputationResult(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IODEclComputationResultRepository odeclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IODEclComputationResultRepository>();
                ODEclComputationResult odeclcomputationresultEntity = odeclcomputationresultRepository.Get(Id);
                if (odeclcomputationresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ODEclComputationResult with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return odeclcomputationresultEntity;
            });
        }

        public ODEclComputationResult[] GetAllODEclComputationResult()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IODEclComputationResultRepository odeclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IODEclComputationResultRepository>();
                IEnumerable<ODEclComputationResult> odeclcomputationresults = odeclcomputationresultRepository.Get().ToArray();
                return odeclcomputationresults.ToArray();
            });
        }


        public ODEclComputationResult[] GetODEclComputationResultBySearch(string searchParam, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IODEclComputationResultRepository odeclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IODEclComputationResultRepository>();
                IEnumerable<ODEclComputationResult> odeclcomputationresults = odeclcomputationresultRepository.GetODEclComputationResultBySearch(searchParam, path);
                return odeclcomputationresults.ToArray();
            });
        }

        public ODEclComputationResult[] GetODEclComputationResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IODEclComputationResultRepository odeclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<IODEclComputationResultRepository>();
                IEnumerable<ODEclComputationResult> odeclcomputationresults = odeclcomputationresultRepository.GetODEclComputationResults(defaultCount, path);
                return odeclcomputationresults.ToArray();
            });
        }

        #endregion

        #region MarginalPdObeDistr operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalPdObeDistr UpdateMarginalPdObeDistr(MarginalPdObeDistr marginalpdobedistr)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdObeDistrRepository marginalpdobedistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdObeDistrRepository>();
                MarginalPdObeDistr updatedEntity = null;
                if (marginalpdobedistr.ID == 0)
                    updatedEntity = marginalpdobedistrRepository.Add(marginalpdobedistr);
                else
                    updatedEntity = marginalpdobedistrRepository.Update(marginalpdobedistr);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalPdObeDistr(int marginalpdobedistrId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdObeDistrRepository marginalpdobedistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdObeDistrRepository>();
                marginalpdobedistrRepository.Remove(marginalpdobedistrId);
            });
        }

        public MarginalPdObeDistr GetMarginalPdObeDistr(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdObeDistrRepository marginalpdobedistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdObeDistrRepository>();
                MarginalPdObeDistr marginalpdobedistrEntity = marginalpdobedistrRepository.Get(Id);
                if (marginalpdobedistrEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalPdObeDistr with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return marginalpdobedistrEntity;
            });
        }

        public MarginalPdObeDistr[] GetAllMarginalPdObeDistr()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdObeDistrRepository marginalpdobedistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdObeDistrRepository>();
                IEnumerable<MarginalPdObeDistr> marginalpdobedistrs = marginalpdobedistrRepository.Get().ToArray();
                return marginalpdobedistrs.ToArray();
            });
        }


        public MarginalPdObeDistr[] GetMarginalPdObeDistrBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdObeDistrRepository marginalpdobedistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdObeDistrRepository>();
                IEnumerable<MarginalPdObeDistr> marginalpdobedistrs = marginalpdobedistrRepository.GetMarginalPdObeDistrBySearch(searchParam);
                return marginalpdobedistrs.ToArray();
            });
        }

        public MarginalPdObeDistr[] GetMarginalPdObeDistrs(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdObeDistrRepository marginalpdobedistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdObeDistrRepository>();
                IEnumerable<MarginalPdObeDistr> marginalpdobedistrs = marginalpdobedistrRepository.GetMarginalPdObeDistrs(defaultCount);
                return marginalpdobedistrs.ToArray();
            });
        }

        #endregion

        #region MarginalPdODDistr operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalPdODDistr UpdateMarginalPdODDistr(MarginalPdODDistr marginalpdODdistr)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdODDistrRepository marginalpdODdistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdODDistrRepository>();
                MarginalPdODDistr updatedEntity = null;
                if (marginalpdODdistr.ID == 0)
                    updatedEntity = marginalpdODdistrRepository.Add(marginalpdODdistr);
                else
                    updatedEntity = marginalpdODdistrRepository.Update(marginalpdODdistr);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalPdODDistr(int marginalpdODdistrId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdODDistrRepository marginalpdODdistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdODDistrRepository>();
                marginalpdODdistrRepository.Remove(marginalpdODdistrId);
            });
        }

        public MarginalPdODDistr GetMarginalPdODDistr(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdODDistrRepository marginalpdODdistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdODDistrRepository>();
                MarginalPdODDistr marginalpdODdistrEntity = marginalpdODdistrRepository.Get(Id);
                if (marginalpdODdistrEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalPdODDistr with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return marginalpdODdistrEntity;
            });
        }

        public MarginalPdODDistr[] GetAllMarginalPdODDistr()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdODDistrRepository marginalpdODdistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdODDistrRepository>();
                IEnumerable<MarginalPdODDistr> marginalpdODdistrs = marginalpdODdistrRepository.Get().ToArray();
                return marginalpdODdistrs.ToArray();
            });
        }


        public MarginalPdODDistr[] GetMarginalPdODDistrBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdODDistrRepository marginalpdODdistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdODDistrRepository>();
                IEnumerable<MarginalPdODDistr> marginalpdODdistrs = marginalpdODdistrRepository.GetMarginalPdODDistrBySearch(searchParam);
                return marginalpdODdistrs.ToArray();
            });
        }

        public MarginalPdODDistr[] GetMarginalPdODDistrs(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalPdODDistrRepository marginalpdODdistrRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPdODDistrRepository>();
                IEnumerable<MarginalPdODDistr> marginalpdODdistrs = marginalpdODdistrRepository.GetMarginalPdODDistrs(defaultCount);
                return marginalpdODdistrs.ToArray();
            });
        }

        #endregion

        #region LGDComptResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LGDComptResult UpdateLGDComptResult(LGDComptResult lgdcomptresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILGDComptResultRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<ILGDComptResultRepository>();
                LGDComptResult updatedEntity = null;
                if (lgdcomptresult.Id == 0)
                    updatedEntity = lgdcomptresultRepository.Add(lgdcomptresult);
                else
                    updatedEntity = lgdcomptresultRepository.Update(lgdcomptresult);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLGDComptResult(int lgdcomptresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILGDComptResultRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<ILGDComptResultRepository>();
                lgdcomptresultRepository.Remove(lgdcomptresultId);
            });
        }

        public LGDComptResult GetLGDComptResult(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILGDComptResultRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<ILGDComptResultRepository>();
                LGDComptResult lgdcomptresultEntity = lgdcomptresultRepository.Get(Id);
                if (lgdcomptresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LGDComptResult with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return lgdcomptresultEntity;
            });
        }

        public LGDComptResult[] GetAllLGDComptResult()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILGDComptResultRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<ILGDComptResultRepository>();
                IEnumerable<LGDComptResult> lgdcomptresults = lgdcomptresultRepository.Get().ToArray();
                return lgdcomptresults.ToArray();
            });
        }


        public LGDComptResult[] GetLGDComptResultBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILGDComptResultRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<ILGDComptResultRepository>();
                IEnumerable<LGDComptResult> lgdcomptresults = lgdcomptresultRepository.GetLGDComptResultBySearch(searchParam);
                return lgdcomptresults.ToArray();
            });
        }

        public LGDComptResult[] GetLGDComptResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILGDComptResultRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<ILGDComptResultRepository>();
                IEnumerable<LGDComptResult> lgdcomptresults = lgdcomptresultRepository.GetLGDComptResults(defaultCount, path);
                return lgdcomptresults.ToArray();
            });
        }

        #endregion

        #region ObeLGDComptResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ObeLGDComptResult UpdateObeLGDComptResult(ObeLGDComptResult obelgdcomptresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeLGDComptResultRepository obelgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IObeLGDComptResultRepository>();
                ObeLGDComptResult updatedEntity = null;
                if (obelgdcomptresult.Id == 0)
                    updatedEntity = obelgdcomptresultRepository.Add(obelgdcomptresult);
                else
                    updatedEntity = obelgdcomptresultRepository.Update(obelgdcomptresult);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteObeLGDComptResult(int obelgdcomptresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeLGDComptResultRepository obelgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IObeLGDComptResultRepository>();
                obelgdcomptresultRepository.Remove(obelgdcomptresultId);
            });
        }

        public ObeLGDComptResult GetObeLGDComptResult(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeLGDComptResultRepository obelgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IObeLGDComptResultRepository>();
                ObeLGDComptResult obelgdcomptresultEntity = obelgdcomptresultRepository.Get(Id);
                if (obelgdcomptresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ObeLGDComptResult with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return obelgdcomptresultEntity;
            });
        }

        public ObeLGDComptResult[] GetAllObeLGDComptResult()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeLGDComptResultRepository obelgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IObeLGDComptResultRepository>();
                IEnumerable<ObeLGDComptResult> obelgdcomptresults = obelgdcomptresultRepository.Get().ToArray();
                return obelgdcomptresults.ToArray();
            });
        }


        public ObeLGDComptResult[] GetObeLGDComptResultBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeLGDComptResultRepository obelgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IObeLGDComptResultRepository>();
                IEnumerable<ObeLGDComptResult> obelgdcomptresults = obelgdcomptresultRepository.GetObeLGDComptResultBySearch(searchParam);
                return obelgdcomptresults.ToArray();
            });
        }

        public ObeLGDComptResult[] GetObeLGDComptResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeLGDComptResultRepository obelgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IObeLGDComptResultRepository>();
                IEnumerable<ObeLGDComptResult> obelgdcomptresults = obelgdcomptresultRepository.GetObeLGDComptResults(defaultCount, path);
                return obelgdcomptresults.ToArray();
            });
        }

        #endregion

        #region IfrsBondsMonthlyEAD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsBondsMonthlyEAD UpdateIfrsBondsMonthlyEAD(IfrsBondsMonthlyEAD ifrsbondsmonthlyead)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondsMonthlyEADRepository ifrsbondsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondsMonthlyEADRepository>();

                IfrsBondsMonthlyEAD updatedEntity = null;

                if (ifrsbondsmonthlyead.Id == 0)
                    updatedEntity = ifrsbondsmonthlyeadRepository.Add(ifrsbondsmonthlyead);
                else
                    updatedEntity = ifrsbondsmonthlyeadRepository.Update(ifrsbondsmonthlyead);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsBondsMonthlyEAD(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondsMonthlyEADRepository ifrsbondsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondsMonthlyEADRepository>();

                ifrsbondsmonthlyeadRepository.Remove(Id);
            });
        }

        public IfrsBondsMonthlyEAD GetIfrsBondsMonthlyEAD(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondsMonthlyEADRepository ifrsbondsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondsMonthlyEADRepository>();

                IfrsBondsMonthlyEAD ifrsbondsmonthlyeadEntity = ifrsbondsmonthlyeadRepository.Get(Id);
                if (ifrsbondsmonthlyeadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsBondsMonthlyEAD with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsbondsmonthlyeadEntity;
            });
        }

        public IfrsBondsMonthlyEAD[] GetAllIfrsBondsMonthlyEAD(int defaultcount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsBondsMonthlyEADRepository ifrsbondsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondsMonthlyEADRepository>();

                IEnumerable<IfrsBondsMonthlyEAD> ifrsbondsmonthlyeads = ifrsbondsmonthlyeadRepository.Get().Take(defaultcount).ToArray();

                return ifrsbondsmonthlyeads.ToArray();
            });
        }

        public IfrsBondsMonthlyEAD[] GetIfrsBondMonthlyEADBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsBondsMonthlyEADRepository ifrsbondmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsBondsMonthlyEADRepository>();
                IEnumerable<IfrsBondsMonthlyEAD> ifrsbondmonthlyeads = ifrsbondmonthlyeadRepository.GetIfrsBondMonthlyEADBySearch(searchParam).ToArray();
                return ifrsbondmonthlyeads.ToArray();
            });
        }

        

        #endregion

        #region IfrsMonthlyEAD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsMonthlyEAD UpdateIfrsMonthlyEAD(IfrsMonthlyEAD ifrsmonthlyead)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyEADRepository ifrsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyEADRepository>();

                IfrsMonthlyEAD updatedEntity = null;

                if (ifrsmonthlyead.Id == 0)
                    updatedEntity = ifrsmonthlyeadRepository.Add(ifrsmonthlyead);
                else
                    updatedEntity = ifrsmonthlyeadRepository.Update(ifrsmonthlyead);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsMonthlyEAD(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyEADRepository ifrsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyEADRepository>();

                ifrsmonthlyeadRepository.Remove(Id);
            });
        }

        public IfrsMonthlyEAD GetIfrsMonthlyEAD(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyEADRepository ifrsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyEADRepository>();

                IfrsMonthlyEAD ifrsmonthlyeadEntity = ifrsmonthlyeadRepository.Get(Id);
                if (ifrsmonthlyeadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsMonthlyEAD with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsmonthlyeadEntity;
            });
        }

        public IfrsMonthlyEAD[] GetIfrsMonthlyEADBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsMonthlyEADRepository ifrsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyEADRepository>();
                IEnumerable<IfrsMonthlyEAD> ifrsmonthlyeads = ifrsmonthlyeadRepository.GetIfrsMonthlyEADBySearch(searchParam);
                return ifrsmonthlyeads.ToArray();
            });
        }

        public IfrsMonthlyEAD[] GetAllIfrsMonthlyEAD(int defaultcount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyEADRepository ifrsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyEADRepository>();

                IEnumerable<IfrsMonthlyEAD> ifrsmonthlyeads = ifrsmonthlyeadRepository.Get().Take(defaultcount).ToArray();

                return ifrsmonthlyeads.ToArray();
            });
        }
                #endregion

        #region LoanClassificationSICRSignFlag operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanClassificationSICRSignFlag UpdateLoanClassificationSICRSignFlag(LoanClassificationSICRSignFlag loanClassSignFlag)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanClassificationSICRFlagRepository ifrsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<ILoanClassificationSICRFlagRepository>();

                LoanClassificationSICRSignFlag updatedEntity = null;

                if (loanClassSignFlag.Id == 0)
                    updatedEntity = ifrsmonthlyeadRepository.Add(loanClassSignFlag);
                else
                    updatedEntity = ifrsmonthlyeadRepository.Update(loanClassSignFlag);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanClassificationSICRSignFlag(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanClassificationSICRFlagRepository loanClassSICRSignFlagRepository = _DataRepositoryFactory.GetDataRepository<ILoanClassificationSICRFlagRepository>();

                loanClassSICRSignFlagRepository.Remove(Id);
            });
        }

        public LoanClassificationSICRSignFlag GetLoanClassificationSICRSignFlag(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanClassificationSICRFlagRepository loanClassSICRSignFlagRepository = _DataRepositoryFactory.GetDataRepository<ILoanClassificationSICRFlagRepository>();

                LoanClassificationSICRSignFlag loanClassSICRSignFlagEntity = loanClassSICRSignFlagRepository.Get(Id);
                if (loanClassSICRSignFlagEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanClassificationSICRSignFlag with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanClassSICRSignFlagEntity;
            });
        }

        public KeyValueData[] GetGroupedClassification()
        {
            
            var connectionString = GetDataConnection();
            
            var grpList = new List<KeyValueData>();
           // var kvModel = new List<KeyValueModel>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_get_grouping", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var grpType = new KeyValueData();
                        if (reader["ID"] != DBNull.Value)
                            grpType.Key = reader["ID"].ToString();
                        if (reader["GrpDescription"] != DBNull.Value)
                            grpType.Value = reader["GrpDescription"].ToString();
                        grpList.Add(grpType);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return grpList.ToArray();
        }

        public KeyValueData[] GetSplitClassification(int loanClassId)
        {

            var connectionString = GetDataConnection();

            var grpList = new List<KeyValueData>();
            // var kvModel = new List<KeyValueModel>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_get_splited_classification", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "loanClassId",
                    Value = loanClassId,
                });
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var grpType = new KeyValueData();
                        if (reader["ProductType"] != DBNull.Value)
                            grpType.Key = reader["ProductType"].ToString();
                        if (reader["SubType"] != DBNull.Value)
                            grpType.Value = reader["SubType"].ToString();
                        grpList.Add(grpType);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return grpList.ToArray();
        }        

        public LoanClassificationSignificantFlagData[] GetAllLoanClassificationSICRSignFlagData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanClassificationSICRFlagRepository loanClassSignFlagRepository = _DataRepositoryFactory.GetDataRepository<ILoanClassificationSICRFlagRepository>();


                List<LoanClassificationSignificantFlagData> loanClassSICRSignFlag = new List<LoanClassificationSignificantFlagData>();
                IEnumerable<loanClassSignFlagInfo> loanClassSignFlagInfos = loanClassSignFlagRepository.GetAllLoanClassificationSICRSignFlagData().OrderBy(C=>C.loanClassSignFlag.LoanClassificationId).ThenBy(c=>c.loanClassSignFlag.SICR_ParamID).ToArray();

                foreach (var loanClassSignFlagInfo in loanClassSignFlagInfos)
                {
                    loanClassSICRSignFlag.Add(
                        new LoanClassificationSignificantFlagData
                        {
                            Id = loanClassSignFlagInfo.loanClassSignFlag.EntityId,
                            LoanClassificationId= loanClassSignFlagInfo.loanClassSignFlag.LoanClassificationId,
                            ProductType = loanClassSignFlagInfo.loanClassSignFlag.ProductType,
                            SubType = loanClassSignFlagInfo.loanClassSignFlag.SubType,
                            SICR_Flag = loanClassSignFlagInfo.loanClassSignFlag.SICR_Flag,                           
                            SICR_ParamID = loanClassSignFlagInfo.loanClassSignFlag.SICR_ParamID,
                            SICRParameterName = loanClassSignFlagInfo.SICRParameter.SICR_Param 
                        });
                }

                return loanClassSICRSignFlag.ToArray();
            });
        }

        #endregion
        
        #region LoanECLResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanECLResult UpdateLoanECLResult(LoanECLResult loaneclresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanECLResultRepository loaneclresultRepository = _DataRepositoryFactory.GetDataRepository<ILoanECLResultRepository>();

                LoanECLResult updatedEntity = null;

                if (loaneclresult.ID == 0)
                    updatedEntity = loaneclresultRepository.Add(loaneclresult);
                else
                    updatedEntity = loaneclresultRepository.Update(loaneclresult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanECLResult(int loaneclresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanECLResultRepository loaneclresultRepository = _DataRepositoryFactory.GetDataRepository<ILoanECLResultRepository>();

                loaneclresultRepository.Remove(loaneclresultId);
            });
        }

        public LoanECLResult GetLoanECLResult(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanECLResultRepository loaneclresultRepository = _DataRepositoryFactory.GetDataRepository<ILoanECLResultRepository>();

                LoanECLResult loaneclresultEntity = loaneclresultRepository.Get(ID);
                if (loaneclresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanECLResult with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loaneclresultEntity;
            });
        }

        public LoanECLResult[] GetAllLoanECLResults()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanECLResultRepository loaneclresultRepository = _DataRepositoryFactory.GetDataRepository<ILoanECLResultRepository>();
                IEnumerable<LoanECLResult> loaneclresults = loaneclresultRepository.Get().ToArray();
                return loaneclresults.ToArray();
            });
        }

        public LoanECLResult[] GetLoanECLResultBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ILoanECLResultRepository>();
                IEnumerable<LoanECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetLoanECLResultBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public LoanECLResult[] GetLoanECLResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ILoanECLResultRepository>();
                IEnumerable<LoanECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetLoanECLResults(defaultCount, path);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

    
        #region ObeECLResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ObeECLResult UpdateObeECLResult(ObeECLResult obeeclresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IObeECLResultRepository obeeclresultRepository = _DataRepositoryFactory.GetDataRepository<IObeECLResultRepository>();

                ObeECLResult updatedEntity = null;

                if (obeeclresult.ID == 0)
                    updatedEntity = obeeclresultRepository.Add(obeeclresult);
                else
                    updatedEntity = obeeclresultRepository.Update(obeeclresult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteObeECLResult(int obeeclresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IObeECLResultRepository obeeclresultRepository = _DataRepositoryFactory.GetDataRepository<IObeECLResultRepository>();

                obeeclresultRepository.Remove(obeeclresultId);
            });
        }

        public ObeECLResult GetObeECLResult(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IObeECLResultRepository obeeclresultRepository = _DataRepositoryFactory.GetDataRepository<IObeECLResultRepository>();

                ObeECLResult obeeclresultEntity = obeeclresultRepository.Get(ID);
                if (obeeclresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ObeECLResult with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return obeeclresultEntity;
            });
        }

        public ObeECLResult[] GetAllObeECLResults()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeECLResultRepository obeeclresultRepository = _DataRepositoryFactory.GetDataRepository<IObeECLResultRepository>();
                IEnumerable<ObeECLResult> obeeclresults = obeeclresultRepository.Get().ToArray();
                return obeeclresults.ToArray();
            });
        }

        public ObeECLResult[] GetObeECLResultBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IObeECLResultRepository>();
                IEnumerable<ObeECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetObeECLResultBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public ObeECLResult[] GetObeECLResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IObeECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IObeECLResultRepository>();
                IEnumerable<ObeECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetObeECLResults(defaultCount, path);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region BondsECLResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BondsECLResult UpdateBondsECLResult(BondsECLResult bondseclresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondsECLResultRepository bondseclresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLResultRepository>();

                BondsECLResult updatedEntity = null;

                if (bondseclresult.ID == 0)
                    updatedEntity = bondseclresultRepository.Add(bondseclresult);
                else
                    updatedEntity = bondseclresultRepository.Update(bondseclresult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBondsECLResult(int bondseclresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondsECLResultRepository bondseclresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLResultRepository>();

                bondseclresultRepository.Remove(bondseclresultId);
            });
        }

        public BondsECLResult GetBondsECLResult(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondsECLResultRepository bondseclresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLResultRepository>();

                BondsECLResult bondseclresultEntity = bondseclresultRepository.Get(ID);
                if (bondseclresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondsECLResult with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondseclresultEntity;
            });
        }

        public BondsECLResult[] GetAllBondsECLResults()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IBondsECLResultRepository bondseclresultRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLResultRepository>();
                IEnumerable<BondsECLResult> bondseclresults = bondseclresultRepository.Get().ToArray();
                return bondseclresults.ToArray();
            });
        }

        public BondsECLResult[] GetBondsECLResultBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IBondsECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLResultRepository>();
                IEnumerable<BondsECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetBondsECLResultBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public BondsECLResult[] GetBondsECLResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IBondsECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IBondsECLResultRepository>();
                IEnumerable<BondsECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetBondsECLResults(defaultCount, path);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region OdECLResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OdECLResult UpdateOdECLResult(OdECLResult odeclresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOdECLResultRepository odeclresultRepository = _DataRepositoryFactory.GetDataRepository<IOdECLResultRepository>();

                OdECLResult updatedEntity = null;

                if (odeclresult.ID == 0)
                    updatedEntity = odeclresultRepository.Add(odeclresult);
                else
                    updatedEntity = odeclresultRepository.Update(odeclresult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOdECLResult(int odeclresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOdECLResultRepository odeclresultRepository = _DataRepositoryFactory.GetDataRepository<IOdECLResultRepository>();

                odeclresultRepository.Remove(odeclresultId);
            });
        }

        public OdECLResult GetOdECLResult(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOdECLResultRepository odeclresultRepository = _DataRepositoryFactory.GetDataRepository<IOdECLResultRepository>();

                OdECLResult odeclresultEntity = odeclresultRepository.Get(ID);
                if (odeclresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OdECLResult with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return odeclresultEntity;
            });
        }

        public OdECLResult[] GetAllOdECLResults()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOdECLResultRepository odeclresultRepository = _DataRepositoryFactory.GetDataRepository<IOdECLResultRepository>();
                IEnumerable<OdECLResult> odeclresults = odeclresultRepository.Get().ToArray();
                return odeclresults.ToArray();
            });
        }

        public OdECLResult[] GetOdECLResultBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOdECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IOdECLResultRepository>();
                IEnumerable<OdECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetOdECLResultBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public OdECLResult[] GetOdECLResults(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOdECLResultRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IOdECLResultRepository>();
                IEnumerable<OdECLResult> marginalpddstrlbs = marginalpddstrlbRepository.GetOdECLResults(defaultCount, path);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion


        #region CollateralGrowthRate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CollateralGrowthRate UpdateCollateralGrowthRate(CollateralGrowthRate collateralgrowthrate)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralGrowthRateRepository collateralgrowthrateRepository = _DataRepositoryFactory.GetDataRepository<ICollateralGrowthRateRepository>();

                CollateralGrowthRate updatedEntity = null;

                if (collateralgrowthrate.ID == 0)
                    updatedEntity = collateralgrowthrateRepository.Add(collateralgrowthrate);
                else
                    updatedEntity = collateralgrowthrateRepository.Update(collateralgrowthrate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCollateralGrowthRate(int collateralgrowthrateId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralGrowthRateRepository collateralgrowthrateRepository = _DataRepositoryFactory.GetDataRepository<ICollateralGrowthRateRepository>();

                collateralgrowthrateRepository.Remove(collateralgrowthrateId);
            });
        }

        public CollateralGrowthRate GetCollateralGrowthRate(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralGrowthRateRepository collateralgrowthrateRepository = _DataRepositoryFactory.GetDataRepository<ICollateralGrowthRateRepository>();

                CollateralGrowthRate collateralgrowthrateEntity = collateralgrowthrateRepository.Get(ID);
                if (collateralgrowthrateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CollateralGrowthRate with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return collateralgrowthrateEntity;
            });
        }

        public CollateralGrowthRate[] GetAllCollateralGrowthRates()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICollateralGrowthRateRepository collateralgrowthrateRepository = _DataRepositoryFactory.GetDataRepository<ICollateralGrowthRateRepository>();
                IEnumerable<CollateralGrowthRate> collateralgrowthrates = collateralgrowthrateRepository.Get().ToArray();
                return collateralgrowthrates.ToArray();
            });
        }

        public CollateralGrowthRate[] GetCollateralGrowthRateBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICollateralGrowthRateRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICollateralGrowthRateRepository>();
                IEnumerable<CollateralGrowthRate> marginalpddstrlbs = marginalpddstrlbRepository.GetCollateralGrowthRateBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public CollateralGrowthRate[] GetCollateralGrowthRates(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICollateralGrowthRateRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICollateralGrowthRateRepository>();
                IEnumerable<CollateralGrowthRate> marginalpddstrlbs = marginalpddstrlbRepository.GetCollateralGrowthRates(defaultCount);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region CummulativeDefaultAmt operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CummulativeDefaultAmt UpdateCummulativeDefaultAmt(CummulativeDefaultAmt cummulativedefaultamt)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativeDefaultAmtRepository cummulativedefaultamtRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeDefaultAmtRepository>();

                CummulativeDefaultAmt updatedEntity = null;

                if (cummulativedefaultamt.Id == 0)
                    updatedEntity = cummulativedefaultamtRepository.Add(cummulativedefaultamt);
                else
                    updatedEntity = cummulativedefaultamtRepository.Update(cummulativedefaultamt);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCummulativeDefaultAmt(int cummulativedefaultamtId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativeDefaultAmtRepository cummulativedefaultamtRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeDefaultAmtRepository>();

                cummulativedefaultamtRepository.Remove(cummulativedefaultamtId);
            });
        }

        public CummulativeDefaultAmt GetCummulativeDefaultAmt(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativeDefaultAmtRepository cummulativedefaultamtRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeDefaultAmtRepository>();

                CummulativeDefaultAmt cummulativedefaultamtEntity = cummulativedefaultamtRepository.Get(ID);
                if (cummulativedefaultamtEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CummulativeDefaultAmt with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cummulativedefaultamtEntity;
            });
        }

        public CummulativeDefaultAmt[] GetAllCummulativeDefaultAmts()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativeDefaultAmtRepository cummulativedefaultamtRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeDefaultAmtRepository>();
                IEnumerable<CummulativeDefaultAmt> cummulativedefaultamts = cummulativedefaultamtRepository.Get().ToArray();
                return cummulativedefaultamts.ToArray();
            });
        }

        public CummulativeDefaultAmt[] GetCummulativeDefaultAmtBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativeDefaultAmtRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeDefaultAmtRepository>();
                IEnumerable<CummulativeDefaultAmt> marginalpddstrlbs = marginalpddstrlbRepository.GetCummulativeDefaultAmtBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public CummulativeDefaultAmt[] GetCummulativeDefaultAmts(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativeDefaultAmtRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeDefaultAmtRepository>();
                IEnumerable<CummulativeDefaultAmt> marginalpddstrlbs = marginalpddstrlbRepository.GetCummulativeDefaultAmts(defaultCount, path).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.Origin_yr);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region CummulativeLifetimePd operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CummulativeLifetimePd UpdateCummulativeLifetimePd(CummulativeLifetimePd cummulativelifetimepd)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativeLifetimePdRepository cummulativelifetimepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeLifetimePdRepository>();

                CummulativeLifetimePd updatedEntity = null;

                if (cummulativelifetimepd.Id == 0)
                    updatedEntity = cummulativelifetimepdRepository.Add(cummulativelifetimepd);
                else
                    updatedEntity = cummulativelifetimepdRepository.Update(cummulativelifetimepd);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCummulativeLifetimePd(int cummulativelifetimepdId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativeLifetimePdRepository cummulativelifetimepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeLifetimePdRepository>();

                cummulativelifetimepdRepository.Remove(cummulativelifetimepdId);
            });
        }

        public CummulativeLifetimePd GetCummulativeLifetimePd(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativeLifetimePdRepository cummulativelifetimepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeLifetimePdRepository>();

                CummulativeLifetimePd cummulativelifetimepdEntity = cummulativelifetimepdRepository.Get(ID);
                if (cummulativelifetimepdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CummulativeLifetimePd with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cummulativelifetimepdEntity;
            });
        }

        public CummulativeLifetimePd[] GetAllCummulativeLifetimePds()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativeLifetimePdRepository cummulativelifetimepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeLifetimePdRepository>();
                IEnumerable<CummulativeLifetimePd> cummulativelifetimepds = cummulativelifetimepdRepository.Get().ToArray();
                return cummulativelifetimepds.ToArray();
            });
        }

        public CummulativeLifetimePd[] GetCummulativeLifetimePdBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativeLifetimePdRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeLifetimePdRepository>();
                IEnumerable<CummulativeLifetimePd> marginalpddstrlbs = marginalpddstrlbRepository.GetCummulativeLifetimePdBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public CummulativeLifetimePd[] GetCummulativeLifetimePds(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativeLifetimePdRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICummulativeLifetimePdRepository>();
                IEnumerable<CummulativeLifetimePd> marginalpddstrlbs = marginalpddstrlbRepository.GetCummulativeLifetimePds(defaultCount, path).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.CummulativeLifeTimePD);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region CollateralRecAmtStaging operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CollateralRecAmtStaging UpdateCollateralRecAmtStaging(CollateralRecAmtStaging collateralrecamtstaging)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralRecAmtStagingRepository collateralrecamtstagingRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRecAmtStagingRepository>();

                CollateralRecAmtStaging updatedEntity = null;

                if (collateralrecamtstaging.ID == 0)
                    updatedEntity = collateralrecamtstagingRepository.Add(collateralrecamtstaging);
                else
                    updatedEntity = collateralrecamtstagingRepository.Update(collateralrecamtstaging);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCollateralRecAmtStaging(int collateralrecamtstagingId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralRecAmtStagingRepository collateralrecamtstagingRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRecAmtStagingRepository>();

                collateralrecamtstagingRepository.Remove(collateralrecamtstagingId);
            });
        }

        public CollateralRecAmtStaging GetCollateralRecAmtStaging(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralRecAmtStagingRepository collateralrecamtstagingRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRecAmtStagingRepository>();

                CollateralRecAmtStaging collateralrecamtstagingEntity = collateralrecamtstagingRepository.Get(ID);
                if (collateralrecamtstagingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CollateralRecAmtStaging with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return collateralrecamtstagingEntity;
            });
        }

        public CollateralRecAmtStaging[] GetAllCollateralRecAmtStagings()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICollateralRecAmtStagingRepository collateralrecamtstagingRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRecAmtStagingRepository>();
                IEnumerable<CollateralRecAmtStaging> collateralrecamtstagings = collateralrecamtstagingRepository.Get().ToArray();
                return collateralrecamtstagings.ToArray();
            });
        }

        public CollateralRecAmtStaging[] GetCollateralRecAmtStagingBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICollateralRecAmtStagingRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRecAmtStagingRepository>();
                IEnumerable<CollateralRecAmtStaging> marginalpddstrlbs = marginalpddstrlbRepository.GetCollateralRecAmtStagingBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public CollateralRecAmtStaging[] GetCollateralRecAmtStagings(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICollateralRecAmtStagingRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRecAmtStagingRepository>();
                IEnumerable<CollateralRecAmtStaging> marginalpddstrlbs = marginalpddstrlbRepository.GetCollateralRecAmtStagings(defaultCount);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion


        #region HistoricalDefaultFreq operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalDefaultFreq UpdateHistoricalDefaultFreq(HistoricalDefaultFreq historicaldefaultfreq)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalDefaultFreqRepository historicaldefaultfreqRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultFreqRepository>();

                HistoricalDefaultFreq updatedEntity = null;

                if (historicaldefaultfreq.ID == 0)
                    updatedEntity = historicaldefaultfreqRepository.Add(historicaldefaultfreq);
                else
                    updatedEntity = historicaldefaultfreqRepository.Update(historicaldefaultfreq);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalDefaultFreq(int historicaldefaultfreqId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalDefaultFreqRepository historicaldefaultfreqRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultFreqRepository>();

                historicaldefaultfreqRepository.Remove(historicaldefaultfreqId);
            });
        }

        public HistoricalDefaultFreq GetHistoricalDefaultFreq(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalDefaultFreqRepository historicaldefaultfreqRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultFreqRepository>();

                HistoricalDefaultFreq historicaldefaultfreqEntity = historicaldefaultfreqRepository.Get(ID);
                if (historicaldefaultfreqEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalDefaultFreq with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicaldefaultfreqEntity;
            });
        }

        public HistoricalDefaultFreq[] GetAllHistoricalDefaultFreqs()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IHistoricalDefaultFreqRepository historicaldefaultfreqRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultFreqRepository>();
                IEnumerable<HistoricalDefaultFreq> historicaldefaultfreqs = historicaldefaultfreqRepository.Get();
                return historicaldefaultfreqs.ToArray();
            });
        }

        public HistoricalDefaultFreq[] GetHistoricalDefaultFreqBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IHistoricalDefaultFreqRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultFreqRepository>();
                IEnumerable<HistoricalDefaultFreq> marginalpddstrlbs = marginalpddstrlbRepository.GetHistoricalDefaultFreqBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public HistoricalDefaultFreq[] GetHistoricalDefaultFreqs(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IHistoricalDefaultFreqRepository historicaldefaultfreqRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalDefaultFreqRepository>();
                IEnumerable<HistoricalDefaultFreq> historicaldefaultfreqs = historicaldefaultfreqRepository.GetHistoricalDefaultFreqs(defaultCount, path).OrderBy(c => c.ProductType).ThenBy(c => c.Sub_type).ThenBy(c => c.Origin_Yr);
                return historicaldefaultfreqs.ToArray();
            });
        }


        #endregion

        #region CummulativePD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CummulativePD UpdateCummulativePD(CummulativePD cummulativepd)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativePDRepository cummulativepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativePDRepository>();

                CummulativePD updatedEntity = null;

                if (cummulativepd.ID == 0)
                    updatedEntity = cummulativepdRepository.Add(cummulativepd);
                else
                    updatedEntity = cummulativepdRepository.Update(cummulativepd);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCummulativePD(int cummulativepdId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativePDRepository cummulativepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativePDRepository>();

                cummulativepdRepository.Remove(cummulativepdId);
            });
        }

        public CummulativePD GetCummulativePD(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICummulativePDRepository cummulativepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativePDRepository>();

                CummulativePD cummulativepdEntity = cummulativepdRepository.Get(ID);
                if (cummulativepdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CummulativePD with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cummulativepdEntity;
            });
        }

        public CummulativePD[] GetAllCummulativePDs()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativePDRepository cummulativepdRepository = _DataRepositoryFactory.GetDataRepository<ICummulativePDRepository>();
                IEnumerable<CummulativePD> cummulativepds = cummulativepdRepository.Get().ToArray();
                return cummulativepds.ToArray();
            });
        }
        public CummulativePD[] GetCummulativePDBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativePDRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICummulativePDRepository>();
                IEnumerable<CummulativePD> marginalpddstrlbs = marginalpddstrlbRepository.GetCummulativePDBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public CummulativePD[] GetCummulativePDs(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICummulativePDRepository cummulativepdsRepository = _DataRepositoryFactory.GetDataRepository<ICummulativePDRepository>();
                IEnumerable<CummulativePD> cummulativepds = cummulativepdsRepository.GetCummulativePDs(defaultCount, path).OrderBy(c => c.ProductType).ThenBy(c => c.Sub_type).ThenBy(c => c.Origin_Yr);
                return cummulativepds.ToArray();
            });
        }

        #endregion


        #region MarginalCCFPivotSTRLB operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalCCFPivotSTRLB UpdateMarginalCCFPivotSTRLB(MarginalCCFPivotSTRLB marginalccfpivotstrlb)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalCCFPivotSTRLBRepository marginalccfpivotstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFPivotSTRLBRepository>();

                MarginalCCFPivotSTRLB updatedEntity = null;

                if (marginalccfpivotstrlb.ID == 0)
                    updatedEntity = marginalccfpivotstrlbRepository.Add(marginalccfpivotstrlb);
                else
                    updatedEntity = marginalccfpivotstrlbRepository.Update(marginalccfpivotstrlb);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalCCFPivotSTRLB(int marginalccfpivotstrlbId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalCCFPivotSTRLBRepository marginalccfpivotstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFPivotSTRLBRepository>();

                marginalccfpivotstrlbRepository.Remove(marginalccfpivotstrlbId);
            });
        }

        public MarginalCCFPivotSTRLB GetMarginalCCFPivotSTRLB(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalCCFPivotSTRLBRepository marginalccfpivotstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFPivotSTRLBRepository>();

                MarginalCCFPivotSTRLB marginalccfpivotstrlbEntity = marginalccfpivotstrlbRepository.Get(ID);
                if (marginalccfpivotstrlbEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalCCFPivotSTRLB with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return marginalccfpivotstrlbEntity;
            });
        }

        public MarginalCCFPivotSTRLB[] GetAllMarginalCCFPivotSTRLBs()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFPivotSTRLBRepository marginalccfpivotstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFPivotSTRLBRepository>();
                IEnumerable<MarginalCCFPivotSTRLB> marginalccfpivotstrlbs = marginalccfpivotstrlbRepository.Get().ToArray();
                return marginalccfpivotstrlbs.ToArray();
            });
        }

        public MarginalCCFPivotSTRLB[] GetMarginalCCFPivotSTRLBBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFPivotSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFPivotSTRLBRepository>();
                IEnumerable<MarginalCCFPivotSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetMarginalCCFPivotSTRLBBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public MarginalCCFPivotSTRLB[] GetMarginalCCFPivotSTRLBs(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IMarginalCCFPivotSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IMarginalCCFPivotSTRLBRepository>();
                IEnumerable<MarginalCCFPivotSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetMarginalCCFPivotSTRLBs(defaultCount);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region CcfAnalysisOverDraftSTRLB operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CcfAnalysisOverDraftSTRLB UpdateCcfAnalysisOverDraftSTRLB(CcfAnalysisOverDraftSTRLB ccfanalysisoverdraftstrlb)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICcfAnalysisOverDraftSTRLBRepository ccfanalysisoverdraftstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICcfAnalysisOverDraftSTRLBRepository>();

                CcfAnalysisOverDraftSTRLB updatedEntity = null;

                if (ccfanalysisoverdraftstrlb.ID == 0)
                    updatedEntity = ccfanalysisoverdraftstrlbRepository.Add(ccfanalysisoverdraftstrlb);
                else
                    updatedEntity = ccfanalysisoverdraftstrlbRepository.Update(ccfanalysisoverdraftstrlb);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCcfAnalysisOverDraftSTRLB(int ccfanalysisoverdraftstrlbId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICcfAnalysisOverDraftSTRLBRepository ccfanalysisoverdraftstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICcfAnalysisOverDraftSTRLBRepository>();

                ccfanalysisoverdraftstrlbRepository.Remove(ccfanalysisoverdraftstrlbId);
            });
        }

        public CcfAnalysisOverDraftSTRLB GetCcfAnalysisOverDraftSTRLB(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICcfAnalysisOverDraftSTRLBRepository ccfanalysisoverdraftstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICcfAnalysisOverDraftSTRLBRepository>();

                CcfAnalysisOverDraftSTRLB ccfanalysisoverdraftstrlbEntity = ccfanalysisoverdraftstrlbRepository.Get(ID);
                if (ccfanalysisoverdraftstrlbEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CcfAnalysisOverDraftSTRLB with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ccfanalysisoverdraftstrlbEntity;
            });
        }

        public CcfAnalysisOverDraftSTRLB[] GetAllCcfAnalysisOverDraftSTRLBs()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICcfAnalysisOverDraftSTRLBRepository ccfanalysisoverdraftstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICcfAnalysisOverDraftSTRLBRepository>();
                IEnumerable<CcfAnalysisOverDraftSTRLB> ccfanalysisoverdraftstrlbs = ccfanalysisoverdraftstrlbRepository.Get().ToArray();
                return ccfanalysisoverdraftstrlbs.ToArray();
            });
        }

        public CcfAnalysisOverDraftSTRLB[] GetCcfAnalysisOverDraftSTRLBBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICcfAnalysisOverDraftSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICcfAnalysisOverDraftSTRLBRepository>();
                IEnumerable<CcfAnalysisOverDraftSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetCcfAnalysisOverDraftSTRLBBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public CcfAnalysisOverDraftSTRLB[] GetCcfAnalysisOverDraftSTRLBs(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICcfAnalysisOverDraftSTRLBRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<ICcfAnalysisOverDraftSTRLBRepository>();
                IEnumerable<CcfAnalysisOverDraftSTRLB> marginalpddstrlbs = marginalpddstrlbRepository.GetCcfAnalysisOverDraftSTRLBs(defaultCount);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion 

        #region IfrsProjectedCummDefaultFrq operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsProjectedCummDefaultFrq UpdateIfrsProjectedCummDefaultFrq(IfrsProjectedCummDefaultFrq ifrsprojectedcumm)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsProjectedCummDefaultFrqRepository ifrsprojectedcummRepository = _DataRepositoryFactory.GetDataRepository<IIfrsProjectedCummDefaultFrqRepository>();

                IfrsProjectedCummDefaultFrq updatedEntity = null;

                if (ifrsprojectedcumm.Id == 0)
                    updatedEntity = ifrsprojectedcummRepository.Add(ifrsprojectedcumm);
                else
                    updatedEntity = ifrsprojectedcummRepository.Update(ifrsprojectedcumm);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsProjectedCummDefaultFrq(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsProjectedCummDefaultFrqRepository ifrsprojectedcummRepository = _DataRepositoryFactory.GetDataRepository<IIfrsProjectedCummDefaultFrqRepository>();

                ifrsprojectedcummRepository.Remove(Id);
            });
        }

        public IfrsProjectedCummDefaultFrq GetIfrsProjectedCummDefaultFrq(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsProjectedCummDefaultFrqRepository ifrsprojectedcummRepository = _DataRepositoryFactory.GetDataRepository<IIfrsProjectedCummDefaultFrqRepository>();

                IfrsProjectedCummDefaultFrq ifrsprojectedcummEntity = ifrsprojectedcummRepository.Get(Id);
                if (ifrsprojectedcummEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsProjectedCummDefaultFrq with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsprojectedcummEntity;
            });
        }

        public IfrsProjectedCummDefaultFrq[] GetAllIfrsProjectedCummDefaultFrq(int defaultcount, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsProjectedCummDefaultFrqRepository ifrsprojectedcummRepository = _DataRepositoryFactory.GetDataRepository<IIfrsProjectedCummDefaultFrqRepository>();

                IEnumerable<IfrsProjectedCummDefaultFrq> ifrsprojectedcumms = ifrsprojectedcummRepository.GetAllIfrsProjectedCummDefaultFrq(defaultcount, path).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.Origin_yr);

                return ifrsprojectedcumms.ToArray();
            });
        }




        #endregion

        #region IfrsMarginalPDByScenerio operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsMarginalPDByScenerio UpdateIfrsMarginalPDByScenerio(IfrsMarginalPDByScenerio ifrsmarginalpdscenerio)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMarginalPDByScenerioRepository ifrsmarginalpdscenerioRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMarginalPDByScenerioRepository>();

                IfrsMarginalPDByScenerio updatedEntity = null;

                if (ifrsmarginalpdscenerio.ID == 0)
                    updatedEntity = ifrsmarginalpdscenerioRepository.Add(ifrsmarginalpdscenerio);
                else
                    updatedEntity = ifrsmarginalpdscenerioRepository.Update(ifrsmarginalpdscenerio);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsMarginalPDByScenerio(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMarginalPDByScenerioRepository ifrsmarginalpdscenerioRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMarginalPDByScenerioRepository>();

                ifrsmarginalpdscenerioRepository.Remove(ID);
            });
        }

        public IfrsMarginalPDByScenerio GetIfrsMarginalPDByScenerio(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMarginalPDByScenerioRepository ifrsmarginalpdscenerioRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMarginalPDByScenerioRepository>();

                IfrsMarginalPDByScenerio ifrsmarginalpdscenerioEntity = ifrsmarginalpdscenerioRepository.Get(ID);
                if (ifrsmarginalpdscenerioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsMarginalPDByScenerio with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsmarginalpdscenerioEntity;
            });
        }

        public IfrsMarginalPDByScenerio[] GetAllIfrsMarginalPDByScenerio(int defaultcount, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMarginalPDByScenerioRepository ifrsmarginalpdscenerioRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMarginalPDByScenerioRepository>();

                IEnumerable<IfrsMarginalPDByScenerio> ifrsmarginalpdscenerios = ifrsmarginalpdscenerioRepository.GetAllIfrsMarginalPDByScenerio(defaultcount, path).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.Scenerio).ThenBy(c => c.fnyr);

                return ifrsmarginalpdscenerios.ToArray();
            });
        }
        
        #endregion
        
        #region IfrsMonthlyForwardPDMacroVar operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsMonthlyForwardPDMacroVar UpdateIfrsMonthlyForwardPDMacroVar(IfrsMonthlyForwardPDMacroVar ifrsmonthlyforwardpd)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyForwardPDMacroVarRepository ifrsmonthlyforwardpdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyForwardPDMacroVarRepository>();

                IfrsMonthlyForwardPDMacroVar updatedEntity = null;

                if (ifrsmonthlyforwardpd.Id == 0)
                    updatedEntity = ifrsmonthlyforwardpdRepository.Add(ifrsmonthlyforwardpd);
                else
                    updatedEntity = ifrsmonthlyforwardpdRepository.Update(ifrsmonthlyforwardpd);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsMonthlyForwardPDMacroVar(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyForwardPDMacroVarRepository ifrsmonthlyforwardpdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyForwardPDMacroVarRepository>();

                ifrsmonthlyforwardpdRepository.Remove(Id);
            });
        }

        public IfrsMonthlyForwardPDMacroVar GetIfrsMonthlyForwardPDMacroVar(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyForwardPDMacroVarRepository ifrsmonthlyforwardpdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyForwardPDMacroVarRepository>();

                IfrsMonthlyForwardPDMacroVar ifrsmonthlyforwardpdEntity = ifrsmonthlyforwardpdRepository.Get(Id);
                if (ifrsmonthlyforwardpdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsMonthlyForwardPDMacroVar with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsmonthlyforwardpdEntity;
            });
        }

        public IfrsMonthlyForwardPDMacroVar[] GetAllIfrsMonthlyForwardPDMacroVar(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsMonthlyForwardPDMacroVarRepository ifrsmonthlyforwardpdRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyForwardPDMacroVarRepository>();
               
                IEnumerable<IfrsMonthlyForwardPDMacroVar> ifrsmonthlyforwardpds = ifrsmonthlyforwardpdRepository.GetAllIfrsMonthlyForwardPDMacroVar(defaultCount, path).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.Scenerio).ThenByDescending(c => c.Col);

                return ifrsmonthlyforwardpds.ToArray();
            });
        }




        #endregion

        #region InvestmentMarginalPd operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public InvestmentMarginalPd UpdateInvestmentMarginalPd(InvestmentMarginalPd investmentmarginalpd)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentMarginalPdRepository investmentmarginalpdRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentMarginalPdRepository>();

                InvestmentMarginalPd updatedEntity = null;

                if (investmentmarginalpd.ID == 0)
                    updatedEntity = investmentmarginalpdRepository.Add(investmentmarginalpd);
                else
                    updatedEntity = investmentmarginalpdRepository.Update(investmentmarginalpd);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInvestmentMarginalPd(int investmentmarginalpdId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentMarginalPdRepository investmentmarginalpdRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentMarginalPdRepository>();

                investmentmarginalpdRepository.Remove(investmentmarginalpdId);
            });
        }

        public InvestmentMarginalPd GetInvestmentMarginalPd(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentMarginalPdRepository investmentmarginalpdRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentMarginalPdRepository>();

                InvestmentMarginalPd investmentmarginalpdEntity = investmentmarginalpdRepository.Get(ID);
                if (investmentmarginalpdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InvestmentMarginalPd with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return investmentmarginalpdEntity;
            });
        }

        public InvestmentMarginalPd[] GetAllInvestmentMarginalPds()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IInvestmentMarginalPdRepository investmentmarginalpdRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentMarginalPdRepository>();
                IEnumerable<InvestmentMarginalPd> investmentmarginalpds = investmentmarginalpdRepository.Get().ToArray();
                return investmentmarginalpds.ToArray();
            });
        }

        public InvestmentMarginalPd[] GetInvestmentMarginalPdBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IInvestmentMarginalPdRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentMarginalPdRepository>();
                IEnumerable<InvestmentMarginalPd> marginalpddstrlbs = marginalpddstrlbRepository.GetInvestmentMarginalPdBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public InvestmentMarginalPd[] GetInvestmentMarginalPds(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IInvestmentMarginalPdRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentMarginalPdRepository>();
                IEnumerable<InvestmentMarginalPd> marginalpddstrlbs = marginalpddstrlbRepository.GetInvestmentMarginalPds(defaultCount);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region IfrsPdTermStructure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsPdTermStructure UpdateIfrsPdTermStructure(IfrsPdTermStructure IfrsPdTermStructure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdTermStructureRepository IfrsPdTermStructureRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdTermStructureRepository>();

                IfrsPdTermStructure updatedEntity = null;

                if (IfrsPdTermStructure.ID == 0)
                    updatedEntity = IfrsPdTermStructureRepository.Add(IfrsPdTermStructure);
                else
                    updatedEntity = IfrsPdTermStructureRepository.Update(IfrsPdTermStructure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsPdTermStructure(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdTermStructureRepository IfrsPdTermStructureRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdTermStructureRepository>();

                IfrsPdTermStructureRepository.Remove(ID);
            });
        }

        public IfrsPdTermStructure GetIfrsPdTermStructure(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdTermStructureRepository IfrsPdTermStructureRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdTermStructureRepository>();

                IfrsPdTermStructure IfrsPdTermStructureEntity = IfrsPdTermStructureRepository.Get(ID);
                if (IfrsPdTermStructureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsPdTermStructure with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IfrsPdTermStructureEntity;
            });
        }

        public IfrsPdTermStructure[] GetAllIfrsPdTermStructures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsPdTermStructureRepository IfrsPdTermStructureRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPdTermStructureRepository>();

                IEnumerable<IfrsPdTermStructure> IfrsPdTermStructures = IfrsPdTermStructureRepository.Get().ToArray();

                return IfrsPdTermStructures.ToArray();
            });
        }



        #endregion
        
        #region ConsolidatedLoans operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ConsolidatedLoans UpdateConsolidatedLoans(ConsolidatedLoans consolidatedloans)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IConsolidatedLoansRepository consolidatedloansRepository = _DataRepositoryFactory.GetDataRepository<IConsolidatedLoansRepository>();

                ConsolidatedLoans updatedEntity = null;

                if (consolidatedloans.ID == 0)
                    updatedEntity = consolidatedloansRepository.Add(consolidatedloans);
                else
                    updatedEntity = consolidatedloansRepository.Update(consolidatedloans);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteConsolidatedLoans(int consolidatedloansId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IConsolidatedLoansRepository consolidatedloansRepository = _DataRepositoryFactory.GetDataRepository<IConsolidatedLoansRepository>();

                consolidatedloansRepository.Remove(consolidatedloansId);
            });
        }

        public ConsolidatedLoans GetConsolidatedLoans(int ID)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IConsolidatedLoansRepository consolidatedloansRepository = _DataRepositoryFactory.GetDataRepository<IConsolidatedLoansRepository>();

                ConsolidatedLoans consolidatedloansEntity = consolidatedloansRepository.Get(ID);
                if (consolidatedloansEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ConsolidatedLoans with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return consolidatedloansEntity;
            });
        }

        public ConsolidatedLoans[] GetAllConsolidatedLoanss()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IConsolidatedLoansRepository consolidatedloansRepository = _DataRepositoryFactory.GetDataRepository<IConsolidatedLoansRepository>();
                IEnumerable<ConsolidatedLoans> consolidatedloanss = consolidatedloansRepository.Get().ToArray();
                return consolidatedloanss.ToArray();
            });
        }

        public ConsolidatedLoans[] GetConsolidatedLoansBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IConsolidatedLoansRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IConsolidatedLoansRepository>();
                IEnumerable<ConsolidatedLoans> marginalpddstrlbs = marginalpddstrlbRepository.GetConsolidatedLoansBySearch(searchParam);
                return marginalpddstrlbs.ToArray();
            });
        }

        public ConsolidatedLoans[] GetConsolidatedLoanss(int defaultCount)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IConsolidatedLoansRepository marginalpddstrlbRepository = _DataRepositoryFactory.GetDataRepository<IConsolidatedLoansRepository>();
                IEnumerable<ConsolidatedLoans> marginalpddstrlbs = marginalpddstrlbRepository.GetConsolidatedLoanss(defaultCount);
                return marginalpddstrlbs.ToArray();
            });
        }


        #endregion

        #region OverdraftMonthlyEAD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OverdraftMonthlyEAD UpdateOverdraftMonthlyEAD(OverdraftMonthlyEAD odmonthlyead)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOverdraftMonthlyEADRepository odmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftMonthlyEADRepository>();

                OverdraftMonthlyEAD updatedEntity = null;

                if (odmonthlyead.Id == 0)
                    updatedEntity = odmonthlyeadRepository.Add(odmonthlyead);
                else
                    updatedEntity = odmonthlyeadRepository.Update(odmonthlyead);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOverdraftMonthlyEAD(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOverdraftMonthlyEADRepository odmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftMonthlyEADRepository>();

                odmonthlyeadRepository.Remove(Id);
            });
        }

        public OverdraftMonthlyEAD GetOverdraftMonthlyEAD(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOverdraftMonthlyEADRepository odmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftMonthlyEADRepository>();

                OverdraftMonthlyEAD ifrsmonthlyeadEntity = odmonthlyeadRepository.Get(Id);
                if (ifrsmonthlyeadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OverdraftMonthlyEAD with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsmonthlyeadEntity;
            });
        }

        public OverdraftMonthlyEAD[] GetOverdraftMonthlyEADBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOverdraftMonthlyEADRepository odmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftMonthlyEADRepository>();
                IEnumerable<OverdraftMonthlyEAD> ifrsmonthlyeads = odmonthlyeadRepository.GetOverdraftMonthlyEADBySearch(searchParam);
                return ifrsmonthlyeads.ToArray();
            });
        }

        public OverdraftMonthlyEAD[] GetAllOverdraftMonthlyEAD(int defaultcount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOverdraftMonthlyEADRepository odmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftMonthlyEADRepository>();

                IEnumerable<OverdraftMonthlyEAD> ifrsmonthlyeads = odmonthlyeadRepository.Get().Take(defaultcount).ToArray();

                return ifrsmonthlyeads.ToArray();
            });
        }
        #endregion

        #region OverdraftLGDComputation operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OverdraftLGDComputation UpdateOverdraftLGDComputation(OverdraftLGDComputation lgdcomptresult)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOverdraftLGDComputationRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftLGDComputationRepository>();
                OverdraftLGDComputation updatedEntity = null;
                if (lgdcomptresult.Id == 0)
                    updatedEntity = lgdcomptresultRepository.Add(lgdcomptresult);
                else
                    updatedEntity = lgdcomptresultRepository.Update(lgdcomptresult);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOverdraftLGDComputation(int lgdcomptresultId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOverdraftLGDComputationRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftLGDComputationRepository>();
                lgdcomptresultRepository.Remove(lgdcomptresultId);
            });
        }

        public OverdraftLGDComputation GetOverdraftLGDComputation(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOverdraftLGDComputationRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftLGDComputationRepository>();
                OverdraftLGDComputation lgdcomptresultEntity = lgdcomptresultRepository.Get(Id);
                if (lgdcomptresultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OverdraftLGDComputation with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return lgdcomptresultEntity;
            });
        }

        public OverdraftLGDComputation[] GetAllOverdraftLGDComputation()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOverdraftLGDComputationRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftLGDComputationRepository>();
                IEnumerable<OverdraftLGDComputation> lgdcomptresults = lgdcomptresultRepository.Get().ToArray();
                return lgdcomptresults.ToArray();
            });
        }


        public OverdraftLGDComputation[] GetOverdraftLGDComputationBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOverdraftLGDComputationRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftLGDComputationRepository>();
                IEnumerable<OverdraftLGDComputation> lgdcomptresults = lgdcomptresultRepository.GetRecordByRefNo(searchParam);
                return lgdcomptresults.ToArray();
            });
        }

        public OverdraftLGDComputation[] GetOverdraftLGDComputations(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IOverdraftLGDComputationRepository lgdcomptresultRepository = _DataRepositoryFactory.GetDataRepository<IOverdraftLGDComputationRepository>();
                IEnumerable<OverdraftLGDComputation> lgdcomptresults = lgdcomptresultRepository.GetOverdraftLGDComputations(defaultCount, path);
                return lgdcomptresults.ToArray();
            });
        }

        #endregion


        #region IfrsLoansInfo operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsLoansInfo UpdateIfrsLoansInfo(IfrsLoansInfo ifrsloansinfo)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLoansInfoRepository ifrsloansinfoRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLoansInfoRepository>();
                IfrsLoansInfo updatedEntity = null;
                if (ifrsloansinfo.Id == 0)
                    updatedEntity = ifrsloansinfoRepository.Add(ifrsloansinfo);
                else
                    updatedEntity = ifrsloansinfoRepository.Update(ifrsloansinfo);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsLoansInfo(int ifrsloansinfoId)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLoansInfoRepository ifrsloansinfoRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLoansInfoRepository>();
                ifrsloansinfoRepository.Remove(ifrsloansinfoId);
            });
        }

        public IfrsLoansInfo GetIfrsLoansInfo(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLoansInfoRepository ifrsloansinfoRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLoansInfoRepository>();
                IfrsLoansInfo ifrsloansinfoEntity = ifrsloansinfoRepository.Get(Id);
                if (ifrsloansinfoEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsLoansInfo with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return ifrsloansinfoEntity;
            });
        }

        public IfrsLoansInfo[] GetAllIfrsLoansInfo()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLoansInfoRepository ifrsloansinfoRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLoansInfoRepository>();
                IEnumerable<IfrsLoansInfo> ifrsloansinfos = ifrsloansinfoRepository.Get().ToArray();
                return ifrsloansinfos.ToArray();
            });
        }


        public IfrsLoansInfo[] GetIfrsLoansInfoBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLoansInfoRepository ifrsloansinfoRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLoansInfoRepository>();
                IEnumerable<IfrsLoansInfo> ifrsloansinfos = ifrsloansinfoRepository.GetRecordByRefNo(searchParam);
                return ifrsloansinfos.ToArray();
            });
        }

        public IfrsLoansInfo[] GetIfrsLoansInfos(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLoansInfoRepository ifrsloansinfoRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLoansInfoRepository>();
                IEnumerable<IfrsLoansInfo> ifrsloansinfos = ifrsloansinfoRepository.GetIfrsLoansInfos(defaultCount, path);
                return ifrsloansinfos.ToArray();
            });
        }

        #endregion



        #region IfrsLgdProjections operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsLgdProjections UpdateIfrsLgdProjections(IfrsLgdProjections IfrsLgdProjections)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLgdProjectionsRepository IfrsLgdProjectionsRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdProjectionsRepository>();
                IfrsLgdProjections updatedEntity = null;
                if (IfrsLgdProjections.ID == 0)
                    updatedEntity = IfrsLgdProjectionsRepository.Add(IfrsLgdProjections);
                else
                    updatedEntity = IfrsLgdProjectionsRepository.Update(IfrsLgdProjections);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsLgdProjections(int Id)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLgdProjectionsRepository IfrsLgdProjectionsRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdProjectionsRepository>();
                IfrsLgdProjectionsRepository.Remove(Id);
            });
        }

      /*  public IfrsLgdProjections GetIfrsLgdProjections(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLgdProjectionsRepository IfrsLgdProjectionsRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdProjectionsRepository>();
                IfrsLgdProjections IfrsLgdProjectionsEntity = IfrsLgdProjectionsRepository.Get(Id);
                if (IfrsLgdProjectionsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsLgdProjections with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return IfrsLgdProjectionsEntity;
            });
        } */

        public IfrsLgdProjections[] GetAllIfrsLgdProjections()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLgdProjectionsRepository IfrsLgdProjectionsRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdProjectionsRepository>();
                IEnumerable<IfrsLgdProjections> IfrsLgdProjectionss = IfrsLgdProjectionsRepository.Get().ToArray();
                return IfrsLgdProjectionss.ToArray();
            });
        }


        public IfrsLgdProjections[] GetIfrsLgdProjectionsBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLgdProjectionsRepository IfrsLgdProjectionsRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdProjectionsRepository>();
                IEnumerable<IfrsLgdProjections> IfrsLgdProjectionss = IfrsLgdProjectionsRepository.GetRecordByRefNo(searchParam);
                return IfrsLgdProjectionss.ToArray();
            });
        }

        public IfrsLgdProjections[] GetIfrsLgdProjections(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsLgdProjectionsRepository IfrsLgdProjectionsRepository = _DataRepositoryFactory.GetDataRepository<IIfrsLgdProjectionsRepository>();
                IEnumerable<IfrsLgdProjections> IfrsLgdProjectionss = IfrsLgdProjectionsRepository.GetIfrsLgdProjections(defaultCount, path);
                return IfrsLgdProjectionss.ToArray();
            });
        }

        #endregion




        #region IfrsPDProjection operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsPDProjection UpdateIfrsPDProjection(IfrsPDProjection IfrsPDProjection)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsPDProjectionRepository IfrsPDProjectionRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPDProjectionRepository>();
                IfrsPDProjection updatedEntity = null;
                if (IfrsPDProjection.ID == 0)
                    updatedEntity = IfrsPDProjectionRepository.Add(IfrsPDProjection);
                else
                    updatedEntity = IfrsPDProjectionRepository.Update(IfrsPDProjection);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsPDProjection(int Id)
        {
            ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsPDProjectionRepository IfrsPDProjectionRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPDProjectionRepository>();
                IfrsPDProjectionRepository.Remove(Id);
            });
        }

        public IfrsPDProjection GetIfrsPDProjection(int Id)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsPDProjectionRepository IfrsPDProjectionRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPDProjectionRepository>();
                IfrsPDProjection IfrsPDProjectionEntity = IfrsPDProjectionRepository.Get(Id);
                if (IfrsPDProjectionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsPDProjection with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return IfrsPDProjectionEntity;
            });
        }

        public IfrsPDProjection[] GetAllIfrsPDProjection()
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsPDProjectionRepository IfrsPDProjectionRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPDProjectionRepository>();
                IEnumerable<IfrsPDProjection> IfrsPDProjections = IfrsPDProjectionRepository.Get().ToArray();
                return IfrsPDProjections.ToArray();
            });
        }


        public IfrsPDProjection[] GetIfrsPDProjectionBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsPDProjectionRepository IfrsPDProjectionRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPDProjectionRepository>();
                IEnumerable<IfrsPDProjection> IfrsPDProjections = IfrsPDProjectionRepository.GetRecordByRefNo(searchParam);
                return IfrsPDProjections.ToArray();
            });
        }

        public IfrsPDProjection[] GetIfrsPDProjections(int defaultCount, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsPDProjectionRepository IfrsPDProjectionRepository = _DataRepositoryFactory.GetDataRepository<IIfrsPDProjectionRepository>();
                IEnumerable<IfrsPDProjection> IfrsPDProjections = IfrsPDProjectionRepository.GetIfrsPDProjections(defaultCount, path);
                return IfrsPDProjections.ToArray();
            });
        }

        #endregion


    }
}
