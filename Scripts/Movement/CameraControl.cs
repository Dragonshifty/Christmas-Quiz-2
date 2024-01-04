using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam1;
    [SerializeField] CinemachineVirtualCamera cam2;
    Collider camCollOne;
    public bool camSwitch = false;
    void Start()
    {
        // camCollOne = GetComponent<Collider>();
        // if (camCollOne == null)
        // {
        //     Debug.LogError("Collider component not found on the GameObject.");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (!camSwitch)
            {
                cam1.gameObject.SetActive(false);
                cam2.gameObject.SetActive(true);
                camSwitch = true;
            } else
            {
                cam1.gameObject.SetActive(true);
                cam2.gameObject.SetActive(false);
                camSwitch = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Yep");
        
        if (!camSwitch && other.gameObject.tag.Equals("Player"))
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            camSwitch = true;
        } else if (camSwitch && other.gameObject.tag.Equals("Player"))
        {
            cam1.gameObject.SetActive(true);
            cam2.gameObject.SetActive(false);
            camSwitch = false;
        }
    }
}
