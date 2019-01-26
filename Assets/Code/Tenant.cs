using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace Gameplay
{
    // Class for actual ingame instance of a tenant
    public class Tenant : MonoBehaviour
    {
        public TenantData data;

        List<Vector3> movementQueue;
        //Fixed locations
        GameObject entry;
        GameObject exit;

        public float wanderRange;
        public float waitTime;
        public float speed;

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
            movementQueue = new List<Vector3>();
            entry = GameObject.FindGameObjectWithTag("Entry");
            exit = GameObject.FindGameObjectWithTag("Exit");
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
            movementQueue.Add(exit.transform.position);
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
                float z = y / (maxY - minY) * (maxZ - minZ);
                Vector3 target = new Vector3(x, y + transform.localScale.y / 2, z);
                Vector3 destination = target;
                float d = Random.Range(1, wanderRange);
                if (Vector3.Distance(target, transform.position) > d)
                {
                    destination = transform.position + (target - transform.position).normalized * d;
                }
                Debug.Log(destination);
                movementQueue.Add(destination);
                yield return new WaitForSeconds(Random.Range(1, waitTime));
            }
        }
        void Update()
        {
            if (movementQueue.Count > 0)
            {   
            //Remove destination if arrived
                if (Vector3.Distance(transform.position, movementQueue[0]) < 0.01f)
                { 
                    movementQueue.RemoveAt(0);
                }
                //Movement
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, movementQueue[0], speed * Time.deltaTime);
                }
            }
            else
            {
                if (!isInHouse)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Class for tenant data, e.g. for not-yet-rendered tenant proposals
    [System.Serializable]
    public class TenantData
    {
        // Note: class is hardcoded for only a single trait
        public List<Trait> traits;

        public TenantData(Trait trait)
        {
            traits = new List<Trait>();
            traits.Add(trait);
        }

        public bool Conflicts(TenantData other)
        {
            return traits[0].data.title == other.traits[0].data.title
                && traits[0].like != other.traits[0].like;
        }

        public override string ToString()
        {
            var trait = traits[0];
            return (trait.like ? "Likes" : "Hates")
                + " " + trait.data.title;
        }
    }
}
