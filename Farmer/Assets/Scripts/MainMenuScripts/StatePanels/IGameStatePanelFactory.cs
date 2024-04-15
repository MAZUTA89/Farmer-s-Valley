using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainMenuCode
{
    public interface IGameStatePanelFactory
    {
        GameObject Create(string stateName, Transform content);
    }
}
