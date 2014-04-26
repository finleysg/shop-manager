namespace Enfield.ShopManager.Data.Graph
{
    public class ServiceType : AutoMapBase<ServiceType>
    {
        public ServiceType()
        {
            //no-op
        }

        public ServiceType(AvailableServicesView view)
        {
            Id = view.ServiceTypeId;
            Description = view.ServiceTypeDescription;
        }

        private string description;
        public virtual string Description 
        {
            get { return description; }
            set { description = (value == null) ? null : value.ToUpper(); }
        }
    }
}
