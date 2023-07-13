using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorPicker : MonoBehaviour
{
    [SerializeField] private Image colorDisplay;
    public float rColor { get; set; }
    public float gColor { get; set; }
    public float bColor { get; set; }

    private Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = new Color(0, 0, 1, 1);
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
}
