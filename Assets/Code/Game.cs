using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Gameplay {
    public class Game: MonoBehaviour {
        public Tenant tenantPrefab;
        public Transform spawnPoint;
        public List<TraitData> traitPool;
        public List<Tenant> tenants;

        public float timeLimit = 60;
        public float timer;
        public uint month = 1;
        public bool gameOver = false;

        public bool deciding = false;
        public float decidePause = 0.25f;
        public float decidePunishPause = 1;
        public float decidingTimer;
        public TenantData proposal;

        public uint startingRent = 10000;
        public uint startingCash = 10000;
        public float rentIncreaseFactor = 1.5f;
        public float rent;
        public float cash;

        public Text scoreText;
        public Text timerText;
        public Text proposalText;
        public Text decisionText;
        public Text extraText;

        void Awake() {
            timer = timeLimit;
            rent = startingRent;
            cash = startingCash;
            proposalText.text = "";
            decisionText.text = "";
        }

        void Update() {
            /* Logic */

            timer -= Time.deltaTime;

            if (tenants.Count > 0) {
                var sum = 0f;

                foreach (var tenant in tenants) {
                    sum += tenant.data.valueFactor;
                }

                rent = startingRent / sum;
            } else {
                rent = startingRent;
            }

            if (timer < 0.0) {
                ++month;
                //rent = startingRent * (rentIncreaseFactor * month);
                cash = startingCash * Mathf.Pow(0.9f, month);

                if (cash >= rent) {
                    Debug.Log("Rent paid");
                }

                // Game over
                else {
                    timerText.text = "GAME OVER";
                    Debug.Log("Game over");
                    gameOver = true;
                }

                timer = timeLimit;
            }

            if (deciding) {
                // Decision: YES trigger
                if (Input.GetKeyDown(KeyCode.Return)) {
                    // Create new tenant
                    var tenant = GameObject.Instantiate(
                        tenantPrefab,
                        spawnPoint.position,
                        Quaternion.identity,
                        this.transform
                    );

                    tenant.data = proposal;
                    tenant.GetComponent<SpriteRenderer>().color
                        = tenant.data.traits[0].data.debugColor;
                    tenants.Add(tenant);
                    tenant.Enter();
                    tenant.Wander();

                    // UI
                    proposalText.text = "ACCEPT!";
                    decisionText.text = "";
                    extraText.text = "";

                    deciding = false;
                    Debug.Log("Accepted tenant");

                    /* Handle mismatch */

                    var kickList = new List<Tenant>();

                    // Search for conflicts with new tenant
                    foreach (var resident in tenants) {
                        if (tenant.data.Conflicts(resident.data)) {
                            kickList.Add(resident);
                        }
                    }

                    // There was a conflict
                    if (kickList.Count > 0) {
                        // Don't kick new tenant; keep them
                        //kickList.Add(tenant);

                        Debug.Log(
                            "Conflict with " + kickList.Count + " residents!"
                        );

                        // Kick conflicts
                        foreach (var kick in kickList) {
                            tenants.Remove(kick);
                            kick.Kick();
                        }

                        // Punish with larger timer
                        decidingTimer = decidePunishPause;

                        // UI
                        proposalText.text = "CONFLICTS!";
                    }
                }

                // Decision: NO trigger
                if (Input.GetKeyDown(KeyCode.Backspace)) {
                    // UI
                    proposalText.text = "REJECT!";
                    decisionText.text = "";
                    extraText.text = "";

                    deciding = false;
                    Debug.Log("Rejected tenant");
                }
            }

            else {
                decidingTimer -= Time.deltaTime;

                // Decision: initiate
                if (decidingTimer < 0) {
                    proposal = GenerateProposal();
                    decidingTimer = decidePause;

                    // UI
                    proposalText.text = "Proposal:";
                    decisionText.text = proposal.ToString();
                    decisionText.color = proposal.traits[0].data.debugColor;
                    extraText.text = "(Hates "
                        + proposal.traits[0].data.hates + ")";
                    extraText.color = traitPool.Find(
                        x => x.title == proposal.traits[0].data.hates
                    ).debugColor;

                    deciding = true;
                    Debug.Log("New proposal");
                }
            }

            /* UI */

            scoreText.text = ""
                + "Month " + month + ": $" + cash.ToString("N0")
                + "\n"
                + "$" + rent.ToString("N0") + "/mo.";

            int seconds = Mathf.FloorToInt(timer);
            float remainder = Mathf.Round((timer % 1) * 100);
            timerText.text = seconds + ":" + remainder.ToString("00");
        }

        // Procedurally-generate new tenant proposal
        TenantData GenerateProposal() {
            int randomIndex = Random.Range(0, traitPool.Count);
            TraitData data = traitPool[randomIndex];
            bool like = Random.value > 0.5;
            Trait randomTrait = new Trait(data, like);
            float factor = Random.Range(1, 4);

            TenantData proposal = new TenantData(randomTrait, factor);
            return proposal;
        }
    }
}
