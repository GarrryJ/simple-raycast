using System;

namespace simple_raycast {
    public class Player {
        private double speed;
        private double coorX;
        private double coorY;
        private double fov;
        private double direction;
        private double depth;

        public Player(double x, double y) {
            this.depth = 30;
            this.speed = 5;
            this.fov = 3.14159/3;
            this.coorX = x;
            this.coorY = y;
            this.direction = 0;
        }

        public Player(double x, double y, double dir) {
            this.depth = 30;
            this.speed = 5;
            this.fov = 3.14159/3;
            this.coorX = x;
            this.coorY = y;
            this.direction = dir;
        }

        public Player(double x, double y, double dir, double fov) {
            this.depth = 30;
            this.speed = 5;
            this.fov = fov;
            this.coorX = x;
            this.coorY = y;
            this.direction = dir;
        }

        public void turnLeft() {
            this.direction = this.direction + 1.5;
        }

        public void turnRight() {
            this.direction = this.direction - 1.5;
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