using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolbox : MonoBehaviour
{
    [SerializeField] private GameObject colorWindow;
    [SerializeField] private GameObject manipulationWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toogleColorWindow()
    {
        colorWindow.SetActive(!colorWindow.activeSelf);
    }

    public void toogleManipulationWindow()
    {
        manipulationWindow.SetActive(!manipulationWindow.activeSelf);
    }
}
