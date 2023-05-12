namespace CSGAAP.Exceptions
{
    public class AnalyzeException : Exception
    {
        public AnalyzeException() { }
        public AnalyzeException(string msg) : base(msg) { }
        public AnalyzeException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
