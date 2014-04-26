using System;
using System.Security.Principal;
using NHibernate.Event;
using NHibernate.Persister.Entity;

//http://ayende.com/blog/3987/nhibernate-ipreupdateeventlistener-ipreinserteventlistener
namespace Enfield.ShopManager.Data.Audit
{
    public class AuditEventListener : IPreUpdateEventListener, IPreInsertEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var audit = @event.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;

            var time = DateTime.Now;
            var name = WindowsIdentity.GetCurrent().Name;

            Set(@event.Persister, @event.State, "ModifyDate", time);
            Set(@event.Persister, @event.State, "ModifyUser", name);

            audit.ModifyDate = time;
            audit.ModifyUser = name;

            return false;
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var audit = @event.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;

            var time = DateTime.Now;
            var name = WindowsIdentity.GetCurrent().Name;

            Set(@event.Persister, @event.State, "ModifyDate", time);
            Set(@event.Persister, @event.State, "ModifyUser", name);

            audit.ModifyDate = time;
            audit.ModifyUser = name;

            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
    }
}
