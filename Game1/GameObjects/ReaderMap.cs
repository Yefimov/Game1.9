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
    class ReaderMap : ScenicObject
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
                    if (MasMapsToDraw[i, j] == "1") // Описание каждого типа объекта (от 1 до 4) дано в классе ScenicObject
                    {
                        scenicObjects.Add(new ScenicObject(i * cell, j * cell, 100, 1, image));
                    }
                    if (MasMapsToDraw[i, j] == "2")
                    {
                        scenicObjects.Add(new ScenicObject(i * cell, j * cell, 100, 2, image2));
                    }
                    if (MasMapsToDraw[i, j] == "3")
                    {
                        scenicObjects.Add(new ScenicObject(i * cell, j * cell, 100, 3, image3));
                    }
                    // TODO: Добавить текстуру воды
                    //if (MasMapsToDraw[i,j] == "4")
                    //{
                    //    scenicObjects.Add(new ScenicObject(X1, X1, 100, 4, image4));
                    //}
                    // TODO: Добавить текстуру базы игрока ("орёл")
                    //if (MasMapsToDraw[i,j] == "4")
                    //{
                    //    scenicObjects.Add(new ScenicObject(X1, X1, 100, 4, image4));
                    //}
                }
            }
            return scenicObjects;
        }
    }
}
