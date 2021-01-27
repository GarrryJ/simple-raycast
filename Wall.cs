using Microsoft.Xna.Framework;
using System;

namespace simple_raycast {
    public static class Wall {
        public static Color getColorById(int id, int darkness) {
            if (id == 4) 
                id = new Random().Next(3) + 1;
            Color color;
            switch (id) {
                case 1:
                    color = new Color(255 - darkness, 0, 0);
                    break;
                case 2: 
                    color = new Color(0, 255 - darkness, 0);
                    break;
                case 3: 
                    color = new Color(0, 0, 255 - darkness);
                    break;
                default: 
                    color = new Color(0, 0, 0);
                    break;
            }
            return color;
        }
    }
}