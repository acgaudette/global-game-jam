using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioLibrary : MonoBehaviour
{

    public AudioClip[][] tenantTracks;

    public AudioClip[] alien;
    public AudioClip[] coffee;
    public AudioClip[] dinosaur;
    public AudioClip[] fish;
    public AudioClip[] hypeBeast;
    public AudioClip[] Ltrain;
    public AudioClip[] pizzaDelivery;
    public AudioClip[] snake;
    public AudioClip[] streamer;
    public AudioClip[] taxi;
    public AudioClip[] teeth;
    public AudioClip[] vr;


    // Start is called before the first frame update
    void Awake()
    {
        tenantTracks = new AudioClip[12][];

        tenantTracks[0] = alien;
        tenantTracks[1] = coffee;
        tenantTracks[2] = dinosaur;
        tenantTracks[3] = fish;
        tenantTracks[4] = hypeBeast;
        tenantTracks[5] = Ltrain;
        tenantTracks[6] = pizzaDelivery;
        tenantTracks[7] = snake;
        tenantTracks[8] = streamer;
        tenantTracks[9] = taxi;
        tenantTracks[10] = teeth;
        tenantTracks[11] = vr;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
