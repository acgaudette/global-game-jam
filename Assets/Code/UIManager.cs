using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Gameplay
{
    public class UIManager : MonoBehaviour
    {
        public Text Money; // cash
        public Text Rent;
        public Text Timer;
        public Text Month;
        public GameObject AcceptButton;
        public GameObject RejectButton;

        public IconShuffle Portrait;
        public Text PortraitName;
        public RawImage PortraitIcon;
        public RawImage HateIcon;
        public Text Value;

        Vector3 timerPosition;

        void Start() {
            timerPosition = Timer.transform.position;
        }

        public void UpdateProposalUI(TenantData proposal)
        {
            PortraitIcon.texture = proposal.trait.Icon;
            HateIcon.texture = proposal.trait.Hate.SpriteIcon;
            Value.text = "$" + proposal.worth.ToString();

            PortraitName.text = proposal.trait.TraitName;
            PortraitName.color = proposal.trait.TraitColor;
            Portrait.ResetTenant();
            Portrait.LoadTenant();
        }

        public void TimeAndScore(float time, uint month, float cash, float rent)
        {
            int seconds = Mathf.FloorToInt(time);
            float remainder = Mathf.Round((time % 1) * 100);
            Timer.text = seconds + ":" + remainder.ToString("00");

            Month.text = "Month " + month.ToString();
            Money.text = "$" + cash.ToString();
            Rent.text = "$" + rent.ToString();

            if (seconds < 5) {
                Timer.color = Color.red;
                Timer.fontSize = 90;

                // Shake
                Vector2 shake = Random.insideUnitCircle * 6;
                Timer.transform.position = timerPosition
                    + new Vector3(shake.x, shake.y, 0);
            } else {
                Timer.color = Color.white;
                Timer.fontSize = 72;
                Timer.transform.position = timerPosition;
            }
        }

        public void Conflict()
        {
            PortraitName.text = "CONFLICTS!";
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
            // Reset timer data
            //Timer.color = Color.white;
            Timer.fontSize = 72;
            Timer.transform.position = timerPosition;

            Timer.text = "GAME OVER";
        }
    }
}
