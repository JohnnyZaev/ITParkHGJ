using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Services
{
    [RequireComponent(typeof(AudioSource))]
    public class SettingsManager : ServiceBase<SettingsManager>
    {
        [SerializeField] private AudioMixer globalMixer;
        [SerializeField] private GameObject settingsCanvas;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button vfxButton;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider vfxSlider;
        
        private AudioSource _audioSource;
        
        protected override void DoAwake()
        {
            closeButton.onClick.AddListener(CloseSettings);
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            MusicAwakeSettings();
            VFXAwakeSettings();
        }

        private void MusicAwakeSettings()
        {
            musicButton.onClick.AddListener(OnMusicButtonClicked);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            globalMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        }
        private void VFXAwakeSettings()
        {
            vfxButton.onClick.AddListener(OnVFXButtonClicked);
            vfxSlider.onValueChanged.AddListener(SetVFXVolume);
            vfxSlider.value = PlayerPrefs.GetFloat("VFXVolume", 0.5f);
            globalMixer.SetFloat("VFX", Mathf.Log10(PlayerPrefs.GetFloat("VFXVolume")) * 20);
        }

        private void CloseSettings()
        {
            settingsCanvas.SetActive(false);
        }

        private void SetMusicVolume(float volume)
        {
            globalMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
        
        private void SetVFXVolume(float volume)
        {
            globalMixer.SetFloat("VFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("VFXVolume", volume);
        }
        
        

        private void OnMusicButtonClicked()
        {
            if (!(musicSlider.value > 0.003f)) return;
            musicSlider.value = 0.001f;
            SetMusicVolume(musicSlider.value);
        }
        private void OnVFXButtonClicked()
        {
            if (!(vfxSlider.value > 0.003f)) return;
            vfxSlider.value = 0.001f;
            SetVFXVolume(vfxSlider.value);
        }

        public void OpenSettings()
        {
            settingsCanvas.SetActive(true);
        }
    }
}
