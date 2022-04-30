using System;

[Serializable]
public class GameResult
{
    public string playerName;
    public string className;
    public string level;

    public GameResult(string pName, string cName, string lvl)
    {
        playerName = pName; 
        className = cName;
        level = lvl;
    }
}
