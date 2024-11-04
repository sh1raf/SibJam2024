using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private int RoomsCount;
    [SerializeField] private List<Room> rooms;
    [SerializeField] private Room finish;
    [SerializeField] private NavMeshSurface mesh;

    public List<Chest> Chests = new();
    public Transform End;

    private void Awake()
    {
        Vector2 nextPos;
        Vector2 lastPos = transform.position;
        for(int i = 0; i < RoomsCount; i++)
        {
            Room room = Instantiate(rooms[Random.Range(0, rooms.Count)]);
            nextPos = lastPos + room.Size / 2;

            room.transform.position = nextPos;
            room.transform.parent = transform;
            room.GenerateNewDungeons(transform);
            lastPos = nextPos;
        }
        Room finishh = Instantiate(finish);
        nextPos = lastPos + finish.Size / 2;

        finishh.transform.position = nextPos;
        finishh.transform.parent = transform;
        End = finishh.End;
        mesh.BuildNavMesh();

        Chests.AddRange(GetComponentsInChildren<Chest>());
    }

    private void Start()
    {
        mesh.BuildNavMesh();
    }
}
