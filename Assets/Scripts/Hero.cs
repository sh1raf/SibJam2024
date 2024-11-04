using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    [field: SerializeField] public Buff Buff { get; private set; } = Buff.None;
    public BuffSettings Settings { get; private set; }
    private Map _map;

    private List<Transform> _path = new();

    private NavMeshAgent _agent;
    private Animator _animator;

    public Vector2 Direction { get; private set; }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _animator = GetComponent<Animator>();
        Settings = GetComponent<BuffSettings>();
        _map = FindObjectOfType<Map>();
        BuildRandomPath();
    }

    private void BuildRandomPath()
    {
        List<int> NoUsedIndexes = new List<int>{0,1,2,3,4,5,6,7,8,9 };
        for(int i = 0; i < _map.Chests.Count; i++)
        {
            int randomIndex = Random.Range(0, NoUsedIndexes.Count);
            _path.Add(_map.Chests[NoUsedIndexes[randomIndex]].transform);
            NoUsedIndexes.RemoveAt(randomIndex);
        }
        _path.Add(_map.End);

        StartCoroutine(Going());
    }

    public void Freeze()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _agent.isStopped = true;
    }

    private void Detected()
    {
        
    }

    private IEnumerator Going()
    {
        for(int i = 0; i < _path.Count; i++)
        {
            _agent.SetDestination(_path[i].position);
            while(Vector2.Distance(transform.position, _path[i].position) > 0.01f)
            {
                Vector2 direction = new Vector2(_agent.desiredVelocity.x, _agent.desiredVelocity.y);
                direction = direction.normalized;
                Direction = direction;
                if (direction.x > 0)
                {
                    if (direction.y > 0)
                    {
                        _animator.Play("Right-up");
                    }
                    else if (direction.y < 0)
                    {
                        _animator.Play("Right-down");
                    }
                    else
                        _animator.Play("Right");
                }
                else if (direction.x < 0)
                {
                    if (direction.y > 0)
                    {
                        _animator.Play("Left-up");
                    }
                    else if (direction.y < 0)
                    {
                        _animator.Play("Left-down");
                    }
                    else
                        _animator.Play("Left");
                }
                else
                {
                    if (direction.y > 0)
                    {
                        _animator.Play("Up");
                    }
                    else if (direction.y < 0)
                    {
                        _animator.Play("Down");
                    }
                    else
                        _animator.Play("Idle");
                }
                yield return null;
            }
        }
    }
}

public enum Buff
{
    None,
    Drunk,
    Smoke,
    SpeedBoost,
    FoodQuality,
    RoomsCount,
    HeroesCount
}
