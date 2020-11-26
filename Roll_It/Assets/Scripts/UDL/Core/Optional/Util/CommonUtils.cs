﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public static class CommonUtils
{
    public static void OnButtonClick(this Button button, System.Action action)
    {
        if (button == null) return;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action());
    }

    public static void OnToggleChange(this Toggle toggle, System.Action<bool> action)
    {
        if (toggle == null) return;
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener((val) => action(val));
    }

    public static void OnSliderChange(this Slider slider, System.Action<float> action)
    {
        if (slider == null) return;
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener((val) => action(val));
    }

    public static bool GetToggle(this Toggle toggle)
    {
        if (toggle != null) return toggle.isOn;
        return false;
    }

    public static void SetToggle(this Toggle toggle, bool isOn)
    {
        if (toggle != null) toggle.isOn = isOn;
    }

    public static void SetSlider(this Slider slider, float val)
    {
        if (slider != null) slider.value = val;
    }

    public static float GetSlider(this Slider slider)
    {
        if (slider != null) return slider.value;
        return 0;
    }

    public static void SetColor(this Image img, Color color)
    {
        if (img != null) img.color = color;
    }   

    public static void ChangeColor(this CanvasRenderer canvasRenderer, Color color)
    {
        if (canvasRenderer != null)
        {
            canvasRenderer.SetColor(color);
        }
    }

    #region animator
    public static List<Animator> SetBool(this List<Animator> source, string key, bool result)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.SetBool(key, result);
                }
            }
        }
        return source;
    }

    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    public static List<Animator> SetTrigger(this List<Animator> source, string key)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.SetTrigger(key);
                                    }
            }
        }
        return source;
    }

    public static List<Animator> ResetTrigger(this List<Animator> source, string key)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.ResetTrigger(key);
                }
            }
        }
        return source;
    }

    public static List<Animator> SetFloat(this List<Animator> source, string key, float value)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.SetFloat(key, value);
                }
            }
        }
        return source;
    }

    public static List<Animator> SetSpeed(this List<Animator> source, float speed)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled)
            {
                animator.speed = speed;
            }
        }
        return source;
    }

    public static List<Animator> PlayBackTime(this List<Animator> source, float time)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled)
            {
                animator.playbackTime = time;
            }
        }
        return source;
    }

    public static List<Animator> PlayAtTime(this List<Animator> source, string stateName, float time)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.runtimeAnimatorController != null)
            {
                animator.Play(stateName, 0, time);
            }
        }
        return source;
    }

    #endregion

    #region partical system
    public static List<ParticleSystem> SetPause(this List<ParticleSystem> source, bool isPause)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var particle = source[i];
            if (isPause)
            {
                particle.Pause(true);
            }
            else
            {
                particle.Play(true);
            }
        }
        return source;
    }
    #endregion
}





