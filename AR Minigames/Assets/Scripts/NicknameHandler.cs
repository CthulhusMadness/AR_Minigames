using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Text.RegularExpressions;

public class NicknameHandler : MonoBehaviour
{
    #region Fields

    public ConnectionSettings connectionSettings = null;

    [SerializeField] private TMP_InputField nicknameInputField = null;
    [SerializeField] private TMP_Text inputFieldPlaceholder = null;
    [SerializeField] private Color textColor = Color.white;

    private const string placeholderText = "Enter text...";
    private const string nicknameErrorText = "Nickname not acceptable";
    private const string regexPattern = "(\\w+[\\s+]?)";
    private const string restrictionPattern = "[^A-Za-z0-9_ ]";

    public event Action OnNicknameConfirmation;

    #endregion

    #region UnityCallbacks

    private void Awake()
    {
        nicknameInputField.onSelect.AddListener(call => SetPlaceholderText(placeholderText, textColor));
        nicknameInputField.onValueChanged.AddListener(call => RestrictNicknameString());
    }

    #endregion

    #region Methods

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ConfirmNickname()
    {
        MatchCollection matches = Regex.Matches(nicknameInputField.text, regexPattern);
        string newNickname = String.Empty;
        for (int i = 0; i < matches.Count; i++)
            newNickname += matches[i];
        nicknameInputField.interactable = true;
        nicknameInputField.text = "";
        if (string.IsNullOrWhiteSpace(newNickname) || newNickname == "user")
        {
            SetPlaceholderText(nicknameErrorText, Color.red);
        }
        else
        {
            connectionSettings.nickname = newNickname;
            connectionSettings.isNewUser = false;
            OnNicknameConfirmation?.Invoke();
        }
    }

    private void SetPlaceholderText(string text, Color color)
    {
        inputFieldPlaceholder.text = text;
        inputFieldPlaceholder.color = color;
    }

    private void RestrictNicknameString()
    {
        nicknameInputField.text = Regex.Replace(nicknameInputField.text, restrictionPattern, "");
    }

    #endregion
}
