using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;

namespace Project.Screen.Title.Presenter
{
    using Model;
    using View;

    public class TitleScreenPresenter : AbstractPresenter
    {
        public TitleScreenPresenter(TitleScreenModel model, TitleScreenView view) : base(model, view)
        {
        }
    }
}