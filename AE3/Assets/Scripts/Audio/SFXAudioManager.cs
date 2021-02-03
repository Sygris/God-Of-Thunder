using System;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioManager : MonoBehaviour
{
    public static SFXAudioManager SFXManager;

    public List<string> ClipName = new List<string>();
    public List<AudioClip> ClipList = new List<AudioClip>();
    private Dictionary<string, AudioClip> SFXLib = new Dictionary<string, AudioClip>();

    public GameObject SFXPrefab;

    void Start()
    {
        SFXManager = this;

        for (int i = 0; i < ClipName.Count; i++)
            SFXLib.Add(ClipName[i], ClipList[i]);
    }

    public void PlaySFX(string Clip)
    {
        if (SFXLib.ContainsKey(Clip))
        {
            AudioSource SFX = Instantiate(SFXPrefab).GetComponent<AudioSource>();
            SFX.PlayOneShot(SFXLib[Clip]);
            Destroy(SFX.gameObject, SFXLib[Clip].length);
        }
    }

    public void PlaySFXWithoutDestroying(string Clip)
    {
        if (SFXLib.ContainsKey(Clip))
        {
            AudioSource SFX = Instantiate(SFXPrefab, transform).GetComponent<AudioSource>();

            SFX.PlayOneShot(SFXLib[Clip]);
        }
    }

    public void StopClip()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
