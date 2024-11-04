using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEater : MonoBehaviour
{
    [SerializeField] private KeyCode eatKey;
    [SerializeField] private float defaultCooldown;
    [SerializeField] private float eatTime;

    private float _currentCooldown;

    private List<Hero> _targets = new();

    private PlayerMovement _movement;
    private SmokeHolder _holder;
    private LevelsController _level;

    private Animator _animator;

    private bool _canEat = true;

    private void Awake()
    {
        _movement = GetComponentInParent<PlayerMovement>();
        _holder = FindObjectOfType<SmokeHolder>();
        _level = FindObjectOfType<LevelsController>();
        _animator = GetComponentInParent<Animator>();
        _currentCooldown = defaultCooldown;
    }

    private void Update()
    {
        if (Input.GetKeyDown(eatKey) && _canEat)
            StartCoroutine(Eating());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
            _targets.Add(hero);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            if(_targets.Contains(hero))
                _targets.Remove(hero);
        }
    }


    private void Eat(Hero hero)
    {
        if (_targets.Count <= 0)
            return;

        switch (hero.Buff)
        {
            case(Buff.Drunk):
                {
                    _movement.Drunk(hero.Settings.Duration);
                    break;
                }
            case (Buff.Smoke):
                {
                    _holder.AddSmoke();
                    break;
                }
            case (Buff.SpeedBoost):
                {
                    SettingsWithMulti settings = hero.Settings as SettingsWithMulti;
                    _movement.SpeedBoost(settings.Multi, settings.Duration);
                    break;
                }
            case(Buff.FoodQuality):
                {
                    SettingsWithMulti settings = hero.Settings as SettingsWithMulti;
                    _currentCooldown += settings.Multi;
                    break;
                }
            case (Buff.RoomsCount):
                {
                    
                    break;
                }
            case (Buff.HeroesCount):
                {
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
        Hero hero = GetClosestHero();
        hero.Freeze();
        if(hero != null)
        {
            _canEat = false;
            _movement.Freeze();

            _animator.Play("Eat");
            yield return new WaitForSeconds(eatTime);

            Eat(hero);

            _movement.UnFreeze();
            yield return new WaitForSeconds(_currentCooldown);

            _canEat = true;
        }
    }


    private Hero GetClosestHero()
    {
        if(_targets.Count == 0) 
            return null;

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
