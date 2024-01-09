namespace CsprojLangVersionProcessor.Editor
{
    static class LangVersionSettings
    {
        public static LangVersionType type;
    }

    public enum LangVersionType
    {
        UseDefaultVersion = 0,
        CSharp3 = 3,
        CSharp4 = 4,
        CSharp5 = 5,
        CSharp6 = 6,
        CSharp7 = 7,
        CSharp8 = 8,
        CSharp9 = 9,
        CSharp10 = 10,
        Default = 100,
        Latest = 101,
        Preview = 102,
    }
}