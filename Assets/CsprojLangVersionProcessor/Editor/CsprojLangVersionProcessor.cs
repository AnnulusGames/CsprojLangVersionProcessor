using System.Xml.Linq;
using UnityEditor;

namespace CsprojLangVersionProcessor.Editor
{
    public sealed class LangVersionProcessor : AssetPostprocessor
    {
        static string OnGeneratedCSProject(string path, string content)
        {
            return OverwriteLangVersion(content, LangVersionSettings.type);
        }

        static string OverwriteLangVersion(string content, LangVersionType langVersionType)
        {
            var versionString = GetCsVersionString(langVersionType);
            if (versionString == null) return content;

            var doc = XDocument.Parse(content);
            foreach (var element in doc.Descendants("LangVersion"))
            {
                element.Value = versionString;
            }

            return doc.ToString();
        }

        static string GetCsVersionString(LangVersionType type)
        {
            return type switch
            {
                LangVersionType.CSharp3 => "3",
                LangVersionType.CSharp4 => "4",
                LangVersionType.CSharp5 => "5",
                LangVersionType.CSharp6 => "6",
                LangVersionType.CSharp7 => "7.0",
                LangVersionType.CSharp8 => "8.0",
                LangVersionType.CSharp9 => "9.0",
                LangVersionType.CSharp10 => "10.0",
                LangVersionType.Default => "default",
                LangVersionType.Latest => "latest",
                LangVersionType.Preview => "preview",
                _ => null
            }; 
        }
    }
}