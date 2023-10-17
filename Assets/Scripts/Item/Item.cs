using UnityEngine;
public abstract class Item
{
    protected Vector2 position;
    protected Quaternion m_Rot;
    public GameObject gameObject { get; protected set; }
    public Transform transform { get => gameObject.transform; }
    public bool ShouldBeRemove { get; protected set; }
    private bool isInit;
    private bool isEnter;
    protected bool isDestroyOnRemove;
    
    public Item(GameObject obj, Vector2 position)
    {
        gameObject = obj;
        this.position = position;
        isDestroyOnRemove = true;
    }
    protected virtual void Init() { }
    protected virtual void OnEnter()
    {
        if (!isInit)
        {
            isInit = true;
            Init();
        }
        ShouldBeRemove = false;
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }
    public virtual void GameUpdate() 
    {
        if (!ShouldBeRemove)
        {
            OnUpdate();
        }
    }
    protected virtual void OnUpdate()
    {
        if (!isEnter)
        {
            isEnter = true;
            OnEnter();
        }
    }
    public virtual void OnExit() 
    {
        isEnter = false;
        if (isDestroyOnRemove)
        {
            TriggerCenter.Instance.RemoveObserver(TriggerType.OnTriggerEnter, gameObject);
            TriggerCenter.Instance.RemoveObserver(TriggerType.OnTriggerExit, gameObject);
            Object.Destroy(gameObject);
        }
    }
    public virtual void SetPosition(Vector2 position)
    {
        this.position = position;
    }
    public virtual void SetRotation(Quaternion rot)
    {
        m_Rot = rot;
        gameObject.transform.rotation = rot;
    }
    public virtual void Remove()
    {
        ShouldBeRemove = true;
    }
    public void DontDestroyOnRemove()
    {
        isDestroyOnRemove = false;
    }
    public void AddToController()
    {
        if(SceneModelCommand.Instance.GetActiveSceneName()==SceneName.BattleScene)
        {
            BattleScene.Mediator.Instance.GetController<ItemController>().AddItem(this);
        }
    }
}
