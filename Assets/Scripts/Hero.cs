using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [field: SerializeField] public Buff Buff { get; private set; } = Buff.None;

    private Map _map;

    private List<Transform> _path = new();

    private void Start()
    {
        _map = FindObjectOfType<Map>();
        BuildRandomPath();
    }

    private void BuildRandomPath()
    {
        List<int> NoUsedIndexes = new List<int>{0,1,2,3,4,5,6,7,8,9 };
        for(int i = 0; i < _map.Chests.Count; i++)
        {
            int randomIndex = Random.Range(0, NoUsedIndexes.Count);
            _path.Add(_map.Chests[NoUsedIndexes[randomIndex]].transform);
            NoUsedIndexes.RemoveAt(randomIndex);
        }
    }
}

public enum Buff
{
    None,
    Drunk,
    Smoke

}
