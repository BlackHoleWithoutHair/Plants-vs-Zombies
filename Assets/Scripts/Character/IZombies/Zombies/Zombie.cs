using UnityEngine;
using BattleScene;
public class Zombie : IZombie
{
    public Zombie(GameObject obj) : base(obj)
    {
        m_Attr = AttributeFactory.Instance.GetZombieAttribute(ZombieType.Zombie);
    }
    protected override void OnCharacterStart()
    {
        base.OnCharacterStart();
        m_StateController.SetOtherState(typeof(ZombieWalkState));
    }
    public override void UnderAttack(float damage)
    {
        base.UnderAttack(damage);
        Debug.Log(m_Attr.Hp);
    }
}
