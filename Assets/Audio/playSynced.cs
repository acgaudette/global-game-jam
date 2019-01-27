using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSynced : MonoBehaviour
{
    AudioSource baseAudioSource;

    AudioSource audSrc;

    public AudioClip[] thisObjClipArr;

    float oldAudioTime;

    public static bool panFlip;

    // Start is called before the first frame update
    void Start()
    {
        audSrc = GetComponent<AudioSource>();
        baseAudioSource = GameObject.Find("audioManager").GetComponent<AudioSource>();

        audSrc.time = baseAudioSource.time;

        if(playSynced.panFlip)
        {
            audSrc.panStereo = Random.Range(0f, 1.0f);
            playSynced.panFlip = !playSynced.panFlip;
        }
        else
        {
            audSrc.panStereo = Random.Range(-1.0f, 0f);
            playSynced.panFlip = !playSynced.panFlip;
        }

        audSrc.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if(audSrc.volume <1)
        {
            audSrc.volume += .055f;
        }



        if(audSrc.time < oldAudioTime)
        {
            print("relooped");
            audSrc.clip = thisObjClipArr[Random.Range(0, thisObjClipArr.Length)];
            audSrc.time = baseAudioSource.time;
            audSrc.Play();
        }

        oldAudioTime = audSrc.time;
   }
}
