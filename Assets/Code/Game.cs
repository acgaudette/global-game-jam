using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Gameplay {
    public class Game: MonoBehaviour {
        public List<Trait> traitPool;
        public float timeLimit = 25;
        public float decidePause = .25f;
        public float decidePunishPause = .75f;
        public uint startingRent = 200;
        public float rentIncreaseFactor = .8f;
        public uint startingCash = 0;

        // Random generation
        public uint startingCap = 2;
        public uint[] worths;

        [Header("Read-only")]

        public List<Tenant> tenants;
        public float timer;
        public uint month = 1;
        public bool gameOver = false;
        public bool deciding = false;
        public float decidingTimer;
        public TenantData proposal;
        public float rent;
        public float cash;
        public bool kicked = false;

        [Header("Assets")]

        public Tenant tenantPrefab;

        [Header("Scene")]

        public Transform spawnPoint;

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
            extraText.text = "";
        }

        void Update() {
            /* Logic */

            kicked = false;

            // Reset game
            if (Input.GetKeyDown(KeyCode.R)) {
                // Reset variables
                timer = timeLimit;
                month = 1;
                cash = startingCash;
                rent = startingRent;
                proposal = GenerateProposal();
                UpdateProposalUI(proposal);
                gameOver = false;

                // Clear tenants
                foreach (var tenant in tenants) {
                    tenant.Kick();
                }

                tenants.Clear();

                Debug.Log("Restarted game");
            }

            if (gameOver) {
                timerText.text = "GAME OVER";
                return;
            }

            timer -= Time.deltaTime;

            if (timer < 0.0) {
                if (cash >= rent) {
                    Debug.Log("Rent paid");

                    ++month;

                    // Update resources at the end of the month
                    rent = startingRent * (rentIncreaseFactor * month);
                    cash = Mathf.Max(0, cash - rent);

                    timer = timeLimit;
                }

                // Game over
                else {
                    Debug.Log("Game over");
                    gameOver = true;
                }
            }

            if (deciding) {
                // Decision: YES trigger
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    // Create new tenant
                    var tenant = GameObject.Instantiate(
                        tenantPrefab,
                        spawnPoint.position,
                        Quaternion.identity,
                        this.transform
                    );

                    tenant.data = proposal;
                    tenant.GetComponent<SpriteRenderer>().color
                        = proposal.trait.TraitColor;
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
                        if (tenant.data.trait.Conflicts(resident.data.trait)) {
                            kickList.Add(resident);
                        }
                    }

                    // There was a conflict;
                    // keep the new tenant but kick the rest
                    if (kickList.Count > 0) {
                        Debug.Log(
                            "Conflict with " + kickList.Count + " residents!"
                        );

                        // Kick conflicts
                        foreach (var kick in kickList) {
                            tenants.Remove(kick);
                            kick.Kick();
                            // Update cash
                            cash += kick.data.worth;
                        }

                        // Punish with larger timer
                        decidingTimer = decidePunishPause;

                        // UI
                        proposalText.text = "CONFLICTS!";

                        // Event
                        kicked = true;
                    }
                }

                // Decision: NO trigger
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
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
                    UpdateProposalUI(proposal);
                    decidingTimer = decidePause;

                    deciding = true;
                    Debug.Log("New proposal");
                }
            }

            /* UI */

            scoreText.text = ""
                + "Month " + month + ": $" + rent.ToString("N0")
                + "\n"
                + "$" + cash.ToString("N0");

            int seconds = Mathf.FloorToInt(timer);
            float remainder = Mathf.Round((timer % 1) * 100);
            timerText.text = seconds + ":" + remainder.ToString("00");
        }

        // Procedurally-generate new tenant proposal
        TenantData GenerateProposal() {
            int limit = Mathf.Min(traitPool.Count, (int)(startingCap * month));
            int randomIndex = Random.Range(0, limit);
            Trait data = traitPool[randomIndex];
            uint count = 0;
            foreach (var tenant in tenants) {
                if (tenant.data.trait.TraitID == data.TraitID) {
                    ++count;
                }
            }
            int index = Mathf.Min((int)count, worths.Length - 1);
            uint worth = worths[index];

            Debug.Log(worth);
            TenantData proposal = new TenantData(worth, data);
            return proposal;
        }

        void UpdateProposalUI(TenantData proposal) {
            proposalText.text = "Proposal:";
            decisionText.text = proposal.trait.TraitName;
            decisionText.color = proposal.trait.TraitColor;
            extraText.text = proposal.trait.HateSpeech;
            extraText.color = proposal.trait.Hate.TraitColor;
        }
    }
}
