using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine;

public class OrbitCam : MonoBehaviour
{
    public float rotationSpeed = 5f;
    [SerializeField] CinemachineVirtualCamera followCam;
    CinemachineOrbitalTransposer transposer;
    void Awake()
    {
         transposer = followCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            float mouseX = Mouse.current.delta.x.ReadValue();
            Debug.Log("Mouse X delta: " + mouseX);

            transposer.m_Heading.m_Bias += mouseX * rotationSpeed * Time.deltaTime;

         
        }
    }
}
