using Unity.VisualScripting;
using UnityEngine;

// Controls player movement and rotation.
public class VacumKolamController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.

    private Rigidbody rb; // Reference to player's Rigidbody.
    public float jumpForce = 3.0f;
    private bool isFlipped = false; // Flag to check if vacuum is flipped

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {       
        // Cek apakah vacuum terbalik
        if (Vector3.Dot(transform.up, Vector3.down) > 0.5f)
        {
            isFlipped = true; // Jika vacuum terbalik
        }
        else
        {
            isFlipped = false; // Jika vacuum dalam posisi normal
        }
        
            if (Input.GetKeyDown(KeyCode.E))  { // Memeriksa apakah tombol E ditekan
            rb.AddForce(transform.forward * 3f, ForceMode.VelocityChange); //Transform untuk menggerakkan player sesuai local objek
        }
            if (Input.GetButtonDown("Jump")) {// Memeriksa apakah tombol spasi ditekan
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);//Vector3 untuk menggerakkan player sesuai global objek
        }
        
        // Periksa jika tombol Spasi ditekan untuk mengembalikan posisi vacuum
        if (Input.GetButtonDown("Jump") && isFlipped)
        {
            // Lakukan loncatan untuk membantu posisi vacuum kembali
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            // Set rotasi x dan z menjadi 0
            Quaternion uprightRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            rb.MoveRotation(uprightRotation);

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