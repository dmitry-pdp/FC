namespace FC.Core.Models
{
    using System.Collections.Generic;

    public class AttributedModelObject : ModelObject
    {
        private Dictionary<string, ModelAttribute> attributes;

        public AttributedModelObject(uint objectId, uint objectTypeId)
            : base(objectId, objectTypeId) 
        { 
        }

        public Dictionary<string, ModelAttribute> Attributes
        {
            get { return this.attributes ?? (this.attributes = new Dictionary<string, ModelAttribute>()); }
        }
    }
}
