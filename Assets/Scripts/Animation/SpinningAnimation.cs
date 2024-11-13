using UnityEngine;

public class LocalRotation : MonoBehaviour
{
    // Kecepatan rotasi pada setiap sumbu (X, Y, Z)
    public Vector3 rotationSpeed = new Vector3(50f, 0f, 0f);

    private void Update()
    {
        // Melakukan rotasi secara lokal
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}
