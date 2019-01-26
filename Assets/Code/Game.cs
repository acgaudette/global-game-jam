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
        public float decidePause = 0.5f;
        public float decidingTimer;
        public TenantData proposal;

        public Text scoreText;
        public Text timerText;

        void Awake() {
            timer = timeLimit;
        }

        void Update() {
            /* Logic */

            timer -= Time.deltaTime;

            if (deciding) {
                // Decision: YES trigger
                if (Input.GetKeyDown(KeyCode.Return)) {
                    var tenant = GameObject.Instantiate(
                        tenantPrefab,
                        spawnPoint.position,
                        Quaternion.identity,
                        this.transform
                    );

                    tenant.data = proposal;
                    tenants.Add(tenant);

                    deciding = false;
                    Debug.Log("Accepted tenant");
                }

                // Decision: NO trigger
                if (Input.GetKeyDown(KeyCode.Backspace)) {
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
                    deciding = true;
                }
            }

            /* UI */

            scoreText.text = "Tenants: " + tenants.Count.ToString();

            int seconds = Mathf.FloorToInt(timer);
            float remainder = Mathf.Round((timer % 1) * 100);
            timerText.text = seconds + ":" + remainder;
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
