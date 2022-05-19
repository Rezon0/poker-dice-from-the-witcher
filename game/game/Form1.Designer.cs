namespace game
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.PVE_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.PVP_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(530, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(534, 90);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сыграем в... кости??";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PVE_button
            // 
            this.PVE_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PVE_button.Location = new System.Drawing.Point(606, 464);
            this.PVE_button.Name = "PVE_button";
            this.PVE_button.Size = new System.Drawing.Size(396, 89);
            this.PVE_button.TabIndex = 1;
            this.PVE_button.Text = "PVE";
            this.PVE_button.UseVisualStyleBackColor = true;
            this.PVE_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(606, 596);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(396, 89);
            this.button2.TabIndex = 2;
            this.button2.Text = "Выход";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PVP_button
            // 
            this.PVP_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PVP_button.Location = new System.Drawing.Point(606, 341);
            this.PVP_button.Name = "PVP_button";
            this.PVP_button.Size = new System.Drawing.Size(396, 89);
            this.PVP_button.TabIndex = 3;
            this.PVP_button.Text = "PVP";
            this.PVP_button.UseVisualStyleBackColor = true;
            this.PVP_button.Click += new System.EventHandler(this.PVP_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.PVP_button);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PVE_button);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(1600, 900);
            this.MinimumSize = new System.Drawing.Size(1600, 900);
            this.Name = "Form1";
            this.Text = "Покер костями";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button PVE_button;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button PVP_button;
    }
}

