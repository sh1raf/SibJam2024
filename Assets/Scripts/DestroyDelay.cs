using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] private float delay;

    private void Awake()
    {
        StartCoroutine(Delaying());
    }

    private IEnumerator Delaying()
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
