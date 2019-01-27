using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createTenantAudio : MonoBehaviour
{

    public GameObject tenantSource;

    AudioClip[][] myTenantTracks;

    AudioClip[] curArray;

    GameObject tempTenantAudio;

    public bool debug;

    GameObject[] tenantAudioSources;

    // Start is called before the first frame update
    void Start()
    {
        myTenantTracks = GetComponent<audioLibrary>().tenantTracks;
    }

    // Update is called once per frame
    void Update()
    {
        //debug
        if(Input.GetKeyDown("t") && debug)
        {
            makeSource(Random.Range(0,GetComponent<audioLibrary>().tenantTracks.Length), GetComponent<Transform>());
        }
    }

    public void makeSource(int tenantIndex, Transform targTransform)
    {

        curArray = myTenantTracks[tenantIndex];

        tenantAudioSources = GameObject.FindGameObjectsWithTag("tenantSource");

        for (int i = 0; i < tenantAudioSources.Length; i++)
        {
            if(curArray == tenantAudioSources[i].GetComponent<playSynced>().thisObjClipArr)
            {
                print("doubled" + " " + myTenantTracks[tenantIndex].ToString());
                return;
            }
        }


        tempTenantAudio = Instantiate(tenantSource,targTransform);
        tempTenantAudio.GetComponent<AudioSource>().clip = curArray[Random.Range(0, curArray.Length)];
        tempTenantAudio.GetComponent<playSynced>().thisObjClipArr = curArray;
    }
}
