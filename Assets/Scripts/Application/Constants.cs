using Application.SceneLoader;
using UnityEngine.SceneManagement;

namespace ApplicationConstant
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
            public const string ApplicationScreen = "Prefabs/Applications/ApplicationScreen";   
        }
    }
}