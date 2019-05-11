using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARUtil : MonoBehaviour
{   
    public static ARUtil Instance;

    public Button AddMeshButton;
    public Button ConfirmButton;
    public Button CancelButton;
    public Button SnapshotButton;
    public Button BackButton;

    private GameObject SelectedObj;

    private int maxMesh = 3;

    void Awake()
    {
        if(Instance == null) Instance=this;
    }

     void Start()
    {   
        AddListenerToButtons();
        
        AddMesh();
    }

    private void AddListenerToButtons()
    {
        AddMeshButton.onClick.AddListener(AddMesh); 
        SnapshotButton.onClick.AddListener(TakeSnapShot);
        BackButton.onClick.AddListener(BackButtonPressed);
    }

    private void AddMesh()
    {   
        if(maxMesh>0)
        {
            
                var go = Resources.Load<GameObject>(SingletonAR.Instance.MeshName);
                
                if (go == null) 
                {
                    Debug.Log("does not exist");
                    return;
                }
                
                GameObject V = Instantiate(go) as GameObject;
                V.transform.parent = GameObject.FindWithTag("arroot").transform;
                V.SetActive(true);

                maxMesh--;
                SelectedObj = V;
            

            if(SingletonAR.Instance.MeshType != "vegetation")   
                AddMeshButton.interactable = false;
        }

    }



    private void TakeSnapShot()
    {

    }

    private void BackButtonPressed()
    {

    }

}