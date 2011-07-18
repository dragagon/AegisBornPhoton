using System.Collections;

namespace AegisBornCommon.Models
{
    public abstract class AegisBornObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public virtual Hashtable GetHashtable()
        {
            var table = new Hashtable
                            {
                                {1, Id},
                                {2, Name},
                                {3, X},
                                {4, Y},
                                {5, Z},
                            };
            return table;
        }

        protected AegisBornObject(Hashtable table)
        {
            Id = (int) table[1];
            Name = (string) table[2];
            X = (float)table[3];
            Y = (float)table[4];
            Z = (float)table[5];
        }

        protected AegisBornObject()
        {
            
        }

        #region Equality
        public override int GetHashCode()
        { 
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        { 
            if(obj == null || GetType() != obj.GetType())
                return false;

            // Call this if m_data is a value type
            var rhs = (AegisBornObject)obj;
            return Id == rhs.Id;  
        }

        public static bool operator ==(AegisBornObject lhs, AegisBornObject rhs)
        {
            return lhs != null && lhs.Equals(rhs);
        }

        public static bool operator !=(AegisBornObject lhs, AegisBornObject rhs)
        { 
            return !(lhs == rhs);
        }
        #endregion
    }
}
