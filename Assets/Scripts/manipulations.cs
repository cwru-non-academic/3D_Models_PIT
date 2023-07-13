using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulations : MonoBehaviour
{
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private GameObject cameraView;
    public float scale { get; set; }
    public float xRotation { get; set; }
    public float yRotation { get; set; }
    public float zRotation { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraView.transform.position = new Vector3(cameraView.transform.position.x, scale, cameraView.transform.position.z);
        //cameraHolder.transform.localEulerAngles = new Vector3(xRotation, yRotation, zRotation);
    }

    
}
