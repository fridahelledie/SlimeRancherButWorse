using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : MonoBehaviour
{
    public int gemEaten = 0;

    //for the explosion
    public int amount = 2;
    public GameObject explosionPrefab;

 
    public float force = 300f;
    public float radius = 2f;
    public float destroyDelay = 5f;

    public float upForce = 1f;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            Destroy(collision.gameObject);
            gemEaten++;

        }
    }

    private void Update()
    {
        if (gemEaten >= 1)
        {
            
            for (int i = 0; i < amount; i++)
            {
                GameObject spawnedPrefab = Instantiate(explosionPrefab, transform.position, transform.rotation);
                
                Rigidbody rb =  spawnedPrefab.GetComponent<Rigidbody>();

                //apply a random force 
                Vector3 randomDirection = Random.onUnitSphere;

                Vector3 combinedForce = (randomDirection * force) + (Vector3.up * upForce);
                rb.AddForce(combinedForce, ForceMode.Impulse);


                spawnedPrefabs.Add(spawnedPrefab);
            }
            
            gemEaten = 0;
            
            StartCoroutine(DestroyPrefabsAfterDelay());

            foreach (Transform child in transform) 
            { 
                child.gameObject.SetActive(false);
            }


        }


    }

    private IEnumerator DestroyPrefabsAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        foreach (GameObject prefab in spawnedPrefabs)
            { 
            Destroy(prefab); 
        }
        Destroy(gameObject);
    }

}


