using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleImage : MonoBehaviour
{
    public void LoadScece()
    {
        SceneManager_Title.Instance.SceneLoad();
    }

    public void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
