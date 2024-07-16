using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class manipulations : MonoBehaviour
{
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private GameObject cameraView;
    public bool move { get; set; } //bool to set if movement option is selected
    public bool rotate { get; set; } // bool to set if rotation option is selected. Only one (move or rotate) can be true at once.
    public float mouseScaling;
    public float scrollScaling;
    public float movementScaling;
    public float rotationScaling;

    private Vector3 currentRotation; //holds chnages in rotation
    private Vector3 currentPosition; //holds chnages in position
    private Vector3 cameraOriginalPosition; // holds the original cmaera position for reset functions


    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        rotate = true;
        move = false;
        cameraOriginalPosition = cameraView.transform.localPosition;
        currentPosition = new Vector3(0, 0, 0);
        currentRotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) || Input.mouseScrollDelta.y !=0)
        {
            if (move) //assign detected mouse movement to move or rotate if applicable
            {
                currentPosition = currentPosition + new Vector3( Input.GetAxis("Mouse Y") * mouseScaling,Input.mouseScrollDelta.y * scrollScaling, Input.GetAxis("Mouse X") * mouseScaling) * movementScaling;
            }
            else if (rotate)
            {
                currentRotation = currentRotation + new Vector3(Input.GetAxis("Mouse X") * mouseScaling*-1,  Input.mouseScrollDelta.y * scrollScaling, Input.GetAxis("Mouse Y") * mouseScaling) * rotationScaling;
            }
        }
        cameraView.transform.localPosition = cameraOriginalPosition + currentPosition;
        cameraHolder.transform.localEulerAngles = currentRotation;
    }

    public void flip()
    {
        currentRotation= new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + 180.0f);
        cameraHolder.transform.localEulerAngles = currentRotation;
    }
    
    public void resetView()
    {
        currentRotation = new Vector3(0, 0, 0);
        cameraHolder.transform.localEulerAngles = currentRotation;
    }
}
