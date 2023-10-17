using UnityEngine;

public class WallNut : IPlant
{
    private Animator m_Animator;
    public WallNut(GameObject obj) : base(obj)
    {
        m_Attr = AttributeFactory.Instance.GetPlantAttribute(PlantType.WallNut);
        m_Animator = obj.GetComponent<Animator>();
    }
    protected override void OnCharacterUpdate()
    {
        base.OnCharacterUpdate();
        if (m_Attr.Hp < 2667f && m_Attr.Hp >= 1334)
        {
            m_Animator.SetInteger("State", 1);
        }
        else if (m_Attr.Hp < 1334)
        {
            m_Animator.SetInteger("State", 2);
        }
    }
}
