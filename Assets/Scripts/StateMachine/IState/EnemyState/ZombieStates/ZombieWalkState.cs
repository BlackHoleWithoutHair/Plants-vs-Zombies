using UnityEngine;
namespace BattleScene
{
    public class ZombieWalkState : EnemyState
    {

        private Vector3 m_MoveDir;
        public ZombieWalkState(IStateController controller) : base(controller)
        {
            m_MoveDir = Vector3.left;
        }
        public override void StateStart()
        {
            m_Animator.SetBool("isAttack", false);
        }
        public override void StateUpdate()
        {
            //gameObject.transform.position += m_MoveDir * m_Attr.ShareAttr.MoveSpeed * Time.deltaTime;
            if(isFindPlant())
            {
                m_Controller.SetOtherState(typeof(ZombieAttackState));
            }
            if (m_Controller.GetEnemy().m_Attr.Hp <= 0)
            {
                m_Controller.SetOtherState(typeof(ZombieDieState));
            }
        }

    }

}
