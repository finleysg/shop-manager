using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Enfield.ShopManager.Data.Graph;

namespace Enfield.ShopManager.Data.Map
{
    public class LoginAttemptLogMap : ClassMap<LoginAttemptLog>
    {
        public LoginAttemptLogMap()
        {
            Id(m => m.LoginAttemptId).Column("LoginAttemptId");
            Map(m => m.IpAddress);
            References(m => m.Location);
            Map(m => m.LoginDate);
            Map(m => m.Reason);
            Map(m => m.ResultFlag);
            Map(m => m.UserName);
        }
    }
}
