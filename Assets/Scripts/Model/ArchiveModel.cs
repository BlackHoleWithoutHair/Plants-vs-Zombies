using System.Collections.Generic;
public class GameData
{
    public string UserName;
    public List<string> NameList;//未使用名称列表
    public float MusicVolume;
    public float SoundVolume;
    public int StageId;
}
public class ArchiveModel : AbstractModel
{
    public GameData m_GameData;
    public ArchiveModel()
    {
        m_GameData = ArchiveUtility.Instance.GetData();
    }
}
