
namespace formReport
{
  partial class Form1
  {
    /// <summary>
    /// Variabile di progettazione necessaria.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Pulire le risorse in uso.
    /// </summary>
    /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Codice generato da Progettazione Windows Form

    /// <summary>
    /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
    /// il contenuto del metodo con l'editor di codice.
    /// </summary>
    private void InitializeComponent()
    {
      this.dtInitReport = new System.Windows.Forms.DateTimePicker();
      this.dtEndReport = new System.Windows.Forms.DateTimePicker();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnGetDataLfreq = new System.Windows.Forms.Button();
      this.btnGetDataHfreq = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // dtInitReport
      // 
      this.dtInitReport.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.dtInitReport.Location = new System.Drawing.Point(133, 29);
      this.dtInitReport.Name = "dtInitReport";
      this.dtInitReport.Size = new System.Drawing.Size(200, 20);
      this.dtInitReport.TabIndex = 0;
      this.dtInitReport.ValueChanged += new System.EventHandler(this.dtInitReport_ValueChanged);
      // 
      // dtEndReport
      // 
      this.dtEndReport.Location = new System.Drawing.Point(133, 55);
      this.dtEndReport.Name = "dtEndReport";
      this.dtEndReport.Size = new System.Drawing.Size(200, 20);
      this.dtEndReport.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(23, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Inizio Report";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(23, 62);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(62, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Fine Report";
      // 
      // btnGetDataLfreq
      // 
      this.btnGetDataLfreq.Location = new System.Drawing.Point(364, 25);
      this.btnGetDataLfreq.Name = "btnGetDataLfreq";
      this.btnGetDataLfreq.Size = new System.Drawing.Size(97, 50);
      this.btnGetDataLfreq.TabIndex = 4;
      this.btnGetDataLfreq.Text = "Recupera dati";
      this.btnGetDataLfreq.UseVisualStyleBackColor = true;
      this.btnGetDataLfreq.Click += new System.EventHandler(this.btnGetData_Click);
      // 
      // btnGetDataHfreq
      // 
      this.btnGetDataHfreq.Location = new System.Drawing.Point(364, 81);
      this.btnGetDataHfreq.Name = "btnGetDataHfreq";
      this.btnGetDataHfreq.Size = new System.Drawing.Size(97, 50);
      this.btnGetDataHfreq.TabIndex = 5;
      this.btnGetDataHfreq.Text = "Recupera dati";
      this.btnGetDataHfreq.UseVisualStyleBackColor = true;
      this.btnGetDataHfreq.Click += new System.EventHandler(this.btnGetData_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.btnGetDataHfreq);
      this.Controls.Add(this.btnGetDataLfreq);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dtEndReport);
      this.Controls.Add(this.dtInitReport);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DateTimePicker dtInitReport;
    private System.Windows.Forms.DateTimePicker dtEndReport;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnGetDataLfreq;
    private System.Windows.Forms.Button btnGetDataHfreq;
  }
}

