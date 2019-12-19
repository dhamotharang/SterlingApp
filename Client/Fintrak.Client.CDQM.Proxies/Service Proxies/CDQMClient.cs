using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.CDQM.Contracts;
using Fintrak.Client.CDQM.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.CDQM.Proxies
{
    [Export(typeof(ICDQMService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMClient : UserClientBase<ICDQMService>, ICDQMService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }


        #region CDQMAddress

        public CDQMAddress UpdateCDQMAddress(CDQMAddress cdqmAddress)
        {
            return Channel.UpdateCDQMAddress(cdqmAddress);
        }

        public void DeleteCDQMAddress(int addressId)
        {
            Channel.DeleteCDQMAddress(addressId);
        }

        public CDQMAddress GetCDQMAddress(int addressId)
        {
            return Channel.GetCDQMAddress(addressId);
        }

        public CDQMAddress[] GetAllCDQMAddresses()
        {
            return Channel.GetAllCDQMAddresses();
        }


        #endregion

        #region CDQMCountry

        public CDQMCountry UpdateCDQMCountry(CDQMCountry cdqmCountry)
        {
            return Channel.UpdateCDQMCountry(cdqmCountry);
        }

        public void DeleteCDQMCountry(int countryId)
        {
            Channel.DeleteCDQMCountry(countryId);
        }

        public CDQMCountry GetCDQMCountry(int countryId)
        {
            return Channel.GetCDQMCountry(countryId);
        }

        public CDQMCountry[] GetAllCDQMCountries()
        {
            return Channel.GetAllCDQMCountries();
        }


        #endregion

        #region CDQMCustomerMIS

        public CDQMCustomerMIS UpdateCDQMCustomerMIS(CDQMCustomerMIS cdqmCustomerMIS)
        {
            return Channel.UpdateCDQMCustomerMIS(cdqmCustomerMIS);
        }

        public void DeleteCDQMCustomerMIS(int customerMISId)
        {
            Channel.DeleteCDQMCustomerMIS(customerMISId);
        }

        public CDQMCustomerMIS GetCDQMCustomerMIS(int customerMISId)
        {
            return Channel.GetCDQMCustomerMIS(customerMISId);
        }

        public CDQMCustomerMIS[] GetAllCDQMCustomerMIS()
        {
            return Channel.GetAllCDQMCustomerMIS();
        }


        #endregion

        #region CDQMETLConfiguration

        public CDQMETLConfiguration UpdateCDQMETLConfiguration(CDQMETLConfiguration cdqmETLConfiguration)
        {
            return Channel.UpdateCDQMETLConfiguration(cdqmETLConfiguration);
        }

        public void DeleteCDQMETLConfiguration(int etlConfigurationId)
        {
            Channel.DeleteCDQMETLConfiguration(etlConfigurationId);
        }

        public CDQMETLConfiguration GetCDQMETLConfiguration(int etlConfigurationId)
        {
            return Channel.GetCDQMETLConfiguration(etlConfigurationId);
        }

        public CDQMETLConfiguration[] GetAllCDQMETLConfigurations()
        {
            return Channel.GetAllCDQMETLConfigurations();
        }


        #endregion

        #region CDQMGenderGroup

        public CDQMGenderGroup UpdateCDQMGenderGroup(CDQMGenderGroup cdqmGenderGroup)
        {
            return Channel.UpdateCDQMGenderGroup(cdqmGenderGroup);
        }

        public void DeleteCDQMGenderGroup(int genderGroupId)
        {
            Channel.DeleteCDQMGenderGroup(genderGroupId);
        }

        public CDQMGenderGroup GetCDQMGenderGroup(int genderGroupId)
        {
            return Channel.GetCDQMGenderGroup(genderGroupId);
        }

        public CDQMGenderGroup[] GetAllCDQMGenderGroups()
        {
            return Channel.GetAllCDQMGenderGroups();
        }


        #endregion

        #region CDQMMerchant

        public CDQMMerchant UpdateCDQMMerchant(CDQMMerchant cdqmMerchant)
        {
            return Channel.UpdateCDQMMerchant(cdqmMerchant);
        }

        public void DeleteCDQMMerchant(int merchantId)
        {
            Channel.DeleteCDQMMerchant(merchantId);
        }

        public CDQMMerchant GetCDQMMerchant(int merchantId)
        {
            return Channel.GetCDQMMerchant(merchantId);
        }

        public CDQMMerchant[] GetAllCDQMMerchants()
        {
            return Channel.GetAllCDQMMerchants();
        }


        #endregion

        #region CDQMProduct

        public CDQMProduct UpdateCDQMProduct(CDQMProduct cdqmProduct)
        {
            return Channel.UpdateCDQMProduct(cdqmProduct);
        }

        public void DeleteCDQMProduct(int productId)
        {
            Channel.DeleteCDQMProduct(productId);
        }

        public CDQMProduct GetCDQMProduct(int productId)
        {
            return Channel.GetCDQMProduct(productId);
        }

        public CDQMProduct[] GetAllCDQMProducts()
        {
            return Channel.GetAllCDQMProducts();
        }


        #endregion

        #region CDQMTitle

        public CDQMTitle UpdateCDQMTitle(CDQMTitle cdqmTitle)
        {
            return Channel.UpdateCDQMTitle(cdqmTitle);
        }

        public void DeleteCDQMTitle(int titleId)
        {
            Channel.DeleteCDQMTitle(titleId);
        }

        public CDQMTitle GetCDQMTitle(int titleId)
        {
            return Channel.GetCDQMTitle(titleId);
        }

        public CDQMTitle[] GetAllCDQMTitles()
        {
            return Channel.GetAllCDQMTitles();
        }


        #endregion

        #region CDQMCustomerDuplicate

        public CDQMCustomerDuplicate UpdateCDQMCustomerDuplicate(CDQMCustomerDuplicate cdqmCustomerDuplicate)
        {
            return Channel.UpdateCDQMCustomerDuplicate(cdqmCustomerDuplicate);
        }

        public void DeleteCDQMCustomerDuplicate(int cod_CUST_ID)
        {
            Channel.DeleteCDQMCustomerDuplicate(cod_CUST_ID);
        }

        public CDQMCustomerDuplicate GetCDQMCustomerDuplicate(int cod_CUST_ID)
        {
            return Channel.GetCDQMCustomerDuplicate(cod_CUST_ID);
        }

        public CDQMCustomerDuplicate[] GetAllCDQMCustomerDuplicates()
        {
            return Channel.GetAllCDQMCustomerDuplicates();
        }

        public string[] GetCustomerGroupIDs()
        {
            return Channel.GetCustomerGroupIDs();
        }


        #endregion

        #region CDQMCustomerPersistent

        public CDQMCustomerPersistent UpdateCDQMCustomerPersistent(CDQMCustomerPersistent cdqmCustomerPersistent)
        {
            return Channel.UpdateCDQMCustomerPersistent(cdqmCustomerPersistent);
        }

        public void DeleteCDQMCustomerPersistent(int cod_CUST_ID)
        {
            Channel.DeleteCDQMCustomerPersistent(cod_CUST_ID);
        }

        public CDQMCustomerPersistent GetCDQMCustomerPersistent(int cod_CUST_ID)
        {
            return Channel.GetCDQMCustomerPersistent(cod_CUST_ID);
        }

        public CDQMCustomerPersistent[] GetAllCDQMCustomerPersistents()
        {
            return Channel.GetAllCDQMCustomerPersistents();
        }

        public CDQMCustomerPersistent[] GetCustomerPersistents(string groupId)
        {
            return Channel.GetCustomerPersistents(groupId);
        }

        public CDQMCustomerPersistent[] UpdateCustomer(CDQMCustomerPersistent cdqmCustomerPersistent)
        {
            return Channel.UpdateCustomer(cdqmCustomerPersistent);
        }

        #endregion

       
    }
}
