using System.Collections;
using AegisBornCommon;

public static class LoginOperations
{
    public static void Login(PhotonClient game, string username, string password)
    {
        var vartable = new Hashtable
                           {
                               {(byte) ParameterCode.UserName, username},
                               {(byte) ParameterCode.Password, password}
                           };

        game.SendOp(OperationCode.Login, vartable, true, 0, true);
    }

    public static void GetCharacters(PhotonClient game)
    {
        game.SendOp(OperationCode.GetCharacters, new Hashtable(), true, 0, false);
    }

    public static void ExitWorld(PhotonClient game)
    {
        game.SendOp(OperationCode.ExitGame, new Hashtable(), true, 0, false );
    }

    public static void CreateCharacter(PhotonClient game, string characterName, string sex, string characterClass)
    {
        var vartable = new Hashtable
                           {
                               {(byte) ParameterCode.CharacterName, characterName},
                               {(byte) ParameterCode.CharacterSex, sex},
                               {(byte) ParameterCode.CharacterClass, characterClass},
                           };
        game.SendOp(OperationCode.CreateCharacter, vartable, true, 0, false);
    }

    public static void SelectCharacter(PhotonClient game, int characterId)
    {
        var vartable = new Hashtable
                           {
                               {(byte) ParameterCode.ObjectId, characterId},
                           };
        game.SendOp(OperationCode.SelectCharacter, vartable, true, 0, false);
    }
}
