using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] PlayerController controller;

    [SerializeField] private Camera mainCam;


    private static event Action Shake;
    public static void Invoke()
    {
        Shake?.Invoke();//if Shake is not Null, Shake it.
    }

    private void OnEnable()
    {
        Shake += CameraShake;
    }
    private void OnDisable()
    {
        Shake -= CameraShake;
    }

    private void CameraShake()
    {
        
        mainCam.DOComplete();

        if(controller.moveSpeed <= 10)
        {
            mainCam.DOShakePosition(0.3f, 0.1f); 
            mainCam.DOShakeRotation(0.3f, 0.1f); 

        }


        if (controller.moveSpeed > 10f && controller.moveSpeed <= 25f)
        {
            mainCam.DOShakePosition(0.3f, 0.3f);
            mainCam.DOShakeRotation(0.3f, 0.3f);

        }


        if (controller.moveSpeed > 25f && controller.moveSpeed <= 50f)
        {
            mainCam.DOShakePosition(0.3f, 0.5f);
            mainCam.DOShakeRotation(0.3f, 0.5f);

        }

        if (controller.moveSpeed > 50f && controller.moveSpeed <= 75f)
        {
            mainCam.DOShakePosition(0.3f, 0.75f);
            mainCam.DOShakeRotation(0.3f, 0.75f);

        }
        if (controller.moveSpeed > 75f && controller.moveSpeed <= 125f)
        {
            mainCam.DOShakePosition(0.3f, 1.5f);
            mainCam.DOShakeRotation(0.3f, 1.5f);

        }

        if (controller.moveSpeed > 125f)
        {
            mainCam.DOShakePosition(0.3f, 2f);
            mainCam.DOShakeRotation(0.3f, 2f);

        }
    }

}
