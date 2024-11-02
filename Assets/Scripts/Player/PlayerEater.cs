using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEater : MonoBehaviour
{
    [SerializeField] private KeyCode eatKey;

    private List<Hero> _targets = new();

    private PlayerMovement _movement;
    private SmokeHolder _holder;

    private int _smokeCount;

    //private Vector2 defaultSize = new Vector2(1,1);

    private void Awake()
    {
        _movement = GetComponentInParent<PlayerMovement>();
        _holder = FindObjectOfType<SmokeHolder>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(eatKey))
            Eat();

    }

    private void Eat()
    {
        if (_targets.Count <= 0)
            return;

        Hero hero = GetClosestHero();

        switch(hero.Buff)
        {
            case(Buff.Drunk):
                {
                    _movement.Drunk();
                    break;
                }
            case (Buff.Smoke):
                {
                    _holder.AddSmoke();
                    break;
                }
            default:
                {

                    break;
                }
        }

        Destroy(hero.gameObject);
    }
    private IEnumerator Eating()
    {
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hero>(out Hero hero))
            _targets.Add(hero);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hero>(out Hero hero))
        {
            if(_targets.Contains(hero))
                _targets.Remove(hero);
        }
    }

    private Hero GetClosestHero()
    {
        float minDistance = Vector2.Distance(transform.position, _targets[0].transform.position);
        Hero hero = _targets[0];
        for(int i = 0; i < _targets.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, _targets[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                hero = _targets[i];
            }
        }

        return hero;
    }
}
