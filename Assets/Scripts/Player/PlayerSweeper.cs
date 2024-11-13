using UnityEngine;

public class SweeperController : MonoBehaviour
{
    // Referensi ke Wheel Collider
    public WheelCollider depan;
    public WheelCollider belakangKanan;
    public WheelCollider belakangKiri;

    // Parameter kontrol
    public float motorForce = 1500f;
    public float steeringAngle = 30f;

    private float horizontalInput;
    private float verticalInput;

    private void Update()
    {
        // Mengambil input untuk akselerasi dan belokan dari tombol WASD
        horizontalInput = Input.GetAxis("Horizontal"); // A dan D untuk belok
        verticalInput = Input.GetAxis("Vertical"); // W untuk maju, S untuk mundur
        
    }

    private void FixedUpdate()
    {
        // Fungsi untuk menggerakkan dan mengendalikan bajaj
        Move();
        Steer();
    }

    private void Move()
    {
        // Menerapkan gaya ke roda belakang untuk bergerak maju/mundur
        belakangKanan.motorTorque = verticalInput * motorForce;
        belakangKiri.motorTorque = verticalInput * motorForce;
    }

    private void Steer()
    {
        // Menerapkan sudut belokan pada roda depan
        depan.steerAngle = horizontalInput * steeringAngle;
    }
}
