using Platformer.Mechanics;
using Platformer.UI;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace Platformer.UI
{
    /// <summary>
    /// The MetaGameController is responsible for switching control between the high level
    /// contexts of the application, eg the Main Menu and Gameplay systems.
    /// </summary>
    public class MetaGameController : MonoBehaviour
    {
        /// <summary>
        /// The main UI object which used for the menu.
        /// </summary>
        public MainUIController mainMenu;

        /// <summary>
        /// A list of canvas objects which are used during gameplay (when the main ui is turned off)
        /// </summary>
        public Canvas[] gamePlayCanvasii;

        /// <summary>
        /// The game controller.
        /// </summary>
        public GameController gameController;

        bool showMainCanvas = false;
        public int paused = 0;

        void OnEnable()
        {
            _ToggleMainMenu(showMainCanvas);
        }

        /// <summary>
        /// Turn the main menu on or off.
        /// </summary>
        /// <param name="show"></param>
        public void ToggleMainMenu(bool show)
        {
            gameController.gameObject.transform.Find("MenuTrigger").GetComponent<FMODUnity.StudioEventEmitter>().Play(); //Menu toggle event emitter
            if (this.showMainCanvas != show)
            {
                _ToggleMainMenu(show);
            }
        }

        void _ToggleMainMenu(bool show)
        {

            Bus sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/Sfx");
            Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
            if (show)
            {
                Time.timeScale = 0;
                mainMenu.gameObject.SetActive(true);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
                sfxBus.setPaused(true);
                musicBus.setVolume(0.4f);
            }
            else
            {
                Time.timeScale = 1;
                mainMenu.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
                sfxBus.setPaused(false);
                musicBus.setVolume(1f);
            }
            this.showMainCanvas = show;
        }

        void Update()
        {
            if (Input.GetButtonDown("Menu"))
            {
                ToggleMainMenu(show: !showMainCanvas);
            }
        }

    }
}
