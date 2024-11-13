using UnityEngine;

// Controls player movement and rotation.
public class VacumController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
    public float jumpForce = 3.0f; // Force applied when jumping

    private Rigidbody rb; // Reference to player's Rigidbody.
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
        // Jika vacuum terbalik, hentikan gerakan
        if (isFlipped)
        {
            return;
        }

        // Gerakan player (hanya jika tidak terbalik)
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Raycast untuk mendeteksi tabrakan di depan
        RaycastHit hit;
        float rayDistance = 0.5f; // Sesuaikan jaraknya

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            // Jika ada tabrakan dan mencoba bergerak maju, batalkan gerakan
            if (moveVertical > 0)
            {
                movement = Vector3.zero;
            }
        }

        rb.MovePosition(rb.position + movement);
    }
}
