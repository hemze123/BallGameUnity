using UnityEngine;
using UnityEngine.UI;

public class KarpuzHareket : MonoBehaviour
{
    public float hareketHizi = 10f;
    public float ziplamaGucu = 8f;
    public Joystick joystick;
    public Button ziplaButon;

    private Rigidbody rb;
    private bool zipladi = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ziplaButon.onClick.AddListener(Zipla);
    }

    void Update()
    {
        float yatay = joystick.Horizontal;
        float dikey = joystick.Vertical;

        Vector3 hareket = new Vector3(yatay, 0, dikey) * hareketHizi;
        rb.AddForce(hareket);

        if (zipladi && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            zipladi = false;
        }
    }

    // Yeni Zipla fonksiyonu
  public  void Zipla()
    {
        if (!zipladi)
        {
            rb.AddForce(Vector3.up * ziplamaGucu, ForceMode.Impulse);
            zipladi = true;
        }
    }
}
