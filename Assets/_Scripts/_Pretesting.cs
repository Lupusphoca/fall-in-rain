namespace PierreARNAUDET.FallInRain
{
    using UnityEngine;
    using UnityEngine.Audio;

    public class _Pretesting : MonoBehaviour
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

        private const string CENTER_FREQ = "CenterFreq";
        private const string FREQUENCY_GAIN = "FrequencyGain";

        // Enum for frequency bands
        public enum FrequencyBand
        {
            Low,
            Mid,
            High
        }

        // Equalizer presets
        public enum EqualizerPreset
        {
            Flat,
            Bass,
            Treble
        }

        private void Start()
        {
            // Apply a preset
            // ApplyPreset(EqualizerPreset.Bass);
        }

        [ContextMenu("Test")]
        private void Test()
        {
            mainMixer.SetFloat(CENTER_FREQ, 65.00f);
            mainMixer.SetFloat(FREQUENCY_GAIN, 2f);

            mainMixer.SetFloat(CENTER_FREQ, 650.00f);
            mainMixer.SetFloat(FREQUENCY_GAIN, 1f);

            mainMixer.SetFloat(CENTER_FREQ, 6500.00f);
            mainMixer.SetFloat(FREQUENCY_GAIN, -1f);
            // mainMixer.GetFloat("MyParamEqFrequency", out float currentFrequency);
            // mainMixer.SetFloat("MyParamEqFrequency", currentFrequency + 100);
        }

        [ContextMenu("Set Flat Preset")]
        private void FlatPreset()
        {
            ApplyPreset(EqualizerPreset.Flat);
        }

        [ContextMenu("Set Bass Preset")]
        private void BassPreset()
        {
            ApplyPreset(EqualizerPreset.Bass);
        }

        [ContextMenu("Set Treble Preset")]
        private void TreblePreset()
        {
            ApplyPreset(EqualizerPreset.Treble);
        }


        // Method to adjust specific frequency band
        public void SetFrequencyBand(FrequencyBand band, float gain)
        {
            // Clamp gain between -80 and 20 decibels
            float clampedGain = Mathf.Clamp(gain, -80f, 20f);

            switch (band)
            {
                case FrequencyBand.Low:
                    // mainMixer.GetFloat(LOW_FREQ_PARAM, out float currentFrequency);
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

    }
}