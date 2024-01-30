using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMarker : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject markerPrefab;
    public GameObject dictionaryGameObject;
    private List<GameObject> waypointDictionary;

    // This script will simply instantiate the Prefab.
    void Start()
    {
        GameObject dictionaryGameObject = GameObject.FindWithTag("WaypointsDictionary");

    }

    // This script will simply instantiate the Prefab when the appropriate button is pressed.
    public void InstantiateMarker()
    {
        // Instantiate at the current position.
        markerPrefab.SetActive(true);
        GameObject newMarker = Instantiate(markerPrefab, Camera.main.transform.position + (Camera.main.transform.forward * 0.5f) + new Vector3(0, -0.2f, 0), 
                                            Quaternion.LookRotation(Camera.main.transform.forward * 0.5f));
        waypointDictionary = dictionaryGameObject.GetComponent<waypointDic>().dictionary;
        waypointDictionary.Add(newMarker);
        // TODO record the marker (and its initial position) in the appropriate dictionary
    }
}
