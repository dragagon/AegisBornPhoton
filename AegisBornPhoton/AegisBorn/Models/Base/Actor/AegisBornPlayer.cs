using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            BaseStats.STR = aegisBornCharacterDto.STR;
            BaseStats.AGI = aegisBornCharacterDto.AGI;
            BaseStats.VIT = aegisBornCharacterDto.VIT;
            BaseStats.INT = aegisBornCharacterDto.INT;
            BaseStats.DEX = aegisBornCharacterDto.DEX;
            BaseStats.LUK = aegisBornCharacterDto.LUK;
            UserId = aegisBornCharacterDto.UserId;
        }

        public AegisBornCharacterDTO CreateDTO()
        {
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
                                                                  STR = BaseStats.STR,
                                                                  AGI = BaseStats.AGI,
                                                                  VIT = BaseStats.VIT,
                                                                  INT = BaseStats.INT,
                                                                  DEX = BaseStats.DEX,
                                                                  LUK = BaseStats.LUK,
                                                                  UserId = UserId,
                                                              };
            return aegisBornCharacterDto;

        }

   }
}
