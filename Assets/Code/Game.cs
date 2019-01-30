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
        bool overlay = true;

        public float pitchRamp;
        public float volume;

        [Header("Assets")]

        public Tenant tenantPrefab;

        [Header("Scene")]

        public Transform spawnPoint;
        public UIManager ui;
        public GameObject overlayObj;

        public createTenantAudio audioM;

        AudioSource audSrc;

        void Awake() {
            timer = timeLimit;
            rent = startingRent;
            cash = startingCash;

            audSrc = GetComponent<AudioSource>();
        }

        void Update() {
            /* Overlay */

            //if (Input.GetKeyDown(KeyCode.Space)) {
            if (Input.GetButtonDown("Fire1")) {
                if (overlay == false) {
                    Restart();
                    overlay = true;
                    overlayObj.SetActive(overlay);
                }
            }

            if (overlay)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ui.switchOverlay();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    overlay = false;
                    overlayObj.SetActive(overlay);
                    ui.story.SetActive(true);
                    ui.instr.SetActive(false);
                    Restart();
                }
                return;
            }

            /* Logic */

            kicked = false;

            // Reset game
            if (Input.GetKeyDown(KeyCode.R)) {
                Restart();
            }

            if (gameOver) {
                ui.Gameover();
                endingAudio.gameOver = true;
                return;
            }

            timer -= Time.deltaTime;

            if (timer < 0.0) {
                if (cash >= rent) {

                    audSrc.PlayOneShot(GetComponent < SoundEffects > ().newMonth);


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
                    audSrc.PlayOneShot(GetComponent < SoundEffects > ().fail);

                }
            }

            if (deciding) {
                // Decision: YES trigger
                if (Input.GetKeyDown(KeyCode.RightArrow)) {

                    audSrc.PlayOneShot(GetComponent<SoundEffects>().tenantYes);
                    // Create new tenant
                    var tenant = GameObject.Instantiate(
                        tenantPrefab,
                        spawnPoint.position,
                        Quaternion.identity,
                        this.transform
                    );

                    // Audio
                    audioM.makeSource(
                        proposal.trait.TraitID,
                        tenant.transform
                    );

                    tenant.data = proposal;
                    //tenant.GetComponent<SpriteRenderer>().color
                        //= proposal.trait.TraitColor;
                    tenants.Add(tenant);
                    tenant.Enter();
                    tenant.Wander();

                    ui.Accept();

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
                        audSrc.PlayOneShot(GetComponent< SoundEffects > ().cash);

                        // Kick conflicts
                        foreach (var kick in kickList) {
                            tenants.Remove(kick);
                            kick.Kick();
                            // Update cash
                            cash += kick.data.worth;
                        }

                        // Punish with larger timer
                        decidingTimer = decidePunishPause;

                        // Event
                        kicked = true;
                    }
                }

                // Decision: NO trigger
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {

                    audSrc.PlayOneShot(GetComponent<SoundEffects>().tenantNo);

                    ui.Reject();
                    deciding = false;
                    Debug.Log("Rejected tenant");
                }
            }

            else {
                decidingTimer -= Time.deltaTime;

                // Decision: initiate
                if (decidingTimer < 0) {
                    proposal = GenerateProposal();
                    ui.UpdateProposalUI(proposal);
                    decidingTimer = decidePause;

                    deciding = true;
                    Debug.Log("New proposal");
                }
            }

            /* UI */
            ui.TimeAndScore(timer, month, cash, rent);
        }

        // Procedurally-generate new tenant proposal
        TenantData GenerateProposal() {
            //audSrc.pitch = Random.Range(.9f, 1.1f);
            audSrc.PlayOneShot(GetComponent<SoundEffects>().doorBell, volume);

            audSrc.pitch = 1f + pitchRamp * tenants.Count;


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

            TenantData proposal = new TenantData(worth, data);
            ui.UpdateProposalUI(proposal);
            return proposal;
        }

        void Restart() {
            // Reset variables
            timer = timeLimit;
            month = 1;
            cash = startingCash;
            rent = startingRent;
            proposal = GenerateProposal();
            gameOver = false;
            endingAudio.gameOver = false;

            // Clear tenants
            foreach (var tenant in tenants) {
                tenant.Kick();
            }

            tenants.Clear();
            ui.gover.SetActive(false);

            Debug.Log("Restarted game");
        }
    }
}
