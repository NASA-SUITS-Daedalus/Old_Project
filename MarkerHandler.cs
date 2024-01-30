using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class MarkerHandler : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject markerPrefab;

    // The name of the marker (MUST BE SINGULAR)
    public string markerName;

    // The current number of markers
    private int numMarkers = 0;

    /* The dictionary of marker coordinates, represented as a triple
     * 
     * 1. The number of the coordinate (i.e., 1, 2, 3...)
     * 2. The latitude of the coordinate
     * 3. The longitude of the coordinate
     */ 
    private List<(int, float,float)> markerDictionary;

    // The TextMesh to display the coordinates
    public TextMeshPro markerTextMesh;

    // The TextMesh to display the closest point
    public TextMeshPro closestMarkerTextMesh;


    // Initialize the dictionary
    void Start()
    {
        markerDictionary = new List<(int, float, float)>();

    }

    // Calculate the closest marker to the user
    void Update()
    {
        if (numMarkers <= 0) {
            closestMarkerTextMesh.text = String.Format("No {0}s set...", markerName.ToLower());
        }
        else {

            // Note the user's current coordinates
            float lat = TelemetryStreamClient.lat;
            float lon = TelemetryStreamClient.lon;

            // Display the current marker
            Tuple<int, float, float> closest = calculateClosestMarker();
            float distance = calculateDistance(lat, lon, closest[1], closest[2]);
            // closestMarkerTextMesh.text = String.Format("CLOSEST {0}: {1}m away at ({2}, {3})",
            //                                 markerName.ToUpper(), floatPrettyPrint(distance), 
            //                                 floatPrettyPrint(closest[1]),
            //                                 floatPrettyPrint(closest[2]));
            closestMarkerTextMesh.text = String.Format("CLOSEST {0}: {1}m away",
                                            markerName.ToUpper(), floatPrettyPrint(distance));
        }
    }

    // This script will simply instantiate the Prefab when the appropriate button is pressed.
    public void InstantiateMarker()
    {
        // Increment the number of markers
        numMarkers++;

        // Instantiate at the current position.
        GameObject newMarker = Instantiate(markerPrefab, Camera.main.transform.position + (Camera.main.transform.forward * 0.5f) + new Vector3(0, -0.2f, 0), 
                                            Quaternion.LookRotation(Camera.main.transform.forward * 0.5f));
        newMarker.gameObject.SetActive(true);

        // Note the user's current coordinates
        float lat = TelemetryStreamClient.lat;
        float lon = TelemetryStreamClient.lon;

        // Record the marker (and its initial position) in the appropriate dictionary
        markerDictionary.Add(new Tuple<int, float, float>(numMarkers, lat, lon));

        // Update the TextMesh 
        markerTextMesh.text += String.Format("LAT: {0}\tLONG:{1}\n", lat, lon);

    }

    /*
     * Calculate the closest marker to the user
     * 
     * Assumes that at least one marker exists
     */
    private Tuple<int, float, float> calculateClosestMarker()
    {
        // Get the user's current coordinates
        float lat = TelemetryStreamClient.lat;
        float lon = TelemetryStreamClient.lon;

        // Iterate through the current markers to find the closest
        Tuple<int, float, float> cand = markerDictionary[0];
        float candDistance = calculateDistance(lat, lon, cand[1], cand[2]);
        for (int i = 0; i < numMarkers; i++) {

            // Try the next marker
            Tuple<int, float, float> potental = markerDictionary[i];

            // Test if we have found a new closest marker
            float potentialDistance = calculateDistance(lat, lon, potental[1], potental[2]);
            if (potentialDistance < candDistance) {
                cand = markerDictionary[i];
                candDistance = potentialDistance;
            }
        }

        return cand;

    }

    // Calculate the distance between two points
    private float calculateDistance(float x1, float y1, float x2, float y2) {
        return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
    }

    // Print floats nicely
    private string floatPrettyPrint(float val) {
        return (float) Math.Round(val, 2).ToString().Substring(0, Math.Round(val, 2).ToString().IndexOf(".") + 2);
    }

}
