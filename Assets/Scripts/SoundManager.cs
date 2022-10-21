using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    #region SerializedFields
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectsSource;

    #endregion

    #region Fields
    public static SoundManager Instance;

    [Range(0, 1)] float _masterVolume;
    [Range(0, 1)] float _musicVolume;
    [Range(0, 1)] float _effectsVolume;

    bool isMusicMuted;
    bool areEffectsMuted;
    #endregion

    #region Properties
    public float MasterVolume
    {
        get => _masterVolume;
        set
        {
            _masterVolume = value;
            MainVolumeUpdated(value);
        }
    }

    private void MainVolumeUpdated(float value)
    {
        musicSource.volume = _musicVolume * MasterVolume;
        effectsSource.volume = _effectsVolume * MasterVolume;
        PlayerPrefs.SetFloat("_masterVolume", value);
    }

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            musicSource.volume = _musicVolume * MasterVolume;
            PlayerPrefs.SetFloat("_musicVolume", value);
        }
    }
    public float EffectsVolume
    {
        get => _effectsVolume;
        set
        {
            _effectsVolume = value;
            effectsSource.volume = _effectsVolume * MasterVolume;
            PlayerPrefs.SetFloat("_effectsVolume", value);
        }
    }
    public bool IsMusicMuted
    {
        get => isMusicMuted;
        set
        {
            isMusicMuted = value;

            if (value)
                musicSource.Pause();
            else
                musicSource.Play();

            musicSource.mute = isMusicMuted;
            PlayerPrefs.SetInt("IsMusicMuted", value ? 1 : 0);
        }
    }

    public bool AreEffectsMuted
    {
        get => areEffectsMuted;
        set
        {
            areEffectsMuted = value;
            effectsSource.mute = areEffectsMuted;
            areEffectsMuted = PlayerPrefs.GetInt("AreEffectsMuted", 0) == 1 ? true : false;
        }
    }
    #endregion

    #region Methods
    protected void Awake()
    {
        Instance = this;
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        if (effectsSource == null)
        {
            effectsSource = gameObject.AddComponent<AudioSource>();
        }

        MasterVolume = PlayerPrefs.GetFloat("_masterVolume", 1);
        MusicVolume = PlayerPrefs.GetFloat("_musicVolume", 1);
        EffectsVolume = PlayerPrefs.GetFloat("_effectsVolume", 1);
        IsMusicMuted = PlayerPrefs.GetInt("IsMusicMuted", 0) == 1 ? true : false;
        AreEffectsMuted = PlayerPrefs.GetInt("AreEffectsMuted", 0) == 1 ? true : false;

        if (!IsMusicMuted)
        {
            musicSource.Play();
        }
    }

    public void PlayAudioEffectOnce(AudioClip clip)
    {
        float oneShotVolume = _masterVolume * _effectsVolume;
        effectsSource.PlayOneShot(clip, oneShotVolume);
    }
    #endregion
}
