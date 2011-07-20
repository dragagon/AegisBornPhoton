namespace AegisBorn.Models.Base
{
    public class AegisBornObject
    {
        public int Id { get; set; }
        public string Name { get; set; }



        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
        }
    }
}
