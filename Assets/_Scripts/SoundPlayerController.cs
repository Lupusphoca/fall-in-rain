namespace PierreARNAUDET.FallInRain
{
    using UnityEngine;
    
    public class SoundPlayerController : MonoBehaviour
    {
        [SerializeField] private AudioEqualizerController equalizer;

        void Start()
        {
            // Example of dynamic EQ adjustment
            equalizer.SetFrequencyBand(AudioEqualizerController.FrequencyBand.Low, 5f);

            // Apply a preset
            equalizer.ApplyPreset(AudioEqualizerController.EqualizerPreset.Bass);
        }
    }
}