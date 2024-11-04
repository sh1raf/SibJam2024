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
    private Hero _hero;

    private bool _canFreeze = true;

    private void Start()
    {
        _hero = GetComponentInParent<Hero>();
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
        if (!_canFreeze)
            return;

        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, playerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(_hero.Direction, directionToTarget) < angle / 2)
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

    public void Freeze()
    {
        _canFreeze = false;
    }

    private void Check()
    {
        if(_target.GetComponent<PlayerLiveLogic>().CheckMonster())
            _hero.PopUp();
    }

    private void OnDrawGizmos()
    {
        if(_canSeePlayer)
            Gizmos.DrawRay(transform.position, _target.position - transform.position);
        if(_hero != null)
        {
            Gizmos.DrawRay(transform.position, _hero.Direction);

        }
    }

    private void Find()
    {

    }
}
