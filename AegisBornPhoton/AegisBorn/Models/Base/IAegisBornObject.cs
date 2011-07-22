using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor;
using AegisBorn.Models.Base.Actor.Transform;

namespace AegisBorn.Models.Base
{
    public interface IAegisBornObject
    {
        int InstanceId { get; set; }
        bool IsVisible { get; }
        KnownObjectList KnownObjects { get; }
        string Name { get; set; }
        int ObjectId { get; }
        int DbId { get; }
        ObjectTransform Transform { get; }

        void Spawn();
        void Spawn(int x, int y, int z);
        void Decay();
        void OnSpawn();
        void RefreshId();
        void SendInfo(AegisBornPlayer player);
        void ToggleVisible();
    }
}
