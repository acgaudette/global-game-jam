using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Money;
    public Text Rent;
    public Text Timer;
    public Text Month;
    public GameObject Accept;
    public GameObject Reject;

    public IconShuffle Portrait;
    public Text PortraitName;
    public RawImage PortraitIcon;
    public RawImage HateIcon;
    public Text Value;

    void UpdateProposalUI(TenantData proposal)
    {
        PortraitIcon.texture = proposal.trait.Icon;
        HateIcon.texture = proposal.trait.Hate.Icon;
        Value.text = proposal.worth.ToString();

        PortraitName.text = proposal.trait.TraitName;
        PortraitName.color = proposal.trait.TraitColor;
        Portrait.ResetTenant();
        Portrait.LoadTenant();
    }

    public void TimeAndScore(float time, int month, int cash, int rent)
    {
        int seconds = Mathf.FloorToInt(time);
        float remainder = Mathf.Round((time % 1) * 100);
        Timer.text = seconds + ":" + remainder.ToString("00");

        //switch to text later;
        Month.text = "Month: " + month.ToString();

        Money.text = cash.ToString();

        Rent.text = rent.ToString();
    }

    public void Conflict()
    {
        proposalText.text = "CONFLICTS!";
        //could change portrait.
    }

    public void Accept()
    {
        PortraitName.text = "ACCEPTED!";
        //Color?
        Portrait.AcceptTenant();

    }

    public void Reject()
    {
        PortraitName.text = "REJECTED!";
        Portrait.RejectTenant();
    }

    public void Gameover()
    {
        timerText.text = "GAME OVER";
    }
}
