using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enfield.ShopManager.Models;
using System.Web.Caching;
using AutoMapper;

namespace Enfield.ShopManager.Services
{
    public class InvoiceNavigationService : DomainServiceBase
    {
        
        public List<InvoiceNavigationModel> GetVehiclesInShop(int locationId)
        {
            var cacheKey = string.Format("in-shop-{0}", locationId);

            var inShop = HttpRuntime.Cache[cacheKey] as List<InvoiceNavigationModel>;
            if (inShop == null || inShop.Count == 0)
            {
                // load from database
                Logger.InfoFormat("Loading invoice navigation from database for location {0}", locationId);
                var vehicles = InvoiceRepository.GetInvoices(new Data.Query.InvoiceQuery() { HadBeenCompleted = false, LocationId = locationId });
                inShop = Mapper.Map<IList<Data.Graph.InvoiceView>, List<Models.InvoiceNavigationModel>>(vehicles);
                HttpRuntime.Cache.Insert(cacheKey, inShop, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12.0));
            }

            return inShop;
        }

        public void AddVehicleToInShopList(int locationId, InvoiceModel vehicle)
        {
            GetVehiclesInShop(locationId).Add(Mapper.Map<Models.InvoiceModel, Models.InvoiceNavigationModel>(vehicle));
        }

        public void RemoveVehicleFromInShopList(int locationId, InvoiceModel vehicle)
        {
            GetVehiclesInShop(locationId).RemoveAll(x => x.Id == vehicle.Id);
        }

        public List<InvoiceNavigationModel> GetVehiclesCompletedToday(int locationId)
        {
            var cacheKey = string.Format("completed-today-{0}", locationId);

            var inShop = HttpRuntime.Cache[cacheKey] as List<InvoiceNavigationModel>;
            if (inShop == null || inShop.Count == 0)
            {
                // load from database
                Logger.InfoFormat("Loading completed vehicles from database for location {0}", locationId);
                var vehicles = InvoiceRepository.GetInvoices(new Data.Query.InvoiceQuery() { HadBeenCompleted = true, LocationId = locationId, CompletedDateStart = DateTime.Today });
                inShop = Mapper.Map<IList<Data.Graph.InvoiceView>, List<Models.InvoiceNavigationModel>>(vehicles);
                HttpRuntime.Cache.Insert(cacheKey, inShop, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12.0));
            }

            return inShop;
        }

        public void AddVehicleToCompletedTodayList(int locationId, InvoiceModel vehicle)
        {
            GetVehiclesCompletedToday(locationId).Add(Mapper.Map<Models.InvoiceModel, Models.InvoiceNavigationModel>(vehicle));
        }

        public void RemoveVehicleFromCompletedTodayList(int locationId, InvoiceModel vehicle)
        {
            GetVehiclesCompletedToday(locationId).RemoveAll(x => x.Id == vehicle.Id);
        }

        public void ClearCache(int locationId)
        {
            HttpRuntime.Cache.Remove(string.Format("in-shop-{0}", locationId));
            HttpRuntime.Cache.Remove(string.Format("completed-today-{0}", locationId));
        }
    }
}