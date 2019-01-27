using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverAnim : MonoBehaviour
{
    float elapsed;
    float animFraction;
    Vector3 final;
    public float animTime;
    public GameObject Game;
    public GameObject Over;
    public GameObject Tryagain;
    public GameObject Dash;

    Vector3 GameRotation;
    Vector3 OverRotation;
    Vector3 larger;
    Vector3 trypos;
    Vector3 newpos;
    
    public void Start()
    {
        elapsed = 0.0f;
        final = new Vector3(1.0f, 1.0f, 1.0f);
        GameRotation = new Vector3(0.0f, 0.0f, -25f);
        OverRotation = new Vector3(0.0f, 0.0f, 25f);
        larger = new Vector3(1.2f, 1.2f, 1.2f);
        trypos = Tryagain.transform.position;
        newpos = new Vector3(trypos.x, trypos.y - 10f, trypos.z);
    }

    public void playAnim()
    {
        elapsed = 0.0f;
        //endingAudio.gameOver = true;
        Game.transform.localScale = Vector3.zero;
        Over.transform.localScale = Vector3.zero;
        Tryagain.transform.localScale = Vector3.zero;
        Dash.transform.localScale = Vector3.zero;
        
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        animFraction = elapsed / animTime;
        if (animFraction < 1.0f)
        {
            Game.transform.localScale = Vector3.Slerp(Vector3.zero, final, animFraction);
            Game.transform.localEulerAngles = Vector3.Slerp(GameRotation, Vector3.zero, animFraction);
            Over.transform.localScale = Vector3.Slerp(Vector3.zero, final, animFraction);
            Over.transform.localEulerAngles = Vector3.Slerp(OverRotation, Vector3.zero, animFraction);
            Tryagain.transform.localScale = Vector3.Slerp(Vector3.zero, final, animFraction);
            Dash.transform.localScale = Vector3.Slerp(Vector3.zero, final, animFraction);
        }
        else
        {
            Game.transform.localScale = Vector3.Slerp(final, larger, Mathf.Cos(animFraction));
            Over.transform.localScale = Vector3.Slerp(final, larger, Mathf.Cos(animFraction));
            Dash.transform.localScale = Vector3.Slerp(final, larger, Mathf.Cos(animFraction));
            Tryagain.transform.position = Vector3.Slerp(trypos, newpos, Mathf.Cos(animFraction));
        }
    }
}
