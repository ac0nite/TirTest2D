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
        }

        public class Resources
        {
            public const string SplashScreen = "Prefabs/Applications/SplashScreen";
            public const string LoadingScreen = "Prefabs/Applications/LoadingScreen";
            public const string Cannonball = "Prefabs/Gameplay/Cannonball";
            public const string Bomb = "Prefabs/Gameplay/Bomb";
        }
    }
}