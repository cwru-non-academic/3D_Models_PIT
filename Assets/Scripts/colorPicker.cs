using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorPicker : MonoBehaviour
{
    [SerializeField] private Image colorDisplay;

    public Color Preset1;
    public Color Preset2;
    public Color Preset3;
    public Color Preset4;
    public Color Preset5;
    public Color Preset6;
    public Color Preset7;
    public Color Preset8;
    public Color Preset9;
    public Color Preset0;
    public float rColor { get; set; }
    public float gColor { get; set; }
    public float bColor { get; set; }

    private Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        rColor = Preset1.r;
        gColor = Preset1.g;
        bColor = Preset1.b;
    }

    // Update is called once per frame
    void Update()
    {
        currentColor = new Color(rColor, gColor, bColor, 1);
        colorDisplay.color = currentColor;
    }

    public Color getCurrentColor()
    {
        return currentColor;
    }

    public void setPreset(int num)
    {
        switch(num)
        {
            case 1:
                rColor = Preset1.r;
                gColor = Preset1.g;
                bColor = Preset1.b;
                break;
            case 2:
                rColor = Preset2.r;
                gColor = Preset2.g;
                bColor = Preset2.b;
                break;
            case 3:
                rColor = Preset3.r;
                gColor = Preset3.g;
                bColor = Preset3.b;
                break;
            case 4:
                rColor = Preset4.r;
                gColor = Preset4.g;
                bColor = Preset4.b;
                break;
            case 5:
                rColor = Preset5.r;
                gColor = Preset5.g;
                bColor = Preset5.b;
                break;
            case 6:
                rColor = Preset6.r;
                gColor = Preset6.g;
                bColor = Preset6.b;
                break;
            case 7:
                rColor = Preset7.r;
                gColor = Preset7.g;
                bColor = Preset7.b;
                break;
            case 8:
                rColor = Preset8.r;
                gColor = Preset8.g;
                bColor = Preset8.b;
                break;
            case 9:
                rColor = Preset9.r;
                gColor = Preset9.g;
                bColor = Preset9.b;
                break;
            case 0:
                rColor = Preset0.r;
                gColor = Preset0.g;
                bColor = Preset0.b;
                break;
            default:
                rColor = Preset1.r;
                gColor = Preset1.g;
                bColor = Preset1.b;
                break;
        }
    }
}
