using System;
using UnityEngine.SceneManagement;

namespace Application.SceneLoader
{
    public struct SceneRequest
    {
        public string Name;
        public LoadSceneMode LoadMode;
        public UnloadSceneOptions UnloadOptions;
        public Action OnCompleted;
    }
}
