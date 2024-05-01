using PimDeWitte.UnityMainThreadDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class MainMenu : MonoBehaviour
    {
        
        private const string GameSceneName = "FarmScene";
        private LoadMenu _loadMenu;
        private NewGameMenu _newGameMenu;
        private GameObject _menuObject;

        [Inject]
        public void Construct(LoadMenu loadMenu,
            NewGameMenu newGameMenu,
            [Inject(Id = "MainMenuObject")] GameObject menuObject)
        {
            _loadMenu = loadMenu;
            _newGameMenu = newGameMenu;
            _menuObject = menuObject;
        }
        private void Start()
        {
            _menuObject.SetActive(true);
        }
        public void NewGame()
        {
            _menuObject.SetActive(false);
            _newGameMenu.Activate();
        }
        public void LoadGame()
        {
            _menuObject.SetActive(false);
            _loadMenu.Activate();
        }
        public void Settings()
        {

        }

        public void WorkSpace()
        {
            Application.Quit();
        }
        
        public void OnBack()
        {
            _loadMenu.OnBack();
            _menuObject.SetActive(true);
        }
        public void Create()
        {
            _newGameMenu.OnCreate();
        }
        public void BackFromCreate()
        {
            _newGameMenu.OnBack();
            _menuObject.SetActive(true);
        }
    }
}
