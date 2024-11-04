using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private int RoomsCount;
    [SerializeField] private List<Room> rooms;

    private void Awake()
    {
        Vector2 nextPos;
        Vector2 lastPos = transform.position;
        for(int i = 0; i < RoomsCount; i++)
        {
            nextPos = rooms[i].Size / 2;

            Room room = Instantiate(rooms[Random.Range(0, rooms.Count)]);
            room.transform.position = nextPos;
            room.transform.parent = transform;
            lastPos = nextPos;
        }
    }
}
