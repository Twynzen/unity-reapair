using System.Collections;
using System.Collections.Generic;
// using System.Security.Cryptography;
// using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rifleStart;
    [SerializeField] private Text HpText;

    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject Victory;
    [SerializeField] private float moveSpeed = 5f;
    
    private CharacterController characterController;
    public float health = 100; // el jugador nace muerto 

    void Start()
    {
        //Destroy(this); // destruye el script al iniciar 
        characterController = GetComponent<CharacterController>();
        ChangeHealth(0);
    }

    public void ChangeHealth(int hp)
    {
        health += hp;
        if (health > 100)
        {
            health = 100;
        }
        else if (health <= 0)
        {
            Lost();
        }
        HpText.text = health.ToString();
    }

    public void Win()
    {
        Victory.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lost()
    {
        GameOver.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {

        HandleMovement();
        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);
            buf.transform.position = rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(transform.forward);
            buf.transform.rotation = transform.rotation;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Collider[] tar = Physics.OverlapSphere(transform.position, 2);
            foreach (var item in tar)
            {
                if (item.tag == "Enemy")
                {
                    Destroy(item.gameObject);
                }
            }
        }

        Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        foreach (var item in targets)
        {
            if (item.tag == "Heal")
            {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }
            if (item.tag == "Finish")
            {
                Win();
            }
            if (item.tag == "Enemy")
            {
                Lost();
            }
        }
    }

    // AGREGADO para control de wasd 
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D o flechas izq/der
        float vertical = Input.GetAxis("Vertical");     // W/S o flechas arriba/abajo

        // Calcular dirección relativa a donde mira el jugador
        Vector3 movement = transform.right * horizontal + transform.forward * vertical;

        // Mover usando CharacterController (respeta colisiones)
        if (characterController != null && characterController.enabled)
        {
            characterController.Move(movement * moveSpeed * Time.deltaTime);
        }
    }
}
