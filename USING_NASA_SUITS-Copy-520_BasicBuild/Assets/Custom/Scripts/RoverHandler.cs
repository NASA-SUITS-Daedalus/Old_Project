using UnityEngine;
using TSS;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class RoverHandler : MonoBehaviour
{

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
    public void DeployRover(string LocationMarker) {

        float latitude;
        float longitude;

        // Determine what location the user wants to send the rover to. 
        switch (LocationMarker)
        {
            case "A":
                {
                    latitude = 29.5648150f;
                    longitude = -95.0817410f;
                    break;
                }

            case "B":
                {
                    latitude = 29.5646824f;
                    longitude = -95.0811564f;
                    break;
                }

            case "C":
                {
                    latitude = 29.5650460f;
                    longitude = -95.0810944f;
                    break;
                }
            case "D":
                {
                    latitude = 29.5645430f;
                    longitude = -95.0516440f;
                    break;
                }
            case "E":
                {
                    latitude = 29.5648290f;
                    longitude = -95.0813750f;
                    break;
                }
            case "F":
                {
                    latitude = 29.5647012f;
                    longitude = -95.0813750f;
                    break;
                }
            case "G":
                {
                    latitude = 29.5651359f;
                    longitude = -95.0807408f;
                    break;
                }
            case "H":
                {
                    latitude = 29.5651465f;
                    longitude = -95.0814092f;
                    break;
                }
            case "I":
                {
                    latitude = 29.5648850f;
                    longitude = -95.0808360f;
                    break;
                }
            default:
                {
                    return;
                }
        }

        // Send the rover to the specified location. 
        TelemetryStreamClient.DeployRover(latitude, longitude);

    }


    /*
     * Recall the rover based on the user's current coordinates. 
     */
    public void RecallRover() {
        TelemetryStreamClient.RecallRover();
    }

}