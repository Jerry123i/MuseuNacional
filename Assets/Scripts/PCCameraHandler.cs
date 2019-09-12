using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCCameraHandler : MonoBehaviour
{
    public float speed;
    public float zoomRate;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        if (Input.GetKey("q"))
        {
            camera.orthographicSize += zoomRate * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            camera.orthographicSize -= zoomRate * Time.deltaTime;
        }

    }

}
