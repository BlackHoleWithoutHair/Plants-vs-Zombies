using BattleScene;
using UnityEngine;

public class FlagZombie : IZombie
{
    private float CumulativeTime;
    public FlagZombie(GameObject obj) : base(obj)
    {
        m_Attr = AttributeFactory.Instance.GetZombieAttribute(ZombieType.FlagZombie);
    }
    protected override void OnCharacterUpdate()
    {
        base.OnCharacterUpdate();
        m_StateController.SetOtherState(typeof(FlagZombieWalk));
    }
}
