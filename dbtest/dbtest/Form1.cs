using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace dbtest
{
    public partial class Form1 : Form
    {
        MiniDB.Database db = null;
        MiniDB.TableObjects to = null;
        MiniDB.TableObjectsLinks tol = null;
        private int KOSTYL_FLAG = 0;
        public void UpdateListBox()
        {
            checkedListBox1.BeginUpdate();
            MiniDB.ObjectRecord[] recs; 
            recs = to.GetAllRecords();
            int i=0;
            if (recs == null)
            {
                return; 
            }
            foreach (var rec in recs)
            {
                checkedListBox1.Items.Add(rec);
                i++;
            }

           /* to.RemoveByUserFunc(rec =>
            {
                if (rec.GetHashCode() == recs[recs.Length].GetHashCode())
                    return true;
                return false;
            });*/
            //MiniDB.ObjectRecord[] r = new MiniDB.ObjectRecord[1];
            //r[0] = new MiniDB.ObjectRecord(recs[i].ID, recs[i].Name);
            checkedListBox1.EndUpdate();

        }
        public void UpdateComboBox(ComboBox cmb)
        {
            cmb.BeginUpdate();
            MiniDB.ObjectRecord[] recs;
            recs = to.GetAllRecords();
            if (recs == null)
            {
                return;
            }
            foreach (var rec in recs)
            {
                cmb.Items.Add(rec);
            }
            cmb.EndUpdate();
        }
        public void UpdateCListBox(long ID, bool noitemselected)
        {
            checkedListBox2.BeginUpdate();
            MiniDB.ObjectsLinkRecord[] recs;
            recs = tol.GetAllRecords();
            if (recs == null)
            {

                // MessageBox.Show("no links");
                return;
            }
            checkedListBox2.Items.Clear();
            foreach (var rec in recs)
            {
                if ((ID == rec.ParentID) || (noitemselected == true)||(ID==rec.ChildID))checkedListBox2.Items.Add(rec);
            }
            checkedListBox2.EndUpdate();
        }
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            try
            {
                db = MiniDB.Database.Open("ProjectDB");
                to = MiniDB.TableObjects.Bind(db);
                tol = MiniDB.TableObjectsLinks.Bind(db);
                UpdateListBox();
                UpdateComboBox(comboBox1);
                UpdateComboBox(comboBox2);
                UpdateCListBox(0, true);
                UpdateComboBox(comboBox4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //add btn
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 3;
                MiniDB.ObjectRecord[] recs = new MiniDB.ObjectRecord[count];
                for (int i = 0; i < recs.Length; i++)
                {
                    //recs[i] = new MiniDB.ObjectRecord(i, "a" + i);
                    recs[i] = new MiniDB.ObjectRecord("Obj №" + i);
                    checkedListBox1.Items.Add(recs[i]);
                }
                to.AddRecords(recs);
                //UpdateListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //show btn
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string s = "";
                MiniDB.ObjectRecord[] recs = to.GetAllRecords();
                for (int i = 0; i < recs.Length; i++)
                {
                    s += recs[i].ToString() + (i != recs.Length - 1 ? "\n" : "");
                }
                MessageBox.Show(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //delete btn
        private void button3_Click(object sender, EventArgs e)
        {

            try {
                long[] ids = new long[checkedListBox1.CheckedIndices.Count];
                for (int i = checkedListBox1.CheckedIndices.Count-1; i >= 0; i--)
                {
                    string[] words = checkedListBox1.CheckedItems[i].ToString().Split(';','{');
                    MiniDB.ObjectRecord m = (MiniDB.ObjectRecord)checkedListBox1.CheckedItems[i];
                    ids[i] = m.ID;
                    checkedListBox1.Items.Remove(checkedListBox1.CheckedItems[i]);
                }
                to.RemoveByIDs( ids );
                tol.RemoveByChildIDs(ids);
                tol.RemoveByParentIDs(ids);
                KOSTYL_FLAG = 1;
                UpdateCListBox(0, true);
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }
        //select all btn
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemCheckState(i,CheckState.Checked);
            }
        }
        //single add btn
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) return;
                MiniDB.ObjectRecord[] recs = new MiniDB.ObjectRecord[1];
                //textBox1.
                recs[0] = new MiniDB.ObjectRecord(textBox1.Text);
                ///////////////////////////////////
                if(KOSTYL_FLAG==0){recs[0].ID++;}
                ///////////////////////////////////
                to.AddRecords(recs);
                checkedListBox1.Items.Add(recs[0]);
                comboBox2.Items.Add(recs[0]);
                textBox1.ResetText();
                //UpdateListBox();
                UpdateComboBox(comboBox2);//------------------------------------------------???????????
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //show links
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string s = "";
                MiniDB.ObjectsLinkRecord[] recs = tol.GetAllRecords();
                for (int i = 0; i < recs.Length; i++)
                {
                    s += recs[i].ToString() + (i != recs.Length - 1 ? "\n" : "");
                }
                //UpdateListBox();

                MessageBox.Show(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //add links
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 3;
                MiniDB.ObjectsLinkRecord[] recs = new MiniDB.ObjectsLinkRecord[count];
                recs[0] = new MiniDB.ObjectsLinkRecord(3, 1);
                recs[1] = new MiniDB.ObjectsLinkRecord(0, 2);
                recs[2] = new MiniDB.ObjectsLinkRecord(1, 2);
                tol.AddRecords(recs);
                checkedListBox2.Items.Add(recs[0]);
                checkedListBox2.Items.Add(recs[1]);
                checkedListBox2.Items.Add(recs[2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MiniDB.ObjectRecord m = (MiniDB.ObjectRecord)comboBox1.Items[comboBox1.SelectedIndex];
            //MessageBox.Show(m.ID.ToString());
            UpdateCListBox(m.ID, false);
        }
        //select all lnk
        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemCheckState(i, CheckState.Checked);
            }
        }
        //delete lnk
        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                //int[] hash = new int[checkedListBox2.CheckedIndices.Count];
                for (int i = checkedListBox2.CheckedIndices.Count - 1; i >= 0; i--)
                {
                    string[] words = checkedListBox2.CheckedItems[i].ToString().Split(';', '{');
                    MiniDB.ObjectsLinkRecord m = (MiniDB.ObjectsLinkRecord)checkedListBox2.CheckedItems[i];
                    //hash[i] = m.GetHashCode();
                    checkedListBox2.Items.Remove(checkedListBox2.CheckedItems[i]);
                    tol.RemoveByUserFunc(rec =>
                    {
                        if (rec.GetHashCode() == m.GetHashCode())
                            return true;
                        return false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.BeginUpdate();
            comboBox3.Items.Clear();
            MiniDB.ObjectRecord[] recs;
            recs = to.GetAllRecords();
            if (recs == null)
            {
                return;
            }
            foreach (var rec in recs)
            {
                if(rec.GetHashCode()!=comboBox2.SelectedItem.GetHashCode())
                comboBox3.Items.Add(rec);
            }
            comboBox3.EndUpdate();
        }
        //add link
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if ((comboBox2.SelectedItem == null) || (comboBox3.SelectedItem == null)) return;
                MiniDB.ObjectsLinkRecord[] recs = new MiniDB.ObjectsLinkRecord[1];
                MiniDB.ObjectRecord p = (MiniDB.ObjectRecord) comboBox2.SelectedItem;
                MiniDB.ObjectRecord c = (MiniDB.ObjectRecord) comboBox3.SelectedItem;
                recs[0] = new MiniDB.ObjectsLinkRecord(p.ID,c.ID);
                tol.AddRecords(recs);
                checkedListBox2.Items.Add(recs[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            MiniDB.ObjectRecord m = (MiniDB.ObjectRecord) comboBox4.SelectedItem;
            textBox2.Text = m.Name;
        }
        //edit btn
        private void button11_Click(object sender, EventArgs e)
        {
            MiniDB.ObjectRecord m = (MiniDB.ObjectRecord)comboBox4.SelectedItem;
            to.RemoveByUserFunc(rec =>
            {
                if (rec.GetHashCode() == m.GetHashCode())
                    return true;
                return false;
            });
            MiniDB.ObjectRecord[] r = new MiniDB.ObjectRecord[1];
            r[0] = new MiniDB.ObjectRecord(m.ID, textBox2.Text);
            to.AddRecords(r);
            checkedListBox1.Items.Remove(comboBox4.SelectedItem);
            checkedListBox1.Items.Add(r[0]);
            //checkedListBox1.Items.Add(rec);

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
           

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //MessageBox.Show("Enter pressed", "Attention");
                button5_Click(sender, e);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //MessageBox.Show("Enter pressed", "Attention");
                button11_Click(sender, e);
            }
        }
    }
}
