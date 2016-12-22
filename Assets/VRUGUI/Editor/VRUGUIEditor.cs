using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using UnityEngine.EventSystems;

public class VRUGUIEditor : EditorWindow {

    [MenuItem("VR UGUI/Create")]
    static void main()
    {
        GetWindow<VRUGUIEditor>();
    }


    Vector2 scrollPos;
    Camera _EyeCamera;
    Transform _RayTransform;
     
    void OnGUI()
    {
        scrollPos=GUILayout.BeginScrollView(scrollPos);
        GUILayout.Label("如果需要使用htc，则取消OVRInputModule脚本#define HTCVIVE的注释。"
            +"\n如果需要使用Oculus，则注释掉OVRInputModule脚本#define HTCVIVE。"
            +"\n同时注意Players Settings/Virtual Reality SDKs的OpenVR与Oculus。");
        GUILayout.Label("");
       _EyeCamera = EditorGUILayout.ObjectField("眼睛摄像机:",_EyeCamera, typeof(Camera),true) as Camera;
        _RayTransform= EditorGUILayout.ObjectField("右手手柄:", _RayTransform, typeof(Transform), true) as Transform;
        GUILayout.Label("");
        if (GUILayout.Button("create"))
        {
            if (_EyeCamera == null || _RayTransform == null)
            {
                EditorUtility.DisplayDialog("error", "参数不能为null", "OK");
                return;
            }

            Canvas _UICanvas = new GameObject("Canvas",typeof(Canvas)).GetComponent<Canvas>();
            _UICanvas.gameObject.GetComponent<Canvas>().worldCamera = _EyeCamera;
            _UICanvas.gameObject.AddComponent<CanvasScaler>();
            _UICanvas.gameObject.AddComponent<OVRRaycaster>();
            _UICanvas.gameObject.AddComponent<OVRCameraRigInfor>().EyeCamera= _EyeCamera;
            _UICanvas.gameObject.GetComponent<OVRCameraRigInfor>().RayAnchor = _RayTransform;
            _UICanvas.transform.localPosition = new Vector3(0, 0, 2);
            _UICanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(604, 272);
            _UICanvas.transform.localScale = new Vector3(0.007352941f, 0.007352941f, 0.007352941f);

            GameObject _OVRPointer = new GameObject("OVRPointer");
            _OVRPointer.AddComponent<Image>().rectTransform.sizeDelta=new Vector2(32,32);
            _OVRPointer.AddComponent<OVRGazePointer>().cameraRig= _UICanvas.GetComponent<OVRCameraRigInfor>();
            _OVRPointer.transform.parent = _UICanvas.transform;


            OVRInputModule _UIInputModule = new GameObject("EventSystem", typeof(OVRInputModule)).GetComponent<OVRInputModule>();
            _UIInputModule.rayTransform = _RayTransform;
            if (_RayTransform.GetComponent<SteamVR_TrackedObject>() != null)
                _UIInputModule.rightTracked = _RayTransform.GetComponent<SteamVR_TrackedObject>();
        }
        GUILayout.EndScrollView();
    }
}
