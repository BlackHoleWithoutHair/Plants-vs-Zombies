using UnityEngine.SceneManagement;
public enum SceneName
{
    MainMenuScene,
    BattleScene,
}
public class SceneModel : AbstractModel
{
    public int SceneIndex;
    public SceneName sceneName;
    protected override void OnInit()
    {
        SceneIndex = GetActiveSceneIndex();
        SetSceneType(SceneIndex);
    }
    private int GetActiveSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    private void SetSceneType(int index)
    {
        switch (index)
        {
            case 0:
                sceneName = SceneName.MainMenuScene;
                break;
            case 1:
                sceneName = SceneName.BattleScene;
                break;
        }
    }
}