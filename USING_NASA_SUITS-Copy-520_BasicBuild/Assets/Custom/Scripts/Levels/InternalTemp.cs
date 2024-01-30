using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InternalTemp : MonoBehaviour
{

    public GameObject internalTempBar;
    public Slider tempSlider;
    public TextMeshPro tempText;
    public Image Fill;

    public float dangerLowerBound;
    public float safeLowerBound;
    public float safeUpperBound;
    public float dangerUpperBound;

    //public float cautionLowerBoundCold;
    //public float cautionUpperBoundCold;

    //public float cautionLowerBoundHot;
    //public float cautionUpperBoundHot;

    private Color safeColor = Color.green;
    private Color cautionColor = Color.yellow;
    private Color dangerColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float currentTemp = tempSlider.value - 0.0001f; // TODO fix this to reflect the actual level from telemetry stream

        tempSlider.value = currentTemp;
        tempText.text = string.Format("{0} C", currentTemp.ToString("#.#"));

        // Color the bar to indicate the current state
        string state = checkState(currentTemp);
        switch (state)
        {
            case "SAFE":
                Fill.color = safeColor;
                break;
            case "DANGER":
                Fill.color = dangerColor;
                break;
            default:
                Fill.color = cautionColor;
                break;
        }

    }

    // Check the state 
    string checkState(float currentTemp)
    {

        // Check if the temperature is at a safe level
        if (currentTemp > safeLowerBound && currentTemp < safeUpperBound) return "SAFE";

        // Check if the temperature is at a dangerous level
        else if (currentTemp < dangerLowerBound || currentTemp > dangerUpperBound) return "DANGER";

        // Otherwise, caution
        else return "CAUTION";
    }

}