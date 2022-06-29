using Framework2DGame;
using Framework2DGame.Enemies;
using SimpleGame.Items.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace SimpleGame
{
    public class Game
    {
        #region Threading 
        public Game()
        {
            _enemyTypes = AllEnemies.GetDictionary();

            Thread _game = new Thread(LevelSeq);
            _game.Start();

            Thread _damSeq = new Thread(DamageSeq);
            _damSeq.Start();

            if (_levels.Count == 0)
                GenerateNewLevel();
        }
        #endregion


        private readonly List<char[,]> _levels = new List<char[,]>()
    {
        new char[,]
        {
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            { ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            { ' ', '#', ' ', 'e', '*', '$', ' ', ' ', ' ', ' '},
            { ' ', '#', 'E', '#', '#', '#', ' ', ' ', ' ', ' '},
            { ' ', '#', ' ', '#', '#', '#', ' ', ' ', ' ', ' '},
            { ' ', '#', ' ', '#', '#', '#', ' ', ' ', ' ', ' '},
            { ' ', '#', ' ', '#', '$', '#', ' ', ' ', ' ', ' '},
            { ' ', '#', ' ', '#', ' ', '#', ' ', ' ', ' ', ' '},
            { ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' '},
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}
        }
    };

        #region Coords
        public int X { get; private set; } = 1;
        public int Y { get; private set; } = 1;
        public int Plane { get; private set; } = 0;
        public char CurrentPos { get => _levels[Plane][Y, X]; }

        private readonly Dictionary<int, int> Cycles = new Dictionary<int, int>() { { 0, 0 } };
        public int CurrentCycle { get; private set; } = 0;
        #endregion

        #region PlayerProps
        public char PlayerChar { get; private set; } = '@';
        public int HP { get; private set; } = 100;

        public int Atk { get; private set; } = 10;
        public int Agi { get; private set; } = 2;
        public int Def { get; private set; } = 2;
        public int Level { get; private set; } = 0;
        public int Money { get; private set; } = 0;
        public int PowerUps { get; private set; } = 0;
        public bool IsAlive { get; private set; } = true;

        private readonly List<IItem> _inventory = new List<IItem>();
        #endregion

        #region Misc
        private readonly Dictionary<char, EnemyType> _enemyTypes;
        private readonly Dictionary<char, EnemyType> _enemies;

        private bool DamageCame;
        #endregion

        public char[,] GetMap()
        {
            char[,] map = new char[_levels[Plane].GetLength(0), _levels[Plane].GetLength(1)];

            for (int x = 0; x < _levels[Plane].GetLength(0); x++)
                for (int y = 0; y < _levels[Plane].GetLength(1); y++)
                    map[y, x] = _levels[Plane][y, x];

            map[Y, X] = PlayerChar;

            return map;
        }

        #region MoveSeqs
        public void MoveUp()
        {
            int y = Y;
            y--;

            char next = _levels[Plane][y, X];

            if (y < 0 || next == ' ')
                return;
            if (_enemyTypes.ContainsKey(next))
            {
                Battle(next, y, X);
                return;
            }

            Y--;
        }

        public void MoveRight()
        {
            int x = X;
            x++;

            char next = _levels[Plane][Y, x];

            if (x < 0 || next == ' ')
                return;
            if (_enemyTypes.ContainsKey(next))
            {
                Battle(next, Y, x);
                return;
            }
            X++;
        }

        public void MoveDown()
        {
            int y = Y;
            y++;

            char next = _levels[Plane][y, X];

            if (y < 0 || next == ' ')
                return;
            if (_enemyTypes.ContainsKey(next))
            {
                Battle(next, y, X);
                return;
            }

            Y++;
        }

        public void MoveLeft()
        {
            int x = X;
            x--;

            char next = _levels[Plane][Y, x];

            if (x < 0 || next == ' ')
                return;
            if (_enemyTypes.ContainsKey(next))
            {
                Battle(next, Y, x);
                return;
            }
            X--;
        }
        #endregion

        #region ActionSeqs
        private void Battle(char enemySign, int x, int y)
        {
            if (!IsAlive)
                return;

            EnemyType enemy = _enemyTypes[(char)enemySign];

            if (Atk - enemy.Def >= enemy.HP)
            {
                _levels[Plane][x, y] = '#';
                return;
            }
            else
            {

            }


            if (enemy.Damage > Def)
                HP -= enemy.Damage - Def;
            
        }

        private void AddEtem(IItem item)
        {
            var itemRef = _inventory.ContainsItem(item);

            if (itemRef == null)
            {
                _inventory.Add(item);
            }
            else
            {
                itemRef.Count++;
            }
        }

        private void GenerateNewLevel()
        {
            throw new System.NotSupportedException();
        }
        #endregion

        #region Loops
        private void DamageSeq()
        {
            while (IsAlive)
            {
                switch (CurrentPos)
                {
                    case '$':
                        {
                            Money += 10;
                            _levels[Plane][Y, X] = '#';
                        }
                        break;
                    case '*':
                        {
                            if (!DamageCame)
                            {
                                HP -= 10;
                                DamageCame = true;
                            }
                        }
                        break;
                }
                Thread.Sleep(1);
            }
        }

        private void LevelSeq()
        {
            while (IsAlive)
            {
                if (CurrentCycle > --Cycles[Plane])
                {
                    CurrentCycle = 0;

                    for (int x = 0; x < _levels[Plane].GetLength(0); x++)
                        for (int y = 0; y < _levels[Plane].GetLength(1); y++)
                            if (_levels[Plane][y, x] == '*')
                                _levels[Plane][y, x] = '0';
                            else if (_levels[Plane][y, x] == '0')
                                _levels[Plane][y, x] = '*';
                }
                else
                    CurrentCycle++;

                if (HP < 1)
                {
                    IsAlive = false;
                    PlayerChar = 'X';
                }
                DamageCame = false;
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}