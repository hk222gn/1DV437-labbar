using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GetToTheEnd.Model
{
    public enum CurrentLevel
    {
        level1 = 0,
        level2,
        level3,
        END
    }

    class Level
    {
        private const int m_levelWidth = 30;
        private const int m_levelHeight = 30;
        private const string m_levelFolder = "./Content/Level/";

        public Tile[,] m_tiles = new Tile[m_levelWidth, m_levelHeight];
        private bool m_playerStruckLethalTile = false;
        private bool m_playerHitPortal = false;

        internal bool IsCollidingAt(FloatRectangle a_rect)
        {

            Vector2 tileSize = new Vector2(0.034f, 0.034f);
            for (int x = 0; x < m_levelWidth; x++)
            {
                for (int y = 0; y < m_levelHeight; y++)
                {
                    FloatRectangle rect = FloatRectangle.createFromTopLeft(new Vector2((float)x / 30, (float)y / 30), tileSize);
                    if (a_rect.isIntersecting(rect))
                    {
                        if (m_tiles[x, y].GetTileType() == TileType.PORTAL)
                        {
                            m_playerHitPortal = true;
                            return false;
                        }

                        if (m_tiles[x, y].GetTileType() == TileType.LETHAL)
                        {
                            m_playerStruckLethalTile = true;
                            return false;
                        }
                        else if (m_tiles[x, y].GetTileType() == TileType.FAKE) // When the player hits a fake tile, make it an empty tile.
                        {
                            m_tiles[x, y] = Tile.CreateEmpty();
                        }
                        if (m_tiles[x, y].isBlocked() || m_playerStruckLethalTile)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void LoadLevel(CurrentLevel a_level)
        {
            String level = m_levelFolder;

            switch (a_level)
            {
                case CurrentLevel.level1:
                    level += "Level1.txt";
                    break;
                case CurrentLevel.level2:
                    level += "Level2.txt";
                    break;
                case CurrentLevel.level3:
                    level += "Level3.txt";
                    break;
                default:
                    throw new Exception("Unable to load level");
            }

            int y = 0;
            int x = 0;

            if (File.Exists(level))
            {
                using (StreamReader sr = File.OpenText(level))
                {
                    string line;
                    string[] splittedLines;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (String.IsNullOrEmpty(line) || line[0] == ';')
                            continue;

                        splittedLines = line.Split(',');

                        for (int i = 0; i < splittedLines.Length; i++)
                        {
                            int[] tileValues = Array.ConvertAll(splittedLines, int.Parse);
                            if (tileValues[i] == (int)TileType.EMPTY)
                            {
                                m_tiles[x, y] = Tile.CreateEmpty();
                            }
                            else if (tileValues[i] == (int)TileType.BLOCKED)
                            {
                                m_tiles[x, y] = Tile.CreateBlocked();
                            }
                            else if (tileValues[i] == (int)TileType.LETHAL)
                            {
                                m_tiles[x, y] = Tile.CreateLethal();
                            }
                            else if (tileValues[i] == (int)TileType.FAKE)
                            {
                                m_tiles[x, y] = Tile.CreateFake();
                            }
                            else if (tileValues[i] == (int)TileType.PORTAL)
                            {
                                m_tiles[x, y] = Tile.CreatePortal();
                            }

                            x++;
                        }
                        y++;
                        x = 0;
                    }
                }
            }
            else
                throw new Exception("File could not be found");
        }

        public bool DidPlayIntersectWithLethalTile()
        {
            return m_playerStruckLethalTile;
        }

        public bool DidPlayerHitPortal()
        {
            return m_playerHitPortal;
        }

        public int GetLevelWidth()
        {
            return m_levelWidth;
        }

        public int GetLevelHeight()
        {
            return m_levelHeight;
        }
    }
}
