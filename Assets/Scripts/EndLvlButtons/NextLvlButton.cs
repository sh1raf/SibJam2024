using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLvlButton : MonoBehaviour
{
    private Button _button;
    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(FindObjectOfType<SceneLoader>().LoadNextLevel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
}
