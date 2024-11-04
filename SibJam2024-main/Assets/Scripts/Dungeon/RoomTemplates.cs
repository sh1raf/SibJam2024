using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
    public List<GameObject> rooms;
    public float waitTime;

    private bool generationCompleted = false;  // Флаг завершения генерации
    private int maxRooms = 20;    // Максимальное количество комнат

    void Update()
    {
        if (!generationCompleted)  // Если генерация ещё не завершена
        {
            if (waitTime <= 0 || rooms.Count >= maxRooms)
            {
                generationCompleted = true;  // Завершаем генерацию
                // Опционально: здесь вы можете вызвать метод, который заход на завершение,
                // если вам нужно что-то сделать в конце генерации
            }
            else
            {
                waitTime -= Time.deltaTime;  // Уменьшаем время ожидания
            }
        }
    }
    
    // В этом случае вам не нужно ничего добавлять в метод завершения.
    // Если нужно, можете оставить здесь метод, но он будет пустым.
    void CompleteGeneration()
    {
        // Пустой метод - ничего не делаем
    }
}
