using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed;

    public bool canZoom = false;
    public bool zoomBack;

    public Transform zoomPos;

    private Vector3 startPos;


    /// <summary>
    /// Start Camera Fight Position = 0, 8.44, -4.9 
    ///
    /// </summary>

    void Awake()
    {
      //  startPos = transform.position;  when game actually starts
    }

    void Start ()
    {
		
	}

	void Update ()
    {
        if (canZoom)
        {
            transform.position = Vector3.Lerp(transform.position, zoomPos.position, speed * Time.deltaTime);
        }
	}
}
