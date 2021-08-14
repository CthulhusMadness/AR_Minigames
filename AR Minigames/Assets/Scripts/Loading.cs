using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Loading : MonoBehaviour
{
    #region Fields

    [SerializeField] private float animationLapse = .2f;

    private TMP_Text loadingText = null;
    private IEnumerator coroutine = null;

    private bool isLoading = false;

    #endregion

    #region UnityCallbacks

    private void Awake()
    {
        loadingText = GetComponent<TMP_Text>();
        loadingText.enabled = false;
    }

    #endregion

    #region Methods

    public void StartLoading()
    {
        isLoading = true;
        loadingText.enabled = true;
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = LoadingRoutine();
        StartCoroutine(coroutine);
    }

    public void StopLoading()
    {
        isLoading = false;
        loadingText.enabled = false;
        StopCoroutine(coroutine);
    }


    private IEnumerator LoadingRoutine()
    {
        loadingText.text = string.Empty;
        while (isLoading)
        {
            loadingText.text += ".";
            if (loadingText.text.Length > 3)
                loadingText.text = ".";
            yield return new WaitForSeconds(animationLapse);
        }
    }

    #endregion
}
