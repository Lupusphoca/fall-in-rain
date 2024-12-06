namespace PierreARNAUDET.FallInRain
{
    using UnityEngine;
    using UnityEngine.Audio;

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip rainSound;
        [SerializeField] private AudioSource backgroundAudioSource;

        private void Awake()
        {
            Application.runInBackground = true;
        }

        private void Start()
        {
            // Setup audio source to loop
            backgroundAudioSource.resource = rainSound;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }

        // Method to switch sounds
        public void ChangeSoundtrack(AudioClip newClip)
        {
            backgroundAudioSource.Stop();
            backgroundAudioSource.resource = newClip;
            backgroundAudioSource.Play();
        }

        private void Testing()
        {
        }
    }

}