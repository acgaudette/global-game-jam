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
    public GameObject MoneyBubble;
    public GameObject HostilityBubble;
    public float appearAt;
    public float loadspeed;
    public float rejectspeed;
    public float acceptspeed;

    private void Start()
    {
        gameObject.transform.position = leftNode.position;
        MoneyBubble.transform.localScale = Vector3.zero;
        HostilityBubble.transform.localScale = Vector3.zero;
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
            MoneyBubble.transform.localScale = Vector3.Slerp (MoneyBubble.transform.localScale, new Vector3 (1.2744f, 1.08324f, 1.2744f), 20 * Time.deltaTime);
            HostilityBubble.transform.localScale = Vector3.Slerp(HostilityBubble.transform.localScale, new Vector3 (0.95757f, -0.95757f, 0.95757f), 20 * Time.deltaTime);

        }
    }

    public void AcceptTenant()
    {
        state = PortraitState.accept;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, rightNode.position, acceptspeed * Time.deltaTime);
        MoneyBubble.transform.localScale = Vector3.Slerp(MoneyBubble.transform.localScale, Vector3.zero, 100 * Time.deltaTime);
        HostilityBubble.transform.localScale = Vector3.Slerp(HostilityBubble.transform.localScale, Vector3.zero, 100 * Time.deltaTime);
    }

    public void RejectTenant()
    {
        state = PortraitState.reject;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, leftNode.position, rejectspeed * Time.deltaTime);
        MoneyBubble.transform.localScale = Vector3.Slerp(MoneyBubble.transform.localScale, Vector3.zero, 100 * Time.deltaTime);
        HostilityBubble.transform.localScale = Vector3.Slerp(HostilityBubble.transform.localScale, Vector3.zero, 100 * Time.deltaTime);
    }

    public void ResetTenant ()
    {
        state = PortraitState.reset;
        gameObject.transform.position = leftNode.position;
        MoneyBubble.transform.localScale = Vector3.zero;
        HostilityBubble.transform.localScale = Vector3.zero;
    }
}

