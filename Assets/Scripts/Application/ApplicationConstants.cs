using Application.SceneLoader;
using UnityEngine.SceneManagement;

namespace Application
{
    public static class ApplicationConstants
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
    }
}