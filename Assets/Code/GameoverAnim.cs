using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverAnim : MonoBehaviour
{
    float elapsed;
    float animFraction;
    Vector3 final;
    public float animTime;
    public GameObject Gameover;
    public GameObject Tryagain;
    
    public void Start()
    {
        elapsed = 0.0f;
        final = new Vector3(1.0f, 1.0f, 1.0f);
        
    }

    public void playAnim()
    {
        elapsed = 0.0f;
        Gameover.transform.localScale = Vector3.zero;
        Tryagain.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        animFraction = elapsed / animTime;
        if (animFraction < 1.0f)
        {
            Gameover.transform.localScale = Vector3.Slerp(Vector3.zero, final, animFraction);
            Tryagain.transform.localScale = Vector3.Slerp(Vector3.zero, final, animFraction);
            Debug.Log(animFraction);
        }
    }
}
