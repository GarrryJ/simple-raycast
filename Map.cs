namespace simple_raycast {
    public class Map {
        private int mapHeight {get; set;}
        private int mapWidth {get; set;}
        private int[] map {get; set;}

        public Map(int w, int h, int[] map) {
            this.map = map;
            this.mapHeight = h;
            this.mapWidth = w;
        }

        public int getMapHeigth() {
            return this.mapHeight;
        }

        public int getMapWidth() {
            return this.mapWidth;
        }

        public int[] getMap() {
            return this.map;
        }
    }
}