using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private LevelsController _controller;

    private void Awake()
    {
        _controller = FindObjectOfType<LevelsController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            FindObjectOfType<LevelsController>().LevelComplete();
        else if (collision.CompareTag("Heroes"))
            Destroy(collision.gameObject);
    }
}
