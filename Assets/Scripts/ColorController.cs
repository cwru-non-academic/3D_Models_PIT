using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{

    //[SerializeField] private Color[] colors;

    [SerializeField] private int maxDistance;

    private bool textureGrabed = false;
    private Texture2D tex;
    private Mesh mesh;
    private Color[] colors;

    void Update()
    {
        if (Input.mousePresent && Input.GetMouseButton(0))
        {
            RayCastHit(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RayCastHit(Camera.main.ScreenPointToRay(Input.GetTouch(0).position));
        }

        if (Input.GetKeyDown(KeyCode.R)){
            tex.Apply();
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
            /*for(int i = 0; i<maxDistance; i++)
            {
                colors[triangles[OriginalHit.triangleIndex * 3 + i]] = new Color(1, 0, 0, 1);
                //colors[triangles[hit.triangleIndex * 3 + 0]] = new Color(1, 0, 0, 1);
                //colors[triangles[hit.triangleIndex * 3 + 1]] = new Color(1, 0, 0, 1);
                //colors[triangles[hit.triangleIndex * 3 + 2]] = new Color(1, 0, 0, 1);
            }*/

            colors[triangles[OriginalHit.triangleIndex * 3 + 0]] = new Color(1, 0, 0, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 + 1]] = new Color(1, 0, 0, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 + 2]] = new Color(1, 0, 0, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 + 3]] = new Color(0, 1, 0, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 + 4]] = new Color(0, 1, 0, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 + 5]] = new Color(0, 1, 0, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 - 3]] = new Color(0, 0, 1, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 - 2]] = new Color(0, 0, 1, 1);
            colors[triangles[OriginalHit.triangleIndex * 3 - 1]] = new Color(0, 0, 1, 1);

            /*for (int i = 0; i < vertices.Length; i++)
            {
                int trans = Random.Range(0, 2);
                colors[i] = new Color(1, 0, 0, trans);

            }*/

            // assign the array of colors to the Mesh.
            mesh.colors = colors;

            /*Vector2 pixelUV = hit.textureCoord;
            pixelUV.x = (int)(pixelUV.x *tex.width);
            pixelUV.y = (int)(pixelUV.y * tex.height);

            Debug.Log(pixelUV);

            for(int i= (int) pixelUV.x-5; i< (int)pixelUV.x; i++)
            {
                for (int j = (int)pixelUV.y - 5; j < (int)pixelUV.y; j++)
                {
                    if (i >= 0 && j>=0)
                    {
                        tex.SetPixel(i, j, Color.red);
                    }
                }
                    
            }*/


        }
    }
}
