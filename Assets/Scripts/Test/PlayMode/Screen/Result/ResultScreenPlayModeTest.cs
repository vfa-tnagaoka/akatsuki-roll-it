using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UDL.Core;
using UniRx;
using Project.Screen.Result.View;
using Project.Screen.Result;

namespace Project.Screen.Result
{
    public class ResultScreenTest
    {
        [UnityTest]
        public IEnumerator Factory()
        {
            ResultScreenContext context = new ResultScreenContext(90, 3);

            ResultScreenView view = new ResultScreenFactory(context).Instantiate() as ResultScreenView;
            yield return new WaitForSeconds(3000);
        }
    }
}
