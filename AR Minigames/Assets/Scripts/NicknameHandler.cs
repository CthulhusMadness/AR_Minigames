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
    private const string regexPattern = "[A-Za-z]+\\s?";

    public event Action OnNicknameConfirmation;

    #endregion

    #region UnityCallbacks

    private void Awake()
    {
        nicknameInputField.onSelect.AddListener(str => SetPlaceholderText(placeholderText,textColor));
    }

    #endregion

    #region Methods

    public void ConfirmNickname ()
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
            OnNicknameConfirmation?.Invoke();
        }
    }

    private void SetPlaceholderText(string text, Color color)
    {
        inputFieldPlaceholder.text = text;
        inputFieldPlaceholder.color = color;
    }

    #endregion
}
