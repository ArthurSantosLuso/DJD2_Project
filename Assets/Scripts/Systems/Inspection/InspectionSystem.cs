using UnityEngine;

public class InspectionSystem : MonoBehaviour
{
    // After implementing this to the interaction system, change it to a property with
    // public get and private set and create a method to set the object to inspect
    [SerializeField] private Transform objectToInspect;
    [SerializeField] private float rotationSpeed = 100.0f;
    
    private Vector3 previousMousePosition;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePostion = Input.mousePosition - previousMousePosition;
            float rotationX = deltaMousePostion.y * rotationSpeed * Time.deltaTime;
            float rotationY = deltaMousePostion.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInspect.rotation = rotation * objectToInspect.rotation;

            previousMousePosition = Input.mousePosition;
        }
    }
}
