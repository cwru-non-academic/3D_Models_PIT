using UnityEngine.EventSystems;
using UnityEngine;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System;

public class ColorController : MonoBehaviour
{

    [SerializeField] private int maxDistance;
    [SerializeField] private MeshFilter modelMesh;
    [SerializeField] private colorPicker picker;
    [SerializeField] private toolbox tools;
    [SerializeField] private int maxMemory;

    private bool textureGrabed = false;
    private Mesh mesh;
    private Color[] colors;
    private List<Color[]> memory;
    private bool pen = true;
    private bool colorPresetsOn = true;
    private int memoryCount = -1;

    // logger
    private StringBuilder pitInfo;
    private string trialLogFile;

    void Start()
    {
        //starts logger A:\Documents\Unity Projects\CurrentProject\3D_Models_PIT\PITLog__2023_08_04_01_09_38.txt
        pitInfo = new StringBuilder();
        if (!textureGrabed)
        {
            resetColorMesh();
            textureGrabed = true;
        }
    }

    void Update()
    {
        if (Input.mousePresent && Input.GetMouseButton(0) && !mouseOverUI())
        {
            RayCastHit(Camera.main.ScreenPointToRay(Input.mousePosition));
        } else if(Input.mousePresent && Input.GetMouseButtonUp(0) && !mouseOverUI())
        {
            //when click is release save trace
            if (memoryCount < memory.Count - 1)
            {
                //overide history
                memory[memoryCount + 1] = (Color[]) colors.Clone();
                while (memory.Count-1 > memoryCount+1)
                {
                    memory.RemoveAt(memory.Count-1);
                }
            }
            else
            {
                if (memory.Count > maxMemory)
                {
                    memory.RemoveAt(0);
                    memoryCount--;
                }
                memory.Add((Color[])colors.Clone());
            }
            memoryCount++;
        }
        //else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
            //RayCastHit(Camera.main.ScreenPointToRay(Input.GetTouch(0).position));
        //}
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
                memory.Add((Color[])colors.Clone());
                memoryCount++;

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

    public void resetColorMesh()
    {
        memoryCount = -1;
        mesh = modelMesh.mesh;
        Vector3[] vertices = mesh.vertices;

        // create new colors array where the colors will be created.
        colors = new Color[vertices.Length];
        // create a empty memory list
        memory = new List<Color[]>();
        memory.Add((Color[])colors.Clone());
        memoryCount++;
        // assign the array of colors to the Mesh.
        mesh.colors = colors;
    }
    
    public void setPen(bool usingPen)
    {
        pen = usingPen;
    }

    public bool getPen()
    {
        return pen;
    }

    public void setPresetsOn(bool on)
    {
        colorPresetsOn = on;
    }

    public bool getColorPresetsOn()
    {
        return colorPresetsOn;
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

    public void undo()
    {
        if(memoryCount>0)
        {
            memoryCount--;
            colors = (Color[]) memory[memoryCount].Clone();
            mesh.colors = colors;
        }   
    }

    public void redo()
    {
        if (memoryCount < memory.Count-1)
        {
            memoryCount++;
            colors = (Color[])memory[memoryCount].Clone();
            mesh.colors = colors;
        }
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
            int l = 0;
            int c= 0;
            if(lines.Length<colors.Length)
            {
                l = lines.Length;
            } else
            {
                l = colors.Length;
            }
            for (int i =0; i<l; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]) && lines[i] != "\n" && lines[i] != "\r") {
                    string[] array = lines[i].Split(',');
                    colors[c] = new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
                    c++;
                }
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
    //C:\Users\lemfn\Documents\GitHub\3D_Models_PIT\Builds\PITLog__2024_07_16_04_50_54.txt
}
