using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlyer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    [Header("#HeartBeat")]
    public AudioClip heartbeatClip;
    public float heartbeatVolume;
    AudioSource heartbeatPlayer;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlyer = bgmObject.AddComponent<AudioSource>();
        bgmPlyer.playOnAwake = false;
        bgmPlyer.loop = true;
        bgmPlyer.volume = bgmVolume;
        bgmPlyer.clip = bgmClip;

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }

        GameObject heartObject = new GameObject("HeartbeatPlayer");
        heartObject.transform.parent = transform;
        heartbeatPlayer = heartObject.AddComponent<AudioSource>();
        heartbeatPlayer.playOnAwake = false;
        heartbeatPlayer.loop = true;
        heartbeatVolume = heartbeatPlayer.volume;
        heartbeatPlayer.clip = heartbeatClip;
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlyer.Play();
        }
        else
        {
            bgmPlyer.Stop();
        }
    }

    public void PlaySfx(EItemType itemtype)
    {   
        for(int index = 0; index < sfxPlayers.Length;index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)itemtype];
            sfxPlayers[loopIndex].Play();

            break;
        }
    }

    public void HeartBeatSfx(bool isPlay)
    {
        if(isPlay)
        {
            heartbeatPlayer.Play();
        }
        else
        {
            heartbeatPlayer.Stop();
        }
    }

    public bool IsHeartbeatPlaying()
    {
        return heartbeatPlayer.isPlaying;
    }


}
