using TMPro;
using UnityEngine;

public class playercamera : MonoBehaviour
{
    public float MouseSensitivity;
    public Transform mainCam;

    private int score = 0;
    private const int itemsRequired = 6;

    public TMP_Text itemText;
    public GameObject crosshair;
    public TMP_Text winText;

    private float camRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Win()
    {
        Debug.Log("Completed!");
        crosshair.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
    }

    public void addItems(int items)
    {
        Debug.Log("Score Changed!");
        score += items;
        itemText.text = "Items: " + score.ToString();

        if (score >= itemsRequired)
        {
            Win();
        }
    }

    private void Update()
    {
        float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity;
        camRotation -= mouseInputY;
        camRotation = Mathf.Clamp(camRotation, -90f, 90f);
        mainCam.localRotation = Quaternion.Euler(new Vector3(camRotation, 0f, 0f));

        float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX, 0f));

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCam.position, mainCam.forward, out hit))
            {
                Debug.DrawLine(mainCam.position + new Vector3(0f, -1f, 0f), hit.point, Color.green, 1f);
                Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.CompareTag("pickup"))
                {
                    addItems(1);
                    Destroy(hit.collider.gameObject);
                }

            }
            else
            {
                Debug.DrawRay(mainCam.position + new Vector3(0f, -1f, 0f), mainCam.forward * 100f, Color.red, 1f);
            }
        }
    }
}
