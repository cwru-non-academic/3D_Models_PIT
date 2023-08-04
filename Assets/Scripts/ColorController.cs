using UnityEngine.EventSystems;
using UnityEngine;
using System.Text;
using System.IO;
using System.Collections;

public class ColorController : MonoBehaviour
{

    [SerializeField] private int maxDistance;
    [SerializeField] private MeshFilter modelMesh;
    [SerializeField] private colorPicker picker;
    [SerializeField] private toolbox tools;

    private bool textureGrabed = false;
    private Mesh mesh;
    private Color[] colors;
    private bool pen = true;

    // logger
    private StringBuilder pitInfo;
    private string trialLogFile;

    void Start()
    {

        //starts logger A:\Documents\Unity Projects\CurrentProject\3D_Models_PIT\PITLog__2023_08_04_01_09_38.txt
        pitInfo = new StringBuilder();
        if (!textureGrabed)
        {
            mesh = modelMesh.mesh;
            Vector3[] vertices = mesh.vertices;

            // create new colors array where the colors will be created.
            colors = new Color[vertices.Length];

            textureGrabed = true;
        }
    }

    void Update()
    {
        if (Input.mousePresent && Input.GetMouseButton(0) && !mouseOverUI())
        {
            RayCastHit(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RayCastHit(Camera.main.ScreenPointToRay(Input.GetTouch(0).position));
        }
    }

    private void RayCastHit(Ray ray)
    {
        RaycastHit OriginalHit;
        if (Physics.Raycast(ray, out OriginalHit))
        {
            if(!textureGrabed)
            {
                mesh = OriginalHit.transform.GetComponent<MeshFilter>().mesh;
                Vector3[] vertices = mesh.vertices;

                // create new colors array where the colors will be created.
                colors = new Color[vertices.Length];

                textureGrabed = true;
            }

            int[] triangles = mesh.triangles;
            for(int i = 0; i<maxDistance; i++)
            {
                if(pen)
                {
                    colors[triangles[OriginalHit.triangleIndex * 3 + i]] = picker.getCurrentColor();
                } else
                {
                    colors[triangles[OriginalHit.triangleIndex * 3 + i]] = new Color(0,0,0,0);
                }
                
            }

            // assign the array of colors to the Mesh.
            mesh.colors = colors;

        }
    }
    
    public void setPen(bool usingPen)
    {
        pen = usingPen;
    }

    public bool getPen()
    {
        return pen;
    }

    private bool mouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void save()
    {
        Debug.Log("Logging");
        StartCoroutine(logTrial()); 
    }

    IEnumerator logTrial()
    {
        for(int i=0; i<colors.Length; i++)
        {
            pitInfo.Append(colors[i].r.ToString("0.00000")+",");
            pitInfo.Append(colors[i].g.ToString("0.00000") + ",");
            pitInfo.Append(colors[i].b.ToString("0.00000") + ",");
            pitInfo.Append(colors[i].a.ToString("0.00000"));
            pitInfo.AppendLine();
        }
        yield return null;
        StreamWriter file;
        try
        {

            // create log file if it does not already exist. Otherwise open it for appending new trial
            if (!File.Exists(trialLogFile))
            {
                trialLogFile = "PITLog_" + System.String.Format("{0:_yyyy_MM_dd_hh_mm_ss}", System.DateTime.Now) + ".txt";
                file = new StreamWriter(trialLogFile);
            }
            else
            {
                file = File.AppendText(trialLogFile);
            }
            file.WriteLine(pitInfo.ToString());
            file.Close();
            pitInfo = new StringBuilder();
            Debug.Log("FinishedLogging");
        }
        catch (System.Exception e)
        {
            Debug.Log("Error in accessing file: " + e);
        }
        yield return null;
    }

    public void load()
    {
        tools.UpdateLoadLog("Loading file...");
        StartCoroutine(loadTrial());
    }

    IEnumerator loadTrial()
    {
        StreamReader reader = null;
        string path = tools.getPath();
        yield return null;
        try
        {
            reader = new StreamReader(path);
            string readContents = reader.ReadToEnd();
            string[] lines = readContents.Split('\n');
            for (int i =0; i<lines.Length-2; i++)
            {
                string[] array = lines[i].Split(',');
                colors[i] = new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
            }
            mesh.colors = colors;
            tools.UpdateLoadLog("Finished loading");
        } catch (FileNotFoundException)
        {
            tools.UpdateLoadLog("File not found");
        }
        catch (DirectoryNotFoundException)
        {
            tools.UpdateLoadLog("Directory not found");
        }
        catch (System.Exception ex) when (ex is System.ArgumentNullException || ex is System.ArgumentException)
        {
            tools.UpdateLoadLog("Path is empty or invalid");
        }
        catch (System.FormatException)
        {
            tools.UpdateLoadLog("File contents where not in the correct format");
        }
        if (reader != null)
        {
            reader.Close();
        }
        tools.enableLoadBTN();
        yield return null;
    }
}
