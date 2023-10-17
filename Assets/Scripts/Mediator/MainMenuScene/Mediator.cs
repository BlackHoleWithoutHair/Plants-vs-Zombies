namespace MainMenuScene
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

    }
}

