using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 声音类型
/// </summary>
public enum E_AudioType
{
    Master = 0,
    Music = 1,
    Ambience = 2,
    SE = 3,
}

/// <summary>
/// 音效
/// </summary>
public class AudioSourcePool
{
    // 池中的可用 AudioSource
    private Stack<AudioSource> availableSources;

    // 正在使用的 AudioSource
    private List<AudioSource> inUseSources;

    // AudioSource 的预制体
    private AudioSource sourcePrefab;

    // 存放AudioSource的根节点
    private Transform sourceRoot;

    // 池的初始大小
    private int initialPoolSize;

    // 构造函数
    public AudioSourcePool(AudioSource prefab, int poolSize, Transform root)
    {
        sourcePrefab = prefab;
        initialPoolSize = poolSize;
        sourceRoot = root;

        // 初始化可用和正在使用的列表
        availableSources = new Stack<AudioSource>(initialPoolSize);
        inUseSources = new List<AudioSource>(initialPoolSize);

        // 初始化池
        InitializePool();
    }

    // 初始化池，创建指定数量的 AudioSource
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewAudioSource();
        }
    }

    // 创建一个新的 AudioSource 实例并将其放入可用池中
    private void CreateNewAudioSource()
    {
        AudioSource newSource = GameObject.Instantiate(sourcePrefab, sourceRoot);
        newSource.gameObject.SetActive(false); // 初始时禁用
        availableSources.Push(newSource);
    }

    // 获取一个可用的 AudioSource
    public AudioSource GetAudioSource()
    {
        AudioSource audioSource;

        // 如果没有可用的 AudioSource，则创建一个新的
        if (availableSources.Count == 0)
        {
            CreateNewAudioSource();
        }

        // 从池中取出一个 AudioSource
        audioSource = availableSources.Pop();
        inUseSources.Add(audioSource);

        // 启用并重置 AudioSource
        audioSource.gameObject.SetActive(true);
        audioSource.clip = null; // 重置clip
        audioSource.volume = 1f; // 重置音量
        audioSource.loop = false; // 重置循环状态

        return audioSource;
    }

    // 回收一个 AudioSource，将其放回池中
    public void PushAudioSource(AudioSource audioSource)
    {
        // 确保该 AudioSource 在使用列表中
        if (inUseSources.Contains(audioSource))
        {
            // 停止播放并禁用 AudioSource
            audioSource.Stop();
            audioSource.gameObject.SetActive(false);

            // 从使用列表中移除，并放入可用池
            inUseSources.Remove(audioSource);
            availableSources.Push(audioSource);
        }
    }
}

public class AudioMgr : SingletonMono<AudioMgr>
{
    [Header("外部组件")]
    public Transform audioSourcesRoot;
    public AudioSource audioSourcePrefab;
    public AudioSourcePool audioSourcePool;
    public AudioMixer audioMixer;

    public AudioSource globalMusicAS;
    public AudioSource globalAmbienceAS;
    public AudioSource globalSEAS;

    private void Start()
    {
        audioSourcePool = new AudioSourcePool(audioSourcePrefab, 10, audioSourcesRoot);
    }

    #region 音量

    //全局音量
    [SerializeField] private float globalVolume = 1f;
    //音乐音量
    [SerializeField] private float musicVolume = 1f;
    //环境音量
    [SerializeField] private float ambienceVolume = 1f;
    //音效音量
    [SerializeField] private float seVolume = 1f;

    /// <summary>
    /// 设置音量大小
    /// </summary>
    /// <param name="type">声音类型</param>
    /// <param name="volume">音量大小，0到1</param>
    public void SetAudioVolume(E_AudioType type, float volume)
    {
        float remapVolume = Remap01ToDB(volume);

        switch (type)
        {
            case E_AudioType.Master:
                globalVolume = volume;
                audioMixer.SetFloat("Master", remapVolume);
                break;
            case E_AudioType.Music:
                musicVolume = volume;
                audioMixer.SetFloat("Music", remapVolume);
                break;
            case E_AudioType.Ambience:
                ambienceVolume = volume;
                audioMixer.SetFloat("Ambience", remapVolume);
                break;
            case E_AudioType.SE:
                seVolume = volume;
                audioMixer.SetFloat("SFX", remapVolume);
                break;
        }
    }
    /// <summary>
    /// 重映射，匹配0到1和分贝的关系
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private float Remap01ToDB(float value)
    {
        if (value <= 0.0f) value = 0.0001f;
        return Mathf.Log10(value) * 20.0f;
    }

