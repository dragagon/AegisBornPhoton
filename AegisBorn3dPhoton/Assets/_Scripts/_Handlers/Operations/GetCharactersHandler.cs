using System.Collections;
using System.Collections.Generic;
using AegisBornCommon;
using AegisBornCommon.Models;

public class GetCharactersHandler : IOperationHandler
{
    public List<AegisBornCharacter> Characters;
    public int CharacterSlots;

    #region Overrides of IOperationHandler

    public override void OnHandleMessage(PhotonClient gameLogic, OperationCode operationCode, int returnCode, Hashtable returnValues)
    {
        Characters = new List<AegisBornCharacter>();
        CharacterSlots = (int)returnValues[(byte)ParameterCode.CharacterSlots];
        var characterList = returnValues[(byte) ParameterCode.Characters] as Hashtable;
        if (characterList != null)
        {
            foreach (DictionaryEntry character in characterList)
            {
                var characterhash = character.Value as Hashtable;

                if (characterhash != null)
                {
                    Characters.Add(new AegisBornCharacter(characterhash));
                }
            }
        }
    }

    #endregion
}
