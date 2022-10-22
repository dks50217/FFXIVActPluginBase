﻿namespace FFXIV_ACT_BASE.Model
{
    public readonly struct GameIdx
    {
        private readonly int v;

        public GameIdx(int value)
        {
            v = value;
        }

        public static explicit operator int(GameIdx gameIdx) { return gameIdx.v; }
        public static explicit operator GameIdx(int value) { return new GameIdx(value); }

        public static explicit operator DBIdx(GameIdx gameIdx) { return (DBIdx)gameIdx.v; }
    }
}
