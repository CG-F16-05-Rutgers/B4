using UnityEngine;
using System.Collections;

public class CameraControllerFollow : MonoBehaviour
{
    
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    /**
    public Transform target;
    public float smoothTime = 0.1f;
    public float speed = 3.0f;
    public float followDistance = 10f;
    public float verticalBuffer = 1.5f;
    public float horizontalBuffer = 0f;
    private Vector3 velocity = Vector3.zero;

    public Quaternion rotation = Quaternion.identity;
    public float yRotation = 0.0f;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(horizontalBuffer, followDistance, verticalBuffer));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + offset, ref velocity, smoothTime);

        transform.eulerAngles = new Vector3(45, target.transform.eulerAngles.y, 0);
    }
    **/
}
 