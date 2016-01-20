namespace FC.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    using FC.Core.Common;

    public class ModelComputedAttribute : ModelAttribute
    {
        private IDictionary<string, ModelAttribute> attributes;

        private Func<IDictionary<string, ModelAttribute>, float> calculateValueFunction;

        public ModelComputedAttribute(uint objectId, uint objectTypeId, string name, IDictionary<string, ModelAttribute> attributes, Func<IDictionary<string, ModelAttribute>, float> calculateValueFunction)
            : base(objectId, objectTypeId, name)
        {
            this.attributes = attributes.RequireNotNull("attributes");
            this.calculateValueFunction = calculateValueFunction.RequireNotNull("calculateValueFunction");
        }

        public override float GetValue()
        {
            return this.calculateValueFunction(this.attributes);
        }
    }
}
