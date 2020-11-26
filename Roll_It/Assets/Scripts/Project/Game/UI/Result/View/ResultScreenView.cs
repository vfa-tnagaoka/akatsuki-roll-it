using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UDL.Core;
using DG.Tweening;
using System.Threading.Tasks;

namespace Project.Screen.Result.View
{

    public class ResultScreenView : AbstractView
    {
        [SerializeField] Transform starTransform1 = default;
        [SerializeField] Transform starTransform2 = default;
        [SerializeField] Transform starTransform3 = default;
        [SerializeField] Transform particleContainer = default;
        [SerializeField] Text percentText = default;
        [SerializeField] Button closeButton = default;
        [SerializeField] GameObject starObject = default;
        [SerializeField] ParticleSystem starExplosion = default;

        public SimpleSubject OnCloseButtonClick => closeButton.OnClickAsSimpleSubject();

        public Subject<int> percentage = new Subject<int>();

        private void Start()
        {
            ClearScreen();
            OnCloseButtonClick.Subscribe(() =>
            {
                if (starTransform1.childCount > 0 || starTransform2.childCount > 0 || starTransform3.childCount > 0)
                    ClearScreen();
            }).AddTo(disposables);
            closeButton.gameObject.SetActive(false);
        }

        public void SetData(int percent, int stars)
        {
            StartCoroutine(run());
            IEnumerator run()
            {
                UpdateText(percent);
                yield return new WaitForSeconds(1f);
                SetStar(stars);
            }
        }

        private async void SetStar(int stars)
        {
            switch (stars)
            {
                case 1:
                    SpawnStar(starTransform1);
                    await Task.Delay(500);
                    SpawnParticle(starExplosion);
                    await Task.Delay(500);
                    closeButton.gameObject.SetActive(true);
                    break;

                case 2:
                    SpawnStar(starTransform1);
                    await Task.Delay(500);
                    SpawnStar(starTransform2);
                    await Task.Delay(500);
                    SpawnParticle(starExplosion);
                    await Task.Delay(500);
                    closeButton.gameObject.SetActive(true);
                    break;

                case 3:
                    SpawnStar(starTransform1);
                    await Task.Delay(500);
                    SpawnStar(starTransform2);
                    await Task.Delay(500);
                    SpawnStar(starTransform3);
                    await Task.Delay(500);
                    SpawnParticle(starExplosion);
                    await Task.Delay(500);
                    closeButton.gameObject.SetActive(true);
                    break;

                default:
                    closeButton.gameObject.SetActive(true);
                    break;
            }
        }

        private void SpawnStar(Transform parent)
        {
            var clone = Instantiate(starObject, parent);
            var particle = clone.GetComponentsInChildren<ParticleSystem>();
            clone.transform.localPosition = Vector3.zero;
            clone.transform.SetScale(new Vector3(0.1f, 0.1f, 0.1f));
            clone.transform.DOScale(Vector3.one, 0.3f).OnComplete(() =>
            {
                foreach (var item in particle)
                {
                    item.Play();
                }
            });
        }

        private void SpawnParticle(ParticleSystem particle)
        {
            var clone = Instantiate(particle, particleContainer);
            Destroy(clone.gameObject, 3f);
        }

        private void UpdateText(int percent)
        {
            int score = 0;
            percentText.text = "0%";
            Tween t = DOTween.To(() => score, x => score = x, percent, 1f).OnUpdate(() => { percentText.text = score + "%"; }).SetDelay(0.5f);

            //DOTween.To(x => someProperty = x, 0, percent, 0.5f);
            //percentText.DOText(percent.ToString(), 1.5f, true, ScrambleMode.Numerals).OnComplete(() =>
            //  {
            //      percentText.text = percent + "%";
            //  });
        }

        public void ClearScreen()
        {
            closeButton.gameObject.SetActive(false);
            percentText.text = "0%";
            if (starTransform1.childCount > 0)
            {
                var star1 = starTransform1.GetChild(0);
                Destroy(star1.gameObject);
            }
            if (starTransform2.childCount > 0)
            {
                var star2 = starTransform2.GetChild(0);

                Destroy(star2.gameObject);
            }
            if (starTransform3.childCount > 0)
            {
                var star3 = starTransform3.GetChild(0);

                Destroy(star3.gameObject);
            }
        }
    }
}
