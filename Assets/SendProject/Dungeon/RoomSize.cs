using UnityEngine;

public class RoomSize : MonoBehaviour
{
    public Vector2 size = new Vector2(10f, 10f); // ������ ������� �� ���������

    void Start()
    {
        // �������� ���� LongRoom
        if (gameObject.CompareTag("LongRoom"))
        {
            size = new Vector2(20f, 10f);
        }
    }
}
