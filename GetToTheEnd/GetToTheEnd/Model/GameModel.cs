using GetToTheEnd.View;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.Model
{
    class GameModel
    {
        class CollisionDetails
        {
            public Vector2 m_speedAfterCollision;
            public Vector2 m_positionAfterCollision;
            public bool m_hasCollidedWithGround = false;
            private Vector2 a_oldPos;
            private Vector2 a_velocity;

            public CollisionDetails(Vector2 a_oldPos, Vector2 a_velocity)
            {
                m_positionAfterCollision = a_oldPos;
                m_speedAfterCollision = a_velocity;
            }

        }

        private Player m_player;
        private Level m_level;
        private bool m_hasCollidedWithGround = false;

        public GameModel(Level a_level)
        {
            m_player = new Player();
            m_level = a_level;
        }

        public Vector2 GetPlayerPos()
        {
            return m_player.Position;
        }

        public Vector2 GetPlayerTextureSize()
        {
            return m_player.PlayerTextureSize;
        }

        internal void Update(float a_timeElapsed, Camera a_camera)
        {
            float timeStep = 0.001f;
            if (a_timeElapsed > 0)
            {
                int numIterations = (int)(timeStep / a_timeElapsed);

                for (int i = 0; i < numIterations; i++)
                {
                    UpdatePlayer(timeStep, a_camera.GetScale());
                }

                float timeLeft = a_timeElapsed - timeStep * numIterations;
                UpdatePlayer(timeLeft, a_camera.GetScale());
            }
        }

        internal void UpdatePlayer(float a_timeElapsed, float a_scale)
        {
            Vector2 oldPos = m_player.Position;

            m_player.Update(a_timeElapsed);

            m_hasCollidedWithGround = false;
            Vector2 newPos = m_player.Position;

            if (didCollide(m_player.Position, m_player.PlayerTextureSize / a_scale))
            {
                CollisionDetails details = getCollisionDetails(oldPos, newPos, m_player.PlayerTextureSize / a_scale, m_player.Speed);
                m_hasCollidedWithGround = details.m_hasCollidedWithGround;

                //set the new speed and position after collision
                //m_player.Position = details.m_positionAfterCollision;
                m_player.Position = details.m_positionAfterCollision;
                m_player.Speed = details.m_speedAfterCollision;
            }
        }

        private bool didCollide(Vector2 a_centerBottom, Vector2 a_size)
        {
            FloatRectangle occupiedArea = FloatRectangle.createFromCenterBottom(a_centerBottom, a_size);
            if (m_level.IsCollidingAt(occupiedArea))
            {
                return true;
            }
            return false;
        }

        private CollisionDetails getCollisionDetails(Vector2 a_oldPos, Vector2 a_newPosition, Vector2 a_size, Vector2 a_velocity)
        {
            CollisionDetails ret = new CollisionDetails(a_oldPos, a_velocity);

            Vector2 slidingXPosition = new Vector2(a_newPosition.X, a_oldPos.Y); //Y movement ignored
            Vector2 slidingYPosition = new Vector2(a_oldPos.X, a_newPosition.Y); //X movement ignored

            if (didCollide(slidingXPosition, a_size) == false)
            {
                return doOnlyXMovement(ref a_velocity, ret, ref slidingXPosition);
            }
            else if (didCollide(slidingYPosition, a_size) == false)
            {

                return doOnlyYMovement(ref a_velocity, ret, ref slidingYPosition);
            }
            else
            {
                return doStandStill(ret, a_velocity);
            }

        }

        private static CollisionDetails doStandStill(CollisionDetails ret, Vector2 a_velocity)
        {
            if (a_velocity.Y > 0)
            {
                ret.m_hasCollidedWithGround = true;
            }
            ret.m_speedAfterCollision = new Vector2(0, 0);
            return ret;
        }

        private static CollisionDetails doOnlyYMovement(ref Vector2 a_velocity, CollisionDetails ret, ref Vector2 slidingYPosition)
        {
            //a_velocity.X *= -0.5f; //bounce from wall
            a_velocity.X = 0f;
            ret.m_speedAfterCollision = a_velocity;
            ret.m_positionAfterCollision = slidingYPosition;
            return ret;
        }

        private static CollisionDetails doOnlyXMovement(ref Vector2 a_velocity, CollisionDetails ret, ref Vector2 slidingXPosition)
        {
            ret.m_positionAfterCollision = slidingXPosition;
            //did we slide on ground?
            if (a_velocity.Y > 0)
            {
                ret.m_hasCollidedWithGround = true;
            }

            ret.m_speedAfterCollision = doSetSpeedOnVerticalCollision(a_velocity);
            return ret;
        }

        private static Vector2 doSetSpeedOnVerticalCollision(Vector2 a_velocity)
        {
            //did we collide with ground?
            if (a_velocity.Y > 0)
            {
                a_velocity.Y = 0f; //no bounce
            }
            else
            {
                //collide with roof
                //a_velocity.Y *= -0.5f;
                a_velocity.Y = 0f;
            }

            a_velocity.X *= 0.10f;

            return a_velocity;
        }

        internal void movePlayerLeft()
        {
            m_player.Speed = new Vector2(-0.225f, m_player.Speed.Y);
        }

        internal void movePlayerRight()
        {
            m_player.Speed = new Vector2(0.225f, m_player.Speed.Y);
        }

        internal void StopPlayerMovingSideways()
        {
            m_player.Speed = new Vector2(0, m_player.Speed.Y);
        }

        internal bool Jump()
        {
            if (m_hasCollidedWithGround)
            {
                m_player.Jump();
                return true;
            }
            return false;
        }

        internal void KillPlayer()
        {
            m_player.Kill();
        }

        internal bool IsPlayerDead()
        {
            return m_player.IsDead();
        }
    }
}
