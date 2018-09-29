using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject obj;

    private Transform cameraContainer;

    private void Start()
    {
        cameraContainer = Camera.main.transform;
        transform.SetParent(cameraContainer);
        transform.rotation = cameraContainer.rotation;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)); 
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if(obj != null && hit.transform.tag != "No Raycast Target") {
                obj.transform.position = hit.point;
            }
        }
    }
}
