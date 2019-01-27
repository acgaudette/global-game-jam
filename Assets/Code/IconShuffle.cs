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
    public bool dismiss;
    public bool reset;

    private void Start()
    {
        gameObject.transform.position = leftNode.position;
    }

    private void Update()
    {
        if (load)
        {
            LoadTenant();
        }
        if (dismiss)
        {
            DismissTenant();
        }
        if (reset)
        {
            load = false;
            dismiss = false;
            ResetTenant();
            reset = false;
        }
    }

    public void LoadTenant()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, middleNode.position, 8 * Time.deltaTime);
    }

    public void DismissTenant()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, rightNode.position, 5 * Time.deltaTime);

    }

    public void ResetTenant ()
    {
        gameObject.transform.position = leftNode.position;
    }
}

