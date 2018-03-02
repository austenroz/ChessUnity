using UnityEngine;
using UnityEngine;
using System.Collections;

//[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom")]
public class CameraAndCursor : MonoBehaviour
{

    public Transform target;
    public float distance = 8.0f;
    public float xSpeed = 30.0f;
    public float ySpeed = 50.0f;

    private Vector3 moveNewPosition;
    public float xMoveSpeed = 1.0f;
    public float yMoveSpeed = 1.0f;
    public float moveSmoothTime = 8.0f;
    public float maxMoveDistance = 4.0f;

    public float yMinLimit = -10f;
    public float yMaxLimit = 80f;

    public float distanceMin = 6f;
    public float distanceMax = 10f;

    public float smoothTime = 8f;

    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;

    float velocityX = 0.0f;
    float velocityY = 0.0f;

    float CFvelocityX = 0.0f;
    float CFvelocityY = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        moveNewPosition = target.transform.position;
    }
    
    void Update()
    {
        if (target)
        {
            if (Input.GetMouseButton(2))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                CFvelocityX += ((-xMoveSpeed * .02f * Input.GetAxis("Mouse X")) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)) +
                               ((-yMoveSpeed * .02f * Input.GetAxis("Mouse Y")) * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                CFvelocityY += ((xMoveSpeed * .02f * Input.GetAxis("Mouse X")) * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)) +
                               ((-yMoveSpeed * .02f * Input.GetAxis("Mouse Y")) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            //target.transform.position = moveNewPosition;

            moveNewPosition = new Vector3(Mathf.Clamp(target.transform.position.x + CFvelocityX, -maxMoveDistance + 3.5f, maxMoveDistance + 3.5f),
                                          target.transform.position.y,
                                          Mathf.Clamp(target.transform.position.z + CFvelocityY, -maxMoveDistance + 3.5f, maxMoveDistance + 3.5f));


            target.transform.position = moveNewPosition;

            CFvelocityX = Mathf.Lerp(CFvelocityX, 0, Time.deltaTime * moveSmoothTime);
            CFvelocityY = Mathf.Lerp(CFvelocityY, 0, Time.deltaTime * moveSmoothTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                target.transform.position = new Vector3(3.5f, 0, 3.5f);
                moveNewPosition = new Vector3(3.5f, 0, 3.5f);
                CFvelocityX = 0.0f;
                CFvelocityY = 0.0f;
            }
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                velocityX += xSpeed * Input.GetAxis("Mouse X") * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;

            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);

            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}