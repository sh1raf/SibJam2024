using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform start;

    [SerializeField] private List<Transform> ends;

    public Vector2 Size;
}
