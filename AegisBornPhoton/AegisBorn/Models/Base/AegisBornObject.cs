using AegisBorn.Models.Base.Actor.Transform;

namespace AegisBorn.Models.Base
{
    public abstract class AegisBornObject : IAegisBornObject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
