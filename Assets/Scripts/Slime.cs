using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Slime : MonoBehaviour
{
    public float minWaitTime = 10.0f;
    public float maxWaitTime = 60.0f;
    public GameObject gemPrefab;

    public float detectionRange = 10f;
    public float speed = 5f;
    private Transform carrotPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
       // if (carrotPosition == null)
        //{
            //check for target within range
          //  Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);

            //foreach (Collider hit in hits)
            //{
              //  if (hit.CompareTag("Carrot"))
                //{
                  //  carrotPosition = hit.transform;
                    //break; // Stop after finding the first target with the specified tag
            //    }
          //  }

        //}

        //Move towards the carrot if one is found
        //if (carrotPosition != null)
        //{
            //Move towards the carrot if one is found
          //  transform.position = Vector3.MoveTowards(transform.position, carrotPosition.position, speed * Time.deltaTime);

        //}

    }

    //private void OnCollisionEnter(Collision collision)
    //{
      //  if (collision.gameObject.tag == "Carrot")
        //{
          //  Destroy(collision.gameObject);
            //Drop();
            //carrotPosition = null;
            
        //}
    //}
    

    void Drop()
    {
       Instantiate(gemPrefab, transform.position, Quaternion.identity);

    }
}