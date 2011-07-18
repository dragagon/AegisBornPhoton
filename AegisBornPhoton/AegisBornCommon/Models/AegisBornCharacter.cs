using System.Collections;

namespace AegisBornCommon.Models
{
    public class AegisBornCharacter : AegisBornObject
    {
        public string Sex { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }

        public override Hashtable GetHashtable()
        {
            var table = base.GetHashtable();

            table.Add(6, Sex);
            table.Add(7, Class);
            table.Add(8, Level);

            return table;
        }

        public AegisBornCharacter()
        {
            
        }

        public AegisBornCharacter(Hashtable table) : base(table)
        {
            Sex = (string)table[6];
            Class = (string)table[7];
            Level = (int)table[8];
        }
    }
}
