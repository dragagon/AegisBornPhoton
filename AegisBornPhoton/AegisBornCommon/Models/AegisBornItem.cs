using System.Collections;

namespace AegisBornCommon.Models
{
    public abstract class AegisBornItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Hashtable GetHashtable()
        {
            var table = new Hashtable()
                            {
                                {1, Id},
                                {2, Name},
                            };
            return table;
        }

        protected AegisBornItem(Hashtable table)
        {
            Id = (int) table[1];
            Name = (string) table[2];
        }

        protected AegisBornItem()
        {
            
        }
    }
}
