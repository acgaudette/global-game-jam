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
        GameObject house;

        public float wanderRange;
        public float waitTime;
        public float speed;

        bool isInHouse;

        void Awake()
        {
            movementQueue = new List<Vector3>();
            entry = GameObject.FindGameObjectWithTag("Entry");
            exit = GameObject.FindGameObjectWithTag("Exit");
            house = GameObject.FindGameObjectWithTag("Floor");
        }

        public void Wander()
        {
            StartCoroutine("RandomMovement");
        }

        // Kick the tenant out of the house
        public void Kick()
        {
            // TODO: animate and then destroy
            Destroy(gameObject);
        }

        public void Enter()
        {
            movementQueue.Add(entry.transform.position);
            isInHouse = true;
        }

        public void Exit()
        {
            movementQueue.Clear();
            movementQueue.Add(entry.transform.position);
            movementQueue.Add(exit.transform.position);
        }

        IEnumerator RandomMovement()
        {
            while (isInHouse)
            {
                //Might want to offest by height of character, location at feet;
                Vector3 target = new Vector3(house.transform.position.x + Random.Range(-house.transform.localScale.x, house.transform.localScale.x) / 2, house.transform.position.y + Random.Range(-house.transform.localScale.y, house.transform.localScale.y) / 2);
                //Debug.Log(target);
                Vector3 destination = target;

                float d = Random.Range(1, wanderRange);
                if (Vector3.Distance(target, transform.position) > d)
                {
                    destination = transform.position + (target - transform.position).normalized * d;
                }
                movementQueue.Add(destination);
                yield return new WaitForSeconds(Random.Range(1, waitTime));
            }
        }
        void Update()
        {
            if (movementQueue.Count == 0)
            {
                return;
            }
            //Remove destination if arrived
            else if (Vector3.Distance(transform.position, movementQueue[0]) < 0.01f)
            {

                movementQueue.RemoveAt(0);
            }
            //Movement
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, movementQueue[0], speed * Time.deltaTime);
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
