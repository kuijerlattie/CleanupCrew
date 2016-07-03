using UnityEngine;
using System.Collections;

public class StaticFuntions : MonoBehaviour
{
    static GameObject musicObject = null;
    //static Dictionary particles;  //TODO particles loaded in memory
    public static GameObject SpawnParticle(string name, Vector3 position) //documented
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        g.transform.position = position;
        return g;
    }

    public static GameObject SpawnParticleToUI(string name, Vector3 position, ParticleToUI.UITarget UI = ParticleToUI.UITarget.Barrel) //documented
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        g.transform.position = position;
        ParticleToUI PTUI = g.AddComponent<ParticleToUI>();
        PTUI.currentTarget = UI;
        PTUI.Init();
        DestroyParticle DesP = g.GetComponent<DestroyParticle>();
        if (DesP != null) DesP.StayInScene = true;
        return g;
    }

    public static AudioSource PlaySound(GameObject go, string soundname, bool looping = false)
    {
        GameObject g = new GameObject("SoundEffect");
        if (go == null)
            g.transform.position = new Vector3(0, 0, 0);
        else
            g.transform.position = go.transform.position;
        g.AddComponent<RemoveAudioSource>();
        AudioSource audio = g.AddComponent<AudioSource>();
        AudioClip sound = Resources.Load("Audio/" + soundname) as AudioClip;
        audio.clip = sound;
        audio.loop = looping;
        audio.PlayOneShot(sound, 1);
        return audio;
    }

    public static AudioSource PlayMusic(string soundname)
    {
        if (musicObject == null)
        {
            musicObject = new GameObject("Music");
            musicObject.AddComponent<AudioSource>();
        }
        //GameObject g = new GameObject("Music");
        //if (go == null)
        //    g.transform.position = new Vector3(0, 0, 0);
        //else
       //     g.transform.position = go.transform.position;
       // g.AddComponent<RemoveAudioSource>();
        AudioSource audio = musicObject.GetComponent<AudioSource>();
        AudioClip sound = Resources.Load("Audio/" + soundname) as AudioClip;
        audio.clip = sound;
        audio.loop = true;
        audio.Play();
        return audio;
    }
}
