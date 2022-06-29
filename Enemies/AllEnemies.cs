using System;
using System.Collections.Generic;
using System.IO;

namespace Framework2DGame.Enemies
{
    public static class AllEnemies
    {
        private static string _fullPath = "./Hell.cfg";

        public static Dictionary<char, EnemyType> GetDictionary()
        {
            try
            {
                if (!File.Exists(_fullPath))
                    throw new ArgumentNullException();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            string[] fileStrings = File.ReadAllLines(_fullPath);

            Dictionary<char, EnemyType> enemies = new Dictionary<char, EnemyType>();

            foreach (string str in fileStrings)
            {
                if (str == string.Empty)
                    continue;

                string[] stats = str.Split(' ');

                if (stats.Length != 7)
                    continue;
                try
                {
                    EnemyType enemy = new EnemyType(stats[1], int.Parse(stats[2]), int.Parse(stats[3]), int.Parse(stats[4]), int.Parse(stats[5]), int.Parse(stats[6]));
                    enemies.Add(char.Parse(stats[0]), enemy);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            try
            {
                if (enemies.Count < 1)
                    throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return enemies;
        }
    }
}
