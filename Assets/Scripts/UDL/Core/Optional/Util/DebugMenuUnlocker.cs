using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UDL.Core
{
    public class DebugMenuUnlocker : MonoBehaviour
    {
        static string debuggerModeKey = "debuggerMode";
        static bool unlocked {
            get{
                return PlayerPrefsUtil.GetString(debuggerModeKey, "") == "unlocked";
            }
            set{
                PlayerPrefsUtil.SetString(debuggerModeKey, value == true ? "unlocked" : "");
            }
        }

        [SerializeField] Button[] triggers = null;
        [SerializeField] Button button = null;

        int triggerIndex = 0;
        int count = 0;

        SimpleSubject action = new SimpleSubject();

        private void Awake()
        {
            for(int i = 0; i<triggers.Length; i++)
            {
                int index = i;
                Button trigger = triggers[i];
                trigger.onClick.AddListener(() => { OnTriggerClick(index); });
            }
            button.SetActive(false);
            button.onClick.AddListener(() => {
                action.OnNext();
            });

            CheckVisibility();
        }

        void OnTriggerClick(int index)
        {
            if(index == triggerIndex)
            {
                triggerIndex++;
                if(triggerIndex == triggers.Length)
                {
                    triggerIndex = 0;
                    count++;
                    if(count > 4)
                    {
                        count = 0;
                        unlocked = !unlocked;
                        CheckVisibility();
                    }
                }
            }
            else
            {
                count = 0;
            }
        }

        public SimpleSubject OnClickAsSimpleSubject()
        {
            return action;
        }

        void CheckVisibility()
        {
            button.SetActive(unlocked);
        }
    }
}