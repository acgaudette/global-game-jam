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
    float elapsedtime;
    public float animationSpeed;
    Vector3 moneyScale;
    Vector3 hostilityScale;

    private void Start()
    {
        gameObject.transform.position = leftNode.position;
        moneyScale = MoneyBubble.transform.localScale;
        hostilityScale = HostilityBubble.transform.localScale;
        MoneyBubble.transform.localScale = Vector3.zero;
        HostilityBubble.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        switch (state)
        {
            case PortraitState.load:
                elapsedtime += loadspeed * Time.deltaTime;
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, middleNode.position, elapsedtime);
                    MoneyBubble.transform.localScale = Vector3.Slerp(Vector3.zero, moneyScale, elapsedtime);
                    HostilityBubble.transform.localScale = Vector3.Slerp(Vector3.zero, hostilityScale, elapsedtime);
                break;
            case PortraitState.accept:
                elapsedtime += acceptspeed * Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, rightNode.position, elapsedtime);
                MoneyBubble.transform.localScale = Vector3.Slerp(moneyScale, Vector3.zero, 5 * elapsedtime);
                HostilityBubble.transform.localScale = Vector3.Slerp(hostilityScale, Vector3.zero, 5 * elapsedtime);
                break;
            case PortraitState.reject:
                elapsedtime += rejectspeed * Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, leftNode.position, elapsedtime);
                MoneyBubble.transform.localScale = Vector3.Slerp(moneyScale, Vector3.zero, elapsedtime);
                HostilityBubble.transform.localScale = Vector3.Slerp(hostilityScale, Vector3.zero, elapsedtime);
                break;
            default:
                break;
        }
    }

    public void LoadTenant()
    {
        state = PortraitState.load;
        elapsedtime = 0.0f;
    }

    public void AcceptTenant()
    {
        state = PortraitState.accept;
        elapsedtime = 0.0f;   
    }

    public void RejectTenant()
    {
        state = PortraitState.reject;
        elapsedtime = 0.0f;
    }

    public void ResetTenant ()
    {
        state = PortraitState.reset;
        gameObject.transform.position = leftNode.position;
        MoneyBubble.transform.localScale = Vector3.zero;
        HostilityBubble.transform.localScale = Vector3.zero;
    }
}

