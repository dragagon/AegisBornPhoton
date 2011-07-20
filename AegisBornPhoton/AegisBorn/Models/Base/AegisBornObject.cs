namespace AegisBorn.Models.Base
{
    public class AegisBornObject : IAegisBornObject
    {
        public int Id
        {
            get { return ObjectId; }
            set { ObjectId = value;  }
        }
        public string Name { get; set; }



        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
        }

        public int InstanceId
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public KnownObjectList KnownObjects
        {
            get { throw new System.NotImplementedException(); }
        }

        public int ObjectId
        {
            get { throw new System.NotImplementedException(); }
            private set { throw new System.NotImplementedException(); }
        }

        public int DbId
        {
            get { throw new System.NotImplementedException(); }
        }

        public ObjectTransform Transform
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Spawn()
        {
            throw new System.NotImplementedException();
        }

        public void Spawn(int x, int y, int z)
        {
            throw new System.NotImplementedException();
        }

        public void Decay()
        {
            throw new System.NotImplementedException();
        }

        public void OnSpawn()
        {
            throw new System.NotImplementedException();
        }

        public void RefreshId()
        {
            throw new System.NotImplementedException();
        }

        public void SendInfo(Actor.AegisBornPlayer player)
        {
            throw new System.NotImplementedException();
        }

        public void ToggleVisible()
        {
            throw new System.NotImplementedException();
        }
    }
}
