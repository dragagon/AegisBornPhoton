using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base
{
    public abstract class KnownObjectList
    {
        private readonly AegisBornObject _activeObject;
        private Dictionary<int, AegisBornObject> _knownObjects;

        public AegisBornObject ActiveObject { get { return _activeObject; } }

        public KnownObjectList(AegisBornObject aegisBornObject)
        {
            _activeObject = aegisBornObject;
            _knownObjects = new Dictionary<int, AegisBornObject>();
        }

        public virtual bool Add(AegisBornObject aegisBornObject)
        {
            // if it is null, the object is already in the list, or it is outside of the watch radius, skip it.
            if(aegisBornObject == null || Contains(aegisBornObject) || Util.IsInRadius(DistanceToWatch(aegisBornObject), _activeObject, aegisBornObject, true))
            {
                return false;
            }

            _knownObjects.Add(aegisBornObject.Id, aegisBornObject);
            return true;
        }

        public bool Contains(AegisBornObject aegisBornObject)
        {
            if(aegisBornObject == null)
            {
                return false;
            }
            return _activeObject == aegisBornObject || _knownObjects.ContainsKey(aegisBornObject.Id);
        }

        public virtual bool Remove(AegisBornObject aegisBornObject)
        {
            return Remove(aegisBornObject, false);
        }

        public virtual bool Remove(AegisBornObject aegisBornObject, bool forget)
        {
            if (aegisBornObject == null)
            {
                return false;
            }

            // remove on timer not immediately
            if (forget)
            {
                return true;
            }

            _knownObjects.Remove(aegisBornObject.Id);
            return true;

        }

        public virtual void Clear()
        {
            _knownObjects.Clear();
        }

        public virtual int DistanceToForget(AegisBornObject aegisBornObject)
        {
            return 0;
        }

        public virtual int DistanceToWatch(AegisBornObject aegisBornObject)
        {
            return 0;
        }
    }
}
