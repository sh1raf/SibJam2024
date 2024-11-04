using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmokeHolder : MonoBehaviour
{
    [SerializeField] private KeyCode smokeKey;
    [SerializeField] private DestroyDelay smoke;
    [SerializeField] private TMP_Text tmp;

    private int _smokeCounter;

    private void Awake()
    {
        tmp.text = _smokeCounter.ToString();
    }

    public void AddSmoke()
    {
        _smokeCounter++;
        tmp.text = _smokeCounter.ToString();
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
