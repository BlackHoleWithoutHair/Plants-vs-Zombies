namespace BattleScene
{
    public class Facade : IFacade
    {
        private ItemController m_ItemController;
        private PlantController m_PlantController;
        private ZombieController m_ZombieController;
        private UIController m_UIController;
        private SunController m_SunController;
        private AdaptController m_AdaptController;
        private WaveController m_WaveController;
        private CameraController m_CameraController;
        public Facade()
        {
            m_ItemController = new ItemController();
            m_CameraController = new CameraController();
            m_ZombieController = new ZombieController();
            m_WaveController = new WaveController();
            m_PlantController = new PlantController();
            m_UIController = new UIController();
            m_SunController = new SunController();
            m_AdaptController = new AdaptController();

            Mediator.Instance.RegisterController(m_ItemController);
            Mediator.Instance.RegisterController(m_CameraController);
            Mediator.Instance.RegisterController(m_ZombieController);
            Mediator.Instance.RegisterController(m_PlantController);
            Mediator.Instance.RegisterController(m_UIController);
            Mediator.Instance.RegisterController(m_SunController);
            Mediator.Instance.RegisterController(m_AdaptController);
            Mediator.Instance.RegisterController(m_WaveController);

            Mediator.Instance.RegisterSystem(new GroundSystem());

            EventCenter.Instance.RegisterObserver(EventType.OnGameStart, () =>
            {
                m_ZombieController.TurnOnController();
                m_WaveController.TurnOnController();
                m_PlantController.TurnOnController();
                m_SunController.TurnOnController();

            });
        }
        protected override void Init()
        {
            base.Init();
        }
        public override void GameUpdate()
        {
            m_ItemController.GameUpdate();
            m_CameraController.GameUpdate();
            m_PlantController.GameUpdate();
            m_SunController.GameUpdate();
            m_ZombieController.GameUpdate();
            m_WaveController.GameUpdate();
            m_UIController.GameUpdate();
            m_AdaptController.GameUpdate();

        }
    }
}

