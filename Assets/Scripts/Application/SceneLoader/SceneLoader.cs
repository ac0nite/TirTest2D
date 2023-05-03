using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(SceneRequest sceneRequest)
        {
            Debug.Log($"SCENE '{sceneRequest.Name}' LOAD");
            await SceneManager.LoadSceneAsync(sceneRequest.Name, sceneRequest.LoadMode).ToUniTask();
            sceneRequest.OnCompleted?.Invoke();
        }

        public async UniTask UnLoadSceneAsync(SceneRequest sceneRequest)
        {
            Debug.Log($"SCENE '{sceneRequest.Name}' UNLOAD");
            await SceneManager.UnloadSceneAsync(sceneRequest.Name, sceneRequest.UnloadOptions);
            sceneRequest.OnCompleted?.Invoke();
        }
    }
}
