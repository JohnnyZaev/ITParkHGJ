using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseControlCamera : MonoBehaviour
{
   public CinemachineFreeLook freeLookCamera; // Ссылка на компонент Cinemachine FreeLook Camera

    private void Update()
    {
        // Получить входные данные мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Передать входные данные в компонент Cinemachine FreeLook Camera
        freeLookCamera.m_XAxis.m_InputAxisValue = mouseX;
        freeLookCamera.m_YAxis.m_InputAxisValue = mouseY;
    }
}
