using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "CreateLevel")]
public class LevelSO : ScriptableObject
{
    public int BaseRoomsAmount;
    public int BaseHeroesAmount;

    public int AddedHeroesAmount;
    public int AddedRoomsAmount;
}
