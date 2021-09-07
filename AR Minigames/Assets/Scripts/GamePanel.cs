using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    #region Fields

    public MinigameData minigameData = null;

    [SerializeField] TMP_Text title = null;
    [SerializeField] Image icon = null;
    [SerializeField] TMP_Text playersText = null;

    #endregion

    #region UnityCallbacks



    #endregion

    #region Methods

    public void Initialize(MinigameData data)
    {
        minigameData = data;
        title.text = minigameData.minigameName;
        icon.sprite = minigameData.icon;
        int minPlayers = minigameData.playersQuantity.x;
        int maxPlayers = minigameData.playersQuantity.y;
        string playersQuantity = "players ";
        playersQuantity += minPlayers == maxPlayers ? $"{minPlayers}" : $"{minPlayers}-{maxPlayers}";
        playersText.text = playersQuantity;
    }

    #endregion
}
