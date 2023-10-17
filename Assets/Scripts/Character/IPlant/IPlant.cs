using BattleScene;
using UnityEngine;
public abstract class IPlant : ICharacter
{
    public new PlantAttribute m_Attr { get => base.m_Attr as PlantAttribute; set => base.m_Attr = value; }
    protected GameObject m_FirePoint;
    protected IPlant(GameObject obj) : base(obj)
    {
        m_FirePoint = UnityTool.Instance.GetGameObjectInChild(gameObject, "FirePoint");
        if (m_FirePoint == null)
        {
            Debug.Log("IPlant FirePoint is null");
        }
    }
    protected override void OnCharacterDieStart()
    {
        base.OnCharacterDieStart();
        Mediator.Instance.GetSystem<GroundSystem>().RemovePlant(transform.position);
    }
    protected override void OnCharacterUpdate()
    {
        base.OnCharacterUpdate();
        if(FireCondition())
        {
            OnFire();
        }
    }
    protected virtual void OnFire() { }
    protected virtual bool FireCondition()
    {
        return true;
    }
}
