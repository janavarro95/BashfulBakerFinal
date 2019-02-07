using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The game's sound manager.
/// </summary>
public class GameSoundManager : MonoBehaviour
{

    /// <summary>
    /// A dictionary to keep track of all of the currently playing audio sources.
    /// </summary>
    public Dictionary<string, List<AudioSource>> audioSources = new Dictionary<string, List<AudioSource>>();

    // Start is called before the first frame update
    void Start()
    {
        Game.SoundManager = this;
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        cleanUpAudioSources();

        this.gameObject.transform.position = Camera.main.transform.position;
    }

    /// <summary>
    /// Cleans up all of the unplaying audio sources from memory.
    /// </summary>
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

    /// <summary>
    /// Plays an audio clip.
    /// </summary>
    /// <param name="clip"></param>
    public void playSound(AudioClip clip)
    {
        AudioSource source=this.gameObject.AddComponent<AudioSource>();
        source.clip = clip;

        if (audioSources.ContainsKey(clip.name))
        {
            audioSources[clip.name].Add(source);
            source.Play();
            source.volume = Game.Options.muteVolume ? 0f : Game.Options.sfxVolume;
            return;
        }
        else
        {
            List<AudioSource> sources = new List<AudioSource>();
            sources.Add(source);
            audioSources.Add(clip.name, sources);
        }
        source.Play();
        source.volume = Game.Options.muteVolume ? 0f : Game.Options.sfxVolume;
    }

    /// <summary>
    /// Play a sound with a specific pitch.
    /// </summary>
    /// <param name="clip">The clip to play.</param>
    /// <param name="pitch">The pitch for the clip.</param>
    public void playSound(AudioClip clip, float pitch)
    {
        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        source.clip = clip;

        if (audioSources.ContainsKey(clip.name))
        {
            audioSources[clip.name].Add(source);
            source.pitch = pitch;
            source.Play();
            source.volume = Game.Options.muteVolume ? 0f : Game.Options.sfxVolume;
            return;
        }
        else
        {
            List<AudioSource> sources = new List<AudioSource>();
            sources.Add(source);
            source.pitch = pitch;
            audioSources.Add(clip.name, sources);
        }
        source.Play();
        source.volume = Game.Options.muteVolume ? 0f : Game.Options.sfxVolume;
    }

    /// <summary>
    /// Stops the currently playing sound.
    /// </summary>
    /// <param name="clip">The audio clip to stop playing.</param>
    public void stopSound(AudioClip clip)
    {
        if (audioSources.ContainsKey(clip.name))
        {
            audioSources[clip.name].Find(source => source.clip == clip).Stop();
        }
    }

    /// <summary>
    /// Checks if a sound is playing.
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    public bool isSoundPlaying(AudioClip clip)
    {
        if (!this.audioSources.ContainsKey(clip.name)) return false;
        return this.audioSources[clip.name].Count > 0;
    }

}
