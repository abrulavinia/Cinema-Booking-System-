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
        private readonly PackageServices svc;
        private BindingList<TicketSoldPerMovie> salesBinding = new();
        private bool salesColumnsConfigured = false;

        private readonly ListView lvTop = new();
        public FormManagerCinema()
        {
            InitializeComponent();

            svc = new PackageServices();
            svc.createConnection();

            dataGridVanzari.AutoGenerateColumns = false;
            dataGridVanzari.RowHeadersVisible = false;
            dataGridVanzari.AllowUserToAddRows = false;
            dataGridVanzari.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridVanzari.MultiSelect = false;
            dataGridVanzari.DataSource = salesBinding;

            numericTop.Minimum = 1;
            numericTop.Maximum = 100;
            numericTop.Value = 5;

            lvTop.Dock = DockStyle.Fill;
            lvTop.View = View.Details;
            lvTop.FullRowSelect = true;
            lvTop.GridLines = true;
            lvTop.Columns.Add("Movie", 220);
            lvTop.Columns.Add("Hall", 90);
            lvTop.Columns.Add("Time", 150);
            lvTop.Columns.Add("Sold/Total", 110, HorizontalAlignment.Right);

            groupTopScreening.Controls.Clear();
            groupTopScreening.Controls.Add(lvTop);

            buttonRefresh.Click += buttonRefresh_Click;
        }

        private void EnsureSalesColumns()
        {
            if (salesColumnsConfigured) return;
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

            salesColumnsConfigured = true;
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            buttonSearch.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                EnsureSalesColumns();
                var list = await Task.Run(() => svc.GetTicketsSoldPerMovie())
                           ?? new List<TicketSoldPerMovie>();

                salesBinding = new BindingList<TicketSoldPerMovie>(
                    list.OrderByDescending(x => x.SoldTickets).ToList()
                );
                dataGridVanzari.DataSource = salesBinding;

                var totalTickets = list.Sum(x => x.SoldTickets);
                this.Text = $"Manager Cinema – {list.Count} filme, {totalTickets:N0} bilete vandute";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Nu s-au putut incarca vanzarile.\n{ex.Message}",
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
            int idxTitle = dataGridVanzari.Columns["Title"].Index;
            int idxSold = dataGridVanzari.Columns["SoldTickets"].Index;

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx",
                FileName = $"Vanzari bilete per film - {DateTime.Now:yyyy.MM.dd}.xlsx"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Sales");

            ws.Cell(1, 1).Value = "Title";
            ws.Cell(1, 2).Value = "SoldTickets";
            ws.Range(1, 1, 1, 2).Style.Font.Bold = true;

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

            MessageBox.Show(this, "Export reusit.", "OK",
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
                var list = await Task.Run(() => svc.GetMostPopularScreenings(top))
                           ?? new List<Screening>();

                lvTop.BeginUpdate();
                lvTop.Items.Clear();

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
                    lvTop.Items.Add(lvi);
                }

                lvTop.EndUpdate();
                this.Text = $"Manager Cinema – Top {top} screenings";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Nu s-a putut incarca top-ul.\n" + ex.Message,
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
