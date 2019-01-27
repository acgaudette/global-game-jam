using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconShuffle : MonoBehaviour
{

    public Transform leftNode;
    public Transform middleNode;
    public Transform rightNode;
    public enum PortraitState
    {
        load, accept, reject, reset
    };
    PortraitState state;
    public GameObject bubbles;
    public float appearAt;
    public float loadspeed;
    public float rejectspeed;
    public float acceptspeed;

    private void Start()
    {
        gameObject.transform.position = leftNode.position;
        bubbles.SetActive(false);
    }

    private void Update()
    {
        switch (state)
        {
            case PortraitState.load:
                LoadTenant();
                break;
            case PortraitState.accept:
                AcceptTenant();
                break;
            case PortraitState.reject:
                RejectTenant();
                break;
            default:
                break;
        }
    }

    public void LoadTenant()
    {
        state = PortraitState.load;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, middleNode.position, loadspeed * Time.deltaTime);
        if (gameObject.transform.localPosition.x >= appearAt)
        {
            bubbles.SetActive(true);
        }
    }

    public void AcceptTenant()
    {
        state = PortraitState.accept;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, rightNode.position, acceptspeed * Time.deltaTime);
        bubbles.SetActive(false);
    }

    public void RejectTenant()
    {
        state = PortraitState.reject;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, leftNode.position, rejectspeed * Time.deltaTime);
        bubbles.SetActive(false);
    }

    public void ResetTenant ()
    {
        state = PortraitState.reset;
        gameObject.transform.position = leftNode.position;
        bubbles.SetActive(false);
    }
}

