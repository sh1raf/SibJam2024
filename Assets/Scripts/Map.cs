using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    public List<Chest> Chests { get; private set; } = new();

    private void Awake()
    {
        Chests.AddRange(GetComponentsInChildren<Chest>());
    }


}
