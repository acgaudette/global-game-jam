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

        public bool deciding = false;
        public float decidePause = 0.25f;
        public float decidePunishPause = 1;
        public float decidingTimer;
        public TenantData proposal;

        public uint startingRent = 10000;
        public float rent;

        public Text scoreText;
        public Text timerText;
        public Text proposalText;

        void Awake() {
            timer = timeLimit;
            rent = startingRent;
            proposalText.text = "";
        }

        void Update() {
            /* Logic */

            timer -= Time.deltaTime;
            rent = tenants.Count > 0 ?
                startingRent / tenants.Count : startingRent;

            // Game over
            if (timer < 0.0) {
                timerText.text = "GAME OVER";
                return;
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
                    tenants.Add(tenant);
                    tenant.Wander();

                    // UI
                    proposalText.text = "ACCEPT!";

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
                        kickList.Add(tenant);
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
                    proposalText.text = "Proposal:\n"
                        + proposal.ToString();

                    deciding = true;
                    Debug.Log("New proposal");
                }
            }

            /* UI */

            scoreText.text = "$" + rent.ToString("N0")
                + "/mo.";

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

            TenantData proposal = new TenantData(randomTrait);
            return proposal;
        }
    }
}
