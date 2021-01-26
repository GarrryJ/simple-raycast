namespace simple_raycast {
    public class Player {
        private double coorX;
        private double coorY;
        private double fov;
        private double direction;

        public Player(double x, double y) {
            this.fov = 3.14159/3;
            this.coorX = x;
            this.coorY = y;
            this.direction = 0;
        }

        public Player(double x, double y, double dir) {
            this.fov = 3.14159/3;
            this.coorX = x;
            this.coorY = y;
            this.direction = dir;
        }

        public Player(double x, double y, double dir, double fov) {
            this.fov = fov;
            this.coorX = x;
            this.coorY = y;
            this.direction = dir;
        }

        public double getX() {
            return this.coorX;
        }
        
        public void setX(double x) {
            this.coorX = x;
        }

        public double getY() {
            return this.coorY;
        }

        public void setY(double y) {
            this.coorY = y;
        }

        public double getDirection() {
            return this.direction;
        }

        public void setDirection(double dir) {
            this.direction = dir;
        }
    }
}