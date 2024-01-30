using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TSS;
using System;
using System.Collections;
using System.Collections.Generic;

public class EgressProcedure : MonoBehaviour
{

    /*
     * Fields for the interface
     */

    // The text display
    public TextMeshPro SwitchesTSS;

    // The start button
    public GameObject StartButton;

    // The proceed button  
    public GameObject ProceedButton;

    // A flag indicating if we are between steps
    private bool waitingToProceed = false;

    // A flag indicating if the egress procedure is currently active
    private bool PerformingEgress = false;

    // A flag indicating if the egress procedure is using dummy values or not
    private bool UsingDummyValues = true;

    // User's coordinates
    public static float home_lat;
    public static float home_lon;


    // Leave Start() empty since the egress procedure is user-centered
    void Start()
    {

    }

    // Called on every frame
    void Update()
    {
        // if (PerformingEgress) {
        //     UpdateTelemetryFields(); // Make sure the fields are up to date
        // }
    }

    // The main method that executes all of the steps of the egress procedure. 
    public void PerformEgressProcedure()
    {

        // Wait for the user to start...
        // WaitToStart();

        // Step 1
        BootUpSuit();
        // WaitToProceed();

        // Step 2
        PrepareUIA();
        // WaitToProceed();

        // Step 3
        PurgeN2();
        // WaitToProceed();

        // Step 4
        InitialO2Pressure();
        // WaitToProceed();

        // Step 5
        FillEMUWater();
        // WaitToProceed();

        // Step 6
        RefillEMUWater();
        // WaitToProceed();

        // Step 7
        DepressurizeAirlock();
        // WaitToProceed();

        // Step 8
        CompleteAirlock();
        // WaitToProceed();

        // UIA procedures are complete, exit the airlock

        HUD.gameObject.SetActive(true);
        home_lat = TelemetryStreamClient.lat;
        home_lon = TelemetryStreamClient.lon;
        return;

    }

    // Step 1: Boot the suit up
    void BootUpSuit()
    {

        // If any of the switches are on, wait until the user turns them all off
        while (TelemetryStreamClient.emu1_pwr_switch 
            || TelemetryStreamClient.ev1_supply_switch
            || TelemetryStreamClient.emu1_water_waste
            || TelemetryStreamClient.emu1_o2_supply_switch 
            || TelemetryStreamClient.o2_vent_switch
            || TelemetryStreamClient.depress_pump_switch)
        {
            SwitchesTSS.text = "Not all switches are turned off. Make sure all switches are set to off before proceeding";
            // Update telemetry fields?
        }

        // All the switches are off now
        SwitchesTSS.text = "All switches are turned off. Ready to proceed";
        // ProceedButton.gameObject.SetActive(true);
        // ProceedButton.onClick.AddListener(OnProceedButtonClicked);

    }

    // Step 2: Prepare UIA
    void PrepareUIA()
    {

        // Switch O2 vent to open
        while (!TelemetryStreamClient.o2_vent_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 vent to OPEN.";
        }

        // Wait for the UIA supply pressure to drop below 23 psi
        while (TelemetryStreamClient.o2_pressure >= 23)
        {

            // Update the fields
            //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Preparing UIA. Please wait...";
        }

        // Switch O2 vent to closed
        while (TelemetryStreamClient.o2_vent_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 vent to CLOSED.";
        }

    }

    // Step 3: Purge N2
    void PurgeN2()
    {

        // Switch O2 supply to OPEN
        // emu2_o2 = true;
        while (!TelemetryStreamClient.o2_vent_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 supply to OPEN.";
        }

        // Wait for the UIA supply pressure to exceed 3000 psi
        while (TelemetryStreamClient.o2_pressure <= 3000)
        {

            // Update the fields
            // //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Purging N2. Please wait...";
        }

        // Close O2 supply
        while (TelemetryStreamClient.o2_vent_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 vent to CLOSED.";
        }

    }


    // Step 4: Initial O2 Pressurization
    void InitialO2Pressure()
    {
        // Switch O2 supply to OPEN
        // emu2_o2 = true;
        while (!TelemetryStreamClient.emu1_o2_supply_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 supply to OPEN.";
        }

        // Wait for the UIA supply pressure to exceed 1500 psi
        while (TelemetryStreamClient.o2_pressure <= 1500)
        {

            // Update the fields
            // //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Pressurizing. Please wait...";
        }

        // Close O2 supply
        while (TelemetryStreamClient.emu1_o2_supply_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 supply to CLOSED.";
        }

    }

    // Step 5: Fill EMU Water
    void FillEMUWater()
    {
        DumpWasteWater();
        WaitToProceed();

        RefillEMUWater();
    }

    // Step 5.1
    void DumpWasteWater()
    {
        // Tell the user to switch EV-1 waste to OPEN
        // TODO

        // Open EV-1 Waste
        // ev_waste = true;
        while (!TelemetryStreamClient.emu1_water_waste) {
            // Wait...
            SwitchesTSS.text = "Switch the EV-1 waste to OPEN.";
        }

        // Wait for the water level to drop below 5%
        while (TelemetryStreamClient.water_capacity >= 0.5)    // check typing/representation
        {
            // Update the fields
            // //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Dumping waste water. Please wait...";
        }

        // Close EV-1 Waste
        // ev_waste = false;
        while (TelemetryStreamClient.emu1_water_waste) {
            // Wait...
            SwitchesTSS.text = "Switch the EV-1 waste to CLOSED.";
        }

        // Proceed...
        // TODO put right proceed button 

    }

