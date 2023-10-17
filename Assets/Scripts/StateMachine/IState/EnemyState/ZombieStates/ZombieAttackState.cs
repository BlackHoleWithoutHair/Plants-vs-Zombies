using UnityEngine;
namespace BattleScene
{
    public class ZombieAttackState : EnemyState
    {
        private float CumulativeTime;
        public ZombieAttackState(IStateController controller) : base(controller)
        {
            CumulativeTime = m_Attr.ShareAttr.AttackInterval;
        }
        public override void StateStart()
        {
            m_Animator.SetBool("isAttack", true);
        }
        public override void StateUpdate()
        {
            if (m_Attr.Hp >= m_Attr.ShareAttr.CriticalHp)
            {
                CumulativeTime += Time.deltaTime;
                if (CumulativeTime > m_Attr.ShareAttr.AttackInterval)
                {
                    AudioUtility.Instance.PlayOneShot("chompsoft");
                    CumulativeTime = 0;
                    Debug.Log(m_Controller.BeFoundPlant.m_Attr.Hp);
                    Debug.Log(m_Attr.groundPosition);
                    m_Controller.BeFoundPlant.UnderAttack(m_Attr.ShareAttr.Damage);
                }
                if (isFindPlant(m_Controller.BeFoundPlant) == false)
                {
                    m_Controller.SetOtherState(typeof(ZombieWalkState));
                }
            }
            if (m_Attr.Hp <= 0)
            {
                m_Controller.SetOtherState(typeof(ZombieDieState));
            }
        }
        public override void StateEnd()
        {
            m_Controller.BeFoundPlant = null;
            m_Animator.SetBool("isAttack", false);
        }
    }
}

