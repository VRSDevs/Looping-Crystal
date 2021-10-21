using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{

    public CinemachineVirtualCamera cameraCine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Camara1"))
        {
            cameraCine.m_Lens.OrthographicSize = 5;
        }
        else if(collision.CompareTag("Camara2"))
        {
            cameraCine.m_Lens.OrthographicSize = 10;
        }
        else
        {
            cameraCine.m_Lens.OrthographicSize = 5;
        }

    }

}
