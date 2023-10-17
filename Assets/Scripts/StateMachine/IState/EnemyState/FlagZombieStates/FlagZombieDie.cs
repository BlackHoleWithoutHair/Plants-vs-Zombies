using System.Collections;
using UnityEngine;

public class FlagZombieDie : EnemyState
{
    public FlagZombieDie(IStateController controller) : base(controller) { }
    public override void StateStart()
    {
        if (m_Controller.GetEnemy().GetIsBoom())
        {
            m_Animator.SetBool("isBoom", true);
            CoroutinePool.Instance.StartAnimatorCallback(m_Animator, "isBoom", () =>
            {
                CoroutinePool.Instance.StartCoroutine(WaitForRemoveZombie());
            });
        }
        else
        {
            m_Animator.SetBool("isDie", true);
            CoroutinePool.Instance.StartAnimatorCallback(m_Animator, "isDie", () =>
            {
                CoroutinePool.Instance.StartCoroutine(WaitForRemoveZombie());
            });
        }
    }
    private IEnumerator WaitForRemoveZombie()
    {
        yield return new WaitForSeconds(1f);
        enemy.RemoveCharacter();
    }
}
