using System.Collections;
using System.Collections.Generic;
using Project.Game;
using UDL.Core;
using UnityEngine;

public class Main : MonoBehaviour
{
  
    void Start()
    {
        ScreenLoader.SetMaxAspectRatio(3.0f);
        ScreenManager.Instance.Replace(new GameFactory());
    }

}
