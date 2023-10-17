namespace BattleScene
{
    public class Mediator : AbstractMediator
    {
        private static Mediator instance;
        public static Mediator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Mediator();
                }
                return instance;
            }
        }
        public bool isGameStart { get; private set; }
        public bool isFail { get; private set; }
        public bool isWin { get; private set; }
        public Mediator()
        {
            EventCenter.Instance.RegisterObserver(EventType.OnGameStart, () => { isGameStart = true; });
            EventCenter.Instance.RegisterObserver(EventType.OnFail, () => { isFail = true; });
            EventCenter.Instance.RegisterObserver(EventType.OnWin, () => { isWin = true; });
        }
    }
}

