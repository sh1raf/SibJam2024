using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private Image image;

    private AsyncOperation _loading;

    private Animator _animator;

    private bool _sceneClossing = false;

    private LevelsController _controller;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _animator = loadScreen.GetComponent<Animator>();
        _controller = FindObjectOfType<LevelsController>();

        loadScreen.SetActive(false);
    }

    public void LoadScene(string SceneName)
    {
        loadScreen.SetActive(true);
        _animator.SetTrigger("Awake");

        _loading = SceneManager.LoadSceneAsync(SceneName);
        _loading.allowSceneActivation = false;

        StartCoroutine(LoadingFill());
    }

    public void LoadMenu()
    {
        LoadScene(0);
        _controller.OnMenu();
    }

    public void LoadNextLevel()
    {
        if (_controller.CurrentLevelNumber + 1 == 5)
        {
            LoadScene(0);
            return;
        }

        LoadScene(_controller.CurrentLevelNumber + 1);
        _controller.OnNextLevel();
    }

    public void LoadFirstLevel()
    {
        LoadScene(1);
        _controller.OnRestartFirstLevel();
    }

    public void LoadScene(int SceneNumber)
    {
        loadScreen.SetActive(true);
        _animator.SetTrigger("Awake");

        _loading = SceneManager.LoadSceneAsync(SceneNumber);
        _loading.allowSceneActivation = false;

        StartCoroutine(LoadingFill());
    }

    private IEnumerator LoadingFill()
    {
        image.gameObject.SetActive(true);

        while (!_loading.isDone)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, _loading.progress, 0.1f);

            if (_loading.progress >= 0.9f && !_sceneClossing)
            {
                StartCoroutine(EndAnimation());
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator EndAnimation()
    {
        _sceneClossing = true;

        yield return new WaitForSeconds(4f);

        _loading.allowSceneActivation = true;
        _animator.SetTrigger("Sleep");

        image.fillAmount = 0f;

        yield return new WaitForSeconds(0.5f);

        loadScreen.gameObject.SetActive(false);


        _sceneClossing = false;
    }
}
