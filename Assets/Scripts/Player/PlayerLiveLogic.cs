using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLiveLogic : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float freezeCooldown;
    [SerializeField] private DestroyDelay dieAnimation;

    private int _currentHealth;

    private int _smokeCount;

    private PlayerMovement _playerMovement;
    private DungeonGenerator _map;

    private void Awake()
    {
        _map = FindObjectOfType<DungeonGenerator>();
        _playerMovement = GetComponent<PlayerMovement>();

        _currentHealth = maxHealth;
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
            _currentHealth--;

            if(_currentHealth <=0)
            {

            }
            else
            {
                Reincornation();
            }
            return true;
        }

        return false;
    }

    private void Reincornation()
    {
        Chest chest = _map.Chests[Random.Range(0, _map.Chests.Count)];
        Vector2 position = chest.transform.position;
        chest.gameObject.SetActive(false);
        Camera.main.GetComponent<CameraConrtoller>().Freeze();
        Instantiate(dieAnimation, transform.position, Quaternion.identity);
        transform.position = position;
        StartCoroutine(FreezeCooldown());
    }

    private IEnumerator FreezeCooldown()
    {
        _playerMovement.Freeze();

        yield return new WaitForSeconds(freezeCooldown);

        _playerMovement.UnFreeze();
    }
}
