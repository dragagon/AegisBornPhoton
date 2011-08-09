using AegisBorn.Models.Base.Actor.Transform;

namespace AegisBorn.Models.Base
{
    public abstract class AegisBornObject : IAegisBornObject
    {
        public virtual int Id
        {
            get { return ObjectId; }
            set { ObjectId = value;  }
        }
        public virtual string Name { get; set; }

        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float Z { get; set; }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                if (!_isVisible)
                {
                    //Transform.Region = null;
                }
            }
        }

        public int InstanceId
        {
            get;
            set;
        }

        public KnownObjectList KnownObjects
        {
            get { throw new System.NotImplementedException(); }
        }

        public int ObjectId
        {
            get; private set; }

        public int DbId
        {
            get { throw new System.NotImplementedException(); }
        }

        public ObjectTransform Transform { get; private set; }

        public void Spawn()
        {
            // use our Transform to determine region and set up objects and known list.
        }

        public void Spawn(int x, int y, int z)
        {
            // Spawn at a specific point instead of where the transform is. We need to update the transform here too.
        }

        public void Decay()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnSpawn()
        {
        }

        public void RefreshId()
        {
            throw new System.NotImplementedException();
        }

        public void SendInfo(Actor.AegisBornPlayer player)
        {
        }

        public void ToggleVisible()
        {
            if(IsVisible)
            {
                Decay();
            }
            else
            {
                Spawn();
            }
        }
    }
}
