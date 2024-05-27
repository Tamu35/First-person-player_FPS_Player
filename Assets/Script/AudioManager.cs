using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Ses kaynaðý
    public AudioClip walkClip; // Yürüme sesi
    public AudioClip sprintClip; // Koþma sesi
    public AudioClip jumpClip; // Zýplama sesi
    public AudioClip slowWalkClip; // Yavaþ yürüme sesi

    public float volume = 0.5f; // Ses düzeyi
    public float walkSpeed = 1.0f; // Yürüme sesinin çalma hýzý
    public float slowWalkSpeed = 0.5f; // Yavaþ yürüme sesinin çalma hýzý
    public float sprintSpeed = 1.5f; // Koþma sesinin çalma hýzý

    private bool isWalking = false;
    private bool isRunning = false;
    private bool isSlowing = false;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the Inspector");
        }
    }

    void Update()
    {
        // Yürüme hareketi baþladýðýnda yürüme sesini çalma
        if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && !isWalking)
        {
            PlaySound(walkClip, walkSpeed);
            isWalking = true;
        }

        // Yürüme hareketi bittiðinde yürüme sesini durdurma
        if ((Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) && isWalking)
        {
            audioSource.Stop();
            isWalking = false;
        }

        // Koþma sesini çalma
        if (Input.GetKey(KeyCode.LeftShift) && !isRunning)
        {
            PlaySound(sprintClip, sprintSpeed);
            isRunning = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && isRunning)
        {
            audioSource.Stop();
            isRunning = false;
        }

        // Yavaþ yürüme sesini çalma
        if (Input.GetKey(KeyCode.LeftControl) && !isSlowing)
        {
            PlaySound(slowWalkClip, slowWalkSpeed);
            isSlowing = true;
        }
        else if (!Input.GetKey(KeyCode.LeftControl) && isSlowing)
        {
            audioSource.Stop();
            isSlowing = false;
        }

        // Zýplama sesini çalma
        if (Input.GetButtonDown("Jump"))
        {
            PlaySound(jumpClip);
        }
    }

    void PlaySound(AudioClip clip, float pitch = 1.0f)
    {
        audioSource.Stop(); // Önceki sesi durdur

        // Ses düzeyini ve çalma hýzýný ayarla
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        // Yeni sesi çal
        audioSource.clip = clip;
        audioSource.Play();
    }
}
