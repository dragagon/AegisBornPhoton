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

        public void setXYZ(int x, int y, int z)
        {
            setPosition(x, y, z);

            try
            {
                // if(AegisBornWorld.Instance.getRegion(_position) != _region)
                // {
                //   updateRegion();
                // }
            }
            catch (Exception)
            {
                badCoords();
            }
        }

        protected virtual void badCoords()
        {

        }

        public void setXYZInvisible(int x, int y, int z)
        {
            setPosition(x, y, z);
            _activeObject.IsVisible = false;
        }
    }
}
