namespace PierreARNAUDET.FallInRain
{
    using UnityEngine;
    using UnityEngine.Audio;

    public class AudioEqualizerController : MonoBehaviour
    {
        [SerializeField] private AudioMixer mainMixer;

        // Frequency band references
        [Header("Equalizer Bands")]
        [SerializeField] private AudioMixerGroup lowBand;
        [SerializeField] private AudioMixerGroup midBand;
        [SerializeField] private AudioMixerGroup highBand;

        // Expose mixer parameters
        private const string LOW_FREQ_PARAM = "LowFreqGain";
        private const string MID_FREQ_PARAM = "MidFreqGain";
        private const string HIGH_FREQ_PARAM = "HighFreqGain";

        // Method to adjust specific frequency band
        public void SetFrequencyBand(FrequencyBand band, float gain)
        {
            // Clamp gain between -80 and 20 decibels
            float clampedGain = Mathf.Clamp(gain, -80f, 20f);

            switch (band)
            {
                case FrequencyBand.Low:
                    mainMixer.SetFloat(LOW_FREQ_PARAM, clampedGain);
                    break;
                case FrequencyBand.Mid:
                    mainMixer.SetFloat(MID_FREQ_PARAM, clampedGain);
                    break;
                case FrequencyBand.High:
                    mainMixer.SetFloat(HIGH_FREQ_PARAM, clampedGain);
                    break;
            }
        }

        // Enum for frequency bands
        public enum FrequencyBand
        {
            Low,
            Mid,
            High
        }

        // Preset equalizer configurations
        public void ApplyPreset(EqualizerPreset preset)
        {
            switch (preset)
            {
                case EqualizerPreset.Flat:
                    SetFrequencyBand(FrequencyBand.Low, 0);
                    SetFrequencyBand(FrequencyBand.Mid, 0);
                    SetFrequencyBand(FrequencyBand.High, 0);
                    break;
                case EqualizerPreset.Bass:
                    SetFrequencyBand(FrequencyBand.Low, 10);
                    SetFrequencyBand(FrequencyBand.Mid, -5);
                    SetFrequencyBand(FrequencyBand.High, -5);
                    break;
                case EqualizerPreset.Treble:
                    SetFrequencyBand(FrequencyBand.Low, -5);
                    SetFrequencyBand(FrequencyBand.Mid, -5);
                    SetFrequencyBand(FrequencyBand.High, 10);
                    break;
            }
        }

        // Equalizer presets
        public enum EqualizerPreset
        {
            Flat,
            Bass,
            Treble
        }
    }
}