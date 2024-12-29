using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] private float lateralSmoothSpeed = 10f; //Yumuşak geçiş hızım.
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] private AudioSource effectSource,musicSource;
    [SerializeField] AudioClip cheeseClip,deathClip;
    
    private float[] xPosition = { 0f, 0.368f, 0.736f };
    //Başlangıç posizyonum
    private int currentXpositonIndex = 0;
    Vector3 targetPosition;
    
    public bool isAlive = true;

    private float score;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position; // Başlangıç hedefimi beliriyorum.
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            score += Time.deltaTime;
            scoreText.text = "Score: " + score.ToString("f1");
            if (Input.GetKeyDown(KeyCode.A) && currentXpositonIndex > 0)
            {
                currentXpositonIndex--;// Mevcut değeri 1 azaltmak için kullanıyoruz.
                //Bir fonksiyon yazacağım. Zıplayacağım hedefleri belirlemek için.
                UpdateLateralPosition();
            }
            else if (Input.GetKeyDown(KeyCode.D) && currentXpositonIndex < 2)
            {
                currentXpositonIndex++; //sağa doğru hareket ederken index değerini 1 arttıracak.
                //Zıplama hedef belirleyecek fonksiyonu burada çağıracağım.
                UpdateLateralPosition();
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            //İleri yönde hareket kodu
            Vector3 forwardMove =  Vector3.forward *speed * Time.fixedDeltaTime;
        
            //Hedef noktası posisyonuna doğru yumuşak bir geçiş yapalım
            Vector3 currentPosition = rb.position;
            Vector3 lateralMove = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * lateralSmoothSpeed);
        
            //İleri ve yanal hareketi birleştirmem gerek
            Vector3 combineMove = new Vector3(lateralMove.x, transform.position.y, rb.position.z) + forwardMove;
            rb.MovePosition(combineMove);
        }
       
    }

    void UpdateLateralPosition()
    {
        //Hedef posizyonu çelilen x konumuna göre güncelleyecek.
        targetPosition = new Vector3(xPosition[currentXpositonIndex], transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cars"))
        {
            rb.isKinematic = true;
            isAlive = false;
            animator.SetBool("Die",true);
            musicSource.Stop();
            effectSource.clip = deathClip;
            effectSource.Play();
            menuCanvas.SetActive(true);
        }
    }
    
    //Peynir topladıkça olacaklar.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chees"))
        {
            score += 5;
            speed += 0.05f;
            effectSource.clip = cheeseClip;
            effectSource.Play();
            Destroy(other.gameObject);
        }
    }
}
