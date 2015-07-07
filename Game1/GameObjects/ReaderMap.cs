using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameObjects
{
    class ReaderMap
    {
        public static string[,] Reader(string[,] MasMapsToDraw)
        {
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                int size = 26; // Карту можно описать в 26x26 клеток
                string line;
                string[] mapLine;
                MasMapsToDraw = new string[size, size];

                while (!sr.EndOfStream)
                {
                    for (int i = 0; i < size; i++)
                    {
                        line = sr.ReadLine();
                        mapLine = line.Split(' '); // TODO: Пожалуй, для удобства создания карты лучше рассмативать в input.txt не "0 1 0", а "010", а то каждый раз Пробел нажимать при составлении карты - плохая идея
                        for (int j = 0; j < size; j++)
                        {
                            MasMapsToDraw[j, i] = mapLine[j]; // Я правда не знаю, зачем нужно "переворачивать" и писать [j,i] вместо [i,j]
                        }
                    }
                }
            }
            return MasMapsToDraw;
        }

        public static HashSet<ScenicObject> getMap(string[,] MasMapsToDraw, HashSet<ScenicObject> scenicObjects, SpriteInfo image, SpriteInfo image2, SpriteInfo image3)
        {
            int cell = 16; // Ширина блока
            for (int i = 0; i < MasMapsToDraw.GetLength(0); i++)
            {
                for (int j = 0; j < MasMapsToDraw.GetLength(1); j++)
                {
                    int type = Int32.Parse(MasMapsToDraw[i, j]);
                    if (type > 0)
                    {
                        scenicObjects.Add(new ScenicObject(new Vector2(i * cell, j * cell), type, image, 1f));
                    }
                }
            }
            return scenicObjects;
        }
    }
}
