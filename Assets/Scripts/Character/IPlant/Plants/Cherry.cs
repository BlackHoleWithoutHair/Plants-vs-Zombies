using BattleScene;
using System.Collections.Generic;
using UnityEngine;
public class Cherry : IOneTimePlant
{
    private List<IZombie> zombies;
    private GameObject BoomEffect;
    public Cherry(GameObject obj) : base(obj)
    {
        m_Attr = AttributeFactory.Instance.GetPlantAttribute(PlantType.CherryBomb);
        zombies = Mediator.Instance.GetController<ZombieController>().GetZombies();
        BoomEffect = ResourcesFactory.GetEffect("CherryBoom");
    }
    protected override void OnCharacterStart()
    {
        base.OnCharacterStart();
        FireWaitTime = 0.583f;
    }
    protected override void OnFire()
    {
        base.OnFire();
        AudioUtility.Instance.PlayOneShot("cherrybomb");
        Object.Instantiate(BoomEffect, transform.position, Quaternion.identity);
        foreach (IZombie zombie in zombies)
        {
            if(Mathf.Abs(zombie.m_Attr.groundPosition.x-m_Attr.groundPosition.x)<=1&&
                Mathf.Abs(zombie.m_Attr.groundPosition.y-m_Attr.groundPosition.y)<=1)
            {
                zombie.SetIsBoom();
                zombie.UnderAttack(m_Attr.ShareAttr.Damage);
            }
        }
        m_Attr.Hp = -1;
    }
    protected override void OnCharacterDieStart()
    {
        base.OnCharacterDieStart();
        ShouldBeRemove = true;
    }
}
