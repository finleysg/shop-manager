using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;

namespace Enfield.ShopManager.Mapping
{
    public class MapConfiguration
    {
        public static void Bootstrap()
        {
            //Graph-->Model Mapping
            Mapper.CreateMap<Data.Graph.Location, Models.LocationModel>();
            Mapper.CreateMap<Data.Graph.Labor, Models.LaborModel>();
            Mapper.CreateMap<Data.Graph.Labor, Models.PayrollModel>()
                .ForMember(i => i.LocationName, r => r.ResolveUsing<LaborGraphToPayrollResolver>());
            Mapper.CreateMap<Data.Graph.Service, Models.ServiceModel>();
            Mapper.CreateMap<Data.Graph.StockNumberHistory, Models.HistoryModel>();
            Mapper.CreateMap<Data.Graph.Invoice, Models.InvoiceModel>()
                .ForMember(i => i.AccountType, r => r.ResolveUsing<InvoiceGraphToModelResolver>())
                .ForMember(i => i.History, r => r.Ignore());
            Mapper.CreateMap<Data.Graph.Account, Models.AccountSearchModel>();
            Mapper.CreateMap<Data.Graph.Account, Models.AccountModel>();
            Mapper.CreateMap<Data.Graph.Contact, Models.ContactModel>();
            Mapper.CreateMap<Data.Graph.InvoiceView, Models.InvoiceViewModel>();
            Mapper.CreateMap<Data.Graph.InvoiceView, Models.InvoiceNavigationModel>();
            Mapper.CreateMap<Data.Graph.Employee, Models.UserModel>()
                .ForMember(i => i.PasswordString, r => r.Ignore());
            Mapper.CreateMap<Data.Graph.Employee, Models.EmployeeModel>();
            Mapper.CreateMap<Data.Graph.LoginAttemptLog, Models.SecurityLogModel>();
            Mapper.CreateMap<Data.Graph.ServiceType, Models.ServiceTypeModel>();
            Mapper.CreateMap<Data.Graph.LaborType, Models.LaborTypeModel>();
            Mapper.CreateMap<Data.Graph.ServiceTotalsView, Models.ServiceTotalViewModel>();
            Mapper.CreateMap<Data.Graph.EmployeeLogView, Models.EmployeeSignInModel>();

            //Model-->Graph Mapping
            Mapper.CreateMap<Models.AccountModel, Data.Graph.Account>()
                .ForMember(i => i.ContactList, r => r.UseDestinationValue());
            Mapper.CreateMap<Models.ContactModel, Data.Graph.Contact>();
            Mapper.CreateMap<Models.UserModel, Data.Graph.Employee>()
                .ForMember(i => i.Password, r => r.Ignore());
            Mapper.CreateMap<Models.EmployeeModel, Data.Graph.Employee>()
                .ForMember(i => i.Password, r => r.Ignore());
            Mapper.CreateMap<Models.LocationModel, Data.Graph.Location>();
                
            //Filter-->Query Mapping
            Mapper.CreateMap<Models.UserFilterModel, Data.Query.UserQuery>()
                .ForMember(i => i.RoleName, r => r.ResolveUsing<FilterToQueryRoleResolver>())
                .ForMember(i => i.HasSiteAccess, r => r.ResolveUsing<FilterToQuerySiteAccessResolver>());
            Mapper.CreateMap<Models.InvoiceFilterModel, Data.Query.InvoiceQuery>()
                .ForMember(i => i.LocationId, r => r.ResolveUsing<FilterToQueryInvoiceLocationResolver>())
                .ForMember(i => i.HasBeenPaid, r => r.ResolveUsing<FilterToQueryPaidResolver>());
            Mapper.CreateMap<Models.SecurityLogFilterModel, Data.Query.LoginQuery>()
                .ForMember(i => i.LocationId, r => r.ResolveUsing<FilterToQuerySecurityLocationResolver>())
                .ForMember(i => i.ResultFlag, r => r.ResolveUsing<FilterToQueryLoginResultResolver>());
            Mapper.CreateMap<Models.EmployeeLogFilterModel, Data.Query.SignInQuery>();
            Mapper.CreateMap<Models.AccountFilterModel, Data.Query.AccountQuery>();

            //Model-->Model Mapping
            Mapper.CreateMap<Models.InvoiceModel, Models.InvoiceNavigationModel>();
        }
    }
}