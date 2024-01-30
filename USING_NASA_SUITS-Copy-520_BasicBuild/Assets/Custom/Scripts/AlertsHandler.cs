using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AlertsHandler : MonoBehaviour
{

    public GameObject alertObject;
    public string telemetryStreamLabel;
    public int threshold;
    public bool isLowerBound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current value from the telemetry stream
        int currentVal = 0; // TODO update later

        // Check if the value is past the allowed bound
        if ((isLowerBound && currentVal < threshold) || (!isLowerBound && currentVal > threshold))
        {

            // Activate the alert if it is not already active
            if (gameObject.activeSelf) {
                alertObject.SetActive(true);
            }

        }


    }
}
