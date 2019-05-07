using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARUtil : MonoBehaviour
{   
    public Button AddMeshButton;
    public Button ConfirmButton;
    public Button SnapshotButton;
    public Button CancelButton;
    public Button BackButton;

    private GameObject ToSpawnObj;

     void Start()
    {
        AddMesh();
    }

    private void AddListenerToButtons()
    {
        AddMeshButton.onClick.AddListener(AddMesh);
    }

    private void AddMesh()
    {   
        if(Singleton.Instance.isVegetation)
        {
            var go = Resources.Load<GameObject>("VCube");
            
            if (go == null) 
            {
                Debug.Log("does not exist");
                return;
            }
            
            GameObject V = Instantiate(go) as GameObject;
            V.transform.parent = GameObject.FindWithTag("arroot").transform;
            V.SetActive(true);
        }
    }

    private void TakeSnapShot()
    {

    }

    private void BackButtonPressed()
    {

    }

}