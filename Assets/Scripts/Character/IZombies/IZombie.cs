using UnityEngine;

public class IZombie : ICharacter
{
    public new ZombieAttribute m_Attr { get => base.m_Attr as ZombieAttribute; set => base.m_Attr = value; }
    protected bool isBoom;
    protected bool isLostHead;
    protected GameObject HeadPoint;
    private Animator m_Animator;
    protected EnemyStateController m_StateController;
    protected IZombie(GameObject obj) : base(obj)
    {
        HeadPoint = UnityTool.Instance.GetGameObjectInChild(obj, "HeadPoint");
        m_Animator = obj.GetComponent<Animator>();
    }
    protected override void OnCharacterStart()
    {
        base.OnCharacterStart();
        m_StateController = new EnemyStateController(this);
    }
    protected override void OnAlwaysUpdate()
    {
        base.OnAlwaysUpdate();
        m_Attr.groundPosition.x = BattleScene.Mediator.Instance.GetSystem<GroundSystem>().GetColumeByWorldOffsetX(transform.position.x);
        m_StateController?.StateUpdate();
        if (m_Attr.Hp < m_Attr.ShareAttr.CriticalHp && isBoom == false)
        {
            m_Animator.SetBool("isLostHead", true);
            if (isLostHead == false)
            {
                isLostHead = true;
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Zombies/ZombieHead"), HeadPoint.transform.position, Quaternion.identity);
            }
        }
    }
    public void SetIsBoom()
    {
        if (isLostHead == false)
        {
            isBoom = true;
        }
    }
    public bool GetIsBoom()
    {
        return isBoom;
    }
    public ZombieAttribute GetAttr()
    {
        return m_Attr;
    }
}
