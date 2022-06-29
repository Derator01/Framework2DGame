namespace Framework2DGame.Enemies
{
    public class EnemyType
    {
        public EnemyType(string name, int rarity, int damage, int hp, int dex, int def)
        {
            Name = name;
            Rarity = rarity;
            Damage = damage;
            HP = hp;
            Dex = dex;
            Def = def;
        }

        public string Name { get; private set; }
              
        public int Rarity { get; private set; }
              
        public int Damage { get; private set; }
        
        public int HP { get; private set; }

        public int Dex { get; private set; }
              
        public int Def { get; private set; }
    }
}
