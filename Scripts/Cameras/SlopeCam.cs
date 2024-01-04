using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class SlopeCam : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;
    [SerializeField] CinemachineVirtualCamera slopeCam;
    [SerializeField] CinemachineVirtualCamera gameCam;
    public float rotationSpeed = 5f;
    public bool gameTimeActivated;
    List<CinemachineVirtualCamera> Cams;


    private void Start() 
    {
        Cams = new List<CinemachineVirtualCamera>{ followCam, slopeCam, gameCam};
    }

    private void Update() 
    {
        if (Input.GetKey(KeyCode.P))
        {
            // gameCam.enabled = true;
            // followCam.enabled = false;
            EnableCamera(gameCam);
        }

        // if (Input.GetKey(KeyCode.Q))
        // {
        //     // gameCam.enabled = false;
        //     // followCam.enabled = true;
        //     EnableCamera(followCam);
        // }
    }

    public void FocusCamera(string camChoice)
    {
        switch (camChoice)
        {
            case "follow":
                EnableCamera(followCam);
                break;
            case "slope":
                EnableCamera(slopeCam);
                break;
            case "game":
                EnableCamera(gameCam);
                break;
        }
    }

    private void EnableCamera(CinemachineVirtualCamera cam)
    {
        foreach (CinemachineVirtualCamera vCam in Cams)
        {
            if (vCam == cam) 
            {
                cam.enabled = true;
            } else 
            {
                vCam.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            EnableCamera(slopeCam);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (gameTimeActivated)
            {
                EnableCamera(gameCam);
            } else
            {
                EnableCamera(followCam);
            }
            
        }
    }
}
