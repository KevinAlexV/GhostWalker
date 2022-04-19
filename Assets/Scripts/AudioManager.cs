using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioMgr;

    //BGM
    [SerializeField]
    private AudioClip sceneBGM;
    private AudioSource currentTrack;

    //SFX
    public GameObject SFXSource;
    private AudioSource SFXPlayer {
        get{
            if(SFXSource != null){
                return SFXSource.GetComponent<AudioSource>();
            } else {
                return GetComponent<AudioSource>();
            }
        }
    }
    public List<AudioClip> SoundEffects;
    public List<AudioClip> LevelBGM;

    private int currentLevel = 0;


    public void Awake()
    {
        if (audioMgr != null && audioMgr != this)
        {
            Destroy(gameObject);
            return;
        }
        //Play 'awake' SoundEffects for the scene (one time) CHANGE THIS IF WE GO WITH A 'STARTUP' SONG
        currentTrack = GetComponent<AudioSource>();
        currentTrack.volume = 0.4f;
        currentTrack.clip = sceneBGM;
        currentTrack.loop = true;


        //Configure audioSources
        SFXPlayer.volume = 0.5f;
        currentTrack.playOnAwake = false;
        SFXPlayer.playOnAwake = false;


        audioMgr = this;

        currentTrack.Play();
        DontDestroyOnLoad(gameObject);
        GameObject p = GameObject.Find("Player");
        if(p != null) transform.SetParent(p.transform);
        
    }

    public void PlayCharacterSFX(GameObject SourceObject, string SFXName)
    {
        if(SourceObject != null)
        { 
            Transform[] ts = SourceObject.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in ts)
            { 
                if (t.gameObject.name == SFXName)
                { 
                    PlayObjectSFX(t.gameObject);
                    break;
                }

            }
        }
    }

    public void PlayObjectSFX(GameObject SFXObject)
    {
        AudioSource SFXObjectPlayer = SFXObject.GetComponent<AudioSource>();

        SFXObjectPlayer.volume = 0.1f;
        SFXObjectPlayer.Play();
        
    }

    public void PlayGivenSFX(AudioClip newclip)
    {

        Debug.Log($"{newclip.name} is the audio clip being played");

        SFXPlayer.clip = newclip;

        SFXPlayer.volume = 0.1f;
        SFXPlayer.Play();
    }

    public void PlayUISFX(string SFX)
    {

        switch (SFX)
        {
            case "UIPositive":
                SFXPlayer.volume = .75f;
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("UIPositive"));
                break;
            case "UINegative":
                SFXPlayer.volume = .75f;
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("UINegative"));
                break;
            case "UIStart":
                SFXPlayer.volume = .75f;
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("UIStart"));
                break;
            case "UIStop":
                SFXPlayer.volume = .75f;
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("UIStop"));
                break;
            case "UIPing":
                SFXPlayer.volume = .75f;
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("UIPing"));
                break;
            default:
                Debug.Log($"<color=red>AudioManager:</color> Sound effect of name {SFX} is not listed!");
                break;
        }

        SFXPlayer.Play();

    }

    public void PlaySFX(int index, float vol = 1.0f)
    {
        float preVolume = SFXPlayer.volume;
        try
        {
            SFXPlayer.clip = SoundEffects[index];
            SFXPlayer.volume = vol;
            SFXPlayer.Play();
        }
        catch { Debug.Log($"<color=red>Sound not found at index {index}</color>"); }
    }

    public void StopMusic() { currentTrack.Stop(); }

    public void PauseMusic() { currentTrack.Pause(); }

    public void PlayMusic() { currentTrack.Play(); }

    public void ChangeMusic(AudioClip newClip)
    {
        currentLevel++;
        var oldTrack = currentTrack;
        currentTrack = gameObject.AddComponent<AudioSource>();
        currentTrack.volume = 0.0f;
        currentTrack.clip = newClip;
        currentTrack.loop = true;
        currentTrack.Play();
        StartCoroutine(CrossFade(oldTrack, 0f, 0.001f));
        StartCoroutine(CrossFade(currentTrack, 0.2f, 0.001f));
    }

    public void ChangeMusic(int newClip)
    {
        Debug.Log("Changing Music.");

        try
        {
            currentLevel++;
            var oldTrack = currentTrack;
            currentTrack = gameObject.AddComponent<AudioSource>();
            currentTrack.volume = 0.0f;
            currentTrack.clip = LevelBGM[newClip];
            currentTrack.loop = true;
            currentTrack.Play();
            StartCoroutine(CrossFade(oldTrack, 0f, 0.001f));
            StartCoroutine(CrossFade(currentTrack, 0.2f, 0.001f));
        }
        catch
        { Debug.Log("Music not available for level#" + newClip); }
    }

    private IEnumerator CrossFade(AudioSource source, float volumeTarget, float rate){
        float progress = 0f;
        while(source.volume != volumeTarget){
            source.volume = Mathf.Lerp(source.volume, volumeTarget, progress);
            progress += rate;
            yield return new WaitForFixedUpdate();
        }
        if(source.volume == 0){
            Destroy(source);
        }
    }
}
