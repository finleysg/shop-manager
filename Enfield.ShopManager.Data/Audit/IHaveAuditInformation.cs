using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enfield.ShopManager.Data.Audit
{
    public interface IHaveAuditInformation
    {
        string ModifyUser { get; set; }
        DateTime ModifyDate { get; set; }
    }
}
