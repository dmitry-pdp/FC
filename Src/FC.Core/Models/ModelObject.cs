namespace FC.Core.Models
{
    public class ModelObject
    {
        public uint ObjectId;
        public uint ObjectTypeId;

        public ModelObject(uint objectId, uint objectTypeId)
        {
            this.ObjectId = objectId;
            this.ObjectTypeId = objectTypeId;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ModelObject;
            return other != null && other.ObjectId == this.ObjectId;
        }

        public override int GetHashCode()
        {
            return (int)this.ObjectId;
        }
    }
}
