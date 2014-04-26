using System;

namespace Enfield.ShopManager.Data.Graph
{
    public class LoginAttemptLog
    {
        public virtual int LoginAttemptId { get; set; }
        public virtual DateTime LoginDate { get; set; }
        public virtual bool ResultFlag { get; set; }
        public virtual Location Location { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string Reason { get; set; }

        private string userName;
        public virtual string UserName
        {
            get { return userName; }
            set { userName = (value == null) ? null : value.ToUpper(); }
        }
    }
}
