using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmplayer;
    AudidHighPassFilter bgmeffect;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win}

    void Awake()
    {
        instance = this;
        Init();
    }
    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmplayer = bgmObject.AddComponent<AudioSource>();
        bgmplayer.playOnAwake = false;
        bgmplayer.loop = true;
        bgmplayer.volume = bgmVolume;
        bgmplayer.clip = bgmClip;
        bgmeffect = Camera.main.GetComponent<AudioHighPassFilter>();

        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }
    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmplayer.Play();
        } else
        {
            bgmplayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmeffect.enbleed = isPlay;
    }
    public void Playsfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;
            if(sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[0].clip = sfxClips[(int)sfx];
            sfxPlayers[0].Play();
            break;
        }
    }
}
