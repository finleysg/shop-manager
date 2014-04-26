namespace Enfield.ShopManager.Data.Graph
{
    public class LaborType : AutoMapBase<LaborType>
    {
        public LaborType()
        {
            //no-op
        }

        public LaborType(int id)
        {
            Id = id;
        }

        public LaborType(AvailableLaborView view)
        {
            Id = view.LaborTypeId;
            Description = view.LaborTypeDescription;
        }

        private string description;
        public virtual string Description
        {
            get { return description; }
            set { description = (value == null) ? null : value.ToUpper(); }
        }
    }
}
