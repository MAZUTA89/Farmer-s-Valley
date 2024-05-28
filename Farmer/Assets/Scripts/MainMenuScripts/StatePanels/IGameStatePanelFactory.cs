using UnityEngine;

namespace Scripts.MainMenuCode
{
    public interface IGameStatePanelFactory
    {
        GameStatePanel Create(string stateName, Transform content);
    }
}
