using System.Collections;

namespace AegisBornCommon.Models
{
    public class AegisBornCharacter : AegisBornItem
    {
        public string Sex { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public override Hashtable GetHashtable()
        {
            var table = base.GetHashtable();

            table.Add(3, Sex);
            table.Add(4, Class);
            table.Add(5, Level);
            table.Add(6, PositionX);
            table.Add(7, PositionY);

            return table;
        }

        public AegisBornCharacter()
        {
            
        }

        public AegisBornCharacter(Hashtable table) : base(table)
        {
            Sex = (string)table[3];
            Class = (string)table[4];
            Level = (int)table[5];
            PositionX = (int)table[6];
            PositionY = (int)table[7];
        }
    }
}
