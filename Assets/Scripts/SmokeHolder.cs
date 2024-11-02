using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeHolder : MonoBehaviour
{
    [SerializeField] private KeyCode smokeKey;
    [SerializeField] private DestroyDelay smoke;

    private int _smokeCounter;
    public void AddSmoke()
    {
        _smokeCounter++;
    }

    private void Update()
    {
        if (Input.GetKeyUp(smokeKey))
            DropSmoke();
    }

    public void DropSmoke()
    {
        if (_smokeCounter <= 0)
            return;

        Instantiate(smoke, transform.position, Quaternion.identity);
    }
}
