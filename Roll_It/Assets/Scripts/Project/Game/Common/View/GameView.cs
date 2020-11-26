using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UDL.Core;
using UDL.Core.UI;
using Project.Screen.Result.View;
using Project.Game.Model;
using UniRx;

namespace Project.Game.View
{
    public class GameView : AbstractView
    {
        [SerializeField] MeshRenderer showObject;
        [SerializeField] Camera myMainCamera;
        [SerializeField] Camera myModelCamera;
        [SerializeField] GameObject mapParent;
        [SerializeField] GameObject objectReviewParent;

        
        public Subject<Unit> OnFinishMap = new Subject<Unit>();

        
    }
}
