using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Transform cameraContainer;

    private float velocity = 5.0f;

    private Vector3 destination = Vector3.zero;

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
            if(target != null && hit.transform.tag != "Player") {
                destination = hit.point;
            } else {
                destination = Vector3.zero;
            }
        }

        if(destination != Vector3.zero) { 
            target.transform.position = Vector3.MoveTowards(target.transform.position, destination, velocity * Time.deltaTime);
        }
    }
}
