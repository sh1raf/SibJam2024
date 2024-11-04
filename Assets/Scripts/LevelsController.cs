using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsController : MonoBehaviour
{
    [SerializeField] public List<LevelSO> Levels = new();
    [SerializeField] private GameObject restartPanel;

    private int _currentLevel;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LevelComplete()
    {
        _currentLevel++;
    }

    public void LevelFailed()
    {
        restartPanel.SetActive(true);
    }

    public void NextLevelAddHeroes(int amount)
    {
        Levels[_currentLevel + 1].AddedHeroesAmount += amount;
    }
}
