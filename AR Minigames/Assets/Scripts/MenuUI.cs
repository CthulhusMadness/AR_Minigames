using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    #region Fields

    [SerializeField] private ConnectionManager connectionManager = null;
    [SerializeField] private NicknameHandler nicknameHandler = null;

    public Loading loading = null;

    [HideInInspector]
    public bool userConvaidationCompleted = false;

    #endregion

    #region UnityCallbacks

    private void Start()
    {
        connectionManager.OnNewUser += nicknameHandler.Show;
        connectionManager.OnConnectingEnd += loading.StopLoading;
        nicknameHandler.OnNicknameConfirmation += () =>
        {
            userConvaidationCompleted = true;
            nicknameHandler.Hide();
        };
        nicknameHandler.Hide();
        StartCoroutine(StartRoutine());
    }

    #endregion

    #region Methods

    private IEnumerator StartRoutine()
    {
        // check if the application is just got open
        if (Time.realtimeSinceStartup < 10f)
        {
            loading.StartLoading();
            yield return new WaitForSeconds(1f);
            userConvaidationCompleted = connectionManager.Initiate();
            if (!userConvaidationCompleted)
                loading.StopLoading();
            yield return new WaitUntil(() => userConvaidationCompleted);
            loading.StartLoading();
            connectionManager.Initiate();
        }
    }

    #endregion
}
