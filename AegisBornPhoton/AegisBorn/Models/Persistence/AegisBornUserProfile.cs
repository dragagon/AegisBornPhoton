namespace AegisBorn.Models.Persistence
{
    public class AegisBornUserProfile
    {
        public virtual int Id { get; set; }
        public virtual SfGuardUser UserId { get; set; }
        public virtual int CharacterSlots { get; set; }
    }
}
