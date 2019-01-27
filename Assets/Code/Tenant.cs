using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace Gameplay
{
    // Class for actual ingame instance of a tenant
    public class Tenant : MonoBehaviour
    {
        public TenantData data;

        List<Vector2> movementQueue;
        //Fixed locations
        GameObject entry;

        public float wanderRange;
        public float waitTime;
        public float speed;
        public float exitSpeed;
        public float targetReachRange = 0.4f;

        //Floor bounds
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
        public float minZ;
        public float maxZ;

        bool isInHouse;

        void Awake()
        {
            movementQueue = new List<Vector2>();
            entry = GameObject.FindGameObjectWithTag("Entry");
            isInHouse = true;
        }

        public void Wander()
        {
            StartCoroutine("RandomMovement");
        }

        // Kick the tenant out of the house
        public void Kick()
        {
            movementQueue.Clear();
            movementQueue.Add(entry.transform.position);
            isInHouse = false;
        }

        public void Enter()
        {
            movementQueue.Add(entry.transform.position);
            isInHouse = true;
        }

        IEnumerator RandomMovement()
        {
            while (isInHouse)
            {
                //Might want to offest by height of character, location at feet;
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);

                Vector2 target = new Vector3(x, y + transform.localScale.y / 2);
                float d = Random.Range(1, wanderRange);
                Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
                if (Vector2.Distance(target, position2D) > d) {
                    target = position2D + (target - position2D).normalized * d;
                }

                movementQueue.Add(target);
                yield return new WaitForSeconds(Random.Range(1, waitTime));
            }
        }
        void Update()
        {
            if (movementQueue.Count > 0)
            {
                //Remove destination if arrived
                Vector2 position2D = new Vector2(
                    transform.position.x,
                    transform.position.y
                );

                float dist = Vector2.Distance(position2D, movementQueue[0]);
                if (dist < targetReachRange) {
                    movementQueue.RemoveAt(0);
                }
                //Movement
                else {
                    float s = 0;
                    if (isInHouse)
                    {
                        s = speed;
                    }
                    else
                    {
                        s = exitSpeed;
                    }

                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        movementQueue[0],
                        s * Time.deltaTime
                    );
                }
            }
            else
            {
                if (!isInHouse)
                {
                    Destroy(gameObject);
                }
            }

            // Z computation
            float yPct = (transform.position.y - minY) / (maxY - minY);
            float z = yPct * (maxZ - minZ) + minZ;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                z
            );


            // Debug label
            var label = transform.GetChild(0).GetComponent<TextMesh>();
            label.text = "$" + data.worth;
        }
    }

    [System.Serializable]
    public class TenantData
    {
        public float worth;
        public Trait trait;
        public TenantData(float w, Trait data)
        {
            worth = w;
            trait = data;
        }
    }
}
