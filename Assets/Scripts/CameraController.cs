using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    const int orthographicSizeMin = 1;
    const int orthographicSizeMax = 6;
    public float sensitivityX = 8F;
    public float sensitivityY = 8F;
    public float speed = 0.5F;
    public float scrollSpeed = 50F;
    float mHdg = 0F;
    float mPitch = 0F;
    void Start()
    {
        // owt?
    }

    void Update()
    {
    }
    void LateUpdate()
 {
     //if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButton(1)))
        // return;
     float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
     float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;
        

        MoveForwards(Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);

        if (Input.GetKey(KeyCode.W))
         {
            
             MoveForwards(1);
             
         }
     if (Input.GetKey(KeyCode.S))
     {
         MoveBackwards(-1);
         
     }
     if (Input.GetKey(KeyCode.D))
     {
         MoveRight(1);
        
     }
     if (Input.GetKey(KeyCode.A))
     {
         MoveLeft(-1);
        
     }
     if (Input.GetMouseButton(1))
         {
             ChangeHeading(mouseX);
             ChangePitch(-mouseY);
         }
     
 }
    void MoveForwards(float aVal)
    {
        Vector3 fwd = transform.forward;
        //fwd.y = 0;
        fwd.Normalize();
        transform.position += aVal * fwd * speed;
    }

    void MoveBackwards(float aVal)
    {
        Vector3 fwd = transform.forward;
        //fwd.y = 0;
        fwd.Normalize();
        transform.position += aVal * fwd * speed;
    }

    void MoveLeft(float aVal)
    {
        Vector3 fwd = transform.right;
        fwd.y = 0;
        fwd.Normalize();
        transform.position += aVal * fwd * speed;
    }

    void MoveRight(float aVal)
    {
        Vector3 fwd = transform.right;
        fwd.y = 0;
        fwd.Normalize();
        transform.position += aVal * fwd * speed;
    }
    
    void ChangeHeading(float aVal)
    {
        mHdg += aVal;
        WrapAngle(ref mHdg);
        transform.localEulerAngles = new Vector3(mPitch, mHdg, 0);
    }
    void ChangePitch(float aVal)
    {
        mPitch += aVal;
        WrapAngle(ref mPitch);
        transform.localEulerAngles = new Vector3(mPitch, mHdg, 0);
    }
    public static void WrapAngle(ref float angle)
 {
     if (angle < -360F)
         angle += 360F;
     if (angle > 360F)
         angle -= 360F;
 }
}
