public class Player
{
    public string name { get; private set; }
    protected PlayerType type;
    private int xPosition, yPosition;
    

    public Player(string name)
    {
        this.name = name;
        this.type = PlayerType.human;
        this.xPosition = 0;
        this.yPosition = 0;
    }
    
    public string GetPlayerIcon()
    {
        return type switch
        {
            PlayerType.princess => "👸",
            PlayerType.spy => "🕵",
            PlayerType.elf => "🧝",
            PlayerType.mage => "🧙",
            PlayerType.superhero => "🦸",
            PlayerType.human => "🙎",
            PlayerType.prince => "🤴",
            _ => throw new Exception("Error: PlayerType not found...")
        };
    }

    /// <summary>
    /// Manually override player position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPos(int x, int y)
    {
        this.xPosition = x;
        this.yPosition = y;
    }

    public void MoveLeft(int borderLeft) => xPosition = (xPosition == borderLeft) ? xPosition : xPosition--;
    public void MoveRight(int borderRight) => xPosition = (xPosition == borderRight) ? xPosition : xPosition++;
    public void MoveUp(int borderTop) => yPosition = (yPosition == borderTop) ? yPosition : yPosition--;
    public void MoveDown(int borderBottom) => yPosition = (yPosition == borderBottom) ? yPosition : yPosition++;
}