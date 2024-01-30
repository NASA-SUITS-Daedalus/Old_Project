using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AlertsTextHandler : MonoBehaviour
{

    class Alert
    {

        private float nomMax;
        private float nomMin;
        private float errorMax;
        private float errorMin;
        private string name;
        public float currentValue;

        public Alert(float nMax, float nMin, float eMax, float eMin, string n)
        {
            nomMax = nMax;
            nomMin = nMin;
            errorMax = eMax;
            errorMin = eMin;
            name = n;
        }

        public void setValue(float val)
        {
            currentValue = val;
        }

        public bool isInErrorRange()
        {
            return (currentValue <= errorMin || currentValue >= errorMax);
        }

        public bool isInNominalRange()
        {
            return (currentValue <= nomMin || currentValue >= nomMax);
        }

        public bool ErrorTooLow()
        {
            return (currentValue <= errorMin);
        }

        public bool ErrorTooHigh()
        {
            return (currentValue >= errorMax);
        }

        public bool NominalTooLow()
        {
            return (currentValue <= nomMin);
        }

        public bool NominalTooHigh()
        {
            return (currentValue >= nomMax);
        }

    }

    // objects
    private Alert heartRateAlert;
    private Alert suitPressureAlert;
    private Alert fanAlert;
    private Alert o2PressureAlert;
    private Alert o2RateAlert;
    private Alert batteryCapacityAlert;

    // text meshes (to display in the HUD; create in Unity)
    public TextMeshPro heartRateAlertTextMesh;
    public TextMeshPro suitPressureAlertTextMesh;
    public TextMeshPro fanAlertTextMesh;
    public TextMeshPro o2PressureAlertTextMesh;
    public TextMeshPro o2RateAlertTextMesh;
    public TextMeshPro batteryCapacityAlertTextMesh;

    // Start is called before the first frame update
    void Start()
    {

        // Initialize all the alert objects
        heartRateAlert = new Alert(100.0f, 80.0f, 120.0f, 101.0f, "HEART RATE");
        suitPressureAlert = new Alert(4.0f, 2.0f, 1.9f, 1.0f, "SUIT PRESSURE");
        fanAlert = new Alert(40000.0f, 10000.0f, 9999.0f, 1000.0f, "FAN TACHOMETER");
        o2PressureAlert = new Alert(950.0f, 750.0f, 749.0f, 650.0f, "O2 PRESSURE");
        o2RateAlert = new Alert(1.0f, 0.5f, 0.4f, 0.1f, "O2 RATE");
        batteryCapacityAlert = new Alert(30.0f, 0.0f, 50.0f, 31.0f, "BATTERY CAPACITY");
        return;

    }

    // Update is called once per frame
    void Update()
    {
        // // Get the current value from the telemetry stream
        // int currentVal = 0; // TODO update later

        // // Check if the value is past the allowed bound
        // if ((isLowerBound && currentVal < threshold) || (!isLowerBound && currentVal > threshold))
        // {

        //     // Activate the alert if it is not already active
        //     if (gameObject.activeSelf) {
        //         alertObject.SetActive(true);
        //     }

        // }

        // Update the fields at every frame
        UpdateTelemetryFields();

        // Check for being in range
        List<Alert> alertList = new List<Alert>()
        {
            heartRateAlert,
            suitPressureAlert,
            fanAlert,
            o2PressureAlert,
            o2RateAlert,
            batteryCapacityAlert
        };

        List<TextMeshPro> alertTextList = new List<TextMeshPro>()
        {
            heartRateAlertTextMesh,
            suitPressureAlertTextMesh,
            fanAlertTextMesh,
            o2PressureAlertTextMesh,
            o2RateAlertTextMesh,
            batteryCapacityAlertTextMesh
        };

        for (int i = 0; i < 6; i++)
        {
            if (alertList[i].ErrorTooLow())
            {
                // TODO add message
                //alertTextList[i].text = String.Format("{0} is TOO LOW ({1}).", alertList[i].name, alertList[i].currentValue);
                alertTextList[i].text = "error too low"; // TODO fix
                return;
            }
            else if (alertList[i].ErrorTooHigh())
            {
                // TODO add message
                alertTextList[i].text = "error too high"; // TODO fix
                return;
            }
            else if (alertList[i].NominalTooLow())
            {
                // TODO add message
                alertTextList[i].text = "nominal too low"; // TODO fix
                return;
            }
            else if (alertList[i].NominalTooHigh())
            {
                // TODO add message
                alertTextList[i].text = "nominal too high"; // TODO fix
                return;
            }
        }

    }

    // Update all the necessary fields
    void UpdateTelemetryFields()
    {

        heartRateAlert.setValue(TelemetryStreamClient.heart_rate);
        suitPressureAlert.setValue(TelemetryStreamClient.suits_pressure);
        fanAlert.setValue(TelemetryStreamClient.fan_tachometer);
        o2PressureAlert.setValue(TelemetryStreamClient.o2_pressure);
        o2RateAlert.setValue(TelemetryStreamClient.o2_rate);
        batteryCapacityAlert.setValue(TelemetryStreamClient.battery_capacity);

    }
}