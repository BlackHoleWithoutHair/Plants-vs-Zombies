using UnityEngine;
namespace BattleScene
{
    public class FlagZombieAttack : EnemyState
    {
        private float CumulativeTime;
        public FlagZombieAttack(IStateController controller) : base(controller)
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
                    CumulativeTime = 0;
                    m_Controller.BeFoundPlant.UnderAttack(m_Attr.ShareAttr.Damage);
                }
                if (isFindPlant(m_Controller.BeFoundPlant) == false)
                {
                    m_Controller.SetOtherState(typeof(FlagZombieWalk));
                }
            }
            if (m_Attr.Hp <= 0)
            {
                m_Controller.SetOtherState(typeof(FlagZombieDie));
            }
        }
        public override void StateEnd()
        {
            m_Controller.BeFoundPlant = null;
            m_Animator.SetBool("isAttack", false);
        }
    }
}

