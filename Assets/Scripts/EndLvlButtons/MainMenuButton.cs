using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private void Awake()
    {
        var component = GetComponent<Button>();
        .onClick.AddListener(FindObjectOfType<SceneLoader>().LoadScene(0));
    }

    private void OnDisable()
    {
        GetComponent<Button>().RemoveAllListeners();
    }
}
