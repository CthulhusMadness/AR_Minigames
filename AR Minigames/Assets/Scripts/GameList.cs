using System.Collections.Generic;
using UnityEngine;

public class GameList : MonoBehaviour
{
    #region Fields

    [SerializeField] private List<MinigameData> minigames = new List<MinigameData>();
    [SerializeField] private GameObject gamePanelPrefab = null;

    #endregion

    #region UnityCallbacks

    private void Start()
    {
        //SpawnGamePanels();
    }

    #endregion

    #region Methods

    public void SpawnGamePanels()
    {
        for (int i = 0; i < minigames.Count; i++)
        {
            var instance = Instantiate(gamePanelPrefab, transform);
            GamePanel gamePanel = instance.GetComponent<GamePanel>();
            gamePanel.Initialize(minigames[i]);
        }
    }

    #endregion
}
