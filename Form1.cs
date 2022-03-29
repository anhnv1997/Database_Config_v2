using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionConfig
{
    public partial class Form1 : Form
    {
        SQLConn[] sqls = null;
        public Form1()
        {
            InitializeComponent();
            try
            {
                if (File.Exists(Application.StartupPath + "\\SQLConn.xml"))
                {
                    FileXML.ReadXMLSQLConn(Application.StartupPath + "\\SQLConn.xml", ref sqls);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmConnectionConfig: " + ex.Message);
            }
        }

        private void frmConnectionConfig_Load(object sender, EventArgs e)
        {
            try
            {
                if (sqls != null && sqls.Length > 0)
                {
                    //cbDatabaseType.Text = sqls[0].DBType;
                    txtServerName.Text = sqls[0].SQLServerName;
                    txtDatabaseName.Text = sqls[0].SQLDatabase;
                    txtUserName.Text = sqls[0].SQLUserName;
                    txtPassword.Text = CryptorEngine.Decrypt(sqls[0].SQLPassword, true);
                    cbAuthentication.Text = sqls[0].SQLAuthentication;
                    txtDatabaseEx.Text = sqls[0].SQLDatabaseEx;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmConnectionConfig_Load: " + ex.Message);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtServerName.Text == "" || txtDatabaseName.Text == "" || txtUserName.Text == "")
            {
                MessageBox.Show("Các trường dữ liệu không được trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<SQLConn> sqlconnlist = new List<SQLConn>();
            SQLConn sqlconn = null;
            sqlconn = new SQLConn("", txtServerName.Text, txtDatabaseName.Text, txtUserName.Text, CryptorEngine.Encrypt(txtPassword.Text, true), cbAuthentication.Text, txtDatabaseEx.Text);
            sqlconnlist.Add(sqlconn);
            sqls = new SQLConn[sqlconnlist.Count];
            sqlconnlist.CopyTo(sqls);
            FileXML.WriteXMLSQLConn(Application.StartupPath + @"\SQLConn.xml", sqls);
            MessageBox.Show("Lưu thiết lập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
