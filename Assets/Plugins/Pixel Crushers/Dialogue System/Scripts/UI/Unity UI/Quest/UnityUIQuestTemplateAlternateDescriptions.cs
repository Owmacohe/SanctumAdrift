// Copyright (c) Pixel Crushers. All rights reserved.

using TMPro;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{

    [System.Serializable]
    public class UnityUIQuestTemplateAlternateDescriptions
    {

        [Tooltip("(Optional) If set, use if state is success")]
        public TMP_Text successDescription;

        [Tooltip("(Optional) If set, use if state is failure")]
        public TMP_Text failureDescription;

        public void SetActive(bool value)
        {
            if (successDescription != null) successDescription.gameObject.SetActive(value);
            if (failureDescription != null) failureDescription.gameObject.SetActive(value);
        }

    }

}
