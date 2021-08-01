
namespace formReport
{
  partial class frmGenReport
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
      this.tbPath = new System.Windows.Forms.TextBox();
      this.btnSelFile = new System.Windows.Forms.Button();
      this.btnCarica = new System.Windows.Forms.Button();
      this.dgvLista = new System.Windows.Forms.DataGridView();
      this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.btnCreate = new System.Windows.Forms.Button();
      this.dgvListaCreate = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvListaCreate)).BeginInit();
      this.SuspendLayout();
      // 
      // tbPath
      // 
      this.tbPath.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbPath.Location = new System.Drawing.Point(13, 13);
      this.tbPath.Multiline = true;
      this.tbPath.Name = "tbPath";
      this.tbPath.Size = new System.Drawing.Size(320, 35);
      this.tbPath.TabIndex = 0;
      // 
      // btnSelFile
      // 
      this.btnSelFile.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSelFile.Location = new System.Drawing.Point(340, 13);
      this.btnSelFile.Name = "btnSelFile";
      this.btnSelFile.Size = new System.Drawing.Size(133, 35);
      this.btnSelFile.TabIndex = 1;
      this.btnSelFile.Text = "Seleziona File";
      this.btnSelFile.UseVisualStyleBackColor = true;
      this.btnSelFile.Click += new System.EventHandler(this.btnClick);
      // 
      // btnCarica
      // 
      this.btnCarica.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCarica.Location = new System.Drawing.Point(479, 13);
      this.btnCarica.Name = "btnCarica";
      this.btnCarica.Size = new System.Drawing.Size(133, 35);
      this.btnCarica.TabIndex = 2;
      this.btnCarica.Text = "Carica dati...";
      this.btnCarica.UseVisualStyleBackColor = true;
      this.btnCarica.Click += new System.EventHandler(this.btnClick);
      // 
      // dgvLista
      // 
      this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvLista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value});
      this.dgvLista.Location = new System.Drawing.Point(13, 72);
      this.dgvLista.Name = "dgvLista";
      this.dgvLista.Size = new System.Drawing.Size(320, 574);
      this.dgvLista.TabIndex = 3;
      // 
      // Value
      // 
      this.Value.HeaderText = "Value";
      this.Value.Name = "Value";
      // 
      // btnCreate
      // 
      this.btnCreate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCreate.Location = new System.Drawing.Point(618, 13);
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new System.Drawing.Size(133, 35);
      this.btnCreate.TabIndex = 4;
      this.btnCreate.Text = "Crea dati...";
      this.btnCreate.UseVisualStyleBackColor = true;
      this.btnCreate.Click += new System.EventHandler(this.btnClick);
      // 
      // dgvListaCreate
      // 
      this.dgvListaCreate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvListaCreate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
      this.dgvListaCreate.Location = new System.Drawing.Point(340, 72);
      this.dgvListaCreate.Name = "dgvListaCreate";
      this.dgvListaCreate.Size = new System.Drawing.Size(320, 574);
      this.dgvListaCreate.TabIndex = 5;
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.HeaderText = "Value";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      // 
      // frmGenReport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 658);
      this.Controls.Add(this.dgvListaCreate);
      this.Controls.Add(this.btnCreate);
      this.Controls.Add(this.dgvLista);
      this.Controls.Add(this.btnCarica);
      this.Controls.Add(this.btnSelFile);
      this.Controls.Add(this.tbPath);
      this.Name = "frmGenReport";
      this.Text = "frmGenReport";
      ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvListaCreate)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbPath;
    private System.Windows.Forms.Button btnSelFile;
    private System.Windows.Forms.Button btnCarica;
    private System.Windows.Forms.DataGridView dgvLista;
    private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    private System.Windows.Forms.Button btnCreate;
    private System.Windows.Forms.DataGridView dgvListaCreate;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  }
}