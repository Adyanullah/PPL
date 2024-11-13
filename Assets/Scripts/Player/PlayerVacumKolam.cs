using Unity.VisualScripting;
using UnityEngine;

// Controls player movement and rotation.
public class VacumKolamController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.

    private Rigidbody rb; // Reference to player's Rigidbody.
    public float jumpForce = 3.0f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {       
        
            if (Input.GetKeyDown(KeyCode.E))  { // Memeriksa apakah tombol E ditekan
            rb.AddForce(transform.forward * 3f, ForceMode.VelocityChange); //Transform untuk menggerakkan player sesuai local objek
        }
            if (Input.GetButtonDown("Jump")) {// Memeriksa apakah tombol spasi ditekan
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);//Vector3 untuk menggerakkan player sesuai global objek
        }
    }


    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, turn);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}