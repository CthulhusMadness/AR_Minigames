using UnityEngine;

[CreateAssetMenu(fileName = "minigame_data", menuName = "Minigames/Data")]
public class MinigameData : ScriptableObject
{
    #region Fields

    public string minigameName = string.Empty;
    public string sceneName = string.Empty;
    public Sprite icon = null;
    public Vector2Int playersQuantity = Vector2Int.one; // x = min players | y = max players

    #endregion
}
