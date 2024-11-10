using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementScript : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float rotationSpeed = 30F;
    public bool slimeIsOnGround = true;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    public float minWaitTime = 2f;
    public float maxWaitTime = 6f;

    public float detectionRange = 10f;  // Detection range for the target
    public float approachSpeed = 7f;    // Speed when moving towards target
    public GameObject gemPrefab;

    private Transform target; //closets detected target
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(JumpRandom());
    }

    // Update is called once per frame
    void Update()
    {
        //check if there is a target within range
        FindClosestTargetWithTag();

        if (target != null)
        {
            MoveTowardsTarget();

        }
        else
        {
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }

            if (isRotatingLeft == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
            }

            if (isRotatingRight == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
            }

            if (isWalking == true)
            {
                rb.AddForce(transform.forward * movementSpeed);

            }
        }


    }

    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int rotateDirection = Random.Range(1, 3);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 3);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);

        isWalking = true;

        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        yield return new WaitForSeconds(rotateWait);

        if (rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }

        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }

    IEnumerator JumpRandom()
    {
        while (true) // Loop indefinitely
        {
            // Generate a random wait time
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            Jump();
        }
    }
    void Jump()
    {
        if (isWalking == true && slimeIsOnGround)
        {
            rb.AddForce(new Vector3(0, 75, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            slimeIsOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            slimeIsOnGround = false;
        }
    }

    void MoveTowardsTarget()
    {
        // Calculate direction toward the target,
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        directionToTarget.y = 0;

        // Rotate towards the target
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the target
        rb.AddForce(transform.forward * approachSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Carrot")
        {
            Destroy(collision.gameObject);
            Instantiate(gemPrefab, transform.position, Quaternion.identity);

        }
    }

    void FindClosestTargetWithTag()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        float closestDistance = detectionRange;
        target = null;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Carrot"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = hit.transform;
                }
            }
        }
    }
}
