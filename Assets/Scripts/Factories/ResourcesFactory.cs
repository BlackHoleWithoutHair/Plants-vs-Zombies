using System.Collections.Generic;
using UnityEngine;

public class ResourcesFactory
{
    static string BaseSoundPath = "Audios/Sound/";
    static string BaseMusicPath = "Audios/Music/";
    static string BaseCardPath = "Prefabs/Cards/";
    static string Base3DCardPath = "Prefabs/3DCards/";
    static string BasePlantPath = "Prefabs/Plants/";
    static string BaseZombiePath = "Prefabs/Zombies/";
    static string BaseBulletPath = "Prefabs/Bullets/";
    static string BaseEffectPath = "Prefabs/Effects/";
    static string BaseDataPath = "Datas/";
    static string BaseOtherPath = "Prefabs/Other/";
    static Dictionary<string, AudioClip> AudioDic=new Dictionary<string, AudioClip>();
    public static AudioClip GetSoundAudioClip(string name)
    {
        if(!AudioDic.ContainsKey(name))
        {
            AudioDic.Add(name,Resources.Load<AudioClip>(BaseSoundPath + name));
        }
        else if (AudioDic[name]==null)
        {
            AudioDic[name]=Resources.Load<AudioClip>(BaseSoundPath + name);
        }
        Debug.Log(AudioDic[name]);
        return AudioDic[name];
    }
    public static AudioClip GetMusicAudioClip(string name)
    {
        if (!AudioDic.ContainsKey(name))
        {
            AudioDic.Add(name, Resources.Load<AudioClip>(BaseMusicPath + name));
        }
        return AudioDic[name];
    }
    public static T GetResourceFromAll<T>(string name) where T:Object
    {
        T[] results = Resources.LoadAll<T>("");
        foreach(T result in results)
        {
            if(result.name==name)
            {
                return result;
            }
        }
        return default(T);
    }
    public static GameObject GetCard(string name)
    {
        return Resources.Load<GameObject>(BaseCardPath + name+"Card");
    }
    public static GameObject Get3DCard(PlantType type)
    {
        return Resources.Load<GameObject>(Base3DCardPath + type.ToString() + "Card");
    }
    public static GameObject GetBullet(BulletType type)
    {
        return Resources.Load<GameObject>(BaseBulletPath + type.ToString());
    }
    public static GameObject GetEffect(string name)
    {
        return Resources.Load<GameObject>(BaseEffectPath + name);
    }
    public static GameObject GetOtherGameObject(string name)
    {
        return Resources.Load<GameObject>(BaseOtherPath + name);
    }
    public static T GetData<T>() where T : ScriptableObject
    {
        string path="";
        switch(typeof(T).Name)
        {
            case nameof(PlantDataScriptableObject):
                path = BaseDataPath + "PlantData";
                break;
            case nameof(ZombieDataScriptableObject):
                path = BaseDataPath + "ZombieData";
                break;
            case nameof(StageDataScriptableObject):
                path = BaseDataPath + "StageData";
                break;
            case nameof(CardDescribeScriptableObject):
                path = BaseDataPath + "CardDescribe";
                break;
            case nameof(DialogueDataScriptableObject):
                path = BaseDataPath + "DialogueData";
                break;
            case nameof(StageUnlockPlantScriptableObject):
                path = BaseDataPath + "StageUnlockPlantData";
                break;
        }
        return Resources.Load<T>(path);
    }
    public static GameObject GetZombie(ZombieType type)
    {
        return Resources.Load<GameObject>(BaseZombiePath + type.ToString());
    }
    public static GameObject GetPlant(PlantType type)
    {
        return Resources.Load<GameObject>(BasePlantPath + type.ToString());
    }

}
