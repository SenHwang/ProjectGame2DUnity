using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip audioMap;
    public AudioClip audioSrc;

    public GameObject MapSound;
    public GameObject UIMoreSound;

    private static float volumeSound = .25f;
    private static float volumeSRC = .25f;

    AudioClip[] audioMapList;
    AudioClip[] audioSRCList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadAudio();
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    private void LoadAudio()
    {
        audioMapList = Resources.LoadAll<AudioClip>(GAME.MUSIC_PATH);
        audioSRCList = Resources.LoadAll<AudioClip>(GAME.SRC_PATH);
    }

    //private void Start()
    //{
    //    if (audioMap != null) {
    //        UpdateAudioMap(audioMap);
    //    }
    //}

    private static void UpdateAudioMap(AudioClip audio)
    {
        instance.MapSound.GetComponent<AudioSource>().clip = audio;
        instance.MapSound.GetComponent<AudioSource>().volume = volumeSound;
        instance.MapSound.GetComponent<AudioSource>().Play();
    }
    private static void UpdateAudioSRC(AudioClip audio)
    {
        GameObject onjectAudioSrc = Instantiate(instance.UIMoreSound) as GameObject;
        onjectAudioSrc.GetComponent<AudioSource>().clip = audio;
        onjectAudioSrc.GetComponent<AudioSource>().volume = volumeSRC;
        onjectAudioSrc.transform.SetParent(instance.gameObject.transform);
        onjectAudioSrc.GetComponent<AudioSource>().Play();
        //instance.UIMoreSound.GetComponent<AudioSource>().clip = audio;
        //instance.UIMoreSound.GetComponent<AudioSource>().volume = volumeSRC;
    }

    public static void ChangeAudioMapByText(string name)
    {
        if(name == "")
        {
            UpdateAudioMap(null);
            return;
        }

        AudioClip audioMapGet = instance.audioMapList.SingleOrDefault(x => x.name == name);
        if(!audioMapGet)
        {
            UpdateAudioMap(null);
            return;
        }

        UpdateAudioMap(audioMapGet);
    }

    public static void SpawnAudioSRCByText(string name)
    {
        if (name == "")
        {
            UpdateAudioSRC(null);
            return;
        }

        AudioClip audioSRCGet = instance.audioSRCList.SingleOrDefault(x => x.name == name);
        if (!audioSRCGet)
        {
            UpdateAudioSRC(null);
            return;
        }

        UpdateAudioSRC(audioSRCGet);
    }

}
