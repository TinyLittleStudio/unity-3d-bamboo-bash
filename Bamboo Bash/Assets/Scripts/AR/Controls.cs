using UnityEngine;
using TMPro;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private bool gyroEnabled;
    private Gyroscope gyro;

    public GameObject obj;

    private Quaternion rotation;

    private Transform cameraContainer;

    private void Start()
    {
        cameraContainer = Camera.main.transform;
        transform.SetParent(cameraContainer);
        transform.rotation = cameraContainer.rotation;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

        gyroEnabled = EnableGyro(); 
    }

    private bool EnableGyro()
    {
        /*if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rotation = new Quaternion(0, 0, 1, 0);

            return true;
        }
        */
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rotation;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            text.text =Camera.main.transform.rotation + ", " + hit.transform.name + ", " + hit.point;

            if(obj != null && hit.transform.tag != "No Raycast Target") {
                obj.transform.position = hit.point;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            text.text = "none";
        }
    }
}
