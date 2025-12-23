using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    [SerializeField] private Transform hedef;         
    [SerializeField] private float yumusatmaZamani = 0.3f;   
    [SerializeField] private Vector3 offset;         
    [SerializeField] private float lookAtOffset = 2f; 
    [Range(1f, 50f)]
    [SerializeField] private float yatayDonusHizi = 10f;
    [Range(1f, 50f)]
    [SerializeField] private float dikeyDonusHizi = 5f;
    [SerializeField] private Joystick kameraJoystick;

    private Vector3 velocity = Vector3.zero;          
    private float currentYaw;                         
    private float currentPitch;                       
    private Vector3 lookAtPosition;                   
    private void Start()
    {
        if (hedef == null)
        {
            Debug.LogWarning("Lütfen takip edilecek hedefi belirleyin!");
            return;
        }

        offset = transform.position - hedef.position;
    }

    private void LateUpdate()
    {
        if (hedef == null) return;

       
        float horizontalInput = kameraJoystick.Horizontal;
        float verticalInput = kameraJoystick.Vertical;

       
        currentYaw = Mathf.Lerp(currentYaw, currentYaw + horizontalInput * yatayDonusHizi, Time.deltaTime * yatayDonusHizi);
        currentPitch = Mathf.Lerp(currentPitch, Mathf.Clamp(currentPitch - verticalInput * dikeyDonusHizi, -30f, 60f), Time.deltaTime * dikeyDonusHizi);

      
        Vector3 hedefPozisyon = hedef.position + Quaternion.Euler(currentPitch, currentYaw, 0f) * offset;

        
        transform.position = Vector3.SmoothDamp(
            transform.position,
            hedefPozisyon,
            ref velocity,
            yumusatmaZamani
        );

        
        lookAtPosition = hedef.position;
        lookAtPosition.y += lookAtOffset;

       
        transform.LookAt(lookAtPosition);
    }
}
