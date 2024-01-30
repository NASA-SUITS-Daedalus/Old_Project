using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenLevel : MonoBehaviour
{

    public GameObject oxygenLevelBar;
    public Slider oxygenSlider;
    public TextMeshPro percentageText;
    public Image Fill;
    public float cautionUpperBound;
    public float dangerUpperBound;

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

        //float oxygenPercent = oxygenSlider.value - 0.0001f; // TODO fix this to reflect the actual level from telemetry stream
        float oxygenPercent = (float) TelemetryStreamClient.primary_oxygen;

        oxygenSlider.value = oxygenPercent;
        percentageText.text = string.Format("{0}%", oxygenPercent.ToString("#.#"));

        // Color the bar to indicate the current state
        string state = checkState(oxygenPercent);
        switch (state)
        {
            case "SAFE":
                //Fill.color = Color.Lerp(safeColor, cautionColor, (100 - oxygenPercent) / 100); 
                Fill.color = safeColor;
                break;
            case "DANGER":
                Fill.color = dangerColor;
                break;
            default:
                //Fill.color = Color.Lerp(cautionColor, dangerColor, (cautionUpperBound - oxygenPercent) / cautionUpperBound); 
                Fill.color = cautionColor;
                break;
        }

    }

    // Check the state 
    string checkState(float oxygenPercent)
    {

        // Check if the oxygen level is at a safe level
        if (oxygenPercent > cautionUpperBound) return "SAFE";

        // Check if the oxygen level is at a dangerous level
        else if (oxygenPercent < dangerUpperBound) return "DANGER";

        // Otherwise, caution
        else return "CAUTION";
    }


}
