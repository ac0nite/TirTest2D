using Application.SceneLoader;
using UnityEngine.SceneManagement;

namespace Resources
{
    public static class Constants
    {
        public static SceneRequest LoadingSceneRequest = new SceneRequest()
        {
            Name = "Loading",
            LoadMode = LoadSceneMode.Additive,
            UnloadOptions = UnloadSceneOptions.UnloadAllEmbeddedSceneObjects,
            OnCompleted = null
        };
        
        public static SceneRequest GameplaySceneRequest = new SceneRequest()
        {
            Name = "Gameplay",
            LoadMode = LoadSceneMode.Additive,
            UnloadOptions = UnloadSceneOptions.None,
            OnCompleted = null
        };

        public class ID
        {
            public const string ApplicationCanvas = "ApplicationCanvas";
            public const string GameplayCanvas = "GameplayCanvas";
        }

        public class Resources
        {
            public const string SplashScreen = "Prefabs/Applications/SplashScreen";
            public const string LoadingScreen = "Prefabs/Applications/LoadingScreen";
            public const string PreviewScreen = "Prefabs/Gameplay/UI/PreviewScreen";
            public const string GameplayScreen = "Prefabs/Gameplay/UI/GameplayScreen";
            public const string ResultScreen = "Prefabs/Gameplay/UI/ResultScreen";
        }
    }
}