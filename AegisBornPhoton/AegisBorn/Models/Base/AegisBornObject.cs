namespace AegisBorn.Models.Base
{
    public class AegisBornObject : AegisBornCommon.Models.AegisBornObject
    {
        public virtual KnownObjectList KnownList { get; set; }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
        }

        public AegisBornRegion WorldRegion
        {
            get { return null; }
        }
    }
}
