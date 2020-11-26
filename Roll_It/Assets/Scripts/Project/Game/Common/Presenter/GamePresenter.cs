using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;
using Project.Screen.Result.View;

namespace Project.Game.Presenter
{
    using Model;
    using View;

    public class GamePresenter : AbstractPresenter
    {
        public GamePresenter(GameModel model, GameView gameView, GameScreenView screenView, ResultScreenView resultView) : base(model, gameView, screenView, resultView)
        {
            

        }

    }
}