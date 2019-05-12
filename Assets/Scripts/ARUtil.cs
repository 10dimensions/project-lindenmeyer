using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARUtil : MonoBehaviour
{   
    public static ARUtil Instance;

    public Button AddMeshButton;
    public Button SnapshotButton;
    public Button BackButton;

    public Button TestButton;

    private GameObject SelectedObj;

    private int maxMesh = 3;

    public string[] MeshList = {"Bamboo","Beech", "Gandhi"};
    public int MeshListNumber=0;

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

        TestButton.onClick.AddListener(ChangeMesh);
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
                V.transform.parent = GameObject.FindWithTag("argrid").transform;
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

    private void ChangeMesh()
    {   
        GameObject _argrid = GameObject.FindWithTag("argrid");

        foreach (Transform item in _argrid.transform)
        {
            Destroy(item.gameObject);
        }

        MeshListNumber++;

        SingletonAR.Instance.MeshName = MeshList[MeshListNumber];

        if(MeshListNumber == 2) SingletonAR.Instance.MeshType = "sculpture";

        AddMesh();
    }

}