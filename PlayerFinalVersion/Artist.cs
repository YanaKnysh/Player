namespace PlayerFinalVersion
{
    public class Artist
    {
        private string name;

        public string Name {
            get
            {
                return name;
            }
            private set
            {
                if (value == null)
                {
                    name = "unknown artist";
                }
                else
                {
                    name = value;
                }
            }
        }

        public Artist(string name)
        {
            Name = name;
        }
    }
}