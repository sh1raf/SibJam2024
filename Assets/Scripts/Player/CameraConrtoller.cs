using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConrtoller : MonoBehaviour
{
    [SerializeField] private float maxDistanceToTarget;
    [SerializeField] private float camSpeed;   
    [SerializeField] private Transform _target;
    [SerializeField] private float freezeTime;

    private bool _focusing;

    private bool _canFocus = true;

    private Vector2 _camPos { get { return new Vector2(transform.position.x, transform.position.y); }}

    private void LateUpdate()
    {
        if(!_canFocus)
            return;

        float distance = Vector2.Distance(_camPos, _target.position);
        if(distance > maxDistanceToTarget && !_focusing)
        {
            StartCoroutine(Focus());
        }
    }

    private IEnumerator Focus()
    {
        _focusing = true;

        while(Vector2.Distance(_camPos, _target.position) > maxDistanceToTarget)
        {
            Vector2 direction = new Vector2(_target.position.x, _target.position.y) - _camPos;
            transform.Translate(direction * camSpeed);
            yield return null;
        }

        _focusing = false;
    }

    public void Freeze()
    {
        StartCoroutine(Freezing());
    }

    private IEnumerator Freezing()
    {
        _canFocus = false;

        yield return new WaitForSeconds(freezeTime);

        _canFocus = true;
    }
}
