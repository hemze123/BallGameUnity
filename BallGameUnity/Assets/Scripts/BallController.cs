using UnityEngine;

public class BallController : MonoBehaviour
{
    // gemini 
    public float moveSpeed = 5f; // Hareket hızı
    public float acceleration = 10f; // İvmelenme miktarı
    public float deceleration = 15f; // Yavaşlama miktarı
    private Vector2 touchStartPos; // Dokunmanın başladığı nokta
    private Vector2 touchEndPos; // Dokunmanın bittiği nokta
    private Rigidbody rb; // Oyuncunun Rigidbody bileşeni
    private Vector3 moveDirection; // Hareket yönü
    private float currentSpeed; // Anlık hız

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the player!");
        }
    }
    //... (devamı aşağıda)

 void Update()
{
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
          touchEndPos = touch.position;
          Vector2 swipeDelta = touchEndPos - touchStartPos;
          if (swipeDelta.magnitude > 50)
          {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
              // Dönme
              float rotationSpeed = 180f * Time.deltaTime; // Döndürme hızı
              transform.Rotate(Vector3.up, swipeDelta.x > 0 ? rotationSpeed : -rotationSpeed);
              touchStartPos = touch.position; //Yeni dokunma noktası
            }
          }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
          touchEndPos = touch.position;
          Vector2 swipeDelta = touchEndPos - touchStartPos;
          if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y) && swipeDelta.y > 50)
          {
            moveDirection = transform.forward;
            currentSpeed = 0;
          }
        }
    }
}

void FixedUpdate()
{
    if (currentSpeed < moveSpeed && moveDirection != Vector3.zero)
    {
        currentSpeed += acceleration * Time.fixedDeltaTime;
        rb.velocity = moveDirection * currentSpeed;
    }
    else if(currentSpeed > 0 && moveDirection != Vector3.zero)
    {
        currentSpeed -= deceleration * Time.fixedDeltaTime;
        rb.velocity = moveDirection * currentSpeed;
        if(currentSpeed <= 0)
        {
            rb.velocity = Vector3.zero;
            moveDirection = Vector3.zero;
        }
    }
}




     // chat gpt

    // public float pushForce = 15f;    // İtme kuvveti
    // public float turnSpeed = 300f;   // Dönme hızı
    // private Rigidbody rb;
    // private float screenWidth;
    // private bool isPushing = false;

    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     rb.freezeRotation = true;  // X ve Z ekseninde dönmeyi engelle
    //     screenWidth = Screen.width;
    // }

    // void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);

    //         // Parmağın ekrana sürüklenip sürüklenmediğini kontrol et
    //         if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
    //         {
    //             isPushing = true;

    //             // Parmağın ekrandaki pozisyonunu merkeze göre hesapla
    //             float touchX = (touch.position.x - screenWidth / 2) / (screenWidth / 2);

    //             // Yön değişikliği
    //             float turn = touchX * turnSpeed * Time.deltaTime;
    //             transform.Rotate(0, turn, 0);
    //         }
    //     }
    //     else
    //     {
    //         isPushing = false;
    //     }
    // }

    // void FixedUpdate()
    // {
    //     if (isPushing)
    //     {
    //         // Topa ileriye doğru kuvvet uygula
    //         Vector3 forwardForce = transform.forward * pushForce;
    //         rb.AddForce(forwardForce, ForceMode.Force);
    //     }

    //     // Top çok aşağı düşerse resetle
    //     if (transform.position.y < -5f)
    //     {
    //         transform.position = new Vector3(0, 1, 0);
    //         rb.velocity = Vector3.zero;
    //     }
    // }
}
