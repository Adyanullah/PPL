using UnityEngine;
using System.Collections;

public class BlinkController : MonoBehaviour
{
    // Objek mata yang ingin dibuat berkedip
    public GameObject mata;

    // Interval waktu untuk kedipan dalam detik
    public float blinkInterval = 2f;
    public float blinkDuration = 0.1f;

    private void Start()
    {
        // Memulai coroutine untuk berkedip secara berkala
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            // Menunggu hingga waktu blinkInterval
            yield return new WaitForSeconds(blinkInterval);
            
            // Menyembunyikan objek mata
            mata.SetActive(false);

            // Menunggu selama blinkDuration agar terlihat seperti berkedip
            yield return new WaitForSeconds(blinkDuration);

            // Menampilkan kembali objek mata
            mata.SetActive(true);
        }
    }
}
