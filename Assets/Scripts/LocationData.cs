using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DataItem
{
    public string name;
    public string type;
    public string location;
}

[System.Serializable]
public class LocationData 
{
    public List<DataItem> LocationDataList;
}