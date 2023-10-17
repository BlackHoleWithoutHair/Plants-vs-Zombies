public class IFacade
{
    protected bool isGameStart;
    public bool IsGameStart => isGameStart;
    private bool isInit;
    public IFacade() { }
    protected virtual void Init()
    {

    }
    public virtual void GameUpdate()
    {
        if (!isInit)
        {
            isInit = true;
            Init();
        }

    }
}
