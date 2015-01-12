using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.Model
{
    public enum TileType
    {
        EMPTY = 0,
        BLOCKED,
        LETHAL,
        FAKE,
        PORTAL
    }

    class Tile
    {
        private TileType m_type;

        public Tile(TileType a_tileType)
        {
            m_type = a_tileType;
        }

        internal static Tile CreateBlocked()
        {
            return new Tile(TileType.BLOCKED);
        }

        internal static Tile CreateEmpty()
        {
            return new Tile(TileType.EMPTY);
        }

        internal static Tile CreateFake()
        {
            return new Tile(TileType.FAKE);
        }

        internal static Tile CreateLethal()
        {
            return new Tile(TileType.LETHAL);
        }

        internal static Tile CreatePortal()
        {
            return new Tile(TileType.PORTAL);
        }

        internal bool isBlocked()
        {
            return m_type == TileType.BLOCKED;
        }

        internal TileType GetTileType()
        {
            return m_type;
        }
    }
}