    #endregion

    /// <summary>
    /// 音量过渡
    /// </summary>
    /// <param name="audioSource">需要过渡的audioSource</param>
    /// <param name="targerVolume">目标值</param>
    /// <param name="duration">过渡时间</param>
    private void FadeVolume(AudioSource audioSource, float targerVolume, float duration, Action action = null)
    {
        StartCoroutine(Fade(audioSource, audioSource.volume, targerVolume, duration, action));
    }

    #region 背景音乐

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clip">传入的Clip</param>
    /// <param name="fadeInDuration">淡入时间</param>
    /// <param name="fadeOutDuration">淡出时间</param>
    public void PlayMusic(AudioClip clip, float fadeInDuration = 0f, float fadeOutDuration = 0f)
    {
        globalMusicAS.clip = clip;
        globalMusicAS.volume = 0f;
        globalMusicAS.Play();
        FadeVolume(globalMusicAS, 1f, fadeInDuration);
    }
    /// <summary>
    /// 停止音乐
    /// </summary>
    public void StopMusic(float duration = 0)
    {
        FadeVolume(globalMusicAS, 0f, duration, globalMusicAS.Stop);
    }
    /// <summary>
    /// 暂停音乐
    /// </summary>
    public void PauseMusic(float duration = 0)
    {
        FadeVolume(globalMusicAS, 0f, duration, globalMusicAS.Pause);
    }
    /// <summary>
    /// 恢复播放音乐
    /// </summary>
    public void UnPauseMusic(float duration = 0)
    {
        globalMusicAS.UnPause();
        FadeVolume(globalMusicAS, 1f, duration);
    }

    #endregion

    #region 环境音

    /// <summary>
    /// 播放环境音乐
    /// </summary>
    /// <param name="clip">传入的Clip</param>
    /// <param name="fadeInDuration">淡入时间</param>
    /// <param name="fadeOutDuration">淡出时间</param>
    public void PlayAmbience(AudioClip clip, float fadeInDuration = 0f, float fadeOutDuration = 0f)
    {
        globalAmbienceAS.clip = clip;
        globalAmbienceAS.volume = 0f;
        globalAmbienceAS.Play();
        FadeVolume(globalAmbienceAS, 1f, fadeInDuration);
    }
    /// <summary>
    /// 停止音乐
    /// </summary>
    public void StopAmbience(float duration = 0)
    {
        FadeVolume(globalAmbienceAS, 0f, duration, globalAmbienceAS.Stop);
    }
    /// <summary>
    /// 暂停音乐
    /// </summary>
    public void PauseAmbience(float duration = 0)
    {
        FadeVolume(globalAmbienceAS, 0f, duration, globalAmbienceAS.Pause);
    }
    /// <summary>
    /// 恢复播放音乐
    /// </summary>
    public void UnPauseAmbience(float duration = 0)
    {
        globalAmbienceAS.UnPause();
        FadeVolume(globalAmbienceAS, 1f, duration);
    }

    #endregion

    #region 音效

    /// <summary>
    /// 只播放一声，直接用全局的AudioSource
    /// </summary>
    /// <param name="clip"></param>
    public void PlayOneShot(AudioClip clip)
    {
        globalSEAS.PlayOneShot(clip);
    }
    /// <summary>
    /// 要对AudioSource进行特殊操作，比如过渡之类的
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    public AudioSource PlaySE(AudioClip clip)
    {
        AudioSource audioSource = audioSourcePool.GetAudioSource();

        audioSource.clip = clip;
        audioSource.Play();

        return audioSource;
    }
    /// <summary>
    /// 回收AudioSource
    /// </summary>
    /// <param name="audioSource"></param>
    public void StopSE(AudioSource audioSource, float duration = 0f)
    {
        FadeVolume(audioSource, 0f, duration, Push);

        void Push()
        {
            audioSourcePool.PushAudioSource(audioSource);
        }
    }

    #endregion

    #region 特殊效果


    #endregion

    /// <summary>
    /// 过渡音量
    /// </summary>
    private IEnumerator Fade(AudioSource audioSource, float from, float to, float duration, Action onFadeComplete)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float currentValue = Mathf.Lerp(from, to, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            audioSource.volume = currentValue;
            yield return null;
        }
        // 确保结束时设定为最终值
        audioSource.volume = to;
        onFadeComplete?.Invoke();
    }
}
