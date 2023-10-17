using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveCommand : Singleton<ArchiveCommand>
{
    private ArchiveModel model;
    public int StageId { get => model.m_GameData.StageId; set => model.m_GameData.StageId = value; }
    public string UserName { get => model.m_GameData.UserName; set => model.m_GameData.UserName = value; }
    public List<string> NameList { get => model.m_GameData.NameList; }
    public float MusicVolume { get => model.m_GameData.MusicVolume; set => model.m_GameData.MusicVolume = value; }
    public float SoundVolume { get => model.m_GameData.SoundVolume; set => model.m_GameData.SoundVolume = value; }
    private ArchiveCommand()
    {
        model = ModelContainer.Instance.GetModel<ArchiveModel>();
        CoroutinePool.Instance.StartCoroutine(AutoSaveLoop());
        EventCenter.Instance.RegisterObserver(EventType.OnSceneChangeComplete, () =>
        {
            CoroutinePool.Instance.StartCoroutine(AutoSaveLoop());
        });
    }
    public void SaveData()
    {
        ArchiveUtility.Instance.SaveData(model);
    }
    public int GetBigStage()
    {
        return (StageId % 10) == 0 ? (StageId / 10) : (StageId / 10 + 1);
    }
    public int GetSmallStage()
    {
        return (StageId % 10) == 0 ? 10 : (StageId % 10);
    }
    private IEnumerator AutoSaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            ArchiveUtility.Instance.SaveData(model);
        }
    }
}
