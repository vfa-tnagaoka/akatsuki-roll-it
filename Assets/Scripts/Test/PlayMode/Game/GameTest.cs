using NUnit.Framework;
using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UDL.Core;
using Project.Game.View;
// using UnityEngine.TestTools;
using UniRx;

public class GameTest
{
    // private GameView gameView;


    [OneTimeSetUp]
    public void LoadScene()
    {
        Debug.Log("GameTest---> load scene");
        var loadSceneOperation = SceneManager.LoadSceneAsync("UnitTest");
        loadSceneOperation.allowSceneActivation = true;

    }

    [UnityTest]
    [Timeout(300000)]
    // [TestCase(0)]
    public IEnumerator LoadGamelevel_lv0()
    {
        int level = 10;
        // Debug.Log("LoadGamelevel --> start - " + gameView);
        GameView gameView = Theater.Load<GameView>(ResourcePathConfig.GameViewPath);
        WinScreen winView = Theater.Load<WinScreen>(ResourcePathConfig.WinViewPath);
        Debug.Log("GameTest----> LoadGameView - " + gameView + " -- " + winView);

        gameView.WinScreen = winView;
        yield return new WaitForSeconds(0.5f);

        // gameView.Init();
        gameView.ResetMap();
        gameView.LoadMapLevel(level - 1);
        // yield return gameView.StartCoroutine(LoadLevel(0));
        yield return new WaitForSeconds(30000);
    }


}
