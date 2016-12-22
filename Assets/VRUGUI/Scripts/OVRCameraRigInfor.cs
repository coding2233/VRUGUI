

using UnityEngine;
using System.Collections;

public class OVRCameraRigInfor : MonoBehaviour {
    /// <summary>
	/// The left eye camera.
	/// </summary>
	public Camera EyeCamera;

    /// <summary>
    /// Always coincides with average of the left and right eye poses.
    /// </summary>
    public Transform RayAnchor;
}
