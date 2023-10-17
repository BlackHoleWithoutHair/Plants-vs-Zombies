using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class PotatoMine : IOneTimePlant
    {
        private Animator m_Animator;
        private List<IZombie> zombies;
        private bool isAlreadyGrow;
        private bool isWantBoom;
        private bool CanAttack;
        private bool isGrow;
        public PotatoMine(GameObject obj) : base(obj)
        {
            m_Attr = AttributeFactory.Instance.GetPlantAttribute(PlantType.PotatoMine);
            m_Animator = transform.GetComponent<Animator>();
            zombies = Mediator.Instance.GetController<ZombieController>().GetZombies();
            TriggerCenter.Instance.RegisterObserver(TriggerType.OnTriggerEnter, gameObject, "Zombie", (obj) =>
            {
                CanAttack = true;
                IZombie zombie = obj.GetComponent<Symbol>().GetCharacter() as IZombie;
                zombie.UnderAttack(m_Attr.ShareAttr.Damage);
                zombie.SetIsBoom();
            });
        }
        protected override void OnCharacterStart()
        {
            base.OnCharacterStart();
            FireWaitTime = 15f;
        }
        protected override bool FireCondition()
        {
            return base.FireCondition()&&CanAttack;
        }
        protected override void OnFire()
        {
            base.OnFire();
            AudioUtility.Instance.PlayOneShot("potato_mine");
            ItemFactory.Instance.GetEffect(EffectType.PotatoMineBoom, gameObject.transform.position).AddToController();
            m_Attr.Hp = -1;
        }
        protected override void OnCharacterUpdate()
        {
            base.OnCharacterUpdate();
            if (isAlreadyGrow)
            {
                FindZombie();
            }
            if (Timer>FireWaitTime&&!isGrow)
            {
                isGrow = true;
                m_Animator.SetBool("isGrow", true);
                CoroutinePool.Instance.StartAnimatorCallback(m_Animator, "Rise", () =>
                {
                    isAlreadyGrow = true;
                });
            }
            if (isWantBoom)
            {
                m_Animator.SetBool("isWantBoom", true);
            }
            else
            {
                m_Animator.SetBool("isWantBoom", false);
            }
        }
        protected override void OnCharacterDieStart()
        {
            base.OnCharacterDieStart();
            ShouldBeRemove = true;
        }
        protected void FindZombie()
        {
            foreach (IZombie zombie in zombies)
            {
                if(!isWantBoom&&zombie.m_Attr.groundPosition.y==m_Attr.groundPosition.y&&
                    zombie.m_Attr.groundPosition.x-m_Attr.groundPosition.x<=2)
                {
                    isWantBoom = true;
                }
            }
        }
    }

}
