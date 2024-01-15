using CVMSCore.BAL.Common;
using CVMSCore.BAL.Models.Master;
using CVMSCore.BAL.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Service
{
    public class MastersService
    {
        private MastersRepository repo = new MastersRepository();
        public List<CountryMaster> GetCountryList(int id, int countryId, string usertype, string usersubtype)
        {
            List<CountryMaster> countryList = (List<CountryMaster>)null;
            try
            {
                int RegionId = 0;
                countryList = new List<CountryMaster>();
                countryList = this.repo.GetAllCountry(countryId, usertype, usersubtype, RegionId, id);


            }
            catch
            {
            }
            return countryList;
        }

        public List<RegionMaster> GetRegionList()
        {
            List<RegionMaster> regionList = new List<RegionMaster>();
            try
            {
                List<RegionMaster> regionMasterList = new List<RegionMaster>();
                regionList = this.repo.GetAllRegion().Select<RegionMaster, RegionMaster>((Func<RegionMaster, RegionMaster>)(x => new RegionMaster()
                {
                    Region = x.Region,
                    RegionCode = x.RegionCode,
                    RegionID = x.RegionID,
                    ID = Convert.ToString(Security.GetEncryptString(Convert.ToString(x.RegionID).Trim())),
                    Remarks = x.Remarks
                })).ToList<RegionMaster>();
            }
            catch
            {
            }
            return regionList;
        }

        public List<CVMSCore.BAL.Models.Master.Currency> GetCurrencyList()
        {
            List<CVMSCore.BAL.Models.Master.Currency> currencyList = new List<Currency>();
            try
            {
                currencyList = new List<CVMSCore.BAL.Models.Master.Currency>();
                currencyList = this.repo.GetAllCurrency();
            }
            catch
            {
            }
            return currencyList;
        }

        public List<TimeZones> GetTimeZoneList()
        {
            List<TimeZones> timeZoneList = (List<TimeZones>)null;
            try
            {
                timeZoneList = new List<TimeZones>();
                timeZoneList = this.repo.GetAllTimeZone();
            }
            catch
            {
            }
            return timeZoneList;
        }

        public int SaveCountry(CountryMaster objCountry, int userId)
        {
            int num = 102;
            try
            {

                return repo.SaveCountry(objCountry, userId);


            }
            catch
            {
            }
            return num;
        }

        public int UpdateCountry(CountryMaster objCountry, int userId)
        {
            int num = 102;
            try
            {
                return repo.UpdateCountry(objCountry, userId);

            }
            catch
            {
            }
            return num;
        }

        public List<CountryMaster> GetCountryDetail(int CountryId)
        {


            List<CountryMaster> list = new List<CountryMaster>(CountryId);
            return list = repo.GetCountryDetail(CountryId);


        }

        public int DeleteCountry(int Id, int userId)
        {
            int num = 102;
            try
            {

                return repo.DeleteCountry(Id, userId);


            }
            catch
            {
            }
            return num;
        }

        public int SaveCurrency(Currency objModal, int userId)
        {
            int num = 102;
            try
            {

                return repo.SaveCurrency(objModal, userId);


            }
            catch
            {
            }
            return num;
        }

        public int UpdateCurrency(Currency objModal, int userId)
        {
            int num = 102;
            try
            {

                num = this.repo.UpdateCurrency(objModal, userId);


            }
            catch
            {
            }
            return num;
        }

        public List<Currency> GetCurrencyDetail(int currencyId)
        {

            List<Currency> list = new List<Currency>(currencyId);
            return list = repo.GetCurrencyDetail(currencyId);


        }

        public int DeleteCurrency(int Id, int userId)
        {
            int num = 102;
            try
            {

                num = this.repo.DeleteCurrency(Id, userId);


            }
            catch
            {
            }
            return num;
        }
    }
}
