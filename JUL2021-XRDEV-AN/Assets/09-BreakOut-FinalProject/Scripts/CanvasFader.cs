using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Canvas fader works to show and hide a canvas group
/// </summary>

[RequireComponent(typeof(CanvasGroup))]
public sealed class CanvasFader : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Tooltip("Start this canvas group faded out")]
    bool startFadedOut = false;

    [SerializeField]
    [Tooltip("Fade in time")]
    float fadeInTime = 1f;

    [SerializeField]
    [Tooltip("Fade out time")]
    float fadeOutTime = 1f;

    [SerializeField]
    [Tooltip("sets the canvas group as non interactable when faded out, unless it was not interactable to begin with")]
    bool effectsInteractable = true;

    [SerializeField]
    [Tooltip("sets the canvas group as non b when fadlocking of reaycasts when faded out, unless it was not blocking raycasts to begin with")]
    bool effectBlocksRaycasts = true;

    // components
    CanvasGroup canvasGroup = null;

    // state 
    bool originalInteractableState = false;
    bool originalBlocksRaycastsState = false;
    float? fadeTime = null;
    float totalFadeTime = 0f;
    bool fadeIn = true;
    bool paused = false;

    #endregion

    #region Properties

    /// <summary>
    /// The faded value
    /// </summary>
    public float FadedValue { get { return fadeTime.HasValue ? (fadeTime.Value / totalFadeTime) : (fadeIn ? 1f : 0f); } }

    /// <summary>
    /// Whether or not this canvas is showing
    /// </summary>
    public bool IsShowing { get { return fadeIn; } }

    #endregion

    #region Callbacks

    /// <summary>
    /// Callback for when the canvas fader is shown
    /// </summary>
    public Action Shown;

    /// <summary>
    /// Callback for when the canvas fader is hidden
    /// </summary>
    public Action Hidden;

    /// <summary>
    /// Callback for when the canvas fader is fully shown (alpha 1)
    /// </summary>
    public Action FullyShown;

    /// <summary>
    /// Callback for when the canvas fader is fully hidden (alpha 0)
    /// </summary>
    public Action FullyHidden;

    #endregion

    #region Construction
    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalInteractableState = canvasGroup.interactable;
        originalBlocksRaycastsState = canvasGroup.blocksRaycasts;

        if (startFadedOut)
        {
            Hide(0f);
        }
        else
        {
            Show(0f);
        }
    }

    #endregion

    #region Update
    public void Update()
    {
        if (paused)
            return;

        if (fadeTime.HasValue)
        {
            // instant
            if (Mathf.Approximately(totalFadeTime, 0f))
            {
                fadeTime = null;
                canvasGroup.alpha = fadeIn ? 1f : 0f;

                if (fadeIn)
                {
                    FullyShown?.Invoke();
                }
                else
                {
                    FullyHidden?.Invoke();
                }
            }
            else
            {
                canvasGroup.alpha = Mathf.Lerp(fadeIn ? 1f : 0f, fadeIn ? 0f : 1f, fadeTime.Value / totalFadeTime);
                fadeTime -= Time.deltaTime;

                if (fadeTime < 0f)
                {
                    fadeTime = null;
                    canvasGroup.alpha = fadeIn ? 1f : 0f;

                    if (fadeIn)
                    {
                        FullyShown?.Invoke();
                    }
                    else
                    {
                        FullyHidden?.Invoke();
                    }
                }
            }
        }
    }

    #endregion

    #region Display

    /// <summary>
    /// pauses this canvas faders funcitonality
    /// </summary>
    public void Pause()
    {
        paused = true;
    }

    /// <summary>
    /// Unpause this canvas faders funcitonality
    /// </summary>
    public void Unpause()
    {
        paused = false;
    }

    /// <summary>
    /// fade in the canvas group
    /// </summary>
    public void Show()
    {
        Set(fadeInTime, true);
    }

    /// <summary>
    /// fade in the canvas group
    /// </summary>
    /// <param name="overrideTime">Override the fade in time</param>
    public void Show(float overrideTime)
    {
        Set(overrideTime, true);
    }

    /// <summary>
    /// fade out the canvas group
    /// </summary>
    public void Hide()
    {
        Set(fadeOutTime, false);
    }

    /// <summary>
    /// fade out the canvas group
    /// </summary>
    /// <param name="overrideTime">Override the fade in time</param>
    public void Hide(float overrideTime)
    {
        Set(overrideTime, false);
    }

    void Set(float fadeTime, bool value)
    {
        this.fadeTime = fadeTime;
        totalFadeTime = fadeTime;
        fadeIn = value;
        canvasGroup.interactable = effectsInteractable ? value : originalInteractableState;
        canvasGroup.blocksRaycasts = effectBlocksRaycasts ? value : originalBlocksRaycastsState;

        // callbacks
        if (value)
        {
            Shown?.Invoke();
        }
        else
        {
            Hidden?.Invoke();
        }
    }

    #endregion
}

