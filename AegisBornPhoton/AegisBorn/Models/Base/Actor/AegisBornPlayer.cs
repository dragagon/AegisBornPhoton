using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AegisBorn.Models.Base.Actor.Stats;
using AegisBorn.Models.Persistence;
using Photon.SocketServer.Rpc;

namespace AegisBorn.Models.Base.Actor
{
    /// <summary>
    ///  This is a real player on a real client, here we can send packets back to the user when things happen.
    /// </summary>
    public class AegisBornPlayer : AegisBornPlayable
    {
        public Peer Peer { get; set; }
        public void Logout()
        {
            // kick the player out of the game.
        }

        public bool Teleporting { get; set; }

        public void CreateFromDTO( AegisBornCharacterDTO aegisBornCharacterDto)
        {
            Id = aegisBornCharacterDto.Id;
            Name = aegisBornCharacterDto.Name;
            Level = aegisBornCharacterDto.Level;
            Class = aegisBornCharacterDto.Class;
            Sex = aegisBornCharacterDto.Sex;
            X = aegisBornCharacterDto.X;
            Y = aegisBornCharacterDto.Y;
            Z = aegisBornCharacterDto.Z;

            XmlSerializer mySerializer = new XmlSerializer(typeof(BaseStats));
            StringReader inStream = new StringReader(aegisBornCharacterDto.BaseStats);

            BaseStats = (BaseStats)mySerializer.Deserialize(inStream);

            UserId = aegisBornCharacterDto.UserId;
        }

        public AegisBornCharacterDTO CreateDTO()
        {

            XmlSerializer mySerializer = new XmlSerializer(typeof(BaseStats));
            StringWriter outStream = new StringWriter();
            mySerializer.Serialize(outStream, BaseStats);
            AegisBornCharacterDTO aegisBornCharacterDto = new AegisBornCharacterDTO
                                                              {
                                                                  Id = Id,
                                                                  Name = Name,
                                                                  Level = Level,
                                                                  Class = Class,
                                                                  Sex = Sex,
                                                                  X = X,
                                                                  Y = Y,
                                                                  Z = Z,
                                                                  // Change this to be a string that will be serialized/deserialized
                                                                  BaseStats = outStream.ToString(),
                                                                  UserId = UserId,
                                                              };
            return aegisBornCharacterDto;

        }

   }
}
