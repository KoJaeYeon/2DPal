using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Title : Singleton<SceneManager_Title>
{
    public GameObject title;
    public GameObject LoadPanel;
    private Animator animator;

    private void Awake()
    {        
        if(SceneManager_Title.Instance != this && SceneManager_Title.Instance != null) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this);
        animator = title.GetComponent<Animator>();
    }
    public void StartGame()
    {
        animator.Play("FadeIn");
    }
    public void SceneLoad()
    {
        title.SetActive(true);
        SceneManager.LoadScene("InGame");
        animator.Play("FadeOut");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
