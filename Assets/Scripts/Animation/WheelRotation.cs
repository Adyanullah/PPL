using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public Transform[] wheels;       // Array untuk semua roda
    public float wheelRadius = 0.3f; // Radius roda
    public Rigidbody rb;             // Rigidbody dari kendaraan
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    // Enum untuk memilih sumbu rotasi
    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.X; // Default pada sumbu X

    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        // Hitung jarak yang ditempuh sejak frame terakhir
        float distanceTravelled = Vector3.Distance(transform.position, lastPosition);

        // Tentukan arah gerakan: maju (positif) atau mundur (negatif)
        Vector3 movementDirection = (transform.position - lastPosition).normalized;
        float forwardDirection = Vector3.Dot(movementDirection, transform.forward);

        // Tentukan rotasi roda berdasarkan gerakan maju/mundur
        float rotationAmount = (distanceTravelled / (2 * Mathf.PI * wheelRadius)) * 360;
        rotationAmount *= Mathf.Sign(forwardDirection); // Balik arah jika mundur

        // Hitung perubahan rotasi horizontal (yaw) untuk belokan
        float rotationDelta = Quaternion.Angle(lastRotation, transform.rotation);
        float turningDirection = Vector3.Dot(transform.right, transform.rotation * Vector3.forward);
        rotationDelta *= Mathf.Sign(turningDirection); // Tentukan arah belokan

        // Gabungkan rotasi gerakan maju/mundur dan belokan
        float totalRotationAmount = rotationAmount + rotationDelta;

        // Rotasi roda berdasarkan sumbu yang dipilih
        foreach (Transform wheel in wheels)
        {
            switch (rotationAxis)
            {
                case RotationAxis.X:
                    wheel.Rotate(totalRotationAmount, 0, 0, Space.Self);
                    break;
                case RotationAxis.Y:
                    wheel.Rotate(0, totalRotationAmount, 0, Space.Self);
                    break;
                case RotationAxis.Z:
                    wheel.Rotate(0, 0, totalRotationAmount, Space.Self);
                    break;
            }
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
}
