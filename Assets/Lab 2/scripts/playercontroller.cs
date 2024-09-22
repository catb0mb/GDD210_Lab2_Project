using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{

    public GameObject pickup;
    public Rigidbody pickupRB;
    public GameObject lava;
    public Rigidbody lavaRB;

    public TMP_Text itemText;
    public TMP_Text winText;
    public TMP_Text loseText;
    public GameObject crosshair;

    public CharacterController CC;
    public float MoveSpeed;
    public float Gravity = -9.8f;
    public float JumpSpeed;

    public float verticalSpeed;

    private bool isDead = false;

    // Update is called once per frame

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (!isDead) 
        {
            Vector3 movement = Vector3.zero;

            // X/Z movement
            float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
            float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

            movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

            if (CC.isGrounded)
            {
                verticalSpeed = 0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    verticalSpeed = JumpSpeed;
                }
            }

            verticalSpeed += (Gravity * Time.deltaTime);
            movement += (transform.up * verticalSpeed * Time.deltaTime);

            CC.Move(movement);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            Lose();
        }

        if (other.gameObject.CompareTag("lava"))
        {
            Lose();
        }
    }

    void Win()
    {
        Debug.Log("Completed!");
        crosshair.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
    }

    void Lose()
    {
        isDead = true; 
        Debug.Log("Died!");
        crosshair.gameObject.SetActive(false);
        loseText.gameObject.SetActive(true);
    }

    void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        isDead = false;
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        crosshair.gameObject.SetActive(true);
    }
}