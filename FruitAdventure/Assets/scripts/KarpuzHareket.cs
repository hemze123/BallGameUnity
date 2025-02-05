using UnityEngine;
using UnityEngine.UI;

public class KarpuzHareket : MonoBehaviour
{
    public float hareketHizi = 10f;
    public float ziplamaGucu = 8f;
    public float maxZiplamaGucu = 16f;  // Maksimum zıplama gücü
    public float ziplamaArtisHizi = 8f;  // Zıplama gücü artış hızı
    public float surtunmeKuvveti = 0.95f;
    public float donmeHizi = 2f;
    public Joystick joystick;
    public Button ziplaButon;
    public Camera mainCamera;

    // Ses efektleri için değişkenler
    public AudioSource hareketSes; // Hareket sesi
    public AudioSource ziplaSes;    // Zıplama sesi

    private Rigidbody rb;
    private bool zipladi = false;
    private bool ziplamaBasili = false;
    private float mevcutZiplamaGucu;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Zıplama butonu için yeni event'ler ekle
        ziplaButon.GetComponent<Button>().onClick.AddListener(ZiplamaBasla);
        
        if (mainCamera == null)
            mainCamera = Camera.main;

        rb.maxAngularVelocity = 7f;

        // Yer çekimi kuvvetini artır
        rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
    }

    void Update()
    {
        // Zıplama gücünü artır
        if (ziplamaBasili && !zipladi)
        {
            mevcutZiplamaGucu = Mathf.Min(mevcutZiplamaGucu + ziplamaArtisHizi * Time.deltaTime, maxZiplamaGucu);
        }
    }

    void FixedUpdate()
    {
        float yatay = joystick.Horizontal;
        float dikey = joystick.Vertical;

        Vector3 kameraYonu = mainCamera.transform.forward;
        kameraYonu.y = 0;
        kameraYonu.Normalize();

        Vector3 kameraSag = new Vector3(kameraYonu.z, 0, -kameraYonu.x);

        Vector3 hareket = kameraSag * yatay + kameraYonu * dikey;

        if (hareket.magnitude > 0.1f)
        {
            hareket = hareket.normalized * hareketHizi;
            rb.AddForce(new Vector3(hareket.x, 0, hareket.z), ForceMode.Force);

            // Hareket sesi çal
            if (hareketSes != null && !hareketSes.isPlaying)
            {
                hareketSes.Play();
            }

            Vector3 donmeYonu = Vector3.Cross(Vector3.up, hareket.normalized);
            rb.AddTorque(donmeYonu * donmeHizi, ForceMode.Force);
        }
        else
        {
            rb.velocity *= surtunmeKuvveti;
            rb.angularVelocity *= surtunmeKuvveti;

            // Hareket sesi durdur
            if (hareketSes != null && hareketSes.isPlaying)
            {
                hareketSes.Stop();
            }
        }

        if (rb.velocity.magnitude > hareketHizi)
        {
            rb.velocity = rb.velocity.normalized * hareketHizi;
        }

        if (zipladi && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            zipladi = false;
        }
    }

    public void ZiplamaBasla()
    {
        if (!zipladi && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            ziplamaBasili = true;
            mevcutZiplamaGucu = ziplamaGucu;
        }
    }

    public void ZiplamaBitir()
    {
        if (ziplamaBasili && !zipladi)
        {
            rb.AddForce(Vector3.up * mevcutZiplamaGucu, ForceMode.Impulse);
            zipladi = true;
            ziplamaBasili = false;

            // Zıplama sesi çal
            if (ziplaSes != null)
            {
                ziplaSes.Play();
            }
        }
    }
}