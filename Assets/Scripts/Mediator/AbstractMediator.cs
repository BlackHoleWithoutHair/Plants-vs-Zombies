using System.Collections.Generic;

public class AbstractMediator
{
    private List<AbstractController> controllers = new List<AbstractController>();
    private List<AbstractSystem> systems = new List<AbstractSystem>();
    protected AbstractMediator() 
    {
        EventCenter.Instance.RegisterObserver(EventType.OnSceneChangeComplete, () =>
        {
            controllers.Clear();
            systems.Clear();
        });
    }
    public void RegisterController<T>(T controller) where T : AbstractController
    {
        controllers.Add(controller);
    }
    public void RegisterSystem<T>(T sys) where T : AbstractSystem
    {
        systems.Add(sys);
    }
    public T GetController<T>() where T : AbstractController
    {
        foreach (AbstractController controller in controllers)
        {
            if (controller is T)
            {
                return controller as T;
            }
        }
        return default(T);
    }
    public T GetSystem<T>() where T : AbstractSystem
    {
        foreach (AbstractSystem sys in systems)
        {
            if (sys is T)
            {
                return sys as T;
            }
        }
        return default(T);
    }
}
