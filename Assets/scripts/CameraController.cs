//Initialized by Philippe Anderson
//https://stonmanblog.wordpress.com
//sorry the English  rsrsrs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Space(10)]
    [Header("OBJECT TO SHOW")]
    [SerializeField] Transform target; // Object to view

    [Space(10)]
    [Header("CONFIG")]
    [SerializeField] float distance = 3.0f; // Distance between camera and the target
    [SerializeField] float minAngleY = -60.0f; // Minimum angle for camera rotation on target Y axis
    [SerializeField] float maxAngleY = 30.0f; // Maximo angle for camera rotation on target Y axis

    [Space(10)]
    // This value is set for the perspective camera. The equivalent values in camera orthografica are 1 - 10
    [SerializeField] float minZoom = 10f; 
    [SerializeField] float maxZoom = 60f;
    float ZoomValue;

    // Current camera angle of rotation
    float currentX = 0.0f;
    float currentY = 0.0f;
    
    Vector2 mouseMoviment; // Pick up the mouse coordinates
    bool click = false; //Condition to make the drag and drop

    Camera cam; //Scene camera
    

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        ZoomValue = maxZoom; //Synchronizes the variables

    }

    void Update () {

        // Frag and Drop to rotate the camera
        click = Input.GetKey(KeyCode.Mouse0);
		mouseMoviment = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Pick up the mouse scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll < 0f) ZoomValue++;
        else if (scroll > 0f) ZoomValue--;

        //Conditions for maintaining the fixed zoom values
        if (ZoomValue > maxZoom) ZoomValue = maxZoom;
        if (ZoomValue < minZoom) ZoomValue = minZoom;

        cam.orthographicSize = ZoomValue; // Used to Orthografic projection 
        //OR
        cam.fieldOfView = ZoomValue;      // Used to Orthografic projection

        if (click)
        {
            currentX += mouseMoviment.x;
            currentY += mouseMoviment.y;

            currentY = Mathf.Clamp(currentY, minAngleY, maxAngleY);
        }
    }

    private void LateUpdate()
    {
        if (transform == null) return;
        if (target == null) return;

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);
        transform.position = target.position + rotation * dir;
        transform.LookAt(target.position);

    }
    
    
}
