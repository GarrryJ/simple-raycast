﻿using System;

namespace simple_raycast
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Engine())
                game.Run();
        }
    }
}
