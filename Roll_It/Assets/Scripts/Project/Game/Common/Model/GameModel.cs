using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;

namespace Project.Game.Model
{
    public class GameModel : AbstractModel
    {
        // GameContext context;

        // public GameModel(GameContext context)
        // {
        //     this.context = context;
        // }

        public enum GameState
        {
            None,
            GamePlay,
            Result,
        }

    }
}