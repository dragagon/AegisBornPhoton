using System;
using Photon.SocketServer.Mmo;

namespace AegisBorn.Models.Base.Actor.Transform
{
    public class ObjectTransform
    {
        private AegisBornObject _activeObject;
        private Vector _position;
        //private AegisBornRegion _region;

        public AegisBornObject ActiveObject
        {
            get { return _activeObject; }
        }

        public int Heading { get; set; }

        public ObjectTransform(AegisBornObject aegisBornObject)
        {
            _activeObject = aegisBornObject;
        }

        public void SetXYZ(int x, int y, int z)
        {
            SetPosition(x, y, z);

            try
            {
                // if(AegisBornWorld.Instance.getRegion(_position) != _region)
                // {
                //   updateRegion();
                // }
            }
            catch (Exception)
            {
                BadCoords();
            }
        }

        protected virtual void BadCoords()
        {

        }

        public void SetXYZInvisible(int x, int y, int z)
        {
            SetPosition(x, y, z);
            //_activeObject.IsVisible = false;
        }

        public void SetPosition(int x, int y, int z)
	    {
            _position.X = x;
            _position.Y = y;
            _position.Z = z;
        }

        public int X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }
        public int Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }
        public int Z
        {
            get { return _position.Z; }
            set { _position.Z = value; }
        }

    }
}
