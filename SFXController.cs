using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SFXController : MonoBehaviour {

    public static List<AudioSource> SFX = new List<AudioSource>();

    void Awake()
    {
        SFX.Clear(); //for loading in new scenes, must remove destroyed object refs
        foreach (AudioSource source in transform.GetComponentsInChildren<AudioSource>())
        {
            if (!source.loop)
            {
                SFX.Add(source);
            }
        }
    }

    void Update() {
        foreach (AudioSource source in SFX)
        {
            if (source && source.enabled)
            {
                source.enabled = source.isPlaying;
            }
        }
	}

    public static void PlaySound(int id)
    {
        //really goofy way of saying,
        //"stop playing the sound."
        SFX[id].enabled = false;
        //"okay now play the sound."
        SFX[id].enabled = true;
    }
}
