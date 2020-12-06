
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMons = new System.Windows.Forms.Label();
            this.lblStops = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPic
            // 
            this.btnPic.Location = new System.Drawing.Point(13, 24);
            this.btnPic.Name = "btnPic";
            this.btnPic.Size = new System.Drawing.Size(124, 75);
            this.btnPic.TabIndex = 0;
            this.btnPic.Text = "Start/Stop";
            this.btnPic.UseVisualStyleBackColor = true;
            this.btnPic.Click += new System.EventHandler(this.BtnPic_Click);
            // 
            // pbResult
            // 
            this.pbResult.Location = new System.Drawing.Point(193, -4);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(550, 915);
            this.pbResult.TabIndex = 1;
            this.pbResult.TabStop = false;
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.Maroon;
            this.pnlColor.Location = new System.Drawing.Point(35, 159);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(107, 100);
            this.pnlColor.TabIndex = 2;
            this.pnlColor.DoubleClick += new System.EventHandler(this.PnlColor_DoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(13, 331);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 21);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Toss";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pokemons: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "PokeStops: ";
            // 
            // lblMons
            // 
            this.lblMons.AutoSize = true;
            this.lblMons.Location = new System.Drawing.Point(102, 381);
            this.lblMons.Name = "lblMons";
            this.lblMons.Size = new System.Drawing.Size(16, 17);
            this.lblMons.TabIndex = 6;
            this.lblMons.Text = "0";
            // 
            // lblStops
            // 
            this.lblStops.AutoSize = true;
            this.lblStops.Location = new System.Drawing.Point(102, 416);
            this.lblStops.Name = "lblStops";
            this.lblStops.Size = new System.Drawing.Size(16, 17);
            this.lblStops.TabIndex = 7;
            this.lblStops.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 940);
            this.Controls.Add(this.lblStops);
            this.Controls.Add(this.lblMons);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.pbResult);
            this.Controls.Add(this.btnPic);
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMons;
        private System.Windows.Forms.Label lblStops;
    }
}

