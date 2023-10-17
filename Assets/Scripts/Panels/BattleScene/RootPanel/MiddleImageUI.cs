using BattleScene;
using UnityEngine;

public class MiddleImageUI
{
    private int isPlay;
    private bool isZombieWonDisappear;
    private GameObject m_GameObject;
    private GameObject m_ReadyImage;
    private GameObject m_LastAttackImage;
    private GameObject m_NextAttackImage;
    private Animator m_CameraAnimator;
    public MiddleImageUI() { }
    public void GameStart()
    {
        m_GameObject = GameObject.Find("PanelRoot");
        m_ReadyImage = m_GameObject.transform.Find("ReadyImage").gameObject;
        m_LastAttackImage = m_GameObject.transform.Find("LastAttack").gameObject;
        m_NextAttackImage = m_GameObject.transform.Find("NextAttack").gameObject;
        m_CameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        EventCenter.Instance.RegisterObserver(EventType.OnLastWave, () =>
        {
            m_NextAttackImage.SetActive(true);
            m_NextAttackImage.GetComponent<Animator>().Play("LastAttack", 0, 0.0f);
            AudioUtility.Instance.PlayOneShot("hugewave");
            CoroutinePool.Instance.StartAnimatorCallback(m_NextAttackImage.GetComponent<Animator>(), "LastAttack", () =>
            {
                m_LastAttackImage.SetActive(true);
                AudioUtility.Instance.PlayOneShot("finalwave");
            });
        });
        EventCenter.Instance.RegisterObserver(EventType.OnNextWave, () =>
        {
            m_NextAttackImage.SetActive(true);
            m_NextAttackImage.GetComponent<Animator>().Play("LastAttack", 0, 0.0f);
            AudioUtility.Instance.PlayOneShot("hugewave");
        });
        EventCenter.Instance.RegisterObserver(EventType.OnCameraMoveLeftFinish, () =>
        {
            m_ReadyImage.GetComponent<Animator>().enabled = true;
            AudioUtility.Instance.PlayOneShot("readysetplant");
            CoroutinePool.Instance.StartAnimatorCallback(m_ReadyImage.GetComponent<Animator>(), "StartGameText", () =>
            {
                EventCenter.Instance.NotisfyObserver(EventType.OnGameStart);
                AudioUtility.Instance.PlayMusic("bgm1");
            });
        });

    }
    public void GameUpdate()
    {
        if(BattleScene.Mediator.Instance.isFail)
        {
            LostUpdate();
        }
    }
    private void LostUpdate()
    {
        if (BattleScene.Mediator.Instance.isFail)
        {
            if (isPlay == 0)
            {
                isPlay = 1;
                m_CameraAnimator.SetBool("isGameLost", true);
                AudioUtility.Instance.PlayOneShot("losemusic");
            }
            AnimatorStateInfo info = m_CameraAnimator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime > 1f && info.IsName("CameraGameLost"))
            {
                m_GameObject.transform.Find("ZombieWon").gameObject.SetActive(true);
                info = m_GameObject.transform.Find("ZombieWon").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime > 1f && info.IsName("ZombieWon"))
                {
                    isZombieWonDisappear = true;
                }
            }
        }
    }
    public bool GetIsZombieWonDisappear()
    {
        return isZombieWonDisappear;
    }
}
