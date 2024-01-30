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

    // The proceed button  
    public Button ProceedButton;

    // A flag indicating if we are between steps
    public bool waitingToProceed = false;   

    /*
     * Fields from the telemetry stream. 
     * Make sure they are constantly updated using UpdateTelemetryFields().
     */
    private bool emu1 = false;
    private bool emu2 = false;
    private bool isSuitBooted = false;
    private bool emu1_o2 = false;
    private bool emu2_o2 = false;
    private bool ev_waste = false;
    private bool ev1_supply = false;
    private float o2_supply_pressure1 = 0.0f;
    private float o2_supply_pressure2 = 0.0f;
    private bool depress_pump = false;
    private double depress_pump_level;
    // public string ev1_waste;
    // public string ev2_waste;
    private string ev1_supply;
    // private string ev2_supply;


    // Leave Start() empty since the egress procedure is user-centered
    void Start() {

    }

    // Called on every frame
    void Update() {
        UpdateTelemetryFields(); // Make sure the fields are up to date
    }

    // The main method that executes all of the steps of the egress procedure. 
    void PerformEgressProcedure() {

        // Step 1
        BootUpSuit();
        WaitToProceed();

        // Step 2
        PrepareUIA();
        WaitToProceed();

        // Step 3
        PurgeN2();
        WaitToProceed();

        // Step 4
        InitialO2Pressure();
        WaitToProceed();

        // Step 5
        FillEMUWater();
        WaitToProceed();

        // Step 6
        RefillEMUWater();
        WaitToProceed();

        // Step 7
        DepressurizeAirlock();
        WaitToProceed();

        // Step 8
        CompleteAirlock();
        WaitToProceed();

        // UIA procedures are complete, exit the airlock
        return;

    }

    // Step 1: Boot the suit up
    void BootUpSuit() {

        // If any of the switches are on, wait until the user turns them all off
        while (!((emu1 == false) && (emu1_o2 == false) && (ev_waste == false)))
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
    void PrepareUIA() {

        // Switch O2 vent to open
        emu2_o2 = true;

        // Wait for the UIA supply pressure to drop below 23 psi
        while (o2_supply_pressure2 >= 23)
        {

            // Update the fields
            //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Preparing UIA. Please wait...";
        }

        // Switch O2 vent to closed
        emu2_o2 = false;

    }

    // Step 3: Purge N2
    void PurgeN2() {

        // Switch O2 supply to OPEN
        emu2_o2 = true;

        // Wait for the UIA supply pressure to exceed 3000 psi
        while (o2_supply_pressure2 <= 3000)
        {

            // Update the fields
            // //UpdateTelemetryFields();

            // Wait...
            SwitchesTSS.text = "Purging N2. Please wait...";
        }

        // Close O2 supply
        emu2_o2 = false;

    }


    // Step 4: Initial O2 Pressurization
    void InitialO2Pressure()
        {
            // Switch O2 supply to OPEN
            emu2_o2 = true;

            // Wait for the UIA supply pressure to exceed 1500 psi
            while (o2_supply_pressure2 <= 1500)
            {

                // Update the fields
                // //UpdateTelemetryFields();

                // Wait...
                SwitchesTSS.text = "Pressurizing. Please wait...";
            }

            // Close O2 supply
            emu2_o2 = false;

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
            ev_waste = true;

            // Wait for the water level to drop below 5%
            while (Double.Parse(ev1_waste) >= 5.0)    // check typing/representation
            {
                // Update the fields
                // //UpdateTelemetryFields();

                // Wait...
                SwitchesTSS.text = "Dumping waste water. Please wait...";
            }

            // Close EV-1 Waste
            ev_waste = false;

            // Proceed...
            // TODO put right proceed button 

        }

    // Step 5.2
    void RefillEMUWater()
        {
            // Tell the user to switch EV-1 waste to OPEN
            // TODO

            // Open EV-1 Supply
            ev1_supply = true;

            // Wait for the water level to exceed 95%
            while (Double.Parse(ev1_supply) <= 95.0)    // check typing/representation
            {
                // Update the fields
                //UpdateTelemetryFields();
            
                // Wait...
                SwitchesTSS.text = "Refilling EMU water. Please wait...";
            }

            // Close EV-1 Supply
            ev1_supply = false;

            // Proceed...
            // TODO put right proceed button 
        }

    // Step 6: Depressurize Airlock to 10.2 psi
    void DepressurizeAirlock()
    {
            // Switch depress pump to ON
            depress_pump = true;

            // if (pump_fault) {
            //      DepressPumpFaultHandler();
            // }

            // Wait for the airlock pressure to drop below 10.2 psi
            while (depress_pump_level >= 10.2)
            {
                // Update the fields
                //UpdateTelemetryFields();

                // Wait...
                SwitchesTSS.text = "Depressurizing airlock. Please wait...";
            }

            // Close the depress pump
            depress_pump = false;

            // Proceed...
            // TODO add button
    }

    // Step 6 helper...
    void DepressPumpFaultHandler()
        {
            // Switch the depress pump to off
            depress_pump = false;

            // When the fault goes away, proceed...
            // TODO

            // Switch the depress pump to on
            depress_pump = true;

            return;
        }

    // Step 7: Complete EMU Pressurization
    void CompleteEMUPressurization()
        {
            // Switch O2 supply to open
            emu2_o2 = true;

            // Wait for the UIA supply pressure to exceed 3000 psi
            while (o2_supply_pressure2 <= 3000)
            {
                // Update the fields
                //UpdateTelemetryFields();

                // Wait...
                SwitchesTSS.text = "Completing EMU pressurization. Please wait...";
            }

            // Switch O2 supply to close
            emu2_o2 = false;
        }

    // Step 8: Complete Airlock Depressurization
    void CompleteAirlock()
        {
            depress_pump = true;

            while (depress_pump_level >= 0.1)
            {
                // Update the fields
                //UpdateTelemetryFields();

                // Wait...
                SwitchesTSS.text = "Completing airlock depressurization. Please wait...";
            }

            depress_pump = false;
        }

    /*
     * Helper method. 
     * 
     * Make sure that all of the fields from the telemetry stream are 
     * up-to-date. 
     * 
     * Call this anytime the fields need to be checked. 
     */ 
    void UpdateTelemetryFields() {

        emu1 = TelemetryStreamClient.emu1_pwr_switch;
       // emu2 = TelemetryStreamClient.;
       // isSuitBooted = TelemetryStreamClient.;
        emu1_o2 = TelemetryStreamClient.emu1_o2_supply_switch;
        emu2_o2 = TelemetryStreamClient.o2_vent_switch;
        ev_waste = TelemetryStreamClient.emu1_water_waste;
        ev1_supply = TelemetryStreamClient.ev1_supply_switch;
        o2_supply_pressure1 = TelemetryStreamClient.o2_pressure;
        o2_supply_pressure2 = TelemetryStreamClient.o2_pressure;
        depress_pump = TelemetryStreamClient.depress_pump_switch;
        depress_pump_level = TelemetryStreamClient.sub_pressure; //Unsure if correct

    }


    /*
     * We just completed a step, so wait for the user to proceed to the next. 
     * This should be called at the end of every step. 
     */
    void WaitToProceed() {

        // Show the proceed button 
        ProceedButton.gameObject.SetActive(true);

        // We are now waiting to move on to the next step
        waitingToProceed = true;

        while (waitingToProceed) {
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
    void ConfirmProceed() {
        waitingToProceed = false;
        return;
    }


}
