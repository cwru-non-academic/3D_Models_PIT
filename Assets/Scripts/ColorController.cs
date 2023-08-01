using UnityEngine.EventSystems;
using UnityEngine;

public class ColorController : MonoBehaviour
{

    //[SerializeField] private Color[] colors;

    [SerializeField] private int maxDistance;
    [SerializeField] private colorPicker picker;

    private bool textureGrabed = false;
    private Texture2D tex;
    private Mesh mesh;
    private Color[] colors;
    private bool pen = true;

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

                /*tex = Instantiate(rend.material.GetTexture("_MainTex")) as Texture2D;
                rend.material.SetTexture("_MainTex", tex);*/
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
}
