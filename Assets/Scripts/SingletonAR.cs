using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAR : MonoBehaviour
{   
    private static SingletonAR _instance;
    public static SingletonAR Instance
    {
        get{ return _instance;}
    }

    public string MeshType;
    public string MeshName;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

   
}