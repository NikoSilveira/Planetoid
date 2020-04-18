[System.Serializable]

public class PlayerData
{
    public int customButtonIndex;

    public PlayerData(MainMenu mainMenu)
    {
        customButtonIndex = mainMenu.customButtonIndex;
    }
}
