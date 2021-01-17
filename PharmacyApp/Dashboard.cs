using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyApp
{
    public partial class Dashboard : Form
    {
        PharmacyDB _context = new PharmacyDB();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void FillMedicineCombo()
        {
            cmbMedicine.Items.AddRange(_context.Medicines.Select(m => m.Name).ToArray());
        }

        private void FillTagCombo()
        {
            cmbTags.Items.AddRange(_context.Tags.Select(m => m.Name).ToArray());
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedicineForm mdForm = new MedicineForm();
            mdForm.ShowDialog();
        }

        private void FillMedicineDataGrid()
        {
            dtgMedicineDataGrid.DataSource = _context.Medicine_To_Tags
                .Where(m=>m.Medicine.Name.Contains(cmbMedicine.Text)&&
                  m.Tag.Name.Contains(cmbTags.Text))
                .Select(m => new {
                Medicine_Name = m.Medicine.Name,
                Price = m.Medicine.Price + "AZN",
                m.Medicine.Quantity,
                Firm_Name = m.Medicine.Name,
                Receipts = m.Medicine.IsReceipt ? "Reseptli" : "Reseptsiz",
                m.Medicine.ProductionDate,
                m.Medicine.ExpiryDate,
            }).Distinct().ToList();
            dtgMedicineDataGrid.Columns[5].DefaultCellStyle.Format = "dd MMMM yyyy / HH:mm:ss";
            dtgMedicineDataGrid.Columns[6].DefaultCellStyle.Format = "dd MMMM yyyY / HH:mm:ss";
            for (int i = 0; i < dtgMedicineDataGrid.RowCount; i++)
            {
               short count=(short)dtgMedicineDataGrid.Rows[i].Cells[2].Value;
                DateTime exDate = (DateTime)dtgMedicineDataGrid.Rows[i].Cells[6].Value;
                if (count == 0)
                {
                    dtgMedicineDataGrid.Rows[i].DefaultCellStyle.BackColor = Color.DarkRed;
                    dtgMedicineDataGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;

                }
                if(exDate < DateTime.Now)
                {
                    dtgMedicineDataGrid.Rows[i].DefaultCellStyle.BackColor = Color.DarkOrange;
                    dtgMedicineDataGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
                if(exDate<DateTime.Now && count == 0)
                {
                    dtgMedicineDataGrid.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                    dtgMedicineDataGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            FillMedicineCombo();
            FillTagCombo();
            FillMedicineDataGrid();

        }

        private void cmbMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMedicineDataGrid();

        }

        private void cmbMedicine_KeyUp(object sender, KeyEventArgs e)
        {
            FillMedicineDataGrid();
        }

        private void cmbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMedicineDataGrid();
        }

        private void cmbTags_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnOpenBarcode_Click(object sender, EventArgs e)
        {
            BarcodeReaderForm rdb = new BarcodeReaderForm();
            rdb.ShowDialog();
        }
    }
}
