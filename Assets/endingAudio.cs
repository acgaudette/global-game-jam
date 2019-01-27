using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class endingAudio : MonoBehaviour
{

    public static bool gameOver;

    public bool debug;

    public bool gameOverDebug;

    private GameObject[] tenantSources;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            endingAudio.gameOver = gameOverDebug;
        }

        if (endingAudio.gameOver)
        {

            GameObject.Find("audioManager").GetComponent<AudioSource>().mute = true;
            
            tenantSources = GameObject.FindGameObjectsWithTag("tenantSource");

            

            for (int i = 0; i < tenantSources.Length ; i++)
            {
                tenantSources[i].GetComponent<AudioSource>().pitch -= .025f;
               

                if (tenantSources[i].GetComponent<AudioSource>().pitch < 0.1f)
                {
                    tenantSources[i].GetComponent<AudioSource>().volume = 0;
                }
            }
            
        }
        else
        {
            GameObject.Find("audioManager").GetComponent<AudioSource>().mute = false;
        }
        
    }
}
