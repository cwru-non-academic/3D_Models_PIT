using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Michsky.MUIP;

public class toolbox : MonoBehaviour
{
    [SerializeField] private GameObject colorWindow;
    [SerializeField] private GameObject loadWindow;
    [SerializeField] private TextMeshProUGUI loadLog;
    [SerializeField] private TMP_InputField pathInput;
    [SerializeField] private manipulations manipulationScript;
    [SerializeField] private ColorController colorCTR;
    [SerializeField] private ButtonManager loadBTN;
    [SerializeField] private colorPicker picker;

    //buton color backgrounds
    [SerializeField] private Image BGMove;
    [SerializeField] private Image HighMove;
    [SerializeField] private Image BGRotate;
    [SerializeField] private Image HighRotate;
    [SerializeField] private Image BGPen;
    [SerializeField] private Image HighPen;
    [SerializeField] private Image BGEraser;
    [SerializeField] private Image HighEraser;
    [SerializeField] private Image BGColor;
    [SerializeField] private Image HighColor;
    [SerializeField] private Image BGCostumeColor;
    [SerializeField] private Image HighCostumeColor;
    [SerializeField] private Image BGPresetColor;
    [SerializeField] private Image HighPresetColor;

    //for presets
    [SerializeField] private Image BGPreset1Color;
    [SerializeField] private Image HighPreset1Color;
    [SerializeField] private Image BGPreset2Color;
    [SerializeField] private Image HighPreset2Color;
    [SerializeField] private Image BGPreset3Color;
    [SerializeField] private Image HighPreset3Color;
    [SerializeField] private Image BGPreset4Color;
    [SerializeField] private Image HighPreset4Color;
    [SerializeField] private Image BGPreset5Color;
    [SerializeField] private Image HighPreset5Color;
    [SerializeField] private Image BGPreset6Color;
    [SerializeField] private Image HighPreset6Color;
    [SerializeField] private Image BGPreset7Color;
    [SerializeField] private Image HighPreset7Color;
    [SerializeField] private Image BGPreset8Color;
    [SerializeField] private Image HighPreset8Color;
    [SerializeField] private Image BGPreset9Color;
    [SerializeField] private Image HighPreset9Color;
    [SerializeField] private Image BGPreset0Color;
    [SerializeField] private Image HighPreset0Color;

    //button colors
    [SerializeField] private Color BGOn;
    [SerializeField] private Color BGOff;
    [SerializeField] private Color HighOn;
    [SerializeField] private Color HighOff;

    //Misc vars
    [SerializeField] private float darkenFactor;
    // Start is called before the first frame update
    void Start()
    {
        updateBtns();
        setPresetWindow(BGPreset1Color, HighPreset1Color, picker.Preset1);
        setPresetWindow(BGPreset2Color, HighPreset2Color, picker.Preset2);
        setPresetWindow(BGPreset3Color, HighPreset3Color, picker.Preset3);
        setPresetWindow(BGPreset4Color, HighPreset4Color, picker.Preset4);
        setPresetWindow(BGPreset5Color, HighPreset5Color, picker.Preset5);
        setPresetWindow(BGPreset6Color, HighPreset6Color, picker.Preset6);
        setPresetWindow(BGPreset7Color, HighPreset7Color, picker.Preset7);
        setPresetWindow(BGPreset8Color, HighPreset8Color, picker.Preset8);
        setPresetWindow(BGPreset9Color, HighPreset9Color, picker.Preset9);
        setPresetWindow(BGPreset0Color, HighPreset0Color, picker.Preset0);
    }

    // Update is called once per frame
    void Update()
    {
        updateColor(picker.getCurrentColor());
    }
    private void setPresetWindow(Image BG, Image High, Color c)
    {
        BG.color = c;
        High.color = darkenColor(c);
    }

    public void toogleColorWindow()
    {
        colorWindow.SetActive(!colorWindow.activeSelf);
    }

    public void toogleLoadWindow()
    {
        loadWindow.SetActive(!loadWindow.activeSelf);
    }

    public void updateColor(Color c)
    {
        BGColor.color = c;
        HighColor.color = darkenColor(c);
    }

    private Color darkenColor(Color c)
    {
        return new Color(c.r*darkenFactor, c.g*darkenFactor, c.b*darkenFactor, c.a);
    }

    public void updateBtns()
    {
        if (manipulationScript.move)
        {
            BGMove.color = BGOn;
            HighMove.color = HighOn;
            BGRotate.color = BGOff;
            HighRotate.color = HighOff;
        } else
        {
            BGMove.color = BGOff;
            HighMove.color = HighOff;
            BGRotate.color = BGOn;
            HighRotate.color = HighOn;
        }
        if (colorCTR.getPen())
        {
            BGPen.color = BGOn;
            HighPen.color = HighOn;
            BGEraser.color = BGOff;
            HighEraser.color = HighOff;
        }
        else 
        {
            BGPen.color = BGOff;
            HighPen.color = HighOff;
            BGEraser.color = BGOn;
            HighEraser.color = HighOn;
        } if(colorWindow.activeSelf)
        {
            if(colorCTR.getColorPresetsOn())
            {
                BGCostumeColor.color = BGOff;
                HighCostumeColor.color = HighOff;
                BGPresetColor.color = BGOn;
                HighPresetColor.color = HighOn;
            } else
            {
                BGCostumeColor.color = BGOn;
                HighCostumeColor.color = HighOn;
                BGPresetColor.color = BGOff;
                HighPresetColor.color = HighOff;
            }
        }
    }



    public void UpdateLoadLog( string log)
    {
        loadLog.text = log;
    }

    public string getPath()
    {
        return pathInput.text;
    }

    public void enableLoadBTN()
    {
        loadBTN.Interactable(true);
    }
}
