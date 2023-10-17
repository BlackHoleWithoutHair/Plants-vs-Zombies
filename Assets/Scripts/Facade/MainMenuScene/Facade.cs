namespace MainMenuScene
{
    public class Facade : IFacade
    {
        private UIController m_UISystem;
        private AdaptController m_AdaptSystem;
        public Facade()
        {
            m_UISystem = new UIController();
            m_AdaptSystem = new AdaptController();

            Mediator.Instance.RegisterController(m_UISystem);
            Mediator.Instance.RegisterController(m_AdaptSystem);


        }
        public override void GameUpdate()
        {
            m_UISystem.GameUpdate();
            m_AdaptSystem.GameUpdate();
        }
    }
}

