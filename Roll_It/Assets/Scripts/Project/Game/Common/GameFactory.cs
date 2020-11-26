using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;
using UnityEngine.UI;
using Project.Screen.Result.View;


namespace Project.Game
{
    using Model;
    using Presenter;
    using View;
   

    public class GameContext
    {
        // public GameData gameData { get; private set; }
        public SimpleSubject startClick;
        // public GameContext(GameData gameData,SimpleSubject start = null)
        public GameContext(SimpleSubject start = null)
        {
            // this.gameData = gameData;
            this.startClick = start;
        }
    }

    public class GameFactory : IScreenFactory
    {
        public bool History => false;

        GameContext context;

        public GameFactory()
        {
            
        }

        public IModel Instantiate()
        {

            // GameModel model = new GameModel(gameData);
            // GameView view = CreateGameView();
            // GameScreenView screenView = CreateGameScreenView();
            // ResultScreenView resultView = CreateResultScreenView();

            // ScreenLoader.GetCanvas().transform.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

            // new GamePresenter(model, view, screenView, resultView).AddTo(model.disposables);

            // return model;
            return null;
        }

        public static GameView CreateGameView()
        {
            return Theater.Load<GameView>(ResourcePathConfig.GameViewPath);
        }

        public static GameScreenView CreateGameScreenView()
        {
            return ScreenLoader.Load<GameScreenView>(ResourcePathConfig.GameScreenViewPath, 3);
        }

        public ResultScreenView CreateResultScreenView()
        {
            return ScreenLoader.Load<ResultScreenView>(ResourcePathConfig.GameResultScreenViewPath, 4);
        }
    }
}