using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider bgmSlider;  // 背景音樂音量滑動條
    public Slider sfxSlider;  // 音效音量滑動條

    void Start()
    {
        // 初始化滑動條的值
        bgmSlider.value = PlayerPrefs.GetFloat("BGM_VOLUME", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_VOLUME", 1f);

        // 註冊滑動條的數值變化事件
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // 設置背景音樂音量
    public void SetBGMVolume(float volume)
    {
        AudioManager.Instance.SetBGMVolume(volume);
    }

    // 設置音效音量
    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
