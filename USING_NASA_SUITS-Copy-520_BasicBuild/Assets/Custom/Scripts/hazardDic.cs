using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class hazardDic : MonoBehaviour
{
    public List<GameObject> dictionary;
    private float xDif;
    private float yDif;
    private float zDif;
    private float minNorm;
    private float norm;
    private int minIndex;
    private GameObject camera;
    public TextMeshPro hazardText;
    public TextMeshPro dictionaryWindowText;
   // private GameObject firstHazard; //delete afterwards

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new List<GameObject>();
        minNorm = float.MaxValue;
        minIndex = 0;
        camera = GameObject.FindWithTag("MainCamera");
        //hazardText = GameObject.Find("Point1").GetComponent<TMP_Text>();
        //dictionaryWindowText = GameObject.Find("ContentBackPlate").GetComponent<TMP_Text>();
        (hazardText).text = "No Hazards available";
       // firstHazard = GameObject.Find("Hazard");
        //dictionary.Add(firstHazard); //delete afterwards

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            var hazardPosition = dictionary[i].transform.position;
            var cameraPosition = camera.transform.position;
            xDif = hazardPosition.x - cameraPosition.x;
            yDif = hazardPosition.y - cameraPosition.y;
            zDif = hazardPosition.z - cameraPosition.z;
            norm = Mathf.Sqrt(Mathf.Pow(xDif, 2) + Mathf.Pow(yDif, 2) + Mathf.Pow(zDif, 2));
            if (norm < minNorm)
            {
                minNorm = norm;
                minIndex = i;
            }
            (dictionaryWindowText).text += "Hazard " + i + ": " + Math.Round(norm, 3).ToString().Substring(0, 5) + "m \n";
        }
        //(hazardText).text = "Hazard " + minIndex.ToString() + ": " + Math.Round(minNorm, 3).ToString() + "m";
        (hazardText).text = "Hazard " + minIndex.ToString() + ": " + Math.Round(minNorm, 2).ToString().Substring(0, Math.Round(minNorm, 2).ToString().IndexOf(".") + 3) + "m";
    }
}