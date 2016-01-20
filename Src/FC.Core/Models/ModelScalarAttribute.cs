namespace FC.Core.Models
{
    using System;

    public class ModelScalarAttribute : ModelAttribute
    {
        private float maxValue;

        private float currentValue;

        public ModelScalarAttribute(uint objectId, uint objectTypeId, string name, float maxValue, float currentValue = default(float))
            : base(objectId, objectTypeId, name)
        {
            this.maxValue = maxValue;
            this.currentValue = currentValue;
        }

        public float GetMaxValue()
        {
            return this.maxValue;
        }

        public override float GetValue()
        {
            return this.currentValue;
        }

        public void SetValue(float value)
        {
            this.currentValue = Math.Min(this.maxValue, value);
        }
    }
}
