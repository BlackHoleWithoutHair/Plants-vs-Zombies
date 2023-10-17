using BattleScene;
using System.Collections.Generic;
using UnityEngine;

public class WaveController:AbstractController
{
    private List<StageProperty> properties;
    private ZombieController m_ZombieController;
    private MapSystem m_MapController;
    private float RunTimeOld = 0f;
    private float RunTime = 0f;
    private float CumulativeTime = 0f;
    private float FirstZombieCreateTime = 10000f;
    private float StageMaxTime;
    private int CurrentProcessAllZombieNum = 0;
    private int CurrentProcessId = 1;
    private int MaxProcessId = 0;
    public WaveController()
    {
        m_MapController = new MapSystem();
    }
    protected override void Init()
    {
        base.Init();
        m_MapController.GameStart();
    }
    protected override void OnAfterRunInit()
    {
        base.OnAfterRunInit();
        m_ZombieController = Mediator.Instance.GetController<ZombieController>();
        properties = AttributeFactory.Instance.GetStage(ArchiveCommand.Instance.StageId).StageProperties;
        foreach (StageProperty property in properties)
        {
            if (property.ProcessId == CurrentProcessId)
            {
                CurrentProcessAllZombieNum++;
                FirstZombieCreateTime = Mathf.Min(FirstZombieCreateTime, property.CreateTime);
            }
            MaxProcessId = Mathf.Max(MaxProcessId, property.ProcessId);
        }
        StageMaxTime = GetStageMaxTime();
    }
    protected override void OnAfterRunUpdate()
    {
        base.OnAfterRunUpdate();

        if (m_ZombieController.GetCurrentProcessZombieNum() != CurrentProcessAllZombieNum)
        {
            CumulativeTime += Time.deltaTime;
        }
        RunTime = CumulativeTime + RunTimeOld;

        foreach (StageProperty property in properties)
        {
            if (CumulativeTime > property.CreateTime
                && property.GetIsCreate() == false
                && m_ZombieController.GetCurrentProcessZombieNum() < CurrentProcessAllZombieNum
                && CurrentProcessId == property.ProcessId)
            {
                m_ZombieController.AddZombie(property);
            }
        }

    }
    private void OnWin()
    {
        GameObject card = Object.Instantiate(ResourcesFactory.Get3DCard(AttributeFactory.Instance.GetStage(ArchiveCommand.Instance.StageId).AwardCard));
        card.transform.position = m_ZombieController.GetZombieDiePosition();
        card.GetComponent<PlantCard>().m_Panel = Mediator.Instance.GetController<UIController>().GetRootPanel() as PanelRoot;
        AudioUtility.Instance.PlayOneShot("winmusic");
        AudioUtility.Instance.m_MusicSource.Pause();
        ArchiveCommand.Instance.StageId += 1;
        ArchiveCommand.Instance.SaveData();
    }
    public void EnterNextWave()
    {
        CurrentProcessId += 1;
        if (CurrentProcessId == MaxProcessId)
        {
            EventCenter.Instance.NotisfyObserver(EventType.OnLastWave);
        }
        else if (CurrentProcessId < MaxProcessId)
        {
            EventCenter.Instance.NotisfyObserver(EventType.OnNextWave);
        }
        else
        {
            OnWin();
            EventCenter.Instance.NotisfyObserver(EventType.OnWin);
        }
        CurrentProcessAllZombieNum = 0;
        RunTimeOld += CumulativeTime;
        CumulativeTime = 0;

        foreach (StageProperty property in properties)
        {
            if (property.ProcessId == CurrentProcessId)
            {
                CurrentProcessAllZombieNum++;
            }
        }
    }
    public int GetCurrentProcessAllZombieNum()
    {
        return CurrentProcessAllZombieNum;
    }
    public float GetProgressValue()
    {
        return Mathf.Clamp(RunTime - FirstZombieCreateTime, 0f, 10000f) / (StageMaxTime - FirstZombieCreateTime);
    }
    public int GetMaxProcessId()
    {
        return MaxProcessId;
    }
    public List<Vector2> GetFlagPosition()
    {
        List<Vector2> result = new List<Vector2>();
        int process = 1;
        float maxTime = 0;
        float CumulateTime = 0f;
        while (process < MaxProcessId)
        {
            maxTime = 0;
            foreach (StageProperty property in properties)
            {
                if (process == property.ProcessId)
                {
                    maxTime = Mathf.Max(maxTime, property.CreateTime);
                }
            }
            process++;
            CumulateTime += maxTime;
            result.Add(new Vector2(
                -10f - (CumulateTime - FirstZombieCreateTime) / (StageMaxTime - FirstZombieCreateTime) * 130f,
                0f));
        }
        return result;
    }
    private float GetStageMaxTime()
    {
        int process = 1;
        float maxTime = 0;
        float result = 0;
        if (MaxProcessId == 1)
        {
            foreach (StageProperty property in properties)
            {
                if (property.ProcessId == 1)
                {
                    maxTime = Mathf.Max(maxTime, property.CreateTime);
                }
            }
            return maxTime;
        }
        while (process <= MaxProcessId)
        {
            maxTime = 0;
            foreach (StageProperty property in properties)
            {
                if (process == property.ProcessId)
                {
                    maxTime = Mathf.Max(maxTime, property.CreateTime);
                }
            }
            process++;
            result += maxTime;
        }

        return result;
    }
}
