using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Application.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(SceneRequest sceneRequest)
        {
            await SceneManager.LoadSceneAsync(sceneRequest.Name, sceneRequest.LoadMode).ToUniTask();
            sceneRequest.OnCompleted?.Invoke();
        }

        public async UniTask UnLoadSceneAsync(SceneRequest sceneRequest)
        {
            await SceneManager.UnloadSceneAsync(sceneRequest.Name, sceneRequest.UnloadOptions);
            sceneRequest.OnCompleted?.Invoke();
        }
    }
}
