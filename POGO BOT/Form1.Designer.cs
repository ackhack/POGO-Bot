
namespace POGO_BOT
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPic = new System.Windows.Forms.Button();
            this.pbResult = new System.Windows.Forms.PictureBox();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.cbToss = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMons = new System.Windows.Forms.Label();
            this.lblStops = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.lblInstruct = new System.Windows.Forms.Label();
            this.btninstruct = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.lblRelease = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPic
            // 
            this.btnPic.Enabled = false;
            this.btnPic.Location = new System.Drawing.Point(10, 20);
            this.btnPic.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPic.Name = "btnPic";
            this.btnPic.Size = new System.Drawing.Size(93, 61);
            this.btnPic.TabIndex = 0;
            this.btnPic.Text = "Start/Stop";
            this.btnPic.UseVisualStyleBackColor = true;
            this.btnPic.Click += new System.EventHandler(this.BtnPic_Click);
            // 
            // pbResult
            // 
            this.pbResult.Location = new System.Drawing.Point(145, -3);
            this.pbResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(412, 743);
            this.pbResult.TabIndex = 1;
            this.pbResult.TabStop = false;
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.Maroon;
            this.pnlColor.Location = new System.Drawing.Point(26, 129);
            this.pnlColor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(80, 81);
            this.pnlColor.TabIndex = 2;
            this.pnlColor.DoubleClick += new System.EventHandler(this.PnlColor_DoubleClick);
            // 
            // cbToss
            // 
            this.cbToss.AutoSize = true;
            this.cbToss.Location = new System.Drawing.Point(10, 269);
            this.cbToss.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbToss.Name = "cbToss";
            this.cbToss.Size = new System.Drawing.Size(109, 17);
            this.cbToss.TabIndex = 3;
            this.cbToss.Text = "Toss (not reliable)";
            this.cbToss.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 310);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pokemons: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 338);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PokeStops: ";
            // 
            // lblMons
            // 
            this.lblMons.AutoSize = true;
            this.lblMons.Location = new System.Drawing.Point(76, 310);
            this.lblMons.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMons.Name = "lblMons";
            this.lblMons.Size = new System.Drawing.Size(13, 13);
            this.lblMons.TabIndex = 6;
            this.lblMons.Text = "0";
            // 
            // lblStops
            // 
            this.lblStops.AutoSize = true;
            this.lblStops.Location = new System.Drawing.Point(76, 338);
            this.lblStops.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStops.Name = "lblStops";
            this.lblStops.Size = new System.Drawing.Size(13, 13);
            this.lblStops.TabIndex = 7;
            this.lblStops.Text = "0";
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 383);
            this.btnInit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(106, 66);
            this.btnInit.TabIndex = 8;
            this.btnInit.Text = "Init Screen";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.BtnInit_Click);
            // 
            // lblInstruct
            // 
            this.lblInstruct.AutoSize = true;
            this.lblInstruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblInstruct.Location = new System.Drawing.Point(272, 62);
            this.lblInstruct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInstruct.Name = "lblInstruct";
            this.lblInstruct.Size = new System.Drawing.Size(155, 31);
            this.lblInstruct.TabIndex = 9;
            this.lblInstruct.Text = "Instructions";
            this.lblInstruct.Visible = false;
            // 
            // btninstruct
            // 
            this.btninstruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btninstruct.Location = new System.Drawing.Point(145, 62);
            this.btninstruct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btninstruct.Name = "btninstruct";
            this.btninstruct.Size = new System.Drawing.Size(123, 60);
            this.btninstruct.TabIndex = 10;
            this.btninstruct.Text = "OK";
            this.btninstruct.UseVisualStyleBackColor = true;
            this.btninstruct.Visible = false;
            this.btninstruct.Click += new System.EventHandler(this.Btninstruct_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnRedo.Location = new System.Drawing.Point(145, 129);
            this.btnRedo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(123, 60);
            this.btnRedo.TabIndex = 11;
            this.btnRedo.Text = "REDO";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Visible = false;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // lblRelease
            // 
            this.lblRelease.AutoSize = true;
            this.lblRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblRelease.Location = new System.Drawing.Point(148, 192);
            this.lblRelease.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(102, 25);
            this.lblRelease.TabIndex = 12;
            this.lblRelease.Text = "lblRelease";
            this.lblRelease.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 764);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btninstruct);
            this.Controls.Add(this.lblInstruct);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.lblStops);
            this.Controls.Add(this.lblMons);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbToss);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.pbResult);
            this.Controls.Add(this.btnPic);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPic;
        private System.Windows.Forms.PictureBox pbResult;
        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.CheckBox cbToss;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMons;
        private System.Windows.Forms.Label lblStops;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Label lblInstruct;
        private System.Windows.Forms.Button btninstruct;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Label lblRelease;
    }
}

