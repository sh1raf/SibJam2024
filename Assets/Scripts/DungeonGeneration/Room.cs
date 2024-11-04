using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<Direction> exits;
    [SerializeField] private Chest chest;

    [SerializeField] private List<Room> gaps;

    public Vector2 Size;
    public Transform End;

    private void Awake()
    {

    }

    public void GenerateNewDungeons(Transform parent)
    {
        for(int i = 0; i < exits.Count; i++)
        {
            Room room = Instantiate(gaps[i]);
            room.transform.position = transform.position;
            room.transform.parent = parent;

            Chest chestt = Instantiate(chest);
            chestt.transform.parent = transform;

            switch (exits[i])
            {
                case Direction.Left:
                    {
                        room.transform.localPosition += new Vector3(-room.Size.x, -room.Size.y, 0)/2;
                        break;
                    }
                case Direction.Right:
                    {
                        room.transform.localPosition += new Vector3(room.Size.x, room.Size.y, 0) / 2;
                        break;
                    }
                case Direction.Up:
                    {
                        room.transform.localPosition += new Vector3(-room.Size.x, room.Size.y, 0) / 2;
                        break;
                    }
                case Direction.Down:
                    {
                        room.transform.localPosition += new Vector3(room.Size.x, -room.Size.y, 0) / 2;
                        break;
                    }

            }

            chestt.transform.position = room.transform.position;
        }
        for(float i = Size.x/2; i < Size.x; i++)
        {
            for(float j = -Size.y/2; j < Size.y/2; j++)
            {
                Chest chestt = Instantiate(chest);
                chestt.transform.parent = transform;
                chestt.transform.localPosition = new Vector2(i, j);
            }
        }
    }

}

public enum Direction
{
    Up, Down, Right, Left
}

