using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLiveLogic : MonoBehaviour
{
    [SerializeField] private DestroyDelay dieAnimation;

    private int _smokeCount;

    private PlayerMovement _playerMovement;
    private Map _map;

    private void Awake()
    {
        _map = FindObjectOfType<Map>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Smoke"))
        {
            _smokeCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Smoke"))
        {
            _smokeCount--;
        }
    }

    public bool CheckMonster()
    {
        if (_playerMovement.MovementDirection != Vector2.zero && _smokeCount == 0)
        {
            Reincornation();
            return true;
        }

        return false;
    }

    private void Reincornation()
    {

    }
}
