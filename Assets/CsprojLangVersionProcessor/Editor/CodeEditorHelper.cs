using System;
using System.Reflection;
using Unity.CodeEditor;

#if IDE_VISUAL_STUDIO
using Microsoft.Unity.VisualStudio.Editor;
#endif

namespace CsprojLangVersionProcessor.Editor
{
    public static class CodeEditorHelper
    {
        public static void RegenerateCSharpProjects()
        {
#if IDE_VISUAL_STUDIO
            if (CodeEditor.CurrentEditor is VisualStudioEditor visualStudioEditor)
            {
                visualStudioEditor.SyncAll();
                return;
            }
#endif

            if (CodeEditor.CurrentEditor.GetType().Name == "DefaultExternalCodeEditor")
            {
                // SyncVS.Synchronizer.Sync(); (SyncVS is an internal class, so call it with Reflection)

                var syncVsType = Type.GetType("UnityEditor.SyncVS, UnityEditor");
                ThrowIfNull(syncVsType, "Type 'UnityEditor.SyncVS' is not found on the editor.");

                var slnSynchronizerType = Type.GetType("UnityEditor.VisualStudioIntegration.SolutionSynchronizer, UnityEditor");
                ThrowIfNull(slnSynchronizerType, "Type 'UnityEditor.VisualStudioIntegration.SolutionSynchronizer' is not found on the editor.");

                var solutionSynchronizerField = syncVsType.GetField("Synchronizer", BindingFlags.Static | BindingFlags.NonPublic);
                ThrowIfNull(solutionSynchronizerField, "Field 'Synchronizer' is not found in 'SolutionSynchronizer'.");

                var syncMethod = slnSynchronizerType.GetMethod("Sync", BindingFlags.Instance | BindingFlags.Public);
                ThrowIfNull(syncMethod, "Method 'Sync' is not found in 'Synchronizer'.");

                syncMethod.Invoke(solutionSynchronizerField.GetValue(null), Array.Empty<object>());
                
                return;
            }

            // HACK: Make it look like a dummy file has been added.
            CodeEditor.CurrentEditor.SyncIfNeeded(new[] { "CodeEditorHelper.cs" }, Array.Empty<string>(), Array.Empty<string>(), Array.Empty<string>(), Array.Empty<string>());
        }

        static void ThrowIfNull(object value, string message)
        {
            if (value == null)
            {
                throw new Exception(message);
            }
        }
    }
}