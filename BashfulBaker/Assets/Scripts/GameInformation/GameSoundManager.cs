using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to where the audio listener is!
/// </summary>
public class GameSoundManager : MonoBehaviour
{

    public Dictionary<string, List<AudioSource>> audioSources = new Dictionary<string, List<AudioSource>>();

    // Start is called before the first frame update
    void Start()
    {
        Game.SoundManager = this;

    }

    // Update is called once per frame
    void Update()
    {
        cleanUpAudioSources();

    }

    private void cleanUpAudioSources()
    {

        //NOTE THIS DOESNT WORK FOR PAUSING!
        Dictionary<string, List<AudioSource>> removalList = new Dictionary<string, List<AudioSource>>();
        foreach (KeyValuePair<string, List<AudioSource>> pair in audioSources)
        {
            foreach (AudioSource source in pair.Value)
            {
                if (source.isPlaying == false)
                {
                    if (removalList.ContainsKey(pair.Key))
                    {
                        removalList[pair.Key].Add(source);
                    }
                    else
                    {
                        removalList.Add(pair.Key, new List<AudioSource>()
                        {
                            source
                        });
                    }
                }
            }
        }

        foreach (KeyValuePair<string, List<AudioSource>> pair in removalList)
        {
            foreach(AudioSource source in pair.Value)
            {
                audioSources[pair.Key].Remove(source);
                Destroy(source);
            }
        }
    }


    public void playSound(AudioClip clip)
    {
        AudioSource source=this.gameObject.AddComponent<AudioSource>();
        source.clip = clip;

        if (audioSources.ContainsKey(clip.name))
        {
            audioSources[clip.name].Add(source);
            source.Play();
            return;
        }
        else
        {
            List<AudioSource> sources = new List<AudioSource>();
            sources.Add(source);
            audioSources.Add(clip.name, sources);
        }
        source.Play();
    }

    public void stopSound(AudioClip clip)
    {
        if (audioSources.ContainsKey(clip.name))
        {
            audioSources[clip.name].Find(source => source.clip == clip).Stop();
        }
    }

}
