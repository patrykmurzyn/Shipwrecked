using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private static AudioClip playerJump, clickSound;

    private static AudioSource audioSrc;

    void Start()
    {
        playerJump = Resources.Load<AudioClip>("Jump");
        playerJump = Resources.Load<AudioClip>("Click");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Jump":
                audioSrc.PlayOneShot(playerJump);
                break;

            case "Click":
                audioSrc.PlayOneShot(clickSound);
                break;
        }
    }
}
