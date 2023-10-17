public class UIController : AbstractController
{
    private IPanel m_RootPanel;
    public UIController() { }
    protected override void Init()
    {
        base.Init();
        switch (SceneModelCommand.Instance.GetActiveSceneName())
        {
            case SceneName.MainMenuScene:
                m_RootPanel = new MainMenuScene.PanelRoot();
                break;
            case SceneName.BattleScene:
                m_RootPanel = new BattleScene.PanelRoot();
                break;
        }

    }
    protected override void AlwaysUpdate()
    {
        base.AlwaysUpdate();
        m_RootPanel.GameUpdate();
    }
    public IPanel GetRootPanel()
    {
        return m_RootPanel;
    }
}
