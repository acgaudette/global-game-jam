using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionAnim : MonoBehaviour
{
    public float arrowspeed;
    public float titlespeed;

    public GameObject[] Arrows;
    public GameObject Title;
    public GameObject left;
    public GameObject right;

    float elapsedtime;
    Vector3 lower;
    Vector3 higher;
    Vector3 smaller;
    Vector3 longer;
    Vector3 leftOrigin;
    Vector3 rightOrigin;
    Vector3 rightEnd;
    Vector3 leftEnd;


    private void Start()
    {
        elapsedtime = 0.0f;
        higher = new Vector3(1.05f, 1.05f, 1.05f);
        smaller = new Vector3(1.0f, 1.0f, 1.0f);
        longer = new Vector3(1.2f, 1.0f, 1.0f);
        rightOrigin = right.transform.position;
        leftOrigin = left.transform.position;
        rightEnd = new Vector3(right.transform.position.x + 20f, right.transform.position.y, right.transform.position.z);
        leftEnd = new Vector3(left.transform.position.x - 20f, left.transform.position.y, left.transform.position.z);
    }

    public void playAnim()
    {
        elapsedtime = 0.0f;
    }
    private void Update()
    {
        elapsedtime += Time.deltaTime;
        for (int i = 0; i < Arrows.Length; ++i)
        {
            Arrows[i].transform.localScale = Vector3.Slerp(smaller, longer, Mathf.Cos(elapsedtime * arrowspeed));
        }
        if (Title)
        {
            Title.transform.localScale = Vector3.Slerp(smaller, higher, Mathf.Cos(elapsedtime * titlespeed));
        }
        right.transform.position = Vector3.Slerp(rightOrigin, rightEnd, Mathf.Cos(elapsedtime * arrowspeed));
        left.transform.position = Vector3.Slerp(leftOrigin, leftEnd, Mathf.Cos(elapsedtime * arrowspeed));
    }
}
