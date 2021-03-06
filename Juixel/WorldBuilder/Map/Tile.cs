﻿using Juixel.Drawing.Textures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Tools;
using WorldBuilderLib;

namespace WorldBuilder
{
    public struct Tile
    {
        private static Dictionary<int, Color> MainColors = new Dictionary<int, Color>();
        private static Color[] TextureData;

        private static Color GetMainColor(int Index, Rectangle Source)
        {
            if (TextureData == null)
            {
                TextureData = new Color[WBGame.TileSheet.Width * WBGame.TileSheet.Height];
                WBGame.TileSheet.GetData(TextureData);
            }
            if (!MainColors.ContainsKey(Index))
            {
                int Count = 0, RCount = 0, GCount = 0, BCount = 0;
                Color Color;
                for (int y = Source.Y; y < Source.Y + Source.Height; y++)
                    for (int x = Source.X; x < Source.X + Source.Width; x++)
                    {
                        Color = TextureData[y * WBGame.TileSheet.Width + x];
                        Count++;
                        RCount += Color.R;
                        GCount += Color.G;
                        BCount += Color.B;
                    }
                MainColors[Index] = new Color(RCount / Count, GCount / Count, BCount / Count);
            }
            return MainColors[Index];
        }

        public Sprite Sprite;
        public ushort Type;
        public Color MainColor;

        public Tile(TileData Data)
        {
            int Index = Data.Indexes[JRandom.Next(Data.Indexes.Length)];
            int XLength = WBGame.TileSheet.Width / 8;
            Sprite = new Sprite(WBGame.TileSheet, new Rectangle(Index % XLength * 8, Index / XLength * 8, 8, 8));
            Type = Data.Type;
            MainColor = GetMainColor(Type, Sprite.Source);
        }
    }
}
