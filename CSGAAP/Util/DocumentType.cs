namespace CSGAAP.Util
{
    [Flags]
    public enum DocumentType
    {
        PDF = 1 << 0,
        DOC = 1 << 1,
        HTML = 1 << 2,
        GENERIC = 1 << 3,
        ALL = PDF | DOC | HTML | GENERIC
    }
}
