using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSO so;

    private void Awake()
    {
        SpawnDungeon(so.BaseHeroesAmount + so.AddedHeroesAmount, so.BaseRoomsAmount + so.AddedRoomsAmount);
        so.AddedHeroesAmount = 0;
        so.AddedRoomsAmount = 0;
    }

    private void SpawnDungeon(int heroes, int rooms)
    {

    }
}
