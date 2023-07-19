using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class manipulations : MonoBehaviour
{
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private GameObject cameraView;
    public float scale { get; set; }
    public float xRotation { get; set; }
    public float yRotation { get; set; }
    public float zRotation { get; set; }
    private Vector3 cameraOriginalPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraOriginalPosition = cameraView.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        cameraView.transform.localPosition = new Vector3(cameraOriginalPosition.x, cameraOriginalPosition.y-scale , cameraOriginalPosition.z);
        cameraHolder.transform.localEulerAngles = new Vector3(xRotation, yRotation, zRotation);
    }

    
}
