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

    //buton color backgrounds
    [SerializeField] private Image BGMove;
    [SerializeField] private Image HighMove;
    [SerializeField] private Image BGRotate;
    [SerializeField] private Image HighRotate;
    [SerializeField] private Image BGPen;
    [SerializeField] private Image HighPen;
    [SerializeField] private Image BGEraser;
    [SerializeField] private Image HighEraser;

    //button colors
    [SerializeField] private Color BGOn;
    [SerializeField] private Color BGOff;
    [SerializeField] private Color HighOn;
    [SerializeField] private Color HighOff;
    // Start is called before the first frame update
    void Start()
    {
        updateColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toogleColorWindow()
    {
        colorWindow.SetActive(!colorWindow.activeSelf);
    }

    public void toogleLoadWindow()
    {
        loadWindow.SetActive(!loadWindow.activeSelf);
    }

    public void updateColors()
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
