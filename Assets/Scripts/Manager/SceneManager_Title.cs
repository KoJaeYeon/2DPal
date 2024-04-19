using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Title : Singleton<SceneManager_Title>
{
    public GameObject title;
    private Animator animator;

    private void Awake()
    {        
        DontDestroyOnLoad(this);
        animator = title.GetComponent<Animator>();
    }
    public void StartGame()
    {
        animator.Play("FadeIn");
    }
    public void SceneLoad()
    {
        SceneManager.LoadScene("InGame");
        animator.Play("FadeOut");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
