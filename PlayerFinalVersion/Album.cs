namespace PlayerFinalVersion
{
    public class Album
    {
        private string name;
        
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (value == null)
                {
                    name = "unknown album";
                }
                else
                {
                    name = value;
                }
            }
        }

        public int Year { get; set; }

        public Album(string name, int year)
        {
            Name = name;
            Year = year;
        }
    }
}