using UnityEngine;

public class CameraFollow : MonoBehaviour
{
      public Transform target; // Takip edilecek hedef (top)
    public float smoothSpeed = 0.125f; // Kameranın hareket yumuşaklığı

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0, 2, -5); // Hedefin biraz üstünde ve arkasında bir pozisyon
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
