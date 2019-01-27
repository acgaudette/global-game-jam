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
    public float loadTime;
    public float rejectTime;
    public float acceptTime;
    public float IconDisappearTime;
    public float IconAppearTime;
    float elapsedtime;
    float animFraction;
    Vector3 moneyScale;
    Vector3 hostilityScale;
    Vector3 startHostileAngle;
    Vector3 startMoneyAngle;
    Vector3 endAngle;

    private void Start()
    {
        gameObject.transform.position = leftNode.position;
        moneyScale = MoneyBubble.transform.localScale;
        hostilityScale = HostilityBubble.transform.localScale;
        MoneyBubble.transform.localScale = Vector3.zero;
        HostilityBubble.transform.localScale = Vector3.zero;
        startMoneyAngle = new Vector3(0.0f, 0.0f, 90f);
        startHostileAngle = new Vector3(0.0f, 0.0f, -90.0f);
        endAngle = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void Update()
    {
        switch (state)
        {
            case PortraitState.load:
                elapsedtime += Time.deltaTime;
                animFraction = elapsedtime / loadTime;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, middleNode.position, animFraction);
                animFraction = elapsedtime / IconAppearTime;
                MoneyBubble.transform.localScale = Vector3.Slerp(Vector3.zero, moneyScale, animFraction);
                MoneyBubble.transform.localEulerAngles = Vector3.Slerp(startMoneyAngle, endAngle, animFraction);
                HostilityBubble.transform.localScale = Vector3.Slerp(Vector3.zero, hostilityScale, animFraction);
                HostilityBubble.transform.localEulerAngles = Vector3.Slerp(startHostileAngle, endAngle, animFraction);
                break;
            case PortraitState.accept:
                elapsedtime += Time.deltaTime;
                animFraction = elapsedtime / acceptTime;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, rightNode.position, animFraction);
                animFraction = elapsedtime / IconDisappearTime;
                MoneyBubble.transform.localScale = Vector3.Slerp(moneyScale, Vector3.zero, animFraction);
                HostilityBubble.transform.localScale = Vector3.Slerp(hostilityScale, Vector3.zero, animFraction);

                break;
            case PortraitState.reject:
                elapsedtime += Time.deltaTime;
                animFraction = elapsedtime / rejectTime;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, leftNode.position, animFraction);
                animFraction = elapsedtime / IconDisappearTime;
                MoneyBubble.transform.localScale = Vector3.Slerp(moneyScale, Vector3.zero, animFraction);
                HostilityBubble.transform.localScale = Vector3.Slerp(hostilityScale, Vector3.zero, animFraction);
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

