using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestEverything {
    public partial class Form1 : Form {
        MiniDB.Database db = null;
        MiniDB.TableObjects to = null;
        MiniDB.TableObjectsLinks tol = null;

        public Form1 () {
            InitializeComponent();
            try {
                db = MiniDB.Database.Open( "New cool DB" );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }
        void ShowAny<T> (T[] array) {
            string src = "";
            foreach (T e in array) {
                src += e.ToString() + "\r\n";
            }
            MessageBox.Show( src );
        }

        private void bDBrw_Click (object sender, EventArgs e) {
            try {
                string table = (rb_to.Checked ? "Objects" : rb_tol.Checked ? "ObjectsLinks" : tbOtherName.Text);
                #region DEBUG
                //Работа с Database. Пользователю это не потребуется.
                if (chkbRead.Checked) {
                    //read
                    try {
                        //Считываем массив записей из указанной таблицы
                        MiniDB.Record[] read = db.GetAllRecords( table );
                        if (read != null) {
                            //Записываем все в строку, чтобы показать сообщение
                            string res = "";
                            for (int i = 0; i < read.Length; i++) {
                                //Так для каждого Record
                                res += "[ " + i + " ] = { GID:" + read[i].GID + " } > ";
                                //Дописываем все его поля
                                foreach (string s in read[i].Fields) {
                                    res += "'" + s + "'";
                                }
                                res += i != read.Length-1 ? "\n" : "";
                            }
                            //Выводим содержимое таблицы
                            MessageBox.Show( res );
                        } else {
                            MessageBox.Show( "DB is null!" );
                        }
                    } catch (Exception ex) {
                        MessageBox.Show( ex.Message );
                    }
                }//if dbg rd
                if (chkbWrite.Checked) {
                    //write
                    try {
                        int rs = 5;
                        int fs = 3;
                        MiniDB.Record[] write = new MiniDB.Record[rs];
                        for (int rec = 0; rec < rs; rec++) {
                            string[] fields = new string[fs];
                            for (int fld = 0; fld < fs; fld++) {
                                fields[fld] = (rec * 10 + fld) + "";
                            }
                            write[rec] = new MiniDB.Record( fields );
                        }
                        db.AppendRecords( table, write );
                    } catch (Exception ex) {
                        MessageBox.Show( ex.Message );
                    }
                }//if debug wrt
                #endregion
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void to_bind_Click (object sender, EventArgs e) {
            try {
                to = MiniDB.TableObjects.Bind( db );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }
        private void tol_bind_Click (object sender, EventArgs e) {
            try {
                tol = MiniDB.TableObjectsLinks.Bind( db );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void to_addRecord_Click (object sender, EventArgs e) {
            try {
                int count = 3;
                MiniDB.ObjectRecord[] recs = new MiniDB.ObjectRecord[count];
                for (int i = 0; i < recs.Length; i++) {
                    recs[i] = new MiniDB.ObjectRecord( i, "a" + i );
                }
                to.AddRecords( recs );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }
        private void tol_addRecord_Click (object sender, EventArgs e) {
            try {
                int count = 3;
                MiniDB.ObjectsLinkRecord[] recs = new MiniDB.ObjectsLinkRecord[count];
                recs[0] = new MiniDB.ObjectsLinkRecord( 0, 1 );
                recs[1] = new MiniDB.ObjectsLinkRecord( 0, 2 );
                recs[2] = new MiniDB.ObjectsLinkRecord( 1, 2 );
                tol.AddRecords( recs );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void to_clear_Click (object sender, EventArgs e) {
            try {
                to.Clear();
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }
        private void tol_clear_Click (object sender, EventArgs e) {
            try {
                tol.Clear();
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void to_getAll_Click (object sender, EventArgs e) {
            try {
                string s = "";
                MiniDB.ObjectRecord[] recs = to.GetAllRecords();
                for (int i = 0; i < recs.Length; i++) {
                    s += recs[i].ToString() + (i != recs.Length-1?"\n":"");
                }
                MessageBox.Show( s );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }
        private void tol_getAll_Click (object sender, EventArgs e) {
            try {
                string s = "";
                MiniDB.ObjectsLinkRecord[] recs = tol.GetAllRecords();
                for (int i = 0; i < recs.Length; i++) {
                    s += recs[i].ToString() + (i != recs.Length-1?"\n":"");
                }
                MessageBox.Show( s );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void radioButton3_CheckedChanged (object sender, EventArgs e) {
            tbOtherName.ReadOnly = !(sender as RadioButton).Checked;
        }

        private void to_rmvID_Click (object sender, EventArgs e) {
            try {
                long[] ids = new long[2];
                ids[0] = 0;
                ids[1] = 2;
                to.RemoveByIDs( ids );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void tol_rmvPID_Click (object sender, EventArgs e) {
            try {
                long[] ids = new long[1];
                ids[0] = 0;
                tol.RemoveByParentIDs( ids );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void tol_rmvCID_Click (object sender, EventArgs e) {
            try {
                long[] ids = new long[1];
                ids[0] = 2;
                tol.RemoveByChildIDs( ids );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void to_rmvFunc_Click (object sender, EventArgs e) {
            try {
                to.RemoveByUserFunc( rec => {
                    if (rec.ID == 1)
                        return true;
                    return false;
                } );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void tol_rmvFunc_Click (object sender, EventArgs e) {
            try {
                tol.RemoveByUserFunc( rec => {
                    if (rec.ParentID == 1)
                        return true;
                    return false;
                } );
            } catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

    }
}
