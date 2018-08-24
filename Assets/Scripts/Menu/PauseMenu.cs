using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {

        #region Variables
        public GameObject pauseMenu;
        public bool isPause;
        public GameObject player;
        #endregion

        // Use this for initialization
        void Start()
        {
            player = GameObject.Find("Player");
            pauseMenu = GameObject.Find("Pause Menu");
            isPause = false;
        }

        public void MainMenuButton()
        {
            SceneManager.LoadScene("GUI Scene");
        }

        public void UnpauseButton()
        {
            isPause = false;
            player.GetComponent<Player.CharacterMovement>().isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPause)
            {
                isPause = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
            {

                isPause = true;
            }
            if (isPause)
            {
                pauseMenu.active = true;
            }
            else
            {
                pauseMenu.active = false;
            }
        }
    }
}
