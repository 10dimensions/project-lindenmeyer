namespace Mapbox.Examples
{

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;

public class LocationDataHolder : MonoBehaviour
{   
    //public LocationData LocationDataRoot;
    public DataItem[] LocationDataRoot;

    [SerializeField]
	AbstractMap _map;

	[SerializeField]
	[Geocode]
	Vector2d[] _locations;

	[SerializeField]
	float _spawnScale = 100f;

	[SerializeField]
	GameObject _vegetationPrefab, _sculpturePrefab;

    List<GameObject> _spawnedObjects;

    bool HasInitialized = false;
    bool HasSpawned=false;

    void Start()
    {
        //LocationDataRoot = new LocationData();

        LocationProviderFactory.Instance.mapManager.OnInitialized += () => HasInitialized = true;
        DeserializeData();

        //StartCoroutine(InitMapSpawn());
    }

    void Update()
    {   
        if(HasSpawned)
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
    }

    void LateUpdate()
    {
        if(HasInitialized==true)
        {
            StartCoroutine(InitMapSpawn());
            HasInitialized = false;
        }
    }

    private void DeserializeData()
    {
        LocationDataRoot = JsonHelper.FromJson<DataItem>(LoadResourceTextfile("loc_data_test.json"));
    }

    private IEnumerator InitMapSpawn()
    {   
        yield return new WaitForSeconds(3f);

        _locations = new Vector2d[LocationDataRoot.Length];
		_spawnedObjects = new List<GameObject>();
        
		for (int i = 0; i < LocationDataRoot.Length; i++)
	    {
			var locationString = LocationDataRoot[i].location;
			_locations[i] = Conversions.StringToLatLon(locationString);

            var instance = (GameObject)null;

            if(LocationDataRoot[i].type == "vegetation")
			    {   instance = Instantiate(_vegetationPrefab);  }

            else if(LocationDataRoot[i].type == "sculpture")
			    {   instance = Instantiate(_sculpturePrefab); } 
            
			instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			_spawnedObjects.Add(instance);
        }

        HasSpawned = true;
    }

    public static string LoadResourceTextfile(string path)
    {
 
        string filePath = path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        
        return targetFile.text;
    }

    
		
    
}

}   