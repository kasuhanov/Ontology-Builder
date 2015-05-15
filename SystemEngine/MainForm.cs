using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dbtest;
using MiniDB;
using System.Drawing.Imaging;

using Tao.FreeGlut;
using Tao.OpenGl;

namespace SystemEngine
{
    public partial class MainForm : Form
    {
        bool movable = true;


        public MainForm()
        {
            InitializeComponent();
            OpenGlWindow.InitializeContexts();
        }

        private void SetSize()
        {
            // задем размер окна программы изходя из разрешения
            OpenGlWindow.Width = Screen.PrimaryScreen.WorkingArea.Width;
            OpenGlWindow.Height = Screen.PrimaryScreen.WorkingArea.Height;
            OpenGlWindow.Location = new Point(0, 23);

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; //убирает рамку
            this.WindowState = FormWindowState.Maximized;
        }

        private void OpenGlWindow_Load()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGB | Glut.GLUT_DEPTH);
            Gl.glEnable(Gl.GL_DEPTH_TEST); ////
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, OpenGlWindow.Width, OpenGlWindow.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(0, (float)OpenGlWindow.Width, 0, (float)OpenGlWindow.Height, 0, 50);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glAlphaFunc(Gl.GL_GREATER, 0.5f);
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
        }

        Dictionary<ulong, ProjectObject> objects;

        private void DataLoadFrobDB()
        {
            Database DB = Database.Open("ProjectDB");
            TableObjects allObjects = TableObjects.Bind(DB);
            TableObjectsLinks linksOfObjects = TableObjectsLinks.Bind(DB);

            /*
            MiniDB.ObjectRecord[] recs1 = new MiniDB.ObjectRecord[5];
            recs1[0] = new ObjectRecord(0, "Мама");
            recs1[1] = new ObjectRecord(1, "Папа");
            recs1[2] = new ObjectRecord(2, "Сын");
            recs1[3] = new ObjectRecord(3, "Женщина");
            recs1[4] = new ObjectRecord(4, "Мужчина");
            allObjects.AddRecords(recs1);

            MiniDB.ObjectsLinkRecord[] recs2 = new MiniDB.ObjectsLinkRecord[5];
            recs2[0] = new MiniDB.ObjectsLinkRecord(2, 0);
            recs2[1] = new MiniDB.ObjectsLinkRecord(2, 1);
            recs2[2] = new MiniDB.ObjectsLinkRecord(0, 3);
            recs2[3] = new MiniDB.ObjectsLinkRecord(1, 4);
            recs2[4] = new MiniDB.ObjectsLinkRecord(2, 4);
            linksOfObjects.AddRecords(recs2);
            */
            try
            {
                objects = new Dictionary<ulong, ProjectObject>();
                MiniDB.ObjectRecord[] help1 = allObjects.GetAllRecords();
                foreach (MiniDB.ObjectRecord obj in help1)
                {
                    objects.Add((ulong) obj.ID, new ProjectObject(obj.Name, (ulong) obj.ID, objects));
                }
                help1 = null;
                ObjectsLinkRecord[] help2 = linksOfObjects.GetAllRecords();
                foreach (MiniDB.ObjectsLinkRecord obj in help2)
                {
                    objects[(ulong) obj.ParentID].AddChild((ulong) obj.ChildID);
                }
                help2 = null;
            }
            catch (Exception e)
            {
            }
            Random rnd = new Random(0);           
            int pos = 0;
            int maxValue = (int)Math.Pow(objects.Count, 0.5) * 200;
            foreach (var element in objects)
            {
                pos = rnd.Next(maxValue);
                element.Value.X = pos - (maxValue >> 1);
                pos = rnd.Next(maxValue);
                element.Value.Y = pos - (maxValue >> 1);
            }
            /*
            objects[0].X = 200;
            objects[0].Y = 200;
            objects[1].X = -50;
            objects[1].Y = 200;
            objects[3].X = -200;
            objects[3].Y = -200;
            objects[4].X = -50;
            objects[4].Y = -200;
            */
        }

        uint RdrawsBase, RlinksBase;
        /// <summary>
        /// In what radius draw objects
        /// </summary>
        uint Rdraws;
        /// <summary>
        /// In what radius draw links
        /// </summary>
        uint Rlinks;

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetSize();
            OpenGlWindow_Load();
            DataLoadFrobDB();

            OpenGlWindow.MouseWheel += OpenGlWindow_MouseWheel;

            RdrawsBase = (uint)((OpenGlWindow.Width * OpenGlWindow.Width + OpenGlWindow.Height * OpenGlWindow.Height) * 0.25);
            RlinksBase = (uint)(RdrawsBase * Math.Pow(1.5, 2));

            Rdraws = RdrawsBase;
            Rlinks = RlinksBase;

            //// TIMER ////
            Timer drawTimer = new Timer();
            drawTimer.Interval = 50;
            drawTimer.Tick += drawTimer_Tick;
            drawTimer.Start();
            //// END TIMER ////

            this.OpenGlWindow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OpenGlWindow_KeyPress);
            movable = true;
        }
        List<ulong> forMove;
        List<ulong> forDraw;
        private void Draw()
        {
            #region settings
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            Gl.glTranslatef((OpenGlWindow.Width >> 1), (OpenGlWindow.Height >> 1), 0);
            Gl.glScalef(zoom, zoom, 1);
            Gl.glTranslatef(-shiftX, -shiftY, 0);
            #endregion settings

            //// DRAW ////

            #region OPTIMIZATION
            forMove = new List<ulong>();
            forDraw = new List<ulong>();
            foreach (var element in objects)
            {
                ulong dist = element.Value.DistanceToSq(shiftX, shiftY);
                if (dist < Rlinks)
                {
                    if (dist < Rdraws)
                    {
                        forDraw.Add(element.Key);
                    }
                    forMove.Add(element.Key);
                }
            }
            #endregion OPTIMIZATION



            #region MOVE
            if (movable == true)
            {
                for (int i = 0; i < forMove.Count - 1; i++)
                {
                    for (int j = i + 1; j < forMove.Count; j++)
                    {
                        objects[forMove[i]].Move(objects[forMove[j]].X, objects[forMove[j]].Y);
                    }
                }
                foreach (ulong element in forMove)
                {
                    objects[element].AcceptMove();
                }
            }
            #endregion MOVE

            foreach (ulong element in forDraw)
            {
                objects[element].DrawObject();
            }

            #region DRAW LINKS
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glBegin(Gl.GL_LINES);
            foreach (ulong element in forMove)
            {
                objects[element].DrawLinks();
            }
            Gl.glEnd();
            #endregion DRAW LINKS

            forMove.Clear();
            forDraw.Clear();
            //// END DRAW ////
            OpenGlWindow.Invalidate();
        }

        void drawTimer_Tick(object sender, EventArgs e)
        {
            Draw();
        }
        /// <summary>
        /// Shift map on X coord
        /// </summary>
        int shiftX = 0;
        /// <summary>
        /// Shift map on Y coord
        /// </summary>
        int shiftY = 0;
        /// <summary>
        /// Zoom map
        /// </summary>
        float zoom = 1;
        float maxZoom = 3.0f, minZoom = 0.3f;
        /// <summary>
        /// MouseWheel manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenGlWindow_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((e.Delta > 0) && ((zoom * 1.25f) < maxZoom)) zoom *= 1.25f;
            if ((e.Delta < 0) && ((zoom / 1.25f) > minZoom)) zoom /= 1.25f;
            Rdraws = (uint)((double)RdrawsBase / (zoom * zoom));
            Rlinks = (uint)((double)RlinksBase / (zoom * zoom));
        }

        int shiftMouseX;
        int shiftMouseY;
        bool isShiftMouse = false;
        long selected = -1;
        private void OpenGlWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //// MAP MOVE ////
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (isShiftMouse == false)
                {
                    isShiftMouse = true;
                    shiftMouseX = shiftX / 2 - OpenGlWindow.Width / 2 + e.X;
                    shiftMouseY = shiftY / 2 + OpenGlWindow.Height / 2 - e.Y;
                }
                else
                {
                    shiftX += shiftMouseX - (shiftX / 2 - OpenGlWindow.Width / 2 + e.X);
                    shiftY += shiftMouseY - (shiftY / 2 + OpenGlWindow.Height / 2 - e.Y);
                }
            }
            else
            {
                isShiftMouse = false;
            }
            ////END MAP MOVE ////
            //// OBJECTS MOVE ////
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int mouseX = (int)((e.X + (int)(shiftX * zoom) - (OpenGlWindow.Width >> 1)) / zoom);
                int mouseY = (int)((OpenGlWindow.Height - e.Y + (int)(shiftY * zoom) - (int)(OpenGlWindow.Height >> 1)) / zoom);

                if (selected == -1)
                {
                    foreach (var element in objects)
                    {
                        if (element.Value.Intersect(mouseX, mouseY) == true)
                        {
                            selected = (long)element.Value.ID;
                            break;
                        }
                    }
                }
                if (selected != -1) 
                {
                    objects[(ulong)selected].X = mouseX;
                    objects[(ulong)selected].Y = mouseY;
                }      
            }
            else
            {
                    selected = -1;
            }
            //// END OBJECTS MOVE ////           
        }

        private void OpenGlWindow_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //// GET REAL COORDINATE ////
                int mouseX = (int)((e.X + (int)(shiftX * zoom) - (OpenGlWindow.Width >> 1)) / zoom);
                int mouseY = (int)((OpenGlWindow.Height - e.Y + (int)(shiftY * zoom) - (int)(OpenGlWindow.Height >> 1)) / zoom);


                //// DELETE //// //INTERSECTION
                /*
                foreach (var element in objects)
                {
                    if (element.Value.Intersect(mouseX, mouseY) == true) MessageBox.Show(element.Value.ID.ToString() + " " + element.Value.Name);
                }
                */
                
                
                //// END DELETE ////
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            this.Hide();
            //this.form
            form.FormClosing += delegate
            {
                //var glform = new MainForm(); glform.ShowDialog();
                
                this.DataLoadFrobDB();
                this.Show();
            };
            form.Show();
            //this.Close();
        }

        /*
        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            var form = new FormLink();
            form.FormClosing += delegate { this.Show(); };
            form.Show();
        }*/

        private void OpenGlWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                movable = !movable;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String c = @"Press SPACE to disable gravitation effects.
SCROLL with Mouse Wheel to adjust zoom.
HOLD Right Mouse Button to move around network.
HOLD Left Mouse Button while over vertex to drag it around.";
            MessageBox.Show(c);
        }
    }
}
