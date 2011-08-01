using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor;

namespace AegisBorn.Models.Base
{
    public class ObjectKnownList
    {
        private readonly AegisBornObject _activeObject;
        private Dictionary<int, AegisBornObject> _knownObjects;

        public ObjectKnownList(AegisBornObject activeObject)
        {
            _activeObject = activeObject;
        }

        public bool AddKnownObject(AegisBornObject obj)
        {
            if (obj == null)
                return false;

            // Instance -1 is for GMs that can see everything on all instances
            if (ActiveObject.InstanceId != -1
                    && (obj.InstanceId != ActiveObject.InstanceId))
                return false;

            if (obj is AegisBornPlayer
                    && ((AegisBornPlayer)obj).Appearance.IsGhost)
                return false;

            // Check if already know object
            if (KnowsObject(obj))
                return false;

            // Check if object is not inside distance to watch object
            //if (!Util.CheckIfInShortRadius(DistanceToWatchObject(obj), ActiveObject, obj, true))
            //    return false;

            if(KnownObjects.ContainsKey(obj.Id))
            {
                return false;
            }

            KnownObjects.Add(obj.Id, obj);
            return true;
        }

        public bool KnowsObject(AegisBornObject obj)
	{
		if (obj == null)
			return false;
		
		return ActiveObject == obj || KnownObjects.ContainsKey(obj.Id);
	}

        /** Remove all L2Object from _knownObjects */
        public void RemoveAllKnownObjects()
        {
            KnownObjects.Clear();
        }

        public bool RemoveKnownObject(AegisBornObject obj)
        {
            return RemoveKnownObject(obj, false);
        }

        protected bool RemoveKnownObject(AegisBornObject obj, bool forget)
	    {
		    if (obj == null)
			    return false;
		
		    if (forget) // on forget objects removed from list by iterator
			    return true;
		
            if(KnownObjects.ContainsKey(obj.Id))
	        {
	            KnownObjects.Remove(obj.Id);
	            return true;
	        }

            return false;
	    }

        //public void findObjects()
        //{
        //    L2WorldRegion region = ActiveObject.WorldRegion;
        //    if (region == null)
        //        return;

        //    if (ActiveObject is L2Playable)
        //    {
        //        foreach(L2WorldRegion regi in region.getSurroundingRegions()) // offer members of this and surrounding regions
        //        {
        //            Collection<L2Object> vObj = regi.getVisibleObjects().values();
        //                    foreach (L2Object obj in vObj)
        //                    {
        //                        if (obj != getActiveObject())
        //                        {
        //                            AddKnownObject(obj);
        //                            if (obj is L2Character)
        //                                obj.getKnownList().addKnownObject(getActiveObject());
        //                        }
        //                    }
        //        }
        //    }
        //    else if (ActiveObject is AegisBornCharacter)
        //    {
        //        foreach (L2WorldRegion regi in region.getSurroundingRegions()) // offer members of this and surrounding regions
        //        {
        //            if (regi.isActive())
        //            {
        //                Collection<L2Playable> vPls = regi.getVisiblePlayable().values();
        //                //synchronized (KnownListUpdateTaskManager.getInstance().getSync())
        //                {
        //                    //synchronized (regi.getVisiblePlayable())
        //                    {
        //                        foreach (AegisBornObject obj in vPls)
        //                            if (obj != ActiveObject)
        //                                AddKnownObject(obj);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public void forgetObjects(bool fullCheck)
        //{
        //        // Go through knownObjects
        //        var itr = KnownObjects.GetEnumerator();
        //        AegisBornObject obj;
        //        {
        //            while (itr.MoveNext())
        //            {
        //                obj = itr.
        //                if (obj == null)
        //                {
        //                    oIter.remove();
        //                    continue;
        //                }

        //                if (!fullCheck && !(obj is L2Playable))
        //                    continue;

        //                // Remove all objects invisible or too far
        //                if (!obj.isVisible()
        //                        || !Util.checkIfInShortRadius(DistanceToForgetObject(obj), ActiveObject, obj, true))
        //                {
        //                    oIter.remove();
        //                    removeKnownObject(obj, true);
        //                }
        //            }
        //        }
        //}

        public AegisBornObject ActiveObject
        {
            get { return _activeObject; }
        }

        public int DistanceToForgetObject(AegisBornObject obj)
        {
            return 0;
        }

        public int DistanceToWatchObject(AegisBornObject obj)
        {
            return 0;
        }

        /** Return the _knownObjects containing all L2Object known by the L2Character. */
        public Dictionary<int, AegisBornObject> KnownObjects
        {
            get { return _knownObjects ?? (_knownObjects = new Dictionary<int, AegisBornObject>()); }
        }
    }
}
