using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;

namespace Project.Screen.Result
{
    using View;

    public class ResultScreenContext
    {
        public int resultPercentage;
        public int resultStarCount;
        public ResultScreenContext(int percent, int stars)
        {
            resultPercentage = percent;
            resultStarCount = stars;
        }
    }

    public class ResultScreenFactory : IScreenFactory
    {
        public bool History => false;
        ResultScreenContext context;

        public ResultScreenFactory(ResultScreenContext context)
        {
            this.context = context;
        }

        public IModel Instantiate()
        {
            ResultScreenView view = CreateResultScreenView();
            view.SetData(context.resultPercentage, context.resultStarCount);

            return view;
        }

        public static ResultScreenView CreateResultScreenView()
        {
            return ScreenLoader.Load<ResultScreenView>("Prefabs/Screen/Result/ResultScreenView", 3);
        }
    }
}