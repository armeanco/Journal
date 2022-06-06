using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Journal
{
    public partial class Interface : Form
    {
        private DBSQL DB;
        public Interface()
        {
            InitializeComponent();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            Random rng = new Random((int)DateTime.Now.Ticks);
            string addIn = UDatePicker.Text;
            string addOut = UDatePicker1.Text;
            string addRoom = numBox.Text;
            string addComm = commBox.Text;
            string RNG = rng.Next().ToString();
            FormClass newRNG = new FormClass();
            newRNG.cFormID = RNG;
            FormClass newIn = new FormClass();
            newIn.cFormIn = addIn;
            FormClass newOut = new FormClass();
            newOut.cFormOut = addOut;
            FormClass newRoom = new FormClass();
            newRoom.cFormRoom = addRoom;
            FormClass newComm = new FormClass();
            newComm.cFormComm = addComm;
            if(addRoom.Length >= 1)
            {
                DB.AddToList(newIn, newOut, newRoom, newComm, newRNG);
                FillGrid();
            }
 
        }
        private void FillGrid()
        {
            FormClass[] forms = DB.getList();
            ListForm.RowCount = forms.Length > 0 ? forms.Length : 1;
            for(int i = 0; i < forms.Length; ++i)
            {
                ListForm[0, i].Value = forms[i].cFormIn;
                ListForm[1, i].Value = forms[i].cFormOut;
                ListForm[2, i].Value = forms[i].cFormRoom;
                ListForm[3, i].Value = forms[i].cFormComm;
                ListForm[5, i].Value = forms[i].cFormID;
                if (forms[i].cFormCheck.Equals("True"))
                {
                    ListForm[4, i].Value = true;
                }
                else
                {
                    ListForm[4, i].Value = false;
                }
            }
        }
        private void Interface_Load(object sender, EventArgs e)
        {
            string dbPath = Application.StartupPath + @"\..\..\..\ListForm.accdb";
            if(File.Exists(dbPath))
            {
                DBSQL.ConnectionString = dbPath;
                DB = DBSQL.Instance;
                FillGrid();
            }
        }
        private void ListForm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ListForm.CurrentRow.Cells[0].Value != null)
            {
                if (Convert.ToBoolean(ListForm.CurrentRow.Cells[4].Value))
                {
                    DB.updateRow(ListForm.CurrentRow.Cells[5].Value.ToString(), "True");
                }
                else
                {
                    DB.updateRow(ListForm.CurrentRow.Cells[5].Value.ToString(), "False");
                }
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            string updateRoom = numBox.Text;
            string updateIn = UDatePicker.Text;
            string updateOut = UDatePicker1.Text;
            string updateComm = commBox.Text;
            FormClass updRoom = new FormClass();
            FormClass updIn = new FormClass();
            FormClass updOut = new FormClass();
            FormClass updComm = new FormClass();
            updRoom.cFormRoom = updateRoom;
            updIn.cFormIn = updateIn;
            updOut.cFormOut = updateOut;
            updComm.cFormComm = updateComm;
            if (ListForm.CurrentRow.Cells[4].Value.ToString() == "True" && updateRoom.Length >= 1 && ListForm.CurrentRow.Cells[0].Value != null)
            {
                DB.updateCol(ListForm.CurrentRow.Cells[5].Value.ToString(), updateRoom, updateIn, updateOut, updateComm);
                FillGrid();
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ListForm.CurrentRow.Cells[4].Value.ToString() == "True" && ListForm.CurrentRow.Cells[0].Value != null)
            {
                DB.deleteCol(ListForm.CurrentRow.Cells[5].Value.ToString());
                FillGrid();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string getName = SearchView.Text;
            FormClass[] forms = DB.getList();
            ListForm.RowCount = forms.Length > 0 ? forms.Length : 1;
            for (int i = 0; i < forms.Length; ++i)
            {
                ListForm[0, i].Value = forms[i].cFormIn;
                ListForm[1, i].Value = forms[i].cFormOut;
                ListForm[2, i].Value = forms[i].cFormRoom;
                ListForm[3, i].Value = forms[i].cFormComm;
                ListForm[5, i].Value = forms[i].cFormID;
                if (forms[i].cFormRoom == getName)
                {
                    string in_temp = ListForm[0, 0].Value.ToString();
                    string out_temp = ListForm[1, 0].Value.ToString();
                    string room_temp = ListForm[2, 0].Value.ToString();
                    string comm_temp = ListForm[3, 0].Value.ToString();
                    string id_temp = ListForm[5, 0].Value.ToString();
                    ListForm[0, 0].Value = forms[i].cFormIn;
                    ListForm[0, i].Value = in_temp;
                    ListForm[1, 0].Value = forms[i].cFormOut;
                    ListForm[1, i].Value = out_temp;
                    ListForm[2, 0].Value = forms[i].cFormRoom;
                    ListForm[2, i].Value = room_temp;
                    ListForm[3, 0].Value = forms[i].cFormComm;
                    ListForm[3, i].Value = comm_temp;
                    ListForm[5, 0].Value = forms[i].cFormID;
                    ListForm[5, i].Value = id_temp;
                    ListForm[4, 0].Value = true;
                }
            }
        }
    }
}
