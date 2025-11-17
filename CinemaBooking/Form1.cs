using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace CinemaBooking
{
    public partial class FormManagerCinema : Form
    {
        private readonly PackageServices _svc;
        private BindingList<TicketSoldPerMovie> _salesBinding = new();
        private bool _salesColumnsConfigured = false;

        // ListView pentru tab-ul “Top”
        private readonly ListView _lvTop = new();

        public FormManagerCinema()
        {
            InitializeComponent();

            _svc = new PackageServices();
            _svc.createConnection();

            // GRID Sales
            dataGridVanzari.AutoGenerateColumns = false;
            dataGridVanzari.RowHeadersVisible = false;
            dataGridVanzari.AllowUserToAddRows = false;
            dataGridVanzari.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridVanzari.MultiSelect = false;
            dataGridVanzari.DataSource = _salesBinding;

            // Numeric top
            numericTop.Minimum = 1;
            numericTop.Maximum = 100;
            numericTop.Value = 5;

            // ListView Top
            _lvTop.Dock = DockStyle.Fill;
            _lvTop.View = View.Details;
            _lvTop.FullRowSelect = true;
            _lvTop.GridLines = true;
            _lvTop.Columns.Add("Movie", 220);
            _lvTop.Columns.Add("Hall", 90);
            _lvTop.Columns.Add("Time", 150);
            _lvTop.Columns.Add("Sold / Total", 110, HorizontalAlignment.Right);
            groupTopScreening.Controls.Clear();
            groupTopScreening.Controls.Add(_lvTop);

            buttonRefresh.Click += buttonRefresh_Click;
        }

        private void EnsureSalesColumns()
        {
            if (_salesColumnsConfigured) return;

            // Folosim coloanele EXISTENTE din designer:
            var colTitle = dataGridVanzari.Columns["Title"];
            var colSold = dataGridVanzari.Columns["SoldTickets"];

            if (colTitle != null)
            {
                colTitle.DataPropertyName = "Title";
                colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colTitle.MinimumWidth = 180;
            }

            if (colSold != null)
            {
                colSold.DataPropertyName = "SoldTickets";
                colSold.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                colSold.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colSold.DefaultCellStyle.Format = "N0";
            }

            _salesColumnsConfigured = true;
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            buttonSearch.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                EnsureSalesColumns();

                var list = await Task.Run(() => _svc.GetTicketsSoldPerMovie())
                           ?? new List<TicketSoldPerMovie>();

                _salesBinding = new BindingList<TicketSoldPerMovie>(
                    list.OrderByDescending(x => x.SoldTickets).ToList()
                );
                dataGridVanzari.DataSource = _salesBinding;

                var totalTickets = list.Sum(x => x.SoldTickets);
                this.Text = $"Manager Cinema – {list.Count} filme, {totalTickets:N0} bilete vândute";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Nu s-au putut încãrca vânzãrile.\n{ex.Message}",
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                buttonSearch.Enabled = true;
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridVanzari.Rows.Count == 0)
            {
                MessageBox.Show(this, "Nu existã date de exportat.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Indici siguri ai coloanelor dupã Name (cele din designer)
            int idxTitle = dataGridVanzari.Columns["Title"].Index;
            int idxSold = dataGridVanzari.Columns["SoldTickets"].Index;

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx",
                FileName = $"Vanzari bilete per film - {DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Sales");

            // Header
            ws.Cell(1, 1).Value = "Title";
            ws.Cell(1, 2).Value = "SoldTickets";
            ws.Range(1, 1, 1, 2).Style.Font.Bold = true;

            // Rows
            int r = 2;
            foreach (DataGridViewRow row in dataGridVanzari.Rows)
            {
                if (row.IsNewRow) continue;
                ws.Cell(r, 1).Value = row.Cells[idxTitle].Value?.ToString();
                ws.Cell(r, 2).Value = Convert.ToInt64(row.Cells[idxSold].Value ?? 0);
                r++;
            }

            ws.Columns().AdjustToContents();
            wb.SaveAs(sfd.FileName);

            MessageBox.Show(this, "Export reu?it.", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            int top = (int)numericTop.Value;
            if (top <= 0) top = 5;

            buttonRefresh.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                var list = await Task.Run(() => _svc.GetMostPopularScreenings(top))
                           ?? new List<Screening>();

                _lvTop.BeginUpdate();
                _lvTop.Items.Clear();

                foreach (var s in list)
                {
                    string movieTitle = s.Movie?.title ?? "(unknown)";
                    string hall = s.Hall ?? "";
                    string time = s.Time.ToString("yyyy-MM-dd HH:mm");
                    string soldTotal = $"{s.SeatsSold} / {s.SeatsTotal}";

                    var lvi = new ListViewItem(movieTitle);
                    lvi.SubItems.Add(hall);
                    lvi.SubItems.Add(time);
                    lvi.SubItems.Add(soldTotal);
                    _lvTop.Items.Add(lvi);
                }

                _lvTop.EndUpdate();
                this.Text = $"Manager Cinema – Top {top} screenings";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Nu s-a putut încãrca top-ul.\n" + ex.Message,
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                buttonRefresh.Enabled = true;
            }
        }
    }
}
