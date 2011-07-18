using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor;

namespace AegisBorn.Models.Base
{
    public class AegisBornRegion
    {
        private int _coordX, _coordY;
        private IList<AegisBornRegion> _surroundingRegions;
        private bool _active;
        private Dictionary<int, AegisBornObject> _visibleObjects;
        private Dictionary<int, AegisBornPlayable> _allPlayable;

        public AegisBornRegion(int coordX, int coordY)
	    {
		    _allPlayable = new Dictionary<int, AegisBornPlayable>();
            _visibleObjects = new Dictionary<int, AegisBornObject>();
            _surroundingRegions = new List<AegisBornRegion>();

            _coordX = coordX;
            _coordY = coordY;
		
			_active = false;
		    //_zones = new List<AegisBornZone>();
	    }

        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active == value)
                    return;

                _active = value;

                //switchAI(value);
            }
        }

    	public bool AreNeighborsEmpty()
	    {
		    if (Active && _allPlayable.Count != 0)
			    return false;
		
    	    return _surroundingRegions.All(neighbor => !neighbor.Active || neighbor._allPlayable.Count == 0);
	    }


        //private void Activate()
        //{
        //    // first set self to active and do self-tasks...
        //    Active = true;
		
        //    // if the timer to deactivate neighbors is running, cancel it.
        //    lock(this)
        //    {
        //        if(_neighborsTask !=null)
        //        {
        //            _neighborsTask.cancel(true);
        //            _neighborsTask = null;
        //        }
			
        //        _neighborsTask = ThreadPoolManager.getInstance().scheduleGeneral(new NeighborsTask(true), 1000*Config.GRID_NEIGHBOR_TURNON_TIME);
        //    }
        //}

        //private void Deactivate()
        //{
        //    // if the timer to activate neighbors is running, cancel it.
        //    lock(this)
        //    {
        //        if(_neighborsTask !=null)
        //        {
        //            _neighborsTask.cancel(true);
        //            _neighborsTask = null;
        //        }
			
        //        _neighborsTask = ThreadPoolManager.getInstance().scheduleGeneral(new NeighborsTask(false), 1000*Config.GRID_NEIGHBOR_TURNOFF_TIME);
        //    }
        //}

        public void AddVisibleObject(AegisBornObject aegisBornObject)
        {
	        if (aegisBornObject == null)
		        return;
		
            if(aegisBornObject.WorldRegion == this)
            {
                throw new ArgumentException();
            }
		
	        _visibleObjects.Add(aegisBornObject.Id,aegisBornObject);
		
	        if (aegisBornObject is AegisBornPlayable)
	        {
                _allPlayable.Add(aegisBornObject.Id, (AegisBornPlayable)aegisBornObject);
			
		        // if this is the first player to enter the region, activate self & neighbors
                //if (_allPlayable.Count == 1)
                //    Activate();
	        }
        }

        public void RemoveVisibleObject(AegisBornObject aegisBornObject)
	    {
		    if (aegisBornObject == null)
			    return;
		
            if(aegisBornObject.WorldRegion == null || aegisBornObject.WorldRegion != this)
            {
                throw new ArgumentException();
            }
		
		    _visibleObjects.Remove(aegisBornObject.Id);
		
		    if (aegisBornObject is AegisBornPlayable)
		    {
			    _allPlayable.Remove(aegisBornObject.Id);
			
                //if (_allPlayable.Count == 0)
                //    Deactivate();
		    }
	    }

        public void AddSurroundingRegion(AegisBornRegion region)
        {
            _surroundingRegions.Add(region);
        }

        public IList<AegisBornRegion> SurroundingRegions
        {
            get { return _surroundingRegions; }
        }

        public Dictionary<int, AegisBornPlayable> VisiblePlayable
        {
            get { return _allPlayable; }
        }

        public Dictionary<int, AegisBornObject> VisibleObjects
        {
            get { return _visibleObjects; }
        }

        public new String ToString()
        {
            return "(" + _coordX + ", " + _coordY + ")";
        }

        //public void deleteVisibleNpcSpawns()
        //{
        //    var vNPC = _visibleObjects.Values;
        //    foreach (AegisBornNPC target in vNPC)
        //    {
        //        target.deleteMe();
        //        AegisBornSpawn spawn = target.Spawn;
        //        if (spawn != null)
        //        {
        //            spawn.stopRespawn();
        //            SpawnTable.getInstance().deleteSpawn(spawn, false);
        //        }
        //    }
        //}

    }
}
