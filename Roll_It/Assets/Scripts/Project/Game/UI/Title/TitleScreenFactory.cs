using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;

namespace Project.Screen.Title
{
    using Model;
    using Presenter;
    using View;
   

    public class TitleScreenContext
    {
        public TitleScreenContext()
        {
        }
    }

    public class TitleScreenFactory : IScreenFactory
    {
        public bool History => false;

        TitleScreenContext context;

        public TitleScreenFactory(TitleScreenContext context)
        {
            this.context = context;
        }

        public IModel Instantiate()
        {
            TitleScreenView view = CreateTitleScreenView();
            TitleScreenModel model = new TitleScreenModel(context);

            new TitleScreenPresenter(model, view).AddTo(model.disposables);

            return model;
        }

        public static TitleScreenView CreateTitleScreenView()
        {
            return ScreenLoader.Load<TitleScreenView>("Prefabs/Screen/Title/TitleScreenView", 3);
        }
    }
}