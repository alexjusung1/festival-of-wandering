using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Yarn.Unity;

public class CameraSingleton : MonoBehaviour
{
    public static CameraSingleton CamSingle {get; private set;}
    private static GameObject gO;
    private static CinemachineVirtualCamera initCam;
    private static int priority;
    private static int currPriority;
    private static List<CinemachineVirtualCamera> switchedCameras;
    private CinemachineFramingTransposer transposer;

    [YarnCommand("switch_camera")]
    public static void SwitchCamera(GameObject gameObject2) {
        if (gO == gameObject2) {
            ClearSwitchedCameras();
        } else {
            currPriority++;
            CinemachineVirtualCamera camera = gameObject2.GetComponent<CinemachineVirtualCamera>();
            camera.m_Priority = currPriority;
            switchedCameras.Add(camera);
        }
    }

    public static void ClearSwitchedCameras() {
        foreach (CinemachineVirtualCamera camera in switchedCameras) {
            camera.m_Priority = priority - 1;
        }
        initCam.m_Priority = priority;
        currPriority = priority;
        switchedCameras = new List<CinemachineVirtualCamera>();
    }

    void Awake() {
        CamSingle = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gO = gameObject;
        initCam = GetComponent<CinemachineVirtualCamera>();
        priority = initCam.m_Priority;
        currPriority = priority;
        switchedCameras = new List<CinemachineVirtualCamera>();
        //transposer = cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();
        //PanCameraTo(GameObject.Find("Letter"));
    }
}