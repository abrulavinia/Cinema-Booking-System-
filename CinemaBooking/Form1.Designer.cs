namespace CinemaBooking
{
    partial class FormManagerCinema
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            tabControlCinema = new TabControl();
            tabVanzariBilete = new TabPage();
            buttonExport = new Button();
            buttonSearch = new Button();
            dataGridVanzari = new DataGridView();
            Title = new DataGridViewTextBoxColumn();
            SoldTickets = new DataGridViewTextBoxColumn();
            tabTopVanzari = new TabPage();
            buttonRefresh = new Button();
            numericTop = new NumericUpDown();
            groupTopScreening = new GroupBox();
            tabControlCinema.SuspendLayout();
            tabVanzariBilete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridVanzari).BeginInit();
            tabTopVanzari.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericTop).BeginInit();
            SuspendLayout();
            // 
            // tabControlCinema
            // 
            tabControlCinema.Anchor = AnchorStyles.None;
            tabControlCinema.Controls.Add(tabVanzariBilete);
            tabControlCinema.Controls.Add(tabTopVanzari);
            tabControlCinema.Location = new Point(59, 48);
            tabControlCinema.Name = "tabControlCinema";
            tabControlCinema.SelectedIndex = 0;
            tabControlCinema.Size = new Size(950, 570);
            tabControlCinema.TabIndex = 0;
            // 
            // tabVanzariBilete
            // 
            tabVanzariBilete.BackColor = Color.MistyRose;
            tabVanzariBilete.Controls.Add(buttonExport);
            tabVanzariBilete.Controls.Add(buttonSearch);
            tabVanzariBilete.Controls.Add(dataGridVanzari);
            tabVanzariBilete.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            tabVanzariBilete.Location = new Point(4, 29);
            tabVanzariBilete.Name = "tabVanzariBilete";
            tabVanzariBilete.Padding = new Padding(3);
            tabVanzariBilete.Size = new Size(942, 537);
            tabVanzariBilete.TabIndex = 0;
            tabVanzariBilete.Text = "Vanzari Bilete Per Film";
            // 
            // buttonExport
            // 
            buttonExport.BackColor = Color.OldLace;
            buttonExport.Font = new Font("Segoe UI", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 238);
            buttonExport.Location = new Point(695, 269);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(183, 56);
            buttonExport.TabIndex = 3;
            buttonExport.Text = "Export table";
            buttonExport.UseVisualStyleBackColor = false;
            buttonExport.Click += buttonExport_Click;
            // 
            // buttonSearch
            // 
            buttonSearch.BackColor = Color.OldLace;
            buttonSearch.Font = new Font("Segoe UI", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 238);
            buttonSearch.ForeColor = SystemColors.ActiveCaptionText;
            buttonSearch.Location = new Point(708, 126);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(154, 55);
            buttonSearch.TabIndex = 2;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = false;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // dataGridVanzari
            // 
            dataGridVanzari.BackgroundColor = Color.SeaShell;
            dataGridVanzari.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridVanzari.Columns.AddRange(new DataGridViewColumn[] { Title, SoldTickets });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 238);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridVanzari.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridVanzari.Location = new Point(103, 77);
            dataGridVanzari.Name = "dataGridVanzari";
            dataGridVanzari.RowHeadersWidth = 51;
            dataGridVanzari.Size = new Size(459, 328);
            dataGridVanzari.TabIndex = 1;
            // 
            // Title
            // 
            Title.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Title.DataPropertyName = "Title";
            Title.HeaderText = "Title";
            Title.MinimumWidth = 180;
            Title.Name = "Title";
            // 
            // SoldTickets
            // 
            SoldTickets.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            SoldTickets.DataPropertyName = "SoldTickets";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            SoldTickets.DefaultCellStyle = dataGridViewCellStyle1;
            SoldTickets.HeaderText = "SoldTickets";
            SoldTickets.MinimumWidth = 6;
            SoldTickets.Name = "SoldTickets";
            SoldTickets.Width = 112;
            // 
            // tabTopVanzari
            // 
            tabTopVanzari.BackColor = Color.MistyRose;
            tabTopVanzari.Controls.Add(buttonRefresh);
            tabTopVanzari.Controls.Add(numericTop);
            tabTopVanzari.Controls.Add(groupTopScreening);
            tabTopVanzari.Location = new Point(4, 29);
            tabTopVanzari.Name = "tabTopVanzari";
            tabTopVanzari.Padding = new Padding(3);
            tabTopVanzari.Size = new Size(942, 537);
            tabTopVanzari.TabIndex = 1;
            tabTopVanzari.Text = "Top Vanzari";
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = Color.OldLace;
            buttonRefresh.Font = new Font("Segoe UI", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 238);
            buttonRefresh.Location = new Point(494, 367);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(169, 49);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "Refresh top";
            buttonRefresh.UseVisualStyleBackColor = false;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // numericTop
            // 
            numericTop.BackColor = Color.OldLace;
            numericTop.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            numericTop.Location = new Point(249, 376);
            numericTop.Name = "numericTop";
            numericTop.Size = new Size(150, 34);
            numericTop.TabIndex = 1;
            // 
            // groupTopScreening
            // 
            groupTopScreening.BackColor = Color.SeaShell;
            groupTopScreening.Location = new Point(177, 76);
            groupTopScreening.Name = "groupTopScreening";
            groupTopScreening.Size = new Size(578, 222);
            groupTopScreening.TabIndex = 0;
            groupTopScreening.TabStop = false;
            groupTopScreening.Text = "Top Screenings";
            // 
            // FormManagerCinema
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            BackgroundImage = Properties.Resources.Cinema_Photo1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1119, 672);
            Controls.Add(tabControlCinema);
            Name = "FormManagerCinema";
            Text = "Manager Cinema";
            tabControlCinema.ResumeLayout(false);
            tabVanzariBilete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridVanzari).EndInit();
            tabTopVanzari.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericTop).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlCinema;
        private TabPage tabVanzariBilete;
        private TabPage tabTopVanzari;
        private DataGridView dataGridVanzari;
        private Button buttonExport;
        private Button buttonSearch;
        private GroupBox groupTopScreening;
        private Button buttonRefresh;
        private NumericUpDown numericTop;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn SoldTickets;
    }
}
