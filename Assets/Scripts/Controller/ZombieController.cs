using BattleScene;
using System.Collections.Generic;
using UnityEngine;
public class ZombieController : AbstractController
{
    private List<IZombie> zombies = new List<IZombie>();
    private List<bool> zombiesIsDie = new List<bool>();
    private IdleZombieManager m_IdleZombieManager;
    private int CurrentProcessZombieNum = 0;
    private int KillNum = 0;
    private bool isFirstAppear = false;
    private bool isFail;
    private Vector3 ZombieDiePosition;
    public ZombieController()
    {
        m_IdleZombieManager = new IdleZombieManager(this);
    }
    protected override void Init()
    {
        base.Init();
        m_IdleZombieManager.BeforeGameStart();
    }
    protected override void OnAfterRunInit()
    {
        base.OnAfterRunInit();
        m_IdleZombieManager.GameStart();
    }
    protected override void OnAfterRunUpdate()
    {
        base.OnAfterRunUpdate();
        for (int i = 0; i < zombies.Count; i++)
        {
            zombies[i].GameUpdate();
            if (zombies[i].isDie && zombiesIsDie[i] == false)
            {
                ZombieDiePosition = zombies[i].transform.position;
                zombiesIsDie[i] = true;
                KillNum++;
                if (KillNum == Mediator.Instance.GetController<WaveController>().GetCurrentProcessAllZombieNum())
                {
                    KillNum = 0;
                    Mediator.Instance.GetController<WaveController>().EnterNextWave();
                    CurrentProcessZombieNum = 0;
                }
            }
            if (zombies[i].ShouldBeRemove)
            {
                Object.Destroy(zombies[i].gameObject);
                zombies.RemoveAt(i);
                zombiesIsDie.Remove(zombiesIsDie[i]);
            }
        }
        foreach (Zombie zombie in zombies)
        {
            if (zombie.transform.position.x < -8.1f && !isFail)
            {
                isFail = true;
                EventCenter.Instance.NotisfyObserver(EventType.OnFail);
            }
        }
    }
    public List<IZombie> GetZombies()
    {
        return zombies;
    }
    public void AddZombie(StageProperty property)
    {
        if(!isFirstAppear)
        {
            isFirstAppear = true;
            AudioUtility.Instance.PlayOneShot("awooga");
            EventCenter.Instance.NotisfyObserver(EventType.OnFirstZombieAppear);
        }
        zombies.Add(CharacterFactory.Instance.GetZombie(property.Type));
        zombiesIsDie.Add(false);
        property.SetIsCreate();
        CurrentProcessZombieNum++;
    }
    public Vector3 GetZombieDiePosition()
    {
        return ZombieDiePosition;
    }
    public int GetCurrentProcessZombieNum()
    {
        return CurrentProcessZombieNum;
    }
    public int GetKillNum()
    {
        return KillNum;
    }
}
