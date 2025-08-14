namespace DungeonEscape
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // string emoji = "🦹";
            // Console.WriteLine($"Emoji: {emoji}");

            // 
            string[][] map = new string[20][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new string[80];
                if (i == 0 || i == map.Length - 1)
                    for (int j = 0; j < map[i].Length; j++)
                        map[i][j] = "█";
                else
                    for (int j = 0; j < map[i].Length; j++)
                        if (j == 0 || j == map[i].Length - 1)
                            map[i][j] = "█";
                        else
                            map[i][j] = " ";   
            }
            
            // bool isRunning = true;
            // while (isRunning)
            // {   


            // }
            
            map.ToList().ForEach(x =>
            {
                for (int i = 0; i < x.Length; i++)
                    Console.Write(x[i]);
                Console.WriteLine();
            });


            Console.ReadLine();
        }
    }
}