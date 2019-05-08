namespace Mapbox.Examples
{

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;

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
	GameObject _markerPrefab;

    List<GameObject> _spawnedObjects;

    void Start()
    {
        //LocationDataRoot = new LocationData();
        DeserializeData();
        InitMapSpawn();
    }

    private void DeserializeData()
    {
        LocationDataRoot = JsonHelper.FromJson<DataItem>(LoadResourceTextfile("loc_data.json"));
    }

    private void InitMapSpawn()
    {
        _locations = new Vector2d[LocationDataRoot.Length];
		_spawnedObjects = new List<GameObject>();
        
		for (int i = 0; i < LocationDataRoot.Length; i++)
	    {
			var locationString = LocationDataRoot[i].location;
			_locations[i] = Conversions.StringToLatLon(locationString);
			var instance = Instantiate(_markerPrefab);
			instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			_spawnedObjects.Add(instance);
        }
    }

    public static string LoadResourceTextfile(string path)
    {
 
        string filePath = path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        print(targetFile.text);
        return targetFile.text;
    }

    private void Update()
	{
		// int count = _spawnedObjects.Count;
		// for (int i = 0; i < count; i++)
		// 	{
		// 		var spawnedObject = _spawnedObjects[i];
		// 		var location = _locations[i];
		// 		spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
		// 		spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
		// 	}
    }
}

}   