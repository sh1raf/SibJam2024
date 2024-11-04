using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] public List<LevelSO> Levels = new();
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private Button nextLvl;

    private int _currentLevel = 1;
    public int CurrentLevelNumber { get { return _currentLevel; } private set { } }
    private void Awake()
    {
        restartPanel.SetActive(false);
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(restartPanel);
    }

    public void LevelComplete()
    {
        restartPanel.SetActive(true);
    }

    public void LevelFailed()
    {
        restartPanel.SetActive(true);
        nextLvl.gameObject.SetActive(false);
    }

    public void OnNextLevel()
    {
        restartPanel.SetActive(false);
        _currentLevel++;
    }

    public int GetRoomsAmount()
    {
        LevelSO level = Levels[CurrentLevelNumber-1];
        int amount = level.BaseRoomsAmount + level.AddedRoomsAmount;
        level.AddedRoomsAmount = 0;
        return amount;
    }

    public int GetHeroesAmount()
    {
        LevelSO level = Levels[CurrentLevelNumber-1];
        int amount = level.BaseHeroesAmount + level.AddedHeroesAmount;
        level.AddedHeroesAmount = 0;
        return amount;
    }

    public void OnRestartFirstLevel()
    {
        nextLvl.gameObject.SetActive(true);
        restartPanel.SetActive(false);
        _currentLevel = 1;
    }

    public void OnMenu()
    {
        nextLvl.gameObject.SetActive(true);
        restartPanel.SetActive(false);
        _currentLevel = 1;
    }

    public void NextLevelAddHeroes(int amount)
    {
        Levels[_currentLevel + 1].AddedHeroesAmount += amount;
    }
}
