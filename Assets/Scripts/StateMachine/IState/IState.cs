public class IState
{
    protected IStateController m_Controller;
    public IState(IStateController controller)
    {
        m_Controller = controller;
    }
    public virtual void StateStart() { }
    public virtual void StateUpdate() { }
    public virtual void StateEnd() { }
}
