using Cysharp.Threading.Tasks;

namespace Application.SceneLoader
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(SceneRequest sceneRequest);
        UniTask UnLoadSceneAsync(SceneRequest sceneRequest);
    }
}
