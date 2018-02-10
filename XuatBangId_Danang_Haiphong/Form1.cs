using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XuatBangId_Danang_Haiphong
{
    public partial class Form1 : Form
    {
        DataGridViewCheckBoxColumn dgvCmb1;
        
        
        DataGridViewCheckBoxColumn dgvCmb2;
        DataGridViewCheckBoxColumn dgvCmb_ketqua;
        
        public Form1()
        {
            InitializeComponent();
            LoadDataGridView01();
            LoadDataGridView02(null, null);
            LoadDataGridViewKetQua();
        }

       
        private void LoadDataGridView01()
        {
            dataGridView1.DataSource = DAO.GetDataSource.GetTable01();
            if (dgvCmb1 == null) {
                dgvCmb1= new DataGridViewCheckBoxColumn();
                dgvCmb1.ValueType = typeof(bool);
                dgvCmb1.Name = "Chk";
                dgvCmb1.HeaderText = "Chọn";
                dataGridView1.Columns.Add(dgvCmb1);
            }

        }
        private void LoadDataGridView02(string _idkhach, string _hoten)
        {
            if (dgvCmb2 == null)
            {
                dgvCmb2 = new DataGridViewCheckBoxColumn();
                dgvCmb2.ValueType = typeof(bool);
                dgvCmb2.Name = "Chk";
                dgvCmb2.HeaderText = "Chọn";
                dataGridView2.Columns.Add(dgvCmb2);
            }
             dataGridView2.DataSource = DAO.GetDataSource.GetTable02(_idkhach, _hoten);
            
        }
        private void LoadDataGridViewKetQua()
        {
            if (dgvCmb_ketqua == null)
            {
                dgvCmb_ketqua = new DataGridViewCheckBoxColumn();
                dgvCmb_ketqua.ValueType = typeof(bool);
                dgvCmb_ketqua.Name = "Chk";
                dgvCmb_ketqua.HeaderText = "Chọn";
                dataGridView_ketqua.Columns.Add(dgvCmb_ketqua);
            }
            dataGridView_ketqua.DataSource = DAO.GetDataSource.GetTable_ketqua();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id_01 = null;
            string id_02 = null;
            string id_khach01 = null;
            string id_khach02 = null;
            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell cb = (DataGridViewCheckBoxCell)r.Cells["Chk"];
                if(cb.Value != null && (bool)cb.Value)
                {
                    id_01 = r.Cells["so_tt"].Value.ToString();
                    id_khach01 = r.Cells["id_khach"].Value.ToString();

                    break;
                }
            }
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                DataGridViewCheckBoxCell cb = (DataGridViewCheckBoxCell)r.Cells["Chk"];
                if (cb.Value != null && (bool)cb.Value)
                {
                    id_02 = r.Cells["so_tt"].Value.ToString();
                    id_khach02 = r.Cells["id_khach"].Value.ToString();
                    
                    break;
                }
            }

            if (id_khach01 != null && id_khach02 != null && id_khach01 == id_khach02)
            {
                DAO.GetDataSource.Update_dachon_01(id_01);
                DAO.GetDataSource.Update_dachon_02(id_02);
                DAO.GetDataSource.Insert_id(id_khach01, id_khach02, id_01, id_02);



                LoadDataGridView01();
                LoadDataGridView02(null, null);
                LoadDataGridViewKetQua();
            }
            else
            {
                MessageBox.Show("Có lỗi khi chọn, hãy chọn lại !!!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id_khach01 = null;

            if ((dataGridView1.CurrentCell is DataGridViewCheckBoxCell))
            {
                if (dataGridView1.CurrentRow.Cells["Chk"].Value == null || !(bool)dataGridView1.CurrentRow.Cells["Chk"].Value)
                {

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells["Chk"].Value = null;
                    }
                    dataGridView1.CurrentRow.Cells["Chk"].Value = true;
                    id_khach01 = dataGridView1.CurrentRow.Cells["id_khach"].Value.ToString();
                }
                else
                {
                    dataGridView1.CurrentRow.Cells["Chk"].Value = null;
                }
                LoadDataGridView02(id_khach01, null);
            }
            
            
            

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell is DataGridViewCheckBoxCell)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    row.Cells["Chk"].Value = null;
                }
                dataGridView2.CurrentRow.Cells["Chk"].Value = true;
            }

        }
    }



}
