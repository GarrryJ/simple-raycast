using System;

namespace simple_raycast {
    public class Player {
        private const double DIR_SPEED = 0.03;
        private const double DEFAULT_PLAYER_SPEED = 0.1;
        private const double DEFAULT_PLAYER_DEPTH = 7;
        private const double DEFAULT_PLAYER_FOV = Math.PI/3;
        private double speed;
        private double coorX;
        private double coorY;
        private double fov;
        private double direction;
        private double depth;

        public Player(double x, double y) {
            this.depth = DEFAULT_PLAYER_DEPTH;
            this.speed = DEFAULT_PLAYER_SPEED;
            this.fov = DEFAULT_PLAYER_FOV;
            this.coorX = x;
            this.coorY = y;
            this.direction = 0;
        }

        public Player(double x, double y, double dir) {
            this.depth = DEFAULT_PLAYER_DEPTH;
            this.speed = DEFAULT_PLAYER_SPEED;
            this.fov = DEFAULT_PLAYER_FOV;
            this.coorX = x;
            this.coorY = y;
            this.direction = dir;
        }

        public Player(double x, double y, double dir, double fov) {
            this.depth = DEFAULT_PLAYER_DEPTH;
            this.speed = DEFAULT_PLAYER_SPEED;
            this.fov = fov;
            this.coorX = x;
            this.coorY = y;
            this.direction = dir;
        }

        private void checkDirection() {
            if (this.direction >= Math.PI*2)
                this.direction = 0;
            if (this.direction <= -Math.PI*2)
                this.direction = 0;
        }

        public void turnLeft() {
            this.direction = this.direction - DIR_SPEED;
            checkDirection();
        }

        public void turnRight() {
            this.direction = this.direction + DIR_SPEED;
            checkDirection();
        }

        public void goForward() {
            this.coorX = this.coorX + (Math.Sin(this.direction) * speed);
            this.coorY = this.coorY + (Math.Cos(this.direction) * speed);
        }

        public void goBack() {
            this.coorX = this.coorX - (Math.Sin(this.direction) * speed);
            this.coorY = this.coorY - (Math.Cos(this.direction) * speed);
        }

        public double getX() {
            return this.coorX;
        }
        
        public void setX(double x) {
            this.coorX = x;
        }

        public double getSpeed() {
            return this.speed;
        }
        
        public void setSpeed(double s) {
            this.speed = s;
        }

        public double getY() {
            return this.coorY;
        }

        public void setY(double y) {
            this.coorY = y;
        }

        public double getDepth() {
            return this.depth;
        }

        public void setDepth(double d) {
            this.depth = d;
        }

        public double getDirection() {
            return this.direction;
        }

        public void setDirection(double dir) {
            this.direction = dir;
        }

        public double getFOV() {
            return this.fov;
        }

        public void setFOV(double fov) {
            this.fov = fov;
        }
    }
}