    // Step 5.2
    void RefillEMUWater()
    {
        // Tell the user to switch EV-1 waste to OPEN
        // TODO

        // Open EV-1 Supply
        // ev1_supply = true;
        while (!TelemetryStreamClient.ev1_supply_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the EV-1 supply to OPEN.";
        }

        // Wait for the water level to exceed 95%
        while (TelemetryStreamClient.water_capacity <= 95.0)    // check typing/representation
        {
            // Update the fields
            //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Refilling EMU water. Please wait...";
        }

        // Close EV-1 Supply
        // ev1_supply = false;
        while (TelemetryStreamClient.ev1_supply_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the EV-1 supply to CLOSED.";
        }

        // Proceed...
        // TODO put right proceed button 
    }

    // Step 6: Depressurize Airlock to 10.2 psi
    void DepressurizeAirlock()
    {
        // Switch depress pump to ON
        // depress_pump = true;
        while (!TelemetryStreamClient.depress_pump_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the depress pump to OPEN.";
        }

        // if (pump_fault) {
        //      DepressPumpFaultHandler();
        // }

        // Wait for the airlock pressure to drop below 10.2 psi
        while (TelemetryStreamClient.suits_pressure >= 10.2)
        {
            // Update the fields
            //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Depressurizing airlock. Please wait...";
        }

        // Close the depress pump
        // depress_pump = false;
        while (TelemetryStreamClient.depress_pump_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the depress pump to CLOSED.";
        }

        // Proceed...
        // TODO add button
    }

    // Step 6 helper...
    void DepressPumpFaultHandler()
    {
        // Switch the depress pump to off
        // depress_pump = false;

        // When the fault goes away, proceed...
        // TODO

        // Switch the depress pump to on
        // depress_pump = true;

        return;
    }

    // Step 7: Complete EMU Pressurization
    void CompleteEMUPressurization()
    {
        // Switch O2 supply to open
        // emu2_o2 = true;
        while (!TelemetryStreamClient.o2_vent_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 vent to OPEN.";
        }

        // Wait for the UIA supply pressure to exceed 3000 psi
        while (TelemetryStreamClient.o2_pressure2 <= 3000)
        {
            // Update the fields
            //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Completing EMU pressurization. Please wait...";
        }

        // Switch O2 supply to close
        // emu2_o2 = false;
        while (TelemetryStreamClient.o2_vent_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the O2 vent to CLOSED.";
        }
    }

    // Step 8: Complete Airlock Depressurization
    void CompleteAirlock()
    {
        // depress_pump = true;
        while (!TelemetryStreamClient.depress_pump_switch) {
            // Wait...
            SwitchesTSS.text = "Switch the depress pump to OPEN.";
        }

        while (TelemetryStreamClient.sub_pressure >= 0.1)
        {
            // Update the fields
            //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Completing airlock depressurization. Please wait...";
        }

        // depress_pump = false;
        while (TelemetryStreamClient.depress_pump_switch) {
            SwitchesTSS.text = "Switch the depress pump to CLOSED.";
        }
    }

    /*
     * Helper method. 
     * 
     * Make sure that all of the fields from the telemetry stream are 
     * up-to-date. 
     * 
     * Call this anytime the fields need to be checked. 
     */
    // void UpdateTelemetryFields()
    // {

    //     emu1 = TelemetryStreamClient.emu1_pwr_switch;
    //     // emu2 = TelemetryStreamClient.;
    //     // isSuitBooted = TelemetryStreamClient.;
    //     emu1_o2 = TelemetryStreamClient.emu1_o2_supply_switch;
    //     emu2_o2 = TelemetryStreamClient.o2_vent_switch;
    //     ev_waste = TelemetryStreamClient.emu1_water_waste;
    //     ev1_supply = TelemetryStreamClient.ev1_supply_switch;
    //     o2_supply_pressure1 = TelemetryStreamClient.o2_pressure;
    //     o2_supply_pressure2 = TelemetryStreamClient.o2_pressure;
    //     depress_pump = TelemetryStreamClient.depress_pump_switch;
    //     depress_pump_level = TelemetryStreamClient.sub_pressure; //Unsure if correct
    //     water_supply = TelemetryStreamClient.water_capacity;

    // }

    /*
     * Wait for the user to start the egress procedure.
     */
    void WaitToStart()
    {

        // Show the proceed button 
        StartButton.gameObject.SetActive(true);

        // We are now waiting to move on to the next step
        waitingToProceed = true;

        while (waitingToProceed)
        {
            /*
             * Wait for the user to press the Proceed button...
             * In other words, wait for the ConfirmProceed() method to execute.
             */
        }

        // The user pressed the proceed button; hide it. 
        StartButton.gameObject.SetActive(false);

        return;

    }

    /*
     * We just completed a step, so wait for the user to proceed to the next. 
     * This should be called at the end of every step. 
     */
    void WaitToProceed()
    {

        // Show the proceed button 
        ProceedButton.gameObject.SetActive(true);

        // We are now waiting to move on to the next step
        waitingToProceed = true;

        while (waitingToProceed)
        {
            /*
             * Wait for the user to press the Proceed button...
             * In other words, wait for the ConfirmProceed() method to execute.
             */
        }

        // The user pressed the proceed button; hide it. 
        ProceedButton.gameObject.SetActive(false);

        return;

    }

    /*
     * Confirm that the user has pressed the "proceed" button. 
     * Connect this to the ProceedButton's OnClick event in the Unity GUI.
     */
    public void ConfirmProceed()
    {
        waitingToProceed = false;
        return;
    }


}
}
