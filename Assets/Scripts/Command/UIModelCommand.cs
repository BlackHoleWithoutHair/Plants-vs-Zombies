
public class UIModelCommand : Singleton<UIModelCommand>
{
    private UIModel model;
    public int SunNum { get => model.SunNum; set => model.SunNum = value; }
    private UIModelCommand()
    {
        model=ModelContainer.Instance.GetModel<UIModel>();
        EventCenter.Instance.RegisterObserver(EventType.OnSceneChangeComplete, () =>
        {
            SunNum = 150;
        });
    }
    public void AddSun(int num)
    {
        SunNum += num;
    }
    public bool SpendSun(int num)
    {
        if(SunNum-num>=0)
        {
            SunNum=SunNum-num;
            return true;
        }
        return false;
    }

}
