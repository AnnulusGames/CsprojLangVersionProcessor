using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsprojLangVersionProcessor.Editor
{
    static class CsprojLangVersionSettingsRegister
    {
        const string ConfigKey = "CsprojLangVersionProcessor-Version";


        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            if (Enum.TryParse<LangVersionType>(EditorUserSettings.GetConfigValue(ConfigKey), out var result))
            {
                LangVersionSettings.type = result;
            }

            var provider = new SettingsProvider("Project/Editor/Csproj LangVersion", SettingsScope.Project)
            {
                label = "Csproj LangVersion",
                guiHandler = (searchContext) =>
                {
                    using var changeCheck = new EditorGUI.ChangeCheckScope();

                    GUILayout.Space(10);
                    EditorGUILayout.HelpBox("After changing LangVersion, you need to regenerate the csproj files from 'Edit > Preferences > External Tools > Regenerate Project Files'.", MessageType.Warning);

                    LangVersionSettings.type = (LangVersionType)EditorGUILayout.EnumPopup("LangVersion", LangVersionSettings.type);
                    if (changeCheck.changed)
                    {
                        EditorUserSettings.SetConfigValue(ConfigKey, LangVersionSettings.type.ToString());
                    }

                    GUILayout.Space(10);

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Space(15);

                        if (GUILayout.Button("Regenerate Project Files"))
                        {
                            CodeEditorHelper.RegenerateCSharpProjects();
                        }

                        GUILayout.Space(15);
                    }
                },
                keywords = new HashSet<string>(new[] { "csproj", "langVersion" })
            };

            return provider;
        }
    }
}