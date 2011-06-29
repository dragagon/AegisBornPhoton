namespace AegisBornCommon
{
    public enum ParameterCode : short
    {
        /// <summary>
        /// The error code.
        /// </summary>
        ErrorCode = 0,

        /// <summary>
        /// The debug message.
        /// </summary>
        DebugMessage = 1,

        /// <summary>
        /// Client key parameter used to establish secure communication.
        /// </summary>
        ClientKey = 16,

        /// <summary>
        /// Server key parameter used to establish secure communication.
        /// </summary>
        ServerKey = 17,

        /// <summary>
        /// The player's username
        /// </summary>
        UserName = 70,

        /// <summary>
        /// The player's password
        /// </summary>
        Password = 71,

        /// <summary>
        /// The number of character slots the player is allowed to have.
        /// </summary>
        CharacterSlots,

        /// <summary>
        /// The Players characters
        /// </summary>
        Characters,
    }
}
