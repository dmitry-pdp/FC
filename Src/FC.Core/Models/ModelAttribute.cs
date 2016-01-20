namespace FC.Core.Models
{
    using FC.Core.Common;

    public abstract class ModelAttribute : ModelObject
    {
        private string name;

        public ModelAttribute(uint objectId, uint objectTypeId, string name)
            : base(objectId, objectTypeId)
        {
            this.name = name.RequireNotNull("name");
        }

        public string Name
        {
            get { return this.name; }
        }

        public abstract float GetValue();
    }
}
