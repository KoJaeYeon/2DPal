using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            //인스턴스가 null이면
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if(instance == null )
                {
                    GameObject singtonObject = new GameObject();
                    instance = singtonObject.AddComponent<T>();

                    DontDestroyOnLoad(instance);
                }
            }
            return instance;
        }
    }
}
