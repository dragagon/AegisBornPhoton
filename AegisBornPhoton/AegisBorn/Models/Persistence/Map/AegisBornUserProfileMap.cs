using AegisBorn.Models.Base;
using AegisBorn.Models.Persistence;
using FluentNHibernate.Mapping;

namespace AegisBorn.Models.Map
{
    public class AegisBornUserProfileMap : ClassMap<AegisBornUserProfile>
    {
        public AegisBornUserProfileMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.CharacterSlots).Column("character_slots");
            References(x => x.UserId).Column("user_id");
            Table("aegis_born_user_profile");
        }
    }
}
