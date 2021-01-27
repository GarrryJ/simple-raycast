using Microsoft.Xna.Framework;
using System;

namespace simple_raycast {
    public static class Wall {
        public static Color getColorById(int id, int darkness) {
            if (id == 4)
                id = new Random().Next(3) + 1;
            switch (id) {
                case 1: return new Color(255 - darkness, 0, 0);
                case 2: return new Color(0, 255 - darkness, 0);
                case 3: return new Color(0, 0, 255 - darkness);
                default: return new Color(0, 0, 0);
            }
        }
    }
}