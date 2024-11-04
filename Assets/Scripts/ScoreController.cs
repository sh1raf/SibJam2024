using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    
    [SerializeField] private int scoreDefaultEat = 100;
    [SerializeField] private TMP_Text scoreInt;

    private int generalScore;
    private float multiplier;
    private Coroutine multiplierCoroutine;

    public void Start()
    {
        generalScore = 0;
        multiplier = 1f;

    }

    public void Update()
    {
        scoreInt.text = $"{generalScore}";
    }

    public void AddEatScore()
    {
        generalScore += Mathf.RoundToInt(scoreDefaultEat * multiplier);
        Debug.Log("Очки: " + generalScore);
    }

    public void EditMultiplier(float value, float duration = 10f)
    {
        StartCoroutine(Enumerator(value, duration));
    }
    private IEnumerator Enumerator(float value, float duration)
        {
            multiplier *= value;
            yield return new WaitForSeconds(duration);
            multiplier = 1f;
        }
}

