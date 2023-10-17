using UnityEngine;
using UnityEngine.Audio;

public class AudioUtility:Singleton<AudioUtility>
{
    private GameObject SystemObject;
    private AudioMixer mixer;
    public AudioSource m_MusicSource { get; private set; }
    public AudioSource m_EffectSource { get; private set; }
    private AudioUtility()
    {
        Init();
        EventCenter.Instance.RegisterObserver(EventType.OnSceneChangeComplete, () =>
        {
            Init();
        });
    }
    private void Init()
    {
        if (!GameObject.Find("AudioSystem"))
        {
            SystemObject = Object.Instantiate(ResourcesFactory.GetOtherGameObject("AudioSystem"));
        }
        else
        {
            SystemObject = GameObject.Find("AudioSystem");
        }
        mixer = ResourcesFactory.GetResourceFromAll<AudioMixer>("AudioMixer");
        m_MusicSource = SystemObject.GetComponent<AudioSource>();
        m_EffectSource = SystemObject.GetComponents<AudioSource>()[1];
        SetBgmVolume(ArchiveCommand.Instance.MusicVolume);
        SetSfxVolume(ArchiveCommand.Instance.SoundVolume);
    }
    public void SetBgmVolume(float num)
    {
        mixer.SetFloat("BGMVolume", FloatToDb(num));
    }
    public void SetSfxVolume(float num)
    {
        mixer.SetFloat("SFXVolume", FloatToDb(num));
    }
    public void PlayOneShot(string name)
    {
        m_EffectSource.PlayOneShot(ResourcesFactory.GetSoundAudioClip(name));
    }
    public void PlayMusic(string name)
    {
        AudioClip clip = ResourcesFactory.GetMusicAudioClip(name);
        m_MusicSource.clip=clip;
        m_MusicSource.Play();
    }
    public float FloatToDb(float num)
    {
        if(num<=0)
        {
            num = 0.01f;
        }
        return Mathf.Log10(num) * 20f;
    }
}
