using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;

namespace Project.Screen.Title.Model
{
    public class TitleScreenModel : AbstractModel
    {
        TitleScreenContext context;

        public TitleScreenModel(TitleScreenContext context)
        {
            this.context = context;
        }
    }
}