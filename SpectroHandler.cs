using UnityEngine;
using TSS;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class SpectroHandler : MonoBehaviour
{

    // Compounds
    private float SiO2;
    private float TiO2;
    private float Al2O3;
    private float FeO;
    private float MnO;
    private float MgO;
    private float CaO;
    private float K2O;
    private float P2O3;


    /* Unity GUI */

    // The display when the user is scanning
    public GameObject scanningDisplay;

    // Display the scanned compounds
    public TextMeshPro scannedCompounds;

    // Guess what was scanned
    public TextMeshPro specimenGuess;

    void Start() {
    }


    void Update() {

        bool notScanning = (SiO2 == 0 && TiO2 == 0 && Al2O3 == 0 && FeO == 0 && MnO == 0 && MgO == 0 && CaO == 0 && K2O == 0 && P2O3 == 0);

        if (!notScanning)
        {
            scanningDisplay.gameObject.SetActive(true);

            // Check the fields on the telemetry stream and display them
            SiO2 = TelemetryStreamClient.SiO2;
            TiO2 = TelemetryStreamClient.TiO2;
            Al2O3 = TelemetryStreamClient.Al2O3;
            FeO = TelemetryStreamClient.FeO;
            MnO = TelemetryStreamClient.MnO;
            MgO = TelemetryStreamClient.MgO;
            CaO = TelemetryStreamClient.CaO;
            K2O = TelemetryStreamClient.K2O;
            P2O3 = TelemetryStreamClient.P2O3;

            scannedCompounds.text = string.Format("SiO2: {0}\nTiO2: {1}\nAl2O3: {2}\nFeO: {3}\nMnO: {4}\nMgO: {5}\nCaO: {6}\nK2O: {7}\nP2O3: {8}\n", 
                                                    SiO2, TiO2, Al2O3, FeO, MnO, MgO, CaO, K2O, P2O3);

            // Guess what specimen might be present
            specimenGuess.text = "Likely Specimen: " + CheckSpecimen();

            // Hide after three seconds
            Invoke("HideDisplay", 3f);

        }

        else
        {
            scanningDisplay.gameObject.SetActive(false);
        }


    }

    // Hide the display
    void HideDisplay() 
    {
        scanningDisplay.gameObject.SetActive(false);
    }

    // Check what specimen was scanned
    string CheckSpecimen() {

        if (SiO2 == 40.58f
            && TiO2 == 12.83f
            && Al2O3 == 10.91f
            && FeO == 13.18f
            && MnO == 0.19f
            && MgO == 6.7f
            && CaO == 10.64f
            && K2O == -0.11f
            && P2O3 == 0.34f) {
                return "Mare basalt";
            }

        if (SiO2 == 36.89f
           && TiO2 == 2.44f
           && Al2O3 == 9.6f
           && FeO == 14.52f
           && MnO == 0.24f
           && MgO == 5.3f
           && CaO == 8.22f
           && K2O == -0.13f
           && P2O3 == 0.29f)
        {
            return "Vesicular basalt";
        }
        if (SiO2 == 41.62f
            && TiO2 == 2.44f
            && Al2O3 == 9.52f
            && FeO == 18.12f
            && MnO == 0.27f
            && MgO == 11.1f
            && CaO == 8.12f
            && K2O == -0.12f
            && P2O3 == 0.38f)
        {
            return "Olivine basalt";
        }

        if (SiO2 == 46.72f
            && TiO2 == 1.1f
            && Al2O3 == 19.01f
            && FeO == 7.21f
            && MnO == 0.14f
            && MgO == 7.83f
            && CaO == 14.22f
            && K2O == 0.43f
            && P2O3 == 0.65f)
        {
            return "Feldspathic basalt";
        }
        if (SiO2 == 46.53f
            && TiO2 == 3.4f
            && Al2O3 == 11.68f
            && FeO == 16.56f
            && MnO == 0.24f
            && MgO == 6.98f
            && CaO == 11.11f
            && K2O == -0.02f
            && P2O3 == 0.38f)
        {
            return "Pigeonite basalt";
        }
        if (SiO2 == 42.45f
            && TiO2 == 1.56f
            && Al2O3 == 11.44f
            && FeO == 17.91f
            && MnO == 0.27f
            && MgO == 10.45f
            && CaO == 9.37f
            && K2O == -0.08f
            && P2O3 == 0.34f)
        {
            return "Olivine basalt";
        }
        if (SiO2 == 42.56f
            && TiO2 == 9.38f
            && Al2O3 == 12.03f
            && FeO == 11.27f
            && MnO == 0.17f
            && MgO == 9.7f
            && CaO == 10.52f
            && K2O == -0.28f
            && P2O3 == 0.44f)
        {
            return "Ilmenite basalt";
        }

        else {
            return "UNKNOWN";
        }


    }



}
