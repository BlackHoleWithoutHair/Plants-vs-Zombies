using UnityEngine;
namespace BattleScene
{
    public class GameLoop : IGameLoop
    {
        // Start is called before the first frame update
        IFacade mediator;
        protected override void Start()
        {
            base.Start();
            mediator = new Facade();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            mediator.GameUpdate();
        }
    }
}


