using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Models;
using AutoMapper;
using System.Web.Caching;

namespace Enfield.ShopManager.Services
{
    public class LocationService : DomainServiceBase
    {
        private const int All = -1;
        private const string cacheKey = "locations";

        public List<SelectListItem> GetLocationLookup(bool includeAll = false, int? selected = null)
        {
            var locations = GetLocationListing();

            List<SelectListItem> lookup =
                locations.Select(l => new SelectListItem() { Value = l.Id.ToString(), Text = l.Name }).ToList();

            if (includeAll)
            {
                lookup.Add(new SelectListItem() { Value = All.ToString(), Text = "ALL" });
            }

            if (selected.HasValue)
            {
                var selectedItem = lookup.Where(l => l.Value == selected.Value.ToString()).FirstOrDefault();
                if (selectedItem != null) selectedItem.Selected = true;
            }

            return lookup;
        }

        public LocationModel GetLocation(int locationId)
        {
            return GetLocationListing().Where(l => l.Id == locationId).First();
        }

        public List<LocationModel> GetLocationListing()
        {

            var locations = HttpRuntime.Cache[cacheKey] as List<LocationModel>;
            if (locations == null || locations.Count == 0)
            {
                // load from database
                var data = SecurityRepository.GetLocations();
                locations = Mapper.Map<IList<Data.Graph.Location>, List<Models.LocationModel>>(data);
                foreach (var location in locations.Where(l => l.DefaultAccountId != 0)) 
                    location.DefaultAccountName = AccountRepository.GetAccount(location.DefaultAccountId).Name;
                HttpRuntime.Cache.Insert(cacheKey, locations, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12.0));
            }

            return locations;
        }

        public LocationModel UpdateLocation(LocationModel location)
        {
            var loc = Mapper.Map<LocationModel, Data.Graph.Location>(location);
            var existing = SecurityRepository.GetLocation(location.Id);

            existing.Name = loc.Name;
            existing.StaticIpAddress = loc.StaticIpAddress;
            existing.DefaultAccountId = loc.DefaultAccountId;

            loc = SecurityRepository.SaveOrUpdateLocation(existing);

            return Mapper.Map<Data.Graph.Location, LocationModel>(loc);
        }

        public LocationModel CreateLocation(LocationModel location)
        {
            var loc = Mapper.Map<LocationModel, Data.Graph.Location>(location);
            loc = SecurityRepository.SaveOrUpdateLocation(loc);
            HttpRuntime.Cache.Remove(cacheKey);
            return Mapper.Map<Data.Graph.Location, LocationModel>(loc);
        }
    }
}