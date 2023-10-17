using MainMenuScene;
using UnityEngine;
public class StartGameLoop : IGameLoop
{
    // Start is called before the first frame update
    private Facade facade;
    protected override void Start()
    {
        base.Start();
        facade = new Facade();
    }
    protected override void Update()
    {
        base.Update();
        facade.GameUpdate();
    }
}
