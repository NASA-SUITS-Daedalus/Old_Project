using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class waypointDic : MonoBehaviour
{
    public List<GameObject> dictionary;
    private float xDif;
    private float yDif;
    private float zDif;
    private float minNorm;
    private float norm;
    private int minIndex;
    private GameObject camera;
    public TextMeshPro waypointText;
    public TextMeshPro dictionaryWindowText;
  //  private GameObject firstWaypoint; //delete afterwards

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new List<GameObject>();
        minNorm = float.MaxValue;
        minIndex = 0;
        camera = GameObject.FindWithTag("MainCamera");
        //waypointText = GameObject.Find("Point1").GetComponent<TMP_Text>();
        //dictionaryWindowText = GameObject.Find("ContentBackPlate").GetComponent<TMP_Text>();
        (waypointText).text = "No waypoints available";
       // firstWaypoint = GameObject.Find("Waypoint");
      // dictionary.Add(firstWaypoint); //delete afterwards

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            var waypointPosition = dictionary[i].transform.position;
            var cameraPosition = camera.transform.position;
            xDif = waypointPosition.x - cameraPosition.x;
            yDif = waypointPosition.y - cameraPosition.y;
            zDif = waypointPosition.z - cameraPosition.z;
            norm = Mathf.Sqrt(Mathf.Pow(xDif,2) + Mathf.Pow(yDif, 2) + Mathf.Pow(zDif, 2));
            if (norm < minNorm)
            {
                minNorm = norm;
                minIndex = i;
            }
            (dictionaryWindowText).text += "Point "+i+": "+ Math.Round(norm, 3).ToString().Substring(0, 5) + "m \n";
        }
        (waypointText).text = "Point " + minIndex.ToString() + ": " + Math.Round(minNorm, 2).ToString().Substring(0, Math.Round(minNorm, 2).ToString().IndexOf(".") + 3) + "m";
    }
}
