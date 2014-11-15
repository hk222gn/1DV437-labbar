using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Labb1.Model
{
    class Ball
    {
        private Vector2 m_position;
        private Vector2 m_velocity;
        private float m_diameter;

        public Ball(Vector2 a_position, Vector2 a_velocity, float a_diameter)
        {
            m_position = a_position;
            m_velocity = a_velocity;
            m_diameter = a_diameter;
        }

       public Vector2 Position
       {
           get { return m_position; }
           set { m_position = value; }
       }

       public float Diameter
       {
           get { return m_diameter; }
           set { m_diameter = value; }
       }

       public Vector2 Velocity
       {
           get { return m_velocity; }
           set { m_velocity = value; }
       }
    }
}
