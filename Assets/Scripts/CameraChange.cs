using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject[] cameras;
    public int activeCamera;

    private void Start()
    {
        activeCamera = 0;
        foreach(GameObject cam in cameras)
        {
            cam.SetActive(false);
        }
        cameras[activeCamera].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        //Для смены камеры была выбрана клавиша С
        if (Input.GetKeyDown(KeyCode.C))
            ChangeCamera();
    }

    void ChangeCamera()
    {
        cameras[activeCamera].SetActive(false);//выключаем старую камеру
        if (activeCamera + 1 >= cameras.Length)
            activeCamera = 0;
        else
            activeCamera += 1;//шагаем по камерам последовательно

        cameras[activeCamera].SetActive(true);
    }
}
