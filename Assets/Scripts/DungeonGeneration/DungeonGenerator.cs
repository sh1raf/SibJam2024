using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private List<Room> rooms;
    [SerializeField] private Room finish;
    [SerializeField] private NavMeshSurface mesh;
    [SerializeField] private List<Hero> heroes;

    private int _roomsCount;
    private int _heroesCount;
    public List<Chest> Chests = new();
    public Transform End;
    private LevelsController _controller;

    private void Awake()
    {
        _controller = FindObjectOfType<LevelsController>();
        _roomsCount = _controller.GetRoomsAmount();
        _heroesCount = _controller.GetHeroesAmount();

        Vector2 nextPos;
        Vector2 lastPos = transform.position;
        for(int i = 0; i < _roomsCount; i++)
        {
            Room room = Instantiate(rooms[Random.Range(0, rooms.Count)]);
            nextPos = lastPos + room.Size / 2;

            room.transform.position = nextPos;
            room.transform.parent = transform;
            room.GenerateNewDungeons(transform);
            if(i < _heroesCount)
            {
                Hero hero = heroes[Random.Range(0, heroes.Count)];
                Debug.Log($"{hero}   {i}  {_heroesCount}");
                Hero heroInstance = Instantiate(hero);
                heroInstance.GetComponent<NavMeshAgent>().Move(room.transform.position - heroInstance.transform.position);
            }
            lastPos = nextPos;
        }
        Room finishh = Instantiate(finish);
        nextPos = lastPos + finish.Size / 2;

        finishh.transform.position = nextPos;
        finishh.transform.parent = transform;
        End = finishh.End;
        mesh.BuildNavMesh();

    }

    private void OnEnable()
    {
        Chests.AddRange(GetComponentsInChildren<Chest>());
    }

    private void Start()
    {
        mesh.BuildNavMesh();
    }
}
