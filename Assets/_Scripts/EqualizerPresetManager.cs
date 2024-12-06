namespace PierreARNAUDET.FallInRain
{
    using UnityEngine;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class EqualizerPresetData
    {
        public string presetName;
        public float lowBandGain;
        public float midBandGain;
        public float highBandGain;
    }

    public class EqualizerPresetManager : MonoBehaviour
    {
        [SerializeField] private AudioEqualizerController equalizerController;

        // Path for saving presets
        private string PresetSavePath =>
            Path.Combine(Application.persistentDataPath, "equalizer_presets.json");

        // List to store saved presets
        private List<EqualizerPresetData> savedPresets = new List<EqualizerPresetData>();

        // Save current equalizer settings as a new preset
        public void SaveCurrentPreset(string presetName)
        {
            // Get current equalizer values
            float lowGain = GetCurrentBandGain(AudioEqualizerController.FrequencyBand.Low);
            float midGain = GetCurrentBandGain(AudioEqualizerController.FrequencyBand.Mid);
            float highGain = GetCurrentBandGain(AudioEqualizerController.FrequencyBand.High);

            // Create new preset
            var newPreset = new EqualizerPresetData
            {
                presetName = presetName,
                lowBandGain = lowGain,
                midBandGain = midGain,
                highBandGain = highGain
            };

            // Add or update preset
            int existingIndex = savedPresets.FindIndex(p => p.presetName == presetName);
            if (existingIndex != -1)
            {
                savedPresets[existingIndex] = newPreset;
            }
            else
            {
                savedPresets.Add(newPreset);
            }

            // Save to file
            SavePresetsToFile();
        }

        // Load a specific preset
        public void LoadPreset(string presetName)
        {
            var preset = savedPresets.Find(p => p.presetName == presetName);
            if (preset != null)
            {
                equalizerController.SetFrequencyBand(
                    AudioEqualizerController.FrequencyBand.Low, preset.lowBandGain);
                equalizerController.SetFrequencyBand(
                    AudioEqualizerController.FrequencyBand.Mid, preset.midBandGain);
                equalizerController.SetFrequencyBand(
                    AudioEqualizerController.FrequencyBand.High, preset.highBandGain);
            }
        }

        // Get current gain for a specific band
        private float GetCurrentBandGain(AudioEqualizerController.FrequencyBand band)
        {
            float gain = 0f;
            switch (band)
            {
                case AudioEqualizerController.FrequencyBand.Low:
                    PlayerPrefs.GetFloat("LowBandGain", 0f);
                    break;
                case AudioEqualizerController.FrequencyBand.Mid:
                    PlayerPrefs.GetFloat("MidBandGain", 0f);
                    break;
                case AudioEqualizerController.FrequencyBand.High:
                    PlayerPrefs.GetFloat("HighBandGain", 0f);
                    break;
            }
            return gain;
        }

        // Save presets to file
        private void SavePresetsToFile()
        {
            string json = JsonUtility.ToJson(new SerializablePresetList { presets = savedPresets });
            File.WriteAllText(PresetSavePath, json);
        }

        // Load presets from file
        private void LoadPresetsFromFile()
        {
            if (File.Exists(PresetSavePath))
            {
                string json = File.ReadAllText(PresetSavePath);
                var loadedData = JsonUtility.FromJson<SerializablePresetList>(json);
                savedPresets = loadedData.presets;
            }
        }

        // Unity serialization helper class
        [Serializable]
        private class SerializablePresetList
        {
            public List<EqualizerPresetData> presets;
        }

        // Load presets when script starts
        private void Awake()
        {
            LoadPresetsFromFile();
        }

        // Get all saved preset names
        public List<string> GetSavedPresetNames()
        {
            return savedPresets.Select(p => p.presetName).ToList();
        }

        // Delete a specific preset
        public void DeletePreset(string presetName)
        {
            savedPresets.RemoveAll(p => p.presetName == presetName);
            SavePresetsToFile();
        }
    }
}