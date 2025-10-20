using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;  // 背景音樂音效源
    public AudioSource sfxSource;  // 音效音效源

    public AudioClip defaultBGM;   // 預設背景音樂

    private const string BGM_VOLUME_KEY = "BGM_VOLUME";
    private const string SFX_VOLUME_KEY = "SFX_VOLUME";

    void Awake()
    {
        // 單例模式，保證音效管理器只有一個實例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 確保物件不會在場景切換時被銷毀
        }
        else
        {
            Destroy(gameObject);  // 如果有實例，銷毀當前物件
        }

        // 從 PlayerPrefs 加載音量設置
        bgmSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 1f);  // 默認為 1
        sfxSource.volume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);  // 默認為 1

        // 播放背景音樂
        PlayBGM(defaultBGM);
    }

    // 設置背景音樂音量
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);  // 保存設置
        PlayerPrefs.Save();  // 確保設置被寫入磁碟
    }

    // 設置音效音量
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);  // 保存設置
        PlayerPrefs.Save();  // 確保設置被寫入磁碟
    }

    // 播放音效
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);  // 播放一次音效
    }

    // 播放背景音樂
    public void PlayBGM(AudioClip bgm)
    {
        bgmSource.clip = bgm;
        bgmSource.loop = true;  // 設置為循環播放
        bgmSource.Play();
    }
}
