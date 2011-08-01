using System.Collections.Generic;
using System.Linq;
using AegisBorn.Models.Base.Actor;
using Photon.SocketServer.Mmo;

namespace AegisBorn.Models.Base
{
    public sealed class AegisBornWorld
    {
        private const int _numRegionsX = 2;
        private const int _numRegionsY = 2;
        private Dictionary<int, AegisBornPlayer> _allPlayers;

        private Dictionary<int, AegisBornObject> _allObjects;

        private AegisBornRegion[,] _worldRegions;

        private static AegisBornWorld _world;

        private AegisBornWorld()
        {
            _allPlayers = new Dictionary<int, AegisBornPlayer>();
            _allObjects = new Dictionary<int, AegisBornObject>();

            CreateRegions();
        }

        public static AegisBornWorld World
        {
            get { return _world ?? (_world = new AegisBornWorld()); }
        }

        private void CreateRegions()
	    {
		    _worldRegions = new AegisBornRegion[_numRegionsX,_numRegionsY];
		
		    for (int i = 0; i < _numRegionsX; i++)
		    {
			    for (int j = 0; j < _numRegionsY; j++)
			    {
				    _worldRegions[i,j] = new AegisBornRegion(i, j);
			    }
		    }
		
		    for (int x = 0; x < _numRegionsX; x++)
		    {
			    for (int y = 0; y < _numRegionsY; y++)
			    {
				    for (int a = -1; a <= 1; a++)
				    {
					    for (int b = -1; b <= 1; b++)
					    {
						    if (ValidRegion(x + a, y + b))
						    {
							    _worldRegions[x + a,y + b].AddSurroundingRegion(_worldRegions[x,y]);
						    }
					    }
				    }
			    }
		    }
	    }

        private bool ValidRegion(int x, int y)
        {
            return (x >= 0 && x <= _numRegionsX && y >= 0 && y <= _numRegionsY);
        }

        public void StoreObject(AegisBornObject aegisBornObject)
        {
            _allObjects.Add(aegisBornObject.Id, aegisBornObject);
        }

        public void RemoveObject(AegisBornObject aegisBornObject)
        {
            _allObjects.Remove(aegisBornObject.Id);
        }

        public void RemoveObjects(List<AegisBornObject> aegisBornObjects)
        {
            foreach(AegisBornObject aegisBornObject in aegisBornObjects)
            {
                _allObjects.Remove(aegisBornObject.Id);
            }
        }

        public AegisBornObject FindObject(int id)
        {
            return _allObjects[id];
        }

        public int AllObjectsCount
        {
            get { return _allObjects.Count; }
        }

        public Dictionary<int, AegisBornPlayer> AllPlayers
        {
            get { return _allPlayers; }
        }

        public int AllPlayersCount
        {
            get { return _allPlayers.Count; }
        }

        public AegisBornPlayer GetPlayer(string name)
        {
//            return GetPlayer()
            return null;
        }

        public AegisBornPlayer GetPlayer(int playerId)
        {
            return _allPlayers[playerId];
        }

        public void AddVisibleObject(AegisBornObject aegisBornObject, AegisBornRegion aegisBornRegion)
        {
            var player = aegisBornObject as AegisBornPlayer;
            if (player != null)
		    {
    		    if (!player.Teleporting)
			    {
				    if (_allPlayers.ContainsKey(player.Id))
				    {
					    player.Logout();
                        _allPlayers[player.Id].Logout();
					    return;
				    }
				    _allPlayers.Add(player.Id, player);
			    }
		    }

            if(!aegisBornRegion.Active)
            {
                return;
            }

            List<AegisBornObject> visibles = GetVisibleObjects(aegisBornObject, 2000);
            foreach(AegisBornObject abo in visibles)
            {
                abo.KnownList.AddKnownObject(aegisBornObject);
                aegisBornObject.KnownList.AddKnownObject(abo);
            }
        }

        public void AddToAllPlayers(AegisBornPlayer player)
        {
            _allPlayers.Add(player.Id, player);
        }

        public void RemoveFromAllPlayers(AegisBornPlayer player)
        {
            _allPlayers.Remove(player.Id);
        }

