
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class PeaShooter : IBulletPlant
    {
        private Animator m_Animator;
        private List<IZombie> zombies;
        public PeaShooter(GameObject obj) : base(obj)
        {
            m_Attr = AttributeFactory.Instance.GetPlantAttribute(PlantType.PeaShooter);
            m_Animator = UnityTool.Instance.GetComponentFromChild<Animator>(gameObject, "Head");
            zombies = Mediator.Instance.GetController<ZombieController>().GetZombies();
        }
        protected override void OnCharacterStart()
        {
            base.OnCharacterStart();
            FireInterval = m_Attr.ShareAttr.AttackInterval;
        }
        protected override bool FireCondition()
        {
            return base.FireCondition()&&isFindZombieInLine();
        }
        protected override void OnFire()
        {
            base.OnFire();
            m_Animator.SetTrigger("isAttack");
            CoroutinePool.Instance.StartCoroutine(WaitForShoot());
        }
        private bool isFindZombieInLine()
        {
            bool find = false;
            foreach (IZombie zombie in zombies)
            {
                if (zombie.m_Attr.groundPosition.y ==m_Attr.groundPosition.y)
                {
                    find=true; break;
                }
            }
            return find;
        }
        private IEnumerator WaitForShoot()
        {
            yield return new WaitForSeconds(0.5f);
            AudioUtility.Instance.PlayOneShot("firepea");
            IBullet bullet = ItemFactory.Instance.GetBullet(BulletType.Pea, m_FirePoint.transform.position) as IBullet;
            bullet.SetDamage(m_Attr.ShareAttr.Damage);
            bullet.AddToController();
        }
    }
}
