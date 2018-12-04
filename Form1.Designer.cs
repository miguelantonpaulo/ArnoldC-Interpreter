namespace ArnoldC_Interpreter
{
    partial class mainEditorTB
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
            this.fileAddressTBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.editorTBox = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.outputTBox = new System.Windows.Forms.TextBox();
            this.LexerSymbolTable = new System.Windows.Forms.DataGridView();
            this.lexemeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keywordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolTable = new System.Windows.Forms.DataGridView();
            this.IdentifierColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LexemesTable = new System.Windows.Forms.Label();
            this.SymbolTableLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LexerSymbolTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolTable)).BeginInit();
            this.SuspendLayout();
            // 
            // fileAddressTBox
            // 
            this.fileAddressTBox.Enabled = false;
            this.fileAddressTBox.Location = new System.Drawing.Point(142, 21);
            this.fileAddressTBox.Name = "fileAddressTBox";
            this.fileAddressTBox.Size = new System.Drawing.Size(144, 20);
            this.fileAddressTBox.TabIndex = 0;
            this.fileAddressTBox.Text = "No file chosen";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(13, 15);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(123, 34);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Choose file...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // editorTBox
            // 
            this.editorTBox.Location = new System.Drawing.Point(13, 65);
            this.editorTBox.Multiline = true;
            this.editorTBox.Name = "editorTBox";
            this.editorTBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.editorTBox.Size = new System.Drawing.Size(273, 396);
            this.editorTBox.TabIndex = 2;
            // 
            // runButton
            // 
            this.runButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.Location = new System.Drawing.Point(12, 467);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(1211, 31);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "EXECUTE";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // outputTBox
            // 
            this.outputTBox.Location = new System.Drawing.Point(12, 504);
            this.outputTBox.Multiline = true;
            this.outputTBox.Name = "outputTBox";
            this.outputTBox.ReadOnly = true;
            this.outputTBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTBox.Size = new System.Drawing.Size(1211, 131);
            this.outputTBox.TabIndex = 5;
            // 
            // LexerSymbolTable
            // 
            this.LexerSymbolTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LexerSymbolTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lexemeColumn,
            this.keywordColumn});
            this.LexerSymbolTable.Location = new System.Drawing.Point(292, 65);
            this.LexerSymbolTable.Name = "LexerSymbolTable";
            this.LexerSymbolTable.Size = new System.Drawing.Size(459, 396);
            this.LexerSymbolTable.TabIndex = 6;
            // 
            // lexemeColumn
            // 
            this.lexemeColumn.HeaderText = "Lexemes";
            this.lexemeColumn.Name = "lexemeColumn";
            this.lexemeColumn.Width = 200;
            // 
            // keywordColumn
            // 
            this.keywordColumn.HeaderText = "Keywords";
            this.keywordColumn.Name = "keywordColumn";
            this.keywordColumn.Width = 200;
            // 
            // SymbolTable
            // 
            this.SymbolTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SymbolTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdentifierColumn,
            this.ValueColumn});
            this.SymbolTable.Location = new System.Drawing.Point(757, 65);
            this.SymbolTable.Name = "SymbolTable";
            this.SymbolTable.RowTemplate.Height = 24;
            this.SymbolTable.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SymbolTable.Size = new System.Drawing.Size(467, 396);
            this.SymbolTable.TabIndex = 7;
            // 
            // IdentifierColumn
            // 
            this.IdentifierColumn.HeaderText = "Identifier";
            this.IdentifierColumn.MinimumWidth = 10;
            this.IdentifierColumn.Name = "IdentifierColumn";
            this.IdentifierColumn.ReadOnly = true;
            // 
            // ValueColumn
            // 
            this.ValueColumn.HeaderText = "Value";
            this.ValueColumn.MinimumWidth = 10;
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.ReadOnly = true;
            // 
            // LexemesTable
            // 
            this.LexemesTable.AutoSize = true;
            this.LexemesTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LexemesTable.Location = new System.Drawing.Point(487, 24);
            this.LexemesTable.Name = "LexemesTable";
            this.LexemesTable.Size = new System.Drawing.Size(73, 20);
            this.LexemesTable.TabIndex = 8;
            this.LexemesTable.Text = "Lexemes";
            // 
            // SymbolTableLabel
            // 
            this.SymbolTableLabel.AutoSize = true;
            this.SymbolTableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SymbolTableLabel.Location = new System.Drawing.Point(943, 24);
            this.SymbolTableLabel.Name = "SymbolTableLabel";
            this.SymbolTableLabel.Size = new System.Drawing.Size(104, 20);
            this.SymbolTableLabel.TabIndex = 9;
            this.SymbolTableLabel.Text = "Symbol Table";
            // 
            // mainEditorTB
            // 
            this.ClientSize = new System.Drawing.Size(1236, 647);
            this.Controls.Add(this.SymbolTableLabel);
            this.Controls.Add(this.LexemesTable);
            this.Controls.Add(this.SymbolTable);
            this.Controls.Add(this.LexerSymbolTable);
            this.Controls.Add(this.outputTBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.editorTBox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileAddressTBox);
            this.Name = "mainEditorTB";
            this.Text = "Ang Ganda ni Ma\'am Kat Tan ArnoldCInterpreter";
            ((System.ComponentModel.ISupportInitialize)(this.LexerSymbolTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox fileAddressTBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox editorTBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox outputTBox;
        private System.Windows.Forms.DataGridView LexerSymbolTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn lexemeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keywordColumn;
        private System.Windows.Forms.DataGridView SymbolTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdentifierColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
        private System.Windows.Forms.Label LexemesTable;
        private System.Windows.Forms.Label SymbolTableLabel;
    }
}

