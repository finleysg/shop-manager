using System;

namespace Enfield.ShopManager.Data.Graph
{
    public class ErrorLog : AutoMapBase
    {
        public virtual int ErrorLogId { get; set; }
        public virtual int InvoiceId { get; set; }
        public virtual string ExceptionType { get; set; }
        public virtual string Message { get; set; }
        public virtual string StackTrace { get; set; }
        public virtual string ModifyUser { get; set; }
        public virtual DateTime ModifyDate { get; set; }
    }
}
