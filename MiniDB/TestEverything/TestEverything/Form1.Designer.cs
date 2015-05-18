namespace TestEverything {
    partial class Form1 {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent () {
            this.gb_db = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOtherName = new System.Windows.Forms.TextBox();
            this.rb_o = new System.Windows.Forms.RadioButton();
            this.rb_tol = new System.Windows.Forms.RadioButton();
            this.rb_to = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chkbWrite = new System.Windows.Forms.CheckBox();
            this.chkbRead = new System.Windows.Forms.CheckBox();
            this.bDBrw = new System.Windows.Forms.Button();
            this.gb_to = new System.Windows.Forms.GroupBox();
            this.to_getAll = new System.Windows.Forms.Button();
            this.to_rmvFunc = new System.Windows.Forms.Button();
            this.to_rmvName = new System.Windows.Forms.Button();
            this.to_rmvID = new System.Windows.Forms.Button();
            this.to_clear = new System.Windows.Forms.Button();
            this.to_addRecord = new System.Windows.Forms.Button();
            this.to_bind = new System.Windows.Forms.Button();
            this.gb_tol = new System.Windows.Forms.GroupBox();
            this.tol_getAll = new System.Windows.Forms.Button();
            this.tol_rmvFunc = new System.Windows.Forms.Button();
            this.tol_rmvCID = new System.Windows.Forms.Button();
            this.tol_rmvPID = new System.Windows.Forms.Button();
            this.tol_clear = new System.Windows.Forms.Button();
            this.tol_addRecord = new System.Windows.Forms.Button();
            this.tol_bind = new System.Windows.Forms.Button();
            this.gb_db.SuspendLayout();
            this.gb_to.SuspendLayout();
            this.gb_tol.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_db
            // 
            this.gb_db.Controls.Add(this.label2);
            this.gb_db.Controls.Add(this.tbOtherName);
            this.gb_db.Controls.Add(this.rb_o);
            this.gb_db.Controls.Add(this.rb_tol);
            this.gb_db.Controls.Add(this.rb_to);
            this.gb_db.Controls.Add(this.label1);
            this.gb_db.Controls.Add(this.chkbWrite);
            this.gb_db.Controls.Add(this.chkbRead);
            this.gb_db.Controls.Add(this.bDBrw);
            this.gb_db.Location = new System.Drawing.Point(12, 12);
            this.gb_db.Name = "gb_db";
            this.gb_db.Size = new System.Drawing.Size(183, 224);
            this.gb_db.TabIndex = 3;
            this.gb_db.TabStop = false;
            this.gb_db.Text = "Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Action:";
            // 
            // tbOtherName
            // 
            this.tbOtherName.Location = new System.Drawing.Point(6, 104);
            this.tbOtherName.Name = "tbOtherName";
            this.tbOtherName.ReadOnly = true;
            this.tbOtherName.Size = new System.Drawing.Size(170, 20);
            this.tbOtherName.TabIndex = 10;
            this.tbOtherName.Text = "default";
            // 
            // rb_o
            // 
            this.rb_o.AutoSize = true;
            this.rb_o.Location = new System.Drawing.Point(7, 81);
            this.rb_o.Name = "rb_o";
            this.rb_o.Size = new System.Drawing.Size(51, 17);
            this.rb_o.TabIndex = 9;
            this.rb_o.Text = "Other";
            this.rb_o.UseVisualStyleBackColor = true;
            this.rb_o.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rb_tol
            // 
            this.rb_tol.AutoSize = true;
            this.rb_tol.Location = new System.Drawing.Point(6, 58);
            this.rb_tol.Name = "rb_tol";
            this.rb_tol.Size = new System.Drawing.Size(108, 17);
            this.rb_tol.TabIndex = 8;
            this.rb_tol.Text = "TableObjectsLink";
            this.rb_tol.UseVisualStyleBackColor = true;
            // 
            // rb_to
            // 
            this.rb_to.AutoSize = true;
            this.rb_to.Checked = true;
            this.rb_to.Location = new System.Drawing.Point(6, 35);
            this.rb_to.Name = "rb_to";
            this.rb_to.Size = new System.Drawing.Size(83, 17);
            this.rb_to.TabIndex = 7;
            this.rb_to.TabStop = true;
            this.rb_to.Text = "TableObject";
            this.rb_to.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Table name:";
            // 
            // chkbWrite
            // 
            this.chkbWrite.AutoSize = true;
            this.chkbWrite.Location = new System.Drawing.Point(6, 170);
            this.chkbWrite.Name = "chkbWrite";
            this.chkbWrite.Size = new System.Drawing.Size(101, 17);
            this.chkbWrite.TabIndex = 5;
            this.chkbWrite.Text = "Append records";
            this.chkbWrite.UseVisualStyleBackColor = true;
            // 
            // chkbRead
            // 
            this.chkbRead.AutoSize = true;
            this.chkbRead.Location = new System.Drawing.Point(6, 147);
            this.chkbRead.Name = "chkbRead";
            this.chkbRead.Size = new System.Drawing.Size(90, 17);
            this.chkbRead.TabIndex = 4;
            this.chkbRead.Text = "Read records";
            this.chkbRead.UseVisualStyleBackColor = true;
            // 
            // bDBrw
            // 
            this.bDBrw.Location = new System.Drawing.Point(6, 193);
            this.bDBrw.Name = "bDBrw";
            this.bDBrw.Size = new System.Drawing.Size(171, 23);
            this.bDBrw.TabIndex = 3;
            this.bDBrw.Text = "Do";
            this.bDBrw.UseVisualStyleBackColor = true;
            this.bDBrw.Click += new System.EventHandler(this.bDBrw_Click);
            // 
            // gb_to
            // 
            this.gb_to.Controls.Add(this.to_getAll);
            this.gb_to.Controls.Add(this.to_rmvFunc);
            this.gb_to.Controls.Add(this.to_rmvName);
            this.gb_to.Controls.Add(this.to_rmvID);
            this.gb_to.Controls.Add(this.to_clear);
            this.gb_to.Controls.Add(this.to_addRecord);
            this.gb_to.Controls.Add(this.to_bind);
            this.gb_to.Location = new System.Drawing.Point(201, 12);
            this.gb_to.Name = "gb_to";
            this.gb_to.Size = new System.Drawing.Size(183, 224);
            this.gb_to.TabIndex = 4;
            this.gb_to.TabStop = false;
            this.gb_to.Text = "TableObject";
            // 
            // to_getAll
            // 
            this.to_getAll.Location = new System.Drawing.Point(6, 193);
            this.to_getAll.Name = "to_getAll";
            this.to_getAll.Size = new System.Drawing.Size(171, 23);
            this.to_getAll.TabIndex = 12;
            this.to_getAll.Text = "Get all records";
            this.to_getAll.UseVisualStyleBackColor = true;
            this.to_getAll.Click += new System.EventHandler(this.to_getAll_Click);
            // 
            // to_rmvFunc
            // 
            this.to_rmvFunc.Location = new System.Drawing.Point(6, 164);
            this.to_rmvFunc.Name = "to_rmvFunc";
            this.to_rmvFunc.Size = new System.Drawing.Size(171, 23);
            this.to_rmvFunc.TabIndex = 11;
            this.to_rmvFunc.Text = "Remove by myfunc";
            this.to_rmvFunc.UseVisualStyleBackColor = true;
            this.to_rmvFunc.Click += new System.EventHandler(this.to_rmvFunc_Click);
            // 
            // to_rmvName
            // 
            this.to_rmvName.Location = new System.Drawing.Point(6, 135);
            this.to_rmvName.Name = "to_rmvName";
            this.to_rmvName.Size = new System.Drawing.Size(171, 23);
            this.to_rmvName.TabIndex = 10;
            this.to_rmvName.Text = "Remove by Name a1";
            this.to_rmvName.UseVisualStyleBackColor = true;
            // 
            // to_rmvID
            // 
            this.to_rmvID.Location = new System.Drawing.Point(6, 106);
            this.to_rmvID.Name = "to_rmvID";
            this.to_rmvID.Size = new System.Drawing.Size(171, 23);
            this.to_rmvID.TabIndex = 9;
            this.to_rmvID.Text = "Remove by ID 0 2";
            this.to_rmvID.UseVisualStyleBackColor = true;
            this.to_rmvID.Click += new System.EventHandler(this.to_rmvID_Click);
            // 
            // to_clear
            // 
            this.to_clear.Location = new System.Drawing.Point(6, 77);
            this.to_clear.Name = "to_clear";
            this.to_clear.Size = new System.Drawing.Size(171, 23);
            this.to_clear.TabIndex = 8;
            this.to_clear.Text = "Clear";
            this.to_clear.UseVisualStyleBackColor = true;
            this.to_clear.Click += new System.EventHandler(this.to_clear_Click);
            // 
            // to_addRecord
            // 
            this.to_addRecord.Location = new System.Drawing.Point(6, 48);
            this.to_addRecord.Name = "to_addRecord";
            this.to_addRecord.Size = new System.Drawing.Size(171, 23);
            this.to_addRecord.TabIndex = 7;
            this.to_addRecord.Text = "Add records 0,a0 1,a1 2,a2";
            this.to_addRecord.UseVisualStyleBackColor = true;
            this.to_addRecord.Click += new System.EventHandler(this.to_addRecord_Click);
            // 
            // to_bind
            // 
            this.to_bind.Location = new System.Drawing.Point(6, 19);
            this.to_bind.Name = "to_bind";
            this.to_bind.Size = new System.Drawing.Size(171, 23);
            this.to_bind.TabIndex = 6;
            this.to_bind.Text = "Bind";
            this.to_bind.UseVisualStyleBackColor = true;
            this.to_bind.Click += new System.EventHandler(this.to_bind_Click);
            // 
            // gb_tol
            // 
            this.gb_tol.Controls.Add(this.tol_getAll);
            this.gb_tol.Controls.Add(this.tol_rmvFunc);
            this.gb_tol.Controls.Add(this.tol_rmvCID);
            this.gb_tol.Controls.Add(this.tol_rmvPID);
            this.gb_tol.Controls.Add(this.tol_clear);
            this.gb_tol.Controls.Add(this.tol_addRecord);
            this.gb_tol.Controls.Add(this.tol_bind);
            this.gb_tol.Location = new System.Drawing.Point(390, 12);
            this.gb_tol.Name = "gb_tol";
            this.gb_tol.Size = new System.Drawing.Size(183, 224);
            this.gb_tol.TabIndex = 5;
            this.gb_tol.TabStop = false;
            this.gb_tol.Text = "TableObjectsLink";
            // 
            // tol_getAll
            // 
            this.tol_getAll.Location = new System.Drawing.Point(6, 193);
            this.tol_getAll.Name = "tol_getAll";
            this.tol_getAll.Size = new System.Drawing.Size(171, 23);
            this.tol_getAll.TabIndex = 12;
            this.tol_getAll.Text = "Get all records";
            this.tol_getAll.UseVisualStyleBackColor = true;
            this.tol_getAll.Click += new System.EventHandler(this.tol_getAll_Click);
            // 
            // tol_rmvFunc
            // 
            this.tol_rmvFunc.Location = new System.Drawing.Point(6, 164);
            this.tol_rmvFunc.Name = "tol_rmvFunc";
            this.tol_rmvFunc.Size = new System.Drawing.Size(171, 23);
            this.tol_rmvFunc.TabIndex = 11;
            this.tol_rmvFunc.Text = "Remove by myfunc";
            this.tol_rmvFunc.UseVisualStyleBackColor = true;
            this.tol_rmvFunc.Click += new System.EventHandler(this.tol_rmvFunc_Click);
            // 
            // tol_rmvCID
            // 
            this.tol_rmvCID.Location = new System.Drawing.Point(6, 135);
            this.tol_rmvCID.Name = "tol_rmvCID";
            this.tol_rmvCID.Size = new System.Drawing.Size(171, 23);
            this.tol_rmvCID.TabIndex = 10;
            this.tol_rmvCID.Text = "Remove by Child ID 2";
            this.tol_rmvCID.UseVisualStyleBackColor = true;
            this.tol_rmvCID.Click += new System.EventHandler(this.tol_rmvCID_Click);
            // 
            // tol_rmvPID
            // 
            this.tol_rmvPID.Location = new System.Drawing.Point(6, 106);
            this.tol_rmvPID.Name = "tol_rmvPID";
            this.tol_rmvPID.Size = new System.Drawing.Size(171, 23);
            this.tol_rmvPID.TabIndex = 9;
            this.tol_rmvPID.Text = "Remove by Parent ID 0";
            this.tol_rmvPID.UseVisualStyleBackColor = true;
            this.tol_rmvPID.Click += new System.EventHandler(this.tol_rmvPID_Click);
            // 
            // tol_clear
            // 
            this.tol_clear.Location = new System.Drawing.Point(6, 77);
            this.tol_clear.Name = "tol_clear";
            this.tol_clear.Size = new System.Drawing.Size(171, 23);
            this.tol_clear.TabIndex = 8;
            this.tol_clear.Text = "Clear";
            this.tol_clear.UseVisualStyleBackColor = true;
            this.tol_clear.Click += new System.EventHandler(this.tol_clear_Click);
            // 
            // tol_addRecord
            // 
            this.tol_addRecord.Location = new System.Drawing.Point(6, 48);
            this.tol_addRecord.Name = "tol_addRecord";
            this.tol_addRecord.Size = new System.Drawing.Size(171, 23);
            this.tol_addRecord.TabIndex = 7;
            this.tol_addRecord.Text = "Add records 0,1 0,2 1,2";
            this.tol_addRecord.UseVisualStyleBackColor = true;
            this.tol_addRecord.Click += new System.EventHandler(this.tol_addRecord_Click);
            // 
            // tol_bind
            // 
            this.tol_bind.Location = new System.Drawing.Point(6, 19);
            this.tol_bind.Name = "tol_bind";
            this.tol_bind.Size = new System.Drawing.Size(171, 23);
            this.tol_bind.TabIndex = 6;
            this.tol_bind.Text = "Bind";
            this.tol_bind.UseVisualStyleBackColor = true;
            this.tol_bind.Click += new System.EventHandler(this.tol_bind_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 245);
            this.Controls.Add(this.gb_tol);
            this.Controls.Add(this.gb_to);
            this.Controls.Add(this.gb_db);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test";
            this.gb_db.ResumeLayout(false);
            this.gb_db.PerformLayout();
            this.gb_to.ResumeLayout(false);
            this.gb_tol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_db;
        private System.Windows.Forms.CheckBox chkbWrite;
        private System.Windows.Forms.CheckBox chkbRead;
        private System.Windows.Forms.Button bDBrw;
        private System.Windows.Forms.GroupBox gb_to;
        private System.Windows.Forms.Button to_rmvID;
        private System.Windows.Forms.Button to_clear;
        private System.Windows.Forms.Button to_addRecord;
        private System.Windows.Forms.Button to_bind;
        private System.Windows.Forms.Button to_getAll;
        private System.Windows.Forms.Button to_rmvFunc;
        private System.Windows.Forms.Button to_rmvName;
        private System.Windows.Forms.GroupBox gb_tol;
        private System.Windows.Forms.Button tol_getAll;
        private System.Windows.Forms.Button tol_rmvFunc;
        private System.Windows.Forms.Button tol_rmvCID;
        private System.Windows.Forms.Button tol_rmvPID;
        private System.Windows.Forms.Button tol_clear;
        private System.Windows.Forms.Button tol_addRecord;
        private System.Windows.Forms.Button tol_bind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_o;
        private System.Windows.Forms.RadioButton rb_tol;
        private System.Windows.Forms.RadioButton rb_to;
        private System.Windows.Forms.TextBox tbOtherName;
        private System.Windows.Forms.Label label2;
    }
}

