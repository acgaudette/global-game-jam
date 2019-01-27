using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconShuffle : MonoBehaviour
{

    public Transform leftNode;
    public Transform middleNode;
    public Transform rightNode;
    public bool load;
    public bool accept;
    public bool reject;
    public bool reset;
    public GameObject bubbles;
    public float appearAt;

    private void Start()
    {
        gameObject.transform.position = leftNode.position;
        bubbles.SetActive(false);
    }

    private void Update()
    {
        if (load)
        {
            LoadTenant();
        }
        if (accept)
        {
            AcceptTenant();
        }
        if (reject)
        {
            RejectTenant();
        }
        if (reset)
        {
            ResetTenant();
        }

    }

    public void LoadTenant()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, middleNode.position, 8 * Time.deltaTime);
        if (gameObject.transform.localPosition.x >= appearAt)
        {
            bubbles.SetActive(true);
        }
    }

    public void AcceptTenant()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, rightNode.position, 6.5f * Time.deltaTime);
        bubbles.SetActive(false);
    }

    public void RejectTenant()
    {
        load = false;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, leftNode.position, 6.5f * Time.deltaTime);
        bubbles.SetActive(false);
    }

    public void ResetTenant ()
    {
        load = false;
        accept = false;
        reject = false;
        gameObject.transform.position = leftNode.position;
        reset = false;
        bubbles.SetActive(false);
    }
}

