using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UDL.Core;
using DG.Tweening;

public class WinScreen : MonoBehaviour
{
    public SimpleSubject Next = new SimpleSubject();

    [SerializeField] Image[] stars = default;
    [SerializeField] Image starParticlePrefab = default;
    [SerializeField] Text messageText = default;
    [SerializeField] Text timeText = default;
    [SerializeField] Button nextButton = default;
    [SerializeField] GameObject scoreContainer = default;

    [SerializeField] Color[] particleColors = default;

    [SerializeField] GameObject[] effectConfetties = default;

    private void Awake()
    {
        this.gameObject.SetActive(false);
        this.starParticlePrefab.SetActive(false);
        nextButton.onClick.AddListener(() =>
        {
            Debug.Log("---> click next Button");
            Next.OnNext();
            this.gameObject.SetActive(false);
        });
    }

    public void Show(float time)
    {
        this.gameObject.SetActive(true);

        string timeString = "" + ((int)(time * 10)) / 10.0f;
        timeText.text = timeString;

        foreach (var effectConfetti in effectConfetties)
        {
            effectConfetti.SetActive(false);
        }

        foreach (var star in stars)
        {
            star.gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        }

        float delay = 0;
        foreach (var star in stars)
        {
            star.gameObject.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBounce).SetDelay(delay);
            StartCoroutine(DrawParticle(star.transform, delay));
            delay += 0.2f;
        }

        StartCoroutine(ShowConfetiti(1.5f));

        messageText.GetComponent<RectTransform>().localScale = Vector3.zero;
        messageText.gameObject.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBounce).SetDelay(delay);

        delay += 0.1f;

        scoreContainer.GetComponent<RectTransform>().localScale = Vector3.zero;
        scoreContainer.gameObject.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBounce).SetDelay(delay);

        delay += 0.1f;

        nextButton.GetComponent<RectTransform>().localScale = Vector3.zero;
        nextButton.gameObject.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBounce).SetDelay(delay);
    }

    IEnumerator DrawParticle(Transform root, float delay)
    {
        List<Image> ps = new List<Image>();
        for (int i = 0; i < 10; i++)
        {
            var star = Instantiate(starParticlePrefab);
            star.transform.SetParent(root.transform);
            star.transform.localPosition = Vector3.zero;
            star.SetActive(false);
            ps.Add(star);
        }

        for (int i = 0; i < 10; i++)
        {
            var star = ps[i];
            star.SetActive(true);

            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float range = 60;
            Vector3 targetPosition = new Vector3(Mathf.Cos(angle) * range, Mathf.Sin(angle) * range, 1);

            float duration = 1.0f;
            float d = delay + Random.Range(0.0f, 0.1f);
            star.transform.DOLocalMove(targetPosition, duration).SetDelay(d);
            star.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
            star.transform.DOScale(0, duration).SetDelay(d).OnStart(() =>
            {
                //star.transform.localScale = Vector3.one;
            });

            Color c = particleColors[i % particleColors.Length];

            star.color = new Color(c.r, c.g, c.b, 0);
            star.DOColor(new Color(c.r, c.g, c.b, 1), duration).SetDelay(d);


        }
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator ShowConfetiti(float timeShow)
    {
        foreach (var effectConfetti in effectConfetties)
        {
            effectConfetti.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            effectConfetti.GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(timeShow);

        foreach (var effectConfetti in effectConfetties)
        {
            effectConfetti.GetComponent<ParticleSystem>().Stop();
            yield return new WaitForSeconds(0.03f);
            effectConfetti.SetActive(false);
        }
    }
}
