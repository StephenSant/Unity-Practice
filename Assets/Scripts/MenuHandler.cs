using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;//interacts withe scene changing
using UnityEngine.UI;//interacting with GUI elements
using UnityEngine.EventSystems;// BUTTONS!


public class MenuHandler : MonoBehaviour
{
    #region Variables
    public GameObject mainMenu, optionMenu;
    public bool showOptions;
    public Slider volSlider, brightSlider, ambientSlider;
    public Toggle fullScreenToggle;
    public AudioSource mainAudio;
    public Light dirLight;
    public Vector2[] res = new Vector2[7];
    public int resIndex;
    public bool isFullScreen;
    public Dropdown resDropdown;
    #endregion

    public void LoadGame()//starts the game
    {
        SceneManager.LoadScene(1);
    }
    public void ToggleOptions()
    {
        OptionToggle();

    }
    bool OptionToggle()
    {
        if (showOptions)//showoptions == true;
        {
            showOptions = false;
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
            return true;
        }
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionMenu.SetActive(true);

            volSlider = GameObject.Find("Sound Slider").GetComponent<Slider>();
            mainAudio = GameObject.Find("hello world").GetComponent<AudioSource>();
            volSlider.value = mainAudio.volume;

            brightSlider = GameObject.Find("Brightness Slider").GetComponent<Slider>();
            dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
            brightSlider.value = dirLight.intensity;

            ambientSlider = GameObject.Find("Ambient Light Slider").GetComponent<Slider>();
            ambientSlider.value = RenderSettings.ambientIntensity;

            resDropdown = GameObject.Find("Resolution Options").GetComponent<Dropdown>();
            fullScreenToggle = GameObject.Find("FullScreen Button").GetComponent<Toggle>();

            return false;
        }
    }
    public void Volume()
    {
        mainAudio.volume = volSlider.value;
    }
    public void Brightness()
    {
        dirLight.intensity = brightSlider.value;
    }
    public void AmbientLight()
    {
        RenderSettings.ambientIntensity = ambientSlider.value;
    }
    public void Resolution()
    {
        resIndex = resDropdown.value;
        Screen.SetResolution((int)res[resIndex].x, (int)res[resIndex].y, isFullScreen);
    }
    public void Windowed()
    {
        isFullScreen = fullScreenToggle.isOn;
        Screen.SetResolution((int)res[resIndex].x, (int)res[resIndex].y, isFullScreen);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
