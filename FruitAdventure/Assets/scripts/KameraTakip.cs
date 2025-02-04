using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    [SerializeField] private Transform hedef;         // Takip edilecek hedef
    [SerializeField] private float yumusatmaZamani = 0.3f;   // Yumuşatma süresi
    [SerializeField] private Vector3 offset;          // Kamera ile hedef arasındaki başlangıç mesafesi
    [SerializeField] private float lookAtOffset = 2f; // Kameranın baktığı noktanın Y offset'i
    [Range(1f, 50f)]
    [SerializeField] private float yatayDonusHizi = 10f;
    [Range(1f, 50f)]
    [SerializeField] private float dikeyDonusHizi = 5f;
    [SerializeField] private Joystick kameraJoystick;

    private Vector3 velocity = Vector3.zero;          // Kameranın mevcut hızı
    private float currentYaw;                         // Yatay eksende dönüş açısı
    private float currentPitch;                       // Dikey eksende dönüş açısı
    private Vector3 lookAtPosition;                   // Kameranın bakacağı nokta

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

        // Joystick'ten alınan inputlar
        float horizontalInput = kameraJoystick.Horizontal;
        float verticalInput = kameraJoystick.Vertical;

        // Dönüş açısını yumuşatmak için Lerp kullan
        currentYaw = Mathf.Lerp(currentYaw, currentYaw + horizontalInput * yatayDonusHizi, Time.deltaTime * yatayDonusHizi);
        currentPitch = Mathf.Lerp(currentPitch, Mathf.Clamp(currentPitch - verticalInput * dikeyDonusHizi, -30f, 60f), Time.deltaTime * dikeyDonusHizi);

        // Kameranın yeni pozisyonunu hesapla
        Vector3 hedefPozisyon = hedef.position + Quaternion.Euler(currentPitch, currentYaw, 0f) * offset;

        // Kamerayı yumuşak bir şekilde yeni pozisyona taşı
        transform.position = Vector3.SmoothDamp(
            transform.position,
            hedefPozisyon,
            ref velocity,
            yumusatmaZamani
        );

        // Kameranın bakacağı noktayı hesapla (hedefin biraz yukarısı)
        lookAtPosition = hedef.position;
        lookAtPosition.y += lookAtOffset; // Yukarı bakması için offset ekle

        // Kamerayı hedefe doğru döndür
        transform.LookAt(lookAtPosition);
    }
}
