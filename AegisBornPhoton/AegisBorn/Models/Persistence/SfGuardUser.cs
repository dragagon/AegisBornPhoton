namespace AegisBorn.Models.Persistence
{
	public class SfGuardUser
	{
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
	}
}
