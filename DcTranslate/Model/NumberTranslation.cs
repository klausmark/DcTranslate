namespace DcTranslate.Model
{
    public class NumberTranslation
    {
        public NumberTranslation(string fromNumber, string toNumber, string description)
        {
            FromNumber = fromNumber;
            ToNumber = toNumber;
            Description = description;
        }

        public string FromNumber { get; }
        public string ToNumber { get;  }
        public string Description { get; }
    }
}
