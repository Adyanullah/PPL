using Unity.VisualScripting;
using UnityEngine;

// Controls player movement and rotation.
public class VacumController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.

    private Rigidbody rb; // Reference to player's Rigidbody.
    public float jumpForce = 3.0f;
    private bool isColliding = false;
    public float maxTiltAngle = 45f; // Sudut maksimum untuk mendeteksi terbalik

    public float rotationResetSpeed = 2f; // Kecepatan rotasi untuk kembali ke orientasi awal
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {       
        
            if (Input.GetKeyDown(KeyCode.E))  { // Memeriksa apakah tombol E ditekan
            rb.AddForce(-transform.up * 3f, ForceMode.VelocityChange); //Transform untuk menggerakkan player sesuai local objek
        }
            if (Input.GetButtonDown("Jump")) {// Memeriksa apakah tombol spasi ditekan
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);//Vector3 untuk menggerakkan player sesuai global objek
        }
    }


    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Cek apakah player terbalik berdasarkan orientasi sumbu forward
        if (Vector3.Dot(transform.forward, Vector3.down) > 0.5f)
        {
            if (Input.GetButtonDown("Jump")) {// Memeriksa apakah tombol spasi ditekan
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);//Vector3 untuk menggerakkan player sesuai global objek
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Tentukan rotasi target dengan mempertahankan rotasi x -90
            Quaternion targetRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);
        
            // Atur rotasi menuju target secara bertahap
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationResetSpeed * Time.fixedDeltaTime);
            }
            // Hentikan pergerakan jika player terbalik
            return;
        }

        // Gerakan player (hanya jika tidak terbalik)
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = -transform.up * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, turn);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Raycast ke arah depan player untuk mendeteksi dinding
        RaycastHit hit;
        float rayDistance = 0.5f; // Sesuaikan jaraknya

        if (Physics.Raycast(transform.position, -transform.up, out hit, rayDistance))
        {
            // Jika ada tabrakan dan mencoba bergerak maju, batalkan gerakan
            if (moveVertical > 0)
            {
                movement = Vector3.zero;
            }
        }

        rb.MovePosition(rb.position + movement);

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isColliding = false;
        }
    }

    void JumpAndFlip()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Tentukan rotasi target dengan mempertahankan rotasi x -90
        Quaternion targetRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);
        
        // Atur rotasi menuju target secara bertahap
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationResetSpeed * Time.fixedDeltaTime);
    }

}