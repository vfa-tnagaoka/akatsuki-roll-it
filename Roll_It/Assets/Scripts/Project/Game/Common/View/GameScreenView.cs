using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UDL.Core;

namespace Project.Game.View
{

    public class GameScreenView : AbstractView
    {
        [SerializeField] private Button btnPlay;
        [SerializeField] private Button btnNext;
        [SerializeField] private Button btnPlayAgain;

        public Subject<Unit> OnPlayBtn = new Subject<Unit>();
        public Subject<Unit> OnNextBtn = new Subject<Unit>();
        public Subject<Unit> OnPlayAgainBtn = new Subject<Unit>();

        public void Init()
        {
            btnPlay.gameObject.SetActive(true);
            btnNext.gameObject.SetActive(false);

            btnPlay.onClick.AddListener(OnClickBtnPlay);
            btnNext.onClick.AddListener(OnClickBtnNext);
            btnPlayAgain.onClick.AddListener(OnClickBtnPlayAgain);

            OnClickBtnPlay();
        }

        public void PlayAgain()
        {
            btnPlay.gameObject.SetActive(false);
            btnNext.gameObject.SetActive(false);
            btnPlayAgain.gameObject.SetActive(true);
        }

        private void OnClickBtnPlayAgain()
        {
            OnPlayAgainBtn.OnNext(Unit.Default);
            btnPlayAgain.gameObject.SetActive(false);
            btnNext.gameObject.SetActive(true);
        }

        private void OnClickBtnPlay()
        {
            OnPlayBtn.OnNext(Unit.Default);
            btnPlay.gameObject.SetActive(false);
            btnNext.gameObject.SetActive(true);
            btnNext.enabled = true;
        }

        private void OnClickBtnNext()
        {
            OnNextBtn.OnNext(Unit.Default);
        }

        public void LoadMap()
        {
            btnPlay.gameObject.SetActive(true);
            btnNext.gameObject.SetActive(false);
        }

        public void TurnOnBtnNext()
        {
            if (btnNext.enabled) return;
            btnNext.gameObject.SetActive(true);
            btnNext.enabled = true;
        }

    }
}
