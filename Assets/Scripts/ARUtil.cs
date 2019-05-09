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
        MeshButtonPressed();
        AddMesh();
    }

    private void AddListenerToButtons()
    {
        AddMeshButton.onClick.AddListener(AddMesh);
        ConfirmButton.onClick.AddListener(ConfimButtonPressed);

        SnapshotButton.onClick.AddListener(TakeSnapShot);
        BackButton.onClick.AddListener(BackButtonPressed);
    }

    private void AddMesh()
    {   
        if(maxMesh>0)
        {
            if(SingletonAR.Instance.isVegetation)
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
            }
        }

        MeshButtonPressed();
    }

    private void MeshButtonPressed()
    {
        BackButton.interactable = false;
        SnapshotButton.interactable = false;
        AddMeshButton.interactable = false;

        ConfirmButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(true);
    }

    private void ConfimButtonPressed()
    {
        BackButton.interactable = true;
        SnapshotButton.interactable = true;
        AddMeshButton.interactable = true;

        ConfirmButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
    }

    private void CancelButtonPressed()
    {
        BackButton.interactable = true;
        SnapshotButton.interactable = true;
        AddMeshButton.interactable = true;

        ConfirmButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
    }



    private void TakeSnapShot()
    {

    }

    private void BackButtonPressed()
    {

    }

}