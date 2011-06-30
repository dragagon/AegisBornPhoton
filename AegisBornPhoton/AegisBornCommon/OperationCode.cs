namespace AegisBornCommon
{
    public enum OperationCode : short
    {
        /// <summary>
        /// The nil (nothing).
        /// </summary>
        Nil = 0,

        /// <summary>
        /// Code for exchanging keys using PhotonPeer.OpExchangeKeysForEncryption
        /// </summary>
        ExchangeKeysForEncryption = 95,

        /// <summary>
        /// Login to the server
        /// </summary>
        Login = 100,

        /// <summary>
        /// Op Code to leave the game once we are logged in.
        /// </summary>
        ExitGame,

        /// <summary>
        /// Code for getting characters from the server
        /// </summary>
        GetCharacters,

        /// <summary>
        /// Code for creating a new character
        /// </summary>
        CreateCharacter,
    }
}
