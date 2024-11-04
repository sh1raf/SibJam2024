using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform start;
    [field: SerializeField] public Transform End { get; private set; }
    public List<Chest> Chests { get; private set; } = new();

    private void Awake()
    {
        Chests.AddRange(GetComponentsInChildren<Chest>());
    }

}
