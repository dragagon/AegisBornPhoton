// Type: AegisBornCommon.Models.AegisBornCharacter
// Assembly: AegisBornCommon, Version=1.0.0.0, Culture=neutral
// Assembly location: D:\development\photon\AegisBorn3dPhoton\Assets\Plugins\AegisBornCommon.dll

using System.Collections;

namespace AegisBornCommon.Models
{
    public class AegisBornCharacter : AegisBornObject
    {
        public AegisBornCharacter();
        public AegisBornCharacter(Hashtable table);
        public string Sex { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public override Hashtable GetHashtable();
    }
}
