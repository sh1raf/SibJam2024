using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFieldOfView : MonoBehaviour
{
    [SerializeField] private float updateTick;
    [SerializeField] private float radius;
    [Range(0, 360)]
    [SerializeField] private float angle;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstructionMask;

    private bool _canSeePlayer;
    private Transform _target;

    private void Start()
    {
        _target = FindObjectOfType<PlayerMovement>().transform;
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(updateTick);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, playerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    _canSeePlayer = true;
                    Check();
                }
                else
                {
                    _canSeePlayer = false;
                }
            }
            else
            {
                _canSeePlayer = false;
            }
        }
        else if (_canSeePlayer)
        {
            _canSeePlayer = false;
        }
    }

    private void Check()
    {
        _target.GetComponent<PlayerLiveLogic>().CheckMonster();
    }

    private void OnDrawGizmos()
    {
        if(_canSeePlayer)
            Gizmos.DrawRay(transform.position, _target.position - transform.position);
    }

    private void Find()
    {

    }
}