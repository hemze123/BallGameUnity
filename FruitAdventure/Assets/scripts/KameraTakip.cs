using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedef;                     // Takip edilecek karpuz
    public Vector3 offset = new Vector3(0, 8, -12);  // Kameranın uzaklık ve yükseklik ayarı
    public float takipHizi = 5f;                 // Takip etme hızı
    public float scrollHassasiyet = 2f;          // Scroll hassasiyeti (Zoom)
    public float minUzaklik = 5f;                // Minimum uzaklık
    public float maxUzaklik = 20f;               // Maksimum uzaklık
    public float yukseklikLimit = 1.5f;          // Yükseklik sınırı
    public Vector3 hedefOffset = new Vector3(0, 1.5f, 0); // Hedefin biraz daha yukarısını işaret eder

    private float mevcutUzaklik;  // Kameranın anlık uzaklığı

    void Start()
    {
        mevcutUzaklik = offset.magnitude;  // Başlangıç uzaklığı
    }

    void LateUpdate()
    {
        if (hedef != null)
        {
            // Kamera zoom ayarı (fare tekerleği)
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            mevcutUzaklik -= scrollInput * scrollHassasiyet;
            mevcutUzaklik = Mathf.Clamp(mevcutUzaklik, minUzaklik, maxUzaklik);

            // Kameranın pozisyonunu hesapla
            Vector3 hedefPozisyon = hedef.position + hedefOffset - transform.forward * mevcutUzaklik + Vector3.up * offset.y;
            
            // Yükseklik sınırını uygula
            hedefPozisyon.y = Mathf.Max(hedefPozisyon.y, yukseklikLimit);

            // Yumuşak hareket
            transform.position = Vector3.Lerp(transform.position, hedefPozisyon, takipHizi * Time.deltaTime);

            // Hedefin biraz yukarısına bak
            Vector3 bakisNoktasi = hedef.position + hedefOffset;
            transform.LookAt(bakisNoktasi);
        }
    }
}
