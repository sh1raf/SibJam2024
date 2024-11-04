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

    private bool generationCompleted = false;  // ���� ���������� ���������
    private int maxRooms = 20;    // ������������ ���������� ������

    void Update()
    {
        if (!generationCompleted)  // ���� ��������� ��� �� ���������
        {
            if (waitTime <= 0 || rooms.Count >= maxRooms)
            {
                generationCompleted = true;  // ��������� ���������
                // �����������: ����� �� ������ ������� �����, ������� ����� �� ����������,
                // ���� ��� ����� ���-�� ������� � ����� ���������
            }
            else
            {
                waitTime -= Time.deltaTime;  // ��������� ����� ��������
            }
        }
    }
    
    // � ���� ������ ��� �� ����� ������ ��������� � ����� ����������.
    // ���� �����, ������ �������� ����� �����, �� �� ����� ������.
    void CompleteGeneration()
    {
        // ������ ����� - ������ �� ������
    }
}