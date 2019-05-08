using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationDataHolder : MonoBehaviour
{   
    //public LocationData LocationDataRoot;
    public DataItem[] LocationDataRoot;

    void Start()
    {
        //LocationDataRoot = new LocationData();
        DeserializeData();
    }

    private void DeserializeData()
    {
        LocationDataRoot = JsonHelper.FromJson<DataItem>(LoadResourceTextfile("loc_data.json"));
    }

    public static string LoadResourceTextfile(string path)
    {
 
        string filePath = path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        print(targetFile.text);
        return targetFile.text;
    }
}
