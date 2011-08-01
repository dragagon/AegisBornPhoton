using System;
using Photon.SocketServer.Mmo;

namespace AegisBorn.Models.Base
{
    public class ObjectPosition
    {
        private AegisBornObject _activeObject;
        private int _heading = 0;
        private Vector _worldPosition;
        //private AegisBornWorldRegion _worldRegion;

        public ObjectPosition(AegisBornObject activeObject)
        {
            _activeObject = activeObject;
            //setWorldRegion(AegisBornWorld.getInstance().getRegion(getWorldPosition()));
        }

        public void SetXYZ(int x, int y, int z)
        {
            SetWorldPosition(x, y, z);

            try
            {
                //if (AegisBornWorld.getInstance().getRegion(getWorldPosition()) != getWorldRegion())
                //UpdateWorldRegion();
            }
            catch (Exception e)
            {
                //BadCoords();
            }
        }

        protected void BadCoords()
        {

        }

        public void SetWorldPosition(int x, int y, int z)
        {
            WorldPosition = new Vector { X = x, Y = y, Z = z };
        }

        public void SetXYZInvisible(int x, int y, int z)
        {
            //if (x > AegisBornWorld.MAP_MAX_X) x = AegisBornWorld.MAP_MAX_X - 5000;
            //if (x < AegisBornWorld.MAP_MIN_X) x = AegisBornWorld.MAP_MIN_X + 5000;
            //if (y > AegisBornWorld.MAP_MAX_Y) y = AegisBornWorld.MAP_MAX_Y - 5000;
            //if (y < AegisBornWorld.MAP_MIN_Y) y = AegisBornWorld.MAP_MIN_Y + 5000;

            SetWorldPosition(x, y, z);
            //ActiveObject.IsVisible = false;
        }

        public void UpdateWorldRegion()
        {
            //if (!ActiveObject.IsVisible) return;

            //AegisBornWorldRegion newRegion = AegisBornWorld.getInstance().getRegion(WorldPosition);
            //if (newRegion != WorldRegion)
            //{
            //    WorldRegion.removeVisibleObject(ActiveObject());

            //    setWorldRegion(newRegion);

            //    // Add the L2Oject spawn to _visibleObjects and if necessary to _allplayers of its L2WorldRegion
            //    WorldRegion.addVisibleObject(ActiveObject());
            //}
        }

        public AegisBornObject ActiveObject
        { get { return _activeObject; } }

        public int Heading
        {
            get { return _heading; }
            set { _heading = value; }
        }

        public int X
        {
            get { return _worldPosition.X; }
            set { _worldPosition.X = value; }
        }

        public int Y
        {
            get { return _worldPosition.Y; }
            set { _worldPosition.Y = value; }
        }

        public int Z
        {
            get { return _worldPosition.Z; }
            set { _worldPosition.Z = value; }
        }

        public Vector WorldPosition
        {
            get { return _worldPosition; }
            set { _worldPosition = value; }
        }

        //public AegisBornWorldRegion WorldRegion
        //{
        //    get { return _worldRegion; }
        //    set
        //    {
        //        if(_worldRegion != null && ActiveObject is AegisBornCharacter) // confirm revalidation of old region's zones
        //        {
        //            if (value != null)
        //                _worldRegion.revalidateZones((AegisBornCharacter)ActiveObject);    // at world region change
        //            else
        //                _worldRegion.removeFromZones((AegisBornCharacter)ActiveObject);    // at world region change
        //        }

        //        _worldRegion = value;
        //    }
        //}
    }
}
