using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // https://www.youtube.com/watch?v=j1-OyLo77ss how this script works 

    public float radius;
    public float attackRadius;

    [Range(0,360)]
    public float angle;

    public Transform target;
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool canAttackPlayer;
    public bool canChasePlayer;

    public bool ignore = false;
    private void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        InvokeRepeating(nameof(FieldOfViewCheck), 0f, 0.2f);
    }

    private void FieldOfViewCheck()
    {
        if (ignore) { return; }
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        Collider[] attackChecks = Physics.OverlapSphere(transform.position, attackRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    canChasePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            { canSeePlayer = false; }

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            canChasePlayer = false;           
        }


        canAttackPlayer = attackChecks.Length != 0;

    }
}
