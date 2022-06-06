using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Configuration;

namespace Journal
{
    class DBSQL : DbAccess
    {
        private static string conString;
        private static DBSQL instance;
        public DBSQL(string connectionString)
            : base(connectionString)
        {
        }
        public static DBSQL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBSQL(conString);
                }
                return instance;
            }
        }
        public static string ConnectionString
        {
            get
            {
                return conString;
            }
            set
            {
                conString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + value + ";Persist Security Info=False;";
            }
        }
        public object ListForm { get; private set; }
        public FormClass[] getList()
        {
            DataSet ds = new DataSet();
            ArrayList list = new ArrayList();
            string commandStr = "SELECT * FROM Form";
            using (OleDbCommand comm = new OleDbCommand(commandStr))
            {
                ds = GetMultipleQuery(comm);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch
            {
            }
            foreach (DataRow row in dt.Rows)
            {
                FormClass newForm = new FormClass();
                newForm.cFormIn = row[0].ToString();
                newForm.cFormOut = row[1].ToString();
                newForm.cFormRoom = row[2].ToString();
                newForm.cFormComm = row[3].ToString();
                newForm.cFormCheck = row[4].ToString();
                newForm.cFormID = row[5].ToString();
                list.Add(newForm);
            }
            return (FormClass[])list.ToArray(typeof(FormClass));
        }
        public void AddToList(FormClass newIn, FormClass newOut, FormClass newRoom, FormClass newComm, FormClass newRNG)
        {
            string commandStr = "INSERT INTO Form (Time_in,Time_out,Room,Comment,Mark,Rand) VALUES (@Time_in,@Time_out,@Room,@Comment,@Mark,@Rand)";
            using (OleDbCommand comm = new OleDbCommand(commandStr))
            {
                comm.Parameters.AddWithValue("@time_in", newIn.cFormIn);
                comm.Parameters.AddWithValue("@time_out", newOut.cFormOut);
                comm.Parameters.AddWithValue("@room", newRoom.cFormRoom);
                comm.Parameters.AddWithValue("@comment", newComm.cFormComm);
                comm.Parameters.AddWithValue("@mark", newIn.cFormCheck);
                comm.Parameters.AddWithValue("@rand", newRNG.cFormID);
                base.ExecuteSimpleQuery(comm);
            }
        }
        public void updateRow(string listText, string state)
        {
            string commString = "UPDATE Form SET Mark=@state WHERE Room=@listText";
            using (OleDbCommand comm = new OleDbCommand(commString))
            {
                comm.Parameters.AddWithValue("@state", state);
                comm.Parameters.AddWithValue("listText", listText);
                base.ExecuteSimpleQuery(comm);
            }
        }
        public void updateCol(string listText, string state, string time, string t_time, string comment)
        {
            string commString = "UPDATE Form SET Room=@state, Time_in=@time, Time_out=@t_time, Comment=@comment WHERE Rand=@listText";
            using (OleDbCommand comm = new OleDbCommand(commString))
            {
                comm.Parameters.AddWithValue("@state", state);
                comm.Parameters.AddWithValue("@time", time);
                comm.Parameters.AddWithValue("@t_time", t_time);
                comm.Parameters.AddWithValue("@comment", comment);
                comm.Parameters.AddWithValue("listText", listText);
                base.ExecuteSimpleQuery(comm);
            }
        }
        public void deleteCol(string listRoom)
        {
            string commStr = "DELETE * FROM Form WHERE Rand=@listRoom";
            using (OleDbCommand comm = new OleDbCommand(commStr))
            {
                comm.Parameters.AddWithValue("@listRoom", listRoom);
                base.ExecuteSimpleQuery(comm);
            }
        }
    }
}
