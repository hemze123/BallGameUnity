using UnityEngine;

public class NesneDonme : MonoBehaviour
{
    public enum DonmeEkseni { X, Y, Z } // Dönüş ekseni seçenekleri
    public DonmeEkseni secilenEkseni = DonmeEkseni.Y; // Varsayılan Y ekseni

    public float donmeHizi = 50f; // Dönüş hızı

    void Update()
    {
        Vector3 donmeVektoru = Vector3.zero;

        // Seçilen eksene göre dönüş vektörünü belirle
        switch (secilenEkseni)
        {
            case DonmeEkseni.X:
                donmeVektoru = Vector3.right; // X ekseni
                break;
            case DonmeEkseni.Y:
                donmeVektoru = Vector3.down;    // Y ekseni
                break;
            case DonmeEkseni.Z:
                donmeVektoru = Vector3.forward; // Z ekseni
                break;
        }

        // Nesneyi belirlenen eksen etrafında döndür
        transform.Rotate(donmeVektoru * donmeHizi * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Çarpışma durumunda sekme kuvveti uygulama
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Çarpışma normalini al
                Vector3 carpmaNormal = collision.contacts[0].normal;

                // Sekme yönü: hızın tersine çarpma normali etkisi
                Vector3 sekmeYonu = Vector3.Reflect(rb.linearVelocity, carpmaNormal);

                // Yeni hız vektörü
                rb.linearVelocity = sekmeYonu * 2f; // Sekme katsayısını ayarlayın
            }
        }
    }
}
