using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConrtoller : MonoBehaviour
{
    [SerializeField] private float maxDistanceToTarget;
    [SerializeField] private float camSpeed;   
    [SerializeField] private Transform _target;

    private bool _focusing;

    private const float DISTANCE_TRESHOLD = 0.001f;

    private Vector2 _camPos { get { return new Vector2(transform.position.x, transform.position.y); }}

    private void Update()
    {
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
}