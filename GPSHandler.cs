using UnityEngine;
using TSS;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class GPSHandler : MonoBehaviour
{

    // The cone object
    public GameObject cone;

    /*
     * The alert display
     * 
     * This should contain a TextMeshPro that says something along the lines
     * of "ERROR: The rover cannot be deployed if the cone has not been placed.
     * Press the Set Marker button in the ROVER menu to set the cone."
     */
    public GameObject deployAlert; 

    // If the cone is currently active or not
    private bool coneIsActive;

    // The current latitude of the cone
    private float cone_lat;

    // The current longitude of the cone
    private float cone_lon;

    void Start() {
    }

    void Update() {
    }

    /*  
     * Activate the cone. 
     * This determines the rover's goal location. 
     * 
     */
    void ActivateCone() {

        // Activate the cone
        cone.gameObject.SetActive(true);
        coneIsActive = true;

        // Record the cone's location (i.e., the user's current location.)
        cone_lat = TelemetryStreamClient.lat;
        cone_lon = TelemetryStreamClient.lon;
    }

    /*  
     * Deactivate the cone. 
     * The user will not be able to deploy the rover if the cone is inactive. 
     */
    void DeactivateCone() {
        coneIsActive = false;
        cone.gameObject.SetActive(false);
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
    void DeployRover() {
        
        /*
         * Show an error message if the user tries to deploy the rover
         * while the cone is not active. 
         */
        if (!coneIsActive) {
            deployAlert.gameObject.SetActive(true);
            return;
        }
        
        // Send the rover to the cone. 
        SendRoverNavigateCommand(cone_lat, cone_lon);

    }


    /*
     * Recall the rover based on the user's current coordinates. 
     */
    void RecallRover() {
        SendRoverRecallCommand();
    }

}