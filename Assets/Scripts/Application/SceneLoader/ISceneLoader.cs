using Cysharp.Threading.Tasks;

namespace Common.SceneLoader
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(SceneRequest sceneRequest);
        UniTask UnLoadSceneAsync(SceneRequest sceneRequest);
    }
}
