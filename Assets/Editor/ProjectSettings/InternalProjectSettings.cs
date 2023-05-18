using Application.Installer;
using Gameplay.Installer;
using UnityEditor;
using UnityEngine.Windows;
using Zenject;

namespace Editor.ProjectSettings
{
    public static class InternalProjectSettings
    {
        private const string ApplicationSettingsPath = "Assets/Scripts/Application/Installer";
        private const string GameplaySettingsPath = "Assets/Scripts/Gameplay/Installer";
        
        [MenuItem("ProjectSettings/ApplicationSettings")]
        public static void ShowApplicationSettings()
        {
            ShowAssetByType<ApplicationSettingsInstaller>();
        }
        
        [MenuItem("ProjectSettings/GameplaySettings")]
        public static void ShowGameplaySettings()
        {
            ShowAssetByType<GameplaySettingsInstaller>();
        }
        
        private static void ShowAssetByType<T>() where T : ScriptableObjectInstaller
        {
            string filterString = "t:" + typeof(T).FullName;
            string[] guids = AssetDatabase.FindAssets(filterString, new[] { ApplicationSettingsPath, GameplaySettingsPath });
            if (guids.Length != 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                SelectAssetByPath<T>(path);
            }
        }

        private static void SelectAssetByPath<T>(string path) where T : ScriptableObjectInstaller
        {
            T asset;
            if (File.Exists(path))
            {
                asset = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            else
                return;
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
        }
    }
}