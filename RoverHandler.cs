using UnityEngine;
using TSS;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class GPSHandler : MonoBehaviour
{

    public float latitude;
    public float longitude;

    void Start() {
    }

    void Update() {
    }

    /*
     * Deploy the rover based on the goal coordinates (i.e., the cone's 
     * latitude and longitude). 
     * 
     * The rover can only be deployed if the cone is placed. 
     * 
     * If the cone has not been placed (or has been deactivated), show an
     * alert saying the user must place the cone. 
     */
    public void DeployRover() {

        // Send the rover to the cone. 
        TelemetryStreamClient.DeployRover(latitude, longitude);

    }


    /*
     * Recall the rover based on the user's current coordinates. 
     */
    public void RecallRover() {
        TelemetryStreamClient.RecallRover();
    }

}