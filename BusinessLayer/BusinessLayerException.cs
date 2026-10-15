namespace BusinessLayer
{
    public class BusinessLayerException : Exception
    {
        public BusinessLayerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
