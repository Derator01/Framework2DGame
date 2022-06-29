namespace Framework2DGame.Enemies
{
    public class Enemy
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public int Rarity { get; private set; }

        public int Damage { get; private set; }

        public int HP { get; private set; }

        public int Dex { get; private set; }

        public int Def { get; private set; }
    }
}
