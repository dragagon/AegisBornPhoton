using AegisBorn.Models.Base.Actor;
using AegisBorn.Models.Base.Actor.Stats;
using FluentNHibernate.Mapping;

namespace AegisBorn.Models.Map
{
    public class AegisBornCharacterMap : ClassMap<AegisBornCharacter>
    {
        public AegisBornCharacterMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.Name).Column("name");
            Map(x => x.Level).Column("level");
            Map(x => x.Class).Column("class");
            Map(x => x.Sex).Column("sex");
            Map(x => x.X).Column("position_x");
            Map(x => x.Y).Column("position_y");
            Map(x => x.Z).Column("position_z");
            Map(x => x.BaseStats[(int)Stats.STR]).Column("str");
            Map(x => x.BaseStats[(int)Stats.AGI]).Column("agi");
            Map(x => x.BaseStats[(int)Stats.VIT]).Column("vit");
            Map(x => x.BaseStats[(int)Stats.INT]).Column("_int");
            Map(x => x.BaseStats[(int)Stats.DEX]).Column("dex");
            Map(x => x.BaseStats[(int)Stats.LUK]).Column("luk");
            References(x => x.UserId).Column("user_id");
            Table("aegis_born_character");
        }
    }
}
