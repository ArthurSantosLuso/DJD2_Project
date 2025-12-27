using UnityEngine;

public class ClockController : MonoBehaviour
{
    [Header("Clock Hands")]
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform hourHand;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float direction = 0f;

        if (Input.GetKey(KeyCode.K))
            direction = 1f;

        if (Input.GetKey(KeyCode.J))
            direction = -1f;

        if (direction == 0f)
            return;

        RotateHands(direction);
    }

    private void RotateHands(float direction)
    {
        // rotation amount this frame
        float rotationAmount = direction * rotationSpeed * Time.deltaTime;

        // rotate minute hand
        minuteHand.Rotate(0f, 0f, -rotationAmount);

        // rotate hour hand proportionally 
        hourHand.Rotate(0f, 0f, -rotationAmount / 12f);
    }
}
