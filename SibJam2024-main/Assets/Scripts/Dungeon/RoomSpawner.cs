using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    public float waitTime = 4f;

    // Предположим, что размер комнаты известен
    public Vector2 roomSize;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (!spawned && templates.rooms.Count < 20) // Добавлена проверка на количество комнат
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                GameObject room = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                templates.rooms.Add(room);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                GameObject room = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                templates.rooms.Add(room);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                GameObject room = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                templates.rooms.Add(room);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                GameObject room = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                templates.rooms.Add(room);
            }
            spawned = true;
        }
        else if (!spawned)
        {
            // Завершить все открытые места если количество комнат достигло максимума
            Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
            spawned = true;
        }
    }

    // Проверка на коллизии с другими объектами
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
            {
                if (templates.rooms.Count >= 30)
                {
                    // Закрытие всех открытых мест если количество комнат достигло максимума
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                }
                else
                {
                    // Создание блока вместо дубликата комнаты
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                }
                spawned = true;
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}