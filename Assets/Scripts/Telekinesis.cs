using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Telekinis : MonoBehaviour
{
    public enum State
    {
        Idle,
        Pushing,
        Pulling
    }
    private State state = State.Idle;
    [Header("References")]
    public Transform baseTrans;
    public Camera cam;
    [Header("Stats")]
    [Tooltip("Force applied when pulling a target.")]
    public float pullForce = 60;
    [Tooltip("Force applied when pushing a target.")]
    public float pushForce = 60;
    [Tooltip("Maximum distance from the player that a telekinesis target can be.")]
    public float range = 70;
    [Tooltip("Layer mask for objects that can be pulled and pushed.")]
    public LayerMask detectionLayerMask;
    //Current target of telekinesis, if any.
    private Transform target;
    //The world position that the target detection ray hit on the current target.
    private Vector3 targetHitPoint;
    //Rigidbody component of target. For something to be marked as a target, it must have a Rigidbody.
    //So as long as 'target' is not null, this won't be null either.
    private Rigidbody targetRigidbody;
    //If there is no current target, this is always false. Otherwise, true if the target is in range, false if they are not.
    private bool targetIsOutsideRange = false;
    //Gets the Color that the cursor should display based on the state and target distance.

    public ParticleSystem CatchParticlesystem;
    public ParticleSystem ReleaseParticleSystem;
    private Color CursorColor
    {
        get
        {
            if (state == State.Idle)
            {
                //If there is no target, return gray:
                if (target == null)
                    return Color.gray;
                //If there is a target but it's not in range, return orange:
                else if (targetIsOutsideRange)
                    return new Color(1, .6f, 0);
                //If there is a target and it is in range, return red:
                else
                    return Color.red;
            }
            //If we're pushing or pulling, return green:
            else
                return Color.green;
        }
    }
    private void Start()
    {
        CatchParticlesystem.Stop();
        ReleaseParticleSystem.Stop();
    }

    void ClearTarget()
    {
        //Clear and reset variables that relate to targeting
        target = null;
        targetRigidbody = null;
        targetIsOutsideRange = false;
    }

    //Update logic:
    void TargetDetection()
    {
        //get a ray going out of the center of the screen
        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //cast the ray using detectionLayerMask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, detectionLayerMask.value))
        {
            if (hit.rigidbody != null && !hit.rigidbody.isKinematic) //and it has a non-kinematic rigidbody
            {
                //Set the telekinesis target:
                target = hit.transform;
                targetRigidbody = hit.rigidbody;
                targetHitPoint = hit.point;

                //Based on distance, set targetIsOutsideRange:
                if (Vector3.Distance(baseTrans.position, hit.point) > range)
                {
                    targetIsOutsideRange = true;
                }
                   
                else
                    targetIsOutsideRange = false;
            }

            //if the thing the ray his has no rigidbody
            else
            {
                ClearTarget();
            }
        }
        else //if the ray do not hit anythin
        {
            ClearTarget();
        }
    }
    //FixedUpdate logic:
    void PullingAndPushing()
    {
        //if we have a target that is within range
        if (target != null && !targetIsOutsideRange)
        {
            //if the left mouse button is down
            if (Input.GetMouseButton(0))
            {
                //Pull the target from the hit point towards our position:
                targetRigidbody.AddForce((baseTrans.position - targetHitPoint). 
                normalized * pullForce, ForceMode.Acceleration);
                state = State.Pulling;

                CatchParticlesystem.Play();

            }

            //else if the right mouse button is down
            else if (Input.GetMouseButton(1))
            {
                //Push the target from our position towards the hit point:
                targetRigidbody.AddForce((targetHitPoint - baseTrans.position).
                normalized * pushForce, ForceMode.Acceleration);
                state = State.Pushing;

                ReleaseParticleSystem.Play();
            }

            //if neither mouse buttons are held down
            else
            {
                state = State.Idle;
                ReleaseParticleSystem.Clear();
            }
        }
        //if we do not have a target or it is not in range
        else
        {
            state = State.Idle;
        }
    }
    //Unity events:
    void Update()
    {
        TargetDetection();
    }
    void FixedUpdate()
    {
        PullingAndPushing();
    }

    private void OnGUI()
    {
        //draw a 2x2 rectangle at the center of the screen
        UnityEditor.EditorGUI.DrawRect(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 6, 6), CursorColor);
    }

}