        public void RemoveVisibleObject(AegisBornObject aegisBornObject, AegisBornRegion aegisBornRegion)
        {
            if(aegisBornObject == null)
            {
                return;
            }

            if(aegisBornRegion != null)
            {
                aegisBornRegion.RemoveVisibleObject(aegisBornObject);

                foreach(AegisBornRegion region in aegisBornRegion.SurroundingRegions)
                {
                    foreach (var entry in region.VisibleObjects)
                    {
                        entry.Value.KnownList.RemoveKnownObject(aegisBornObject);
                        aegisBornObject.KnownList.RemoveKnownObject(entry.Value);
                    }
                }
            }

            aegisBornObject.KnownList.RemoveAllKnownObjects();

            var player = aegisBornObject as AegisBornPlayer;
            if(player != null && !player.Teleporting)
            {
                RemoveFromAllPlayers(player);
            }
        }

        public List<AegisBornObject> GetVisibleObjects(AegisBornObject aegisBornObject)
        {
            AegisBornRegion reg = aegisBornObject.WorldRegion;

            if(reg == null)
            {
                return new List<AegisBornObject>();
            }

            // Use linq to search the visible objects and only return those that are not us in this and surrounding regions
            List<AegisBornObject> retList = (from surroundingRegion in reg.SurroundingRegions
                                            from vObject in surroundingRegion.VisibleObjects
                                            select vObject.Value
                                            into visibleObject where visibleObject != aegisBornObject where visibleObject.IsVisible select visibleObject).ToList();

            return retList;
        }

        public List<AegisBornObject> GetVisibleObjects(AegisBornObject aegisBornObject, int radius)
        {
            if(aegisBornObject == null || !aegisBornObject.IsVisible)
            {
                return new List<AegisBornObject>();
            }

            float x = aegisBornObject.X;
            float y = aegisBornObject.Y;

            int radiusSquared = radius*radius;

            List<AegisBornObject> retList = (from region in aegisBornObject.WorldRegion.SurroundingRegions
                                             from entry in region.VisibleObjects
                                             where entry.Value != aegisBornObject
                                             let x1 = entry.Value.X
                                             let y1 = entry.Value.Y
                                             let dx = x1 - x
                                             let dy = y1 - y
                                             where (dx*(double) dx) + (dy*(double) dy) < radiusSquared
                                             select entry.Value).ToList();

            return retList;
        }

        public List<AegisBornObject> GetVisibleObjects3D(AegisBornObject aegisBornObject, int radius)
        {
            if (aegisBornObject == null || !aegisBornObject.IsVisible)
            {
                return new List<AegisBornObject>();
            }

            float x = aegisBornObject.X;
            float y = aegisBornObject.Y;
            float z = aegisBornObject.Z;

            int radiusSquared = radius * radius;

            List<AegisBornObject> retList = (from region in aegisBornObject.WorldRegion.SurroundingRegions
                                             from entry in region.VisibleObjects
                                             where entry.Value != aegisBornObject
                                             let x1 = entry.Value.X
                                             let y1 = entry.Value.Y
                                             let z1 = entry.Value.Z 
                                             let dx = x1 - x
                                             let dy = y1 - y
                                             let dz = z1 - z
                                             where (dx * (double)dx) + (dy * (double)dy + (dz * (double)dz)) < radiusSquared
                                             select entry.Value).ToList();

            return retList;
        }

        public List<AegisBornPlayable> getVisiblePlayable(AegisBornObject aegisBornObject)
        {
            AegisBornRegion region = aegisBornObject.WorldRegion;

            var retList = new List<AegisBornPlayable>();

            if (region == null)
            {
                return retList;
            }

            retList.AddRange(from aegisBornRegion in region.SurroundingRegions
                             from aegisBornPlayable in aegisBornRegion.VisiblePlayable
                             where aegisBornPlayable.Value != aegisBornObject && aegisBornPlayable.Value.IsVisible
                             select aegisBornPlayable.Value);

            return retList;
        }

        public AegisBornRegion GetRegion(Vector point)
        {
            return null;
        }

        public AegisBornRegion GetRegion(int x, int y)
        {
            return null;
        }

        public AegisBornRegion[,] WorldRegions
        {
            get { return _worldRegions; }
        }
    }
}
