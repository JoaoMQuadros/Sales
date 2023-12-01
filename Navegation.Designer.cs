namespace Navegation
{
    partial class Navegation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblOqueDevoFazer = new System.Windows.Forms.Label();
            this.btnGoToAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnGoToFillOrCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOqueDevoFazer
            // 
            this.lblOqueDevoFazer.AutoSize = true;
            this.lblOqueDevoFazer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOqueDevoFazer.Location = new System.Drawing.Point(192, 39);
            this.lblOqueDevoFazer.Name = "lblOqueDevoFazer";
            this.lblOqueDevoFazer.Size = new System.Drawing.Size(184, 20);
            this.lblOqueDevoFazer.TabIndex = 0;
            this.lblOqueDevoFazer.Text = "O que você quer fazer?";
            // 
            // btnGoToAdd
            // 
            this.btnGoToAdd.Location = new System.Drawing.Point(196, 124);
            this.btnGoToAdd.Name = "btnGoToAdd";
            this.btnGoToAdd.Size = new System.Drawing.Size(180, 33);
            this.btnGoToAdd.TabIndex = 1;
            this.btnGoToAdd.Text = "Adicionar Conta";
            this.btnGoToAdd.UseVisualStyleBackColor = true;
            this.btnGoToAdd.Click += new System.EventHandler(this.btnGoToAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(491, 414);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnGoToFillOrCancel
            // 
            this.btnGoToFillOrCancel.Location = new System.Drawing.Point(143, 218);
            this.btnGoToFillOrCancel.Name = "btnGoToFillOrCancel";
            this.btnGoToFillOrCancel.Size = new System.Drawing.Size(267, 33);
            this.btnGoToFillOrCancel.TabIndex = 4;
            this.btnGoToFillOrCancel.Text = "Preencha ou cancele um pedido";
            this.btnGoToFillOrCancel.UseVisualStyleBackColor = true;
            this.btnGoToFillOrCancel.Click += new System.EventHandler(this.btnGoToFillOrCancel_Click);
            // 
            // Navegation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 449);
            this.Controls.Add(this.btnGoToFillOrCancel);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGoToAdd);
            this.Controls.Add(this.lblOqueDevoFazer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Navegation";
            this.Text = "Bem Vindo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOqueDevoFazer;
        private System.Windows.Forms.Button btnGoToAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGoToFillOrCancel;
    }
}

