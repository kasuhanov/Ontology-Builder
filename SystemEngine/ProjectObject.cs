using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Imaging;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace SystemEngine
{
    public class ProjectObject
    {
        private Dictionary<ulong, ProjectObject> allObjects;
        private string name;
        private ulong id;
        private List<ulong> childObjects;

        private uint texture_text;
        private int dX, dY;

        private Vector2 position;
        public float X { get { return this.position.x; } set { this.position.x = value; } }
        public float Y { get { return this.position.y; } set { this.position.y = value; } }
        public int R { get; set; }
        public int Lsq { get; set; }

        public string Name { get { return name; } }
        public ulong ID { get { return id; } }
        public ulong this[int index] { get { return childObjects[index]; } }

        Vector2 V, A;
        private float Vmax = 2.5f, Amax = 1.5f;
        public void ClearMovable() 
        {
            V.x = 0; V.y = 0;
            A.x = 0; A.y = 0;
        }
        public ProjectObject()
        {
            this.name = "";
            this.id = 0;
            this.childObjects = new List<ulong>();
            this.R = 12;
            allObjects = null;
            this.Lsq = 10000;

            this.position = new Vector2();
            this.V = new Vector2();
            this.A = new Vector2();

            ClearMovable();
        }

        public ProjectObject(string objectName, ulong objectID, Dictionary<ulong, ProjectObject> allObjectRef)
        {
            this.name = objectName;
            this.id = objectID;
            this.childObjects = new List<ulong>();
            this.R = 15;
            allObjects = allObjectRef;
            this.Lsq = 10000;

            this.position = new Vector2();
            this.V = new Vector2();
            this.A = new Vector2();

            CreateTextTexture(24.0f);
            ClearMovable();
        }

        private void CreateTextTexture(float size)
        {
            dX = this.name.Length * (int)size;
            dY = (int)size * 2;
            texture_text = 0;
            Bitmap text_bmp = new Bitmap(this.dX, this.dY);
            Graphics gfx = Graphics.FromImage(text_bmp);
            Font font = new Font(FontFamily.GenericSerif, size);
            gfx.DrawString(this.name, font, Brushes.Black, new PointF(0, 0));
            BitmapData data = text_bmp.LockBits(new Rectangle(0, 0, text_bmp.Width, text_bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Gl.glGenTextures(1, out texture_text);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture_text);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, text_bmp.Width, text_bmp.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, data.Scan0);

            text_bmp.UnlockBits(data);
            gfx.Dispose();
        }

        #region DRAW
        public void DrawLinks()
        {
            //// DRAW LINKS ////
            //Gl.glColor3f(0.0f, 0.0f, 0.0f);
            //Gl.glBegin(Gl.GL_LINES);
            foreach (var element in this.childObjects)
            {
                Gl.glVertex3d(this.X, this.Y, -15);
                Gl.glVertex3d(this.allObjects[element].X, this.allObjects[element].Y, -15);
            }
            //Gl.glEnd();
            //// END DRAW LINKS ////
        }
        public void DrawObject()
        {
            //// DRAW OBJECT ////
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glBegin(Gl.GL_TRIANGLES);
            float d = (float)Math.PI / 8;
            for (float i = 0.0f; i < 2 * (float)Math.PI; i += d)
            {
                Gl.glVertex3f(this.X, this.Y, -10);
                Gl.glVertex3f(this.X + this.R * (float)Math.Sin(i), this.Y + this.R * (float)Math.Cos(i), -10);
                Gl.glVertex3f(this.X + this.R * (float)Math.Sin(i + d), this.Y + this.R * (float)Math.Cos(i + d), -10);
            }
            Gl.glEnd();
            //// END DRAW OBJECT ////

            //// DRAW TEXT ////
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture_text);

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0f, 1f);
            Gl.glVertex2f(this.X, this.Y);

            Gl.glTexCoord2f(1f, 1f);
            Gl.glVertex2f(this.X + this.dX, this.Y);

            Gl.glTexCoord2f(1f, 0f);
            Gl.glVertex2f(this.X + this.dX, this.Y + dY);

            Gl.glTexCoord2f(0f, 0f);
            Gl.glVertex2f(this.X, this.Y + dY);
            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            //// END DRAW TEXT ////
        }
        #endregion DRAW

        private void CheckMovable()
        {
            if (A.length() > this.Amax)
            {
                A = A.normalize() * this.Amax;
            }

            if (V.length() > this.Vmax)
            {
                V = V.normalize() * this.Vmax;
            }
        }

        public void Move(float coordX, float coordY) //push off from this position
        {
            foreach (var element in this.childObjects)
            {
                allObjects[element].MoveToParent(this.X, this.Y);
            }

            if (this.DistanceToSq((int)coordX, (int)coordY) > this.Lsq)
            {
                return;
            }

            this.A += (this.position - new Vector2(coordX, coordY)).normalize();    
        }

        public void MoveToParent(float coordX, float coordY) //push to this position
        {
            if (this.DistanceToSq((int)coordX, (int)coordY) < (this.Lsq * 3))
            {
                return;
            }

            this.A -= (this.position - new Vector2(coordX, coordY)).normalize();
        }

        public void AcceptMove()
        {
            CheckMovable();

            this.V += this.A;
            this.position += this.V;

            ClearMovable(); //or just dicrease Velocity and Acceleration ???
        }

        public void AddChild(ulong childID) 
        {
            this.childObjects.Add(childID);
        }

        public void RemoveChild(ulong childID)
        {
            this.childObjects.Remove(childID);
        }

        public int NumbersOfChildren() 
        {
            return this.childObjects.Count;
        }
        public bool IsBase() 
        {
            return this.childObjects.Count == 0;
        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }

        public override bool Equals(object obj)
        {
            bool answer = false;
            if (obj is ProjectObject)
            {
                answer = (this.ID == ((ProjectObject)obj).ID);
            }
            else answer = false;

            return answer;
        }

        public override string ToString()
        {
            return "ID = " + this.id + "| Name = " + this.name + ".";
        }

        public bool Intersect(int coordX, int coordY)
        {
            return (this.X - coordX) * (this.X - coordX) + (this.Y - coordY) * (this.Y - coordY) <= (this.R * this.R);
        }

        public uint DistanceToSq(int coordX, int coordY)
        {
            return (uint)((this.X - coordX) * (this.X - coordX)) + (uint)((this.Y - coordY) * (this.Y - coordY));
        }
    }
}
