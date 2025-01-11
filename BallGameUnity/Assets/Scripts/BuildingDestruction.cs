using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    public GameObject fracturedBuilding;  // Parçalanmış bina prefabı
    public float explosionForce = 500f;   // Patlama kuvveti
    public float explosionRadius = 5f;    // Patlama yarıçapı

    private bool isDestroyed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isDestroyed)
        {
            DestroyBuilding();
        }
    }

    void DestroyBuilding()
    {
        isDestroyed = true;

        // Mevcut binayı yok et
        Destroy(gameObject);

        // Parçalanmış binayı oluştur
        GameObject fractured = Instantiate(fracturedBuilding, transform.position, transform.rotation);

        // Tüm parçalarına patlama kuvveti uygula
       foreach (Rigidbody rb in fractured.GetComponentsInChildren<Rigidbody>())
{
    rb.isKinematic = false;  // Çarpışma anında aktif olur
    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
}
    }
}
