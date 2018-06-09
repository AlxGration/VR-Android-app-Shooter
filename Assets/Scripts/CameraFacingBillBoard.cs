using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * скрипт отвечает за то, чтобы объект всегда 
 * находился "лицом" к взору пользователя
 * (установлен на голове кота, на Canvas'е с уровнем жезней)
 **/
public class CameraFacingBillBoard : MonoBehaviour {
    private Transform mainCameraTransform;

	void Start () {
        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
    private void Update()
    {
        transform.LookAt(mainCameraTransform);
    }
}
