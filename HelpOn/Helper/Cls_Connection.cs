using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.OleDb;
 
using System.Drawing.Imaging;

public class Cls_Connection
{
    HttpContext ctx = HttpContext.Current;
    DataSet ds = new DataSet();

    DataTable dt = new DataTable();
     static string connString = (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
   //  static string connString = "Data Source=205.147.96.31;Initial Catalog=db_Helpon;Persist Security Info=True;User ID=User_Helpon;Password=Helpon@2022;Pooling=True;Min Pool Size=0;Max Pool Size=250;Connect Timeout=100000";

    DbCommand dbcommand;
    SqlDatabase db = new SqlDatabase(connString);
    public static SqlConnection scon = new SqlConnection(connString);
    private string _loginname, _password, _action, _IpAddress;
    public string success = "success";
    public string fail = "fail";
    public string pending = "pending";

    private static String changeConnStringItem(string connString)
    {
        String[] conItems = connString.Split(';');
        String result = "";
        foreach (String item in conItems)
        {
            if (item.StartsWith("Data Source"))
            {
                string[] convalue = item.Split('=');
                result += "Data Source=" + Decrypt(convalue[1].ToString().Replace("@", "=")) + ";";
            }
            else if (item.StartsWith("Initial Catalog"))
            {
                string[] convalue = item.Split('=');
                result += "Initial Catalog=" + Decrypt(convalue[1].ToString().Replace("@", "=")) + ";";
            }
            else if (item.StartsWith("User ID"))
            {
                string[] convalue = item.Split('=');
                result += "User ID=" + Decrypt(convalue[1].ToString().Replace("@", "=")) + ";";
            }
            else if (item.StartsWith("Password"))
            {
                string[] convalue = item.Split('=');
                result += "Password=" + Decrypt(convalue[1].ToString().Replace("@", "=")) + ";";
            }
            else
            {
                result += item + ";";
            }
        }
        return result;
    }

    public static string Decrypt(string value)
    {
        try
        {
            string textToDecrypt = value;
            string ToReturn = "";
            string publickey = ConfigurationManager.AppSettings["publickey"];
            string privatekey = ConfigurationManager.AppSettings["privatekey"];
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.Encoding.UTF8.GetBytes(privatekey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                ToReturn = encoding.GetString(ms.ToArray());
            }
            return ToReturn;
        }
        catch (Exception ae)
        {
            throw new Exception(ae.Message, ae.InnerException);
        }
    }
    public Cls_Connection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void CreateConnection()
    {
        if (scon.State != ConnectionState.Open)
        {
            scon.Open();
        }
    }
    public void CloseConnection()
    {
        scon.Close();
    }

    public DataTable selectDataTable(string SQL)
        {
    NEXT1:
        DataTable dtInv = new DataTable();
        try
        {
            dbcommand = db.GetSqlStringCommand(SQL);
            ds = db.ExecuteDataSet(dbcommand);
            if (ds.Tables.Count > 0)
            {
                dtInv = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
        }
        return dtInv;
    }

    public DataSet GetDataSet(string SQL)
    {
        DataSet dsInv = new DataSet();
        try
        {

            dbcommand = db.GetSqlStringCommand(SQL);
            dsInv = db.ExecuteDataSet(dbcommand);
        }
        catch (Exception ex)
        {
            dsInv = null;
        }
        return dsInv;
    }
    public DataTable GetDataTable(string SQL)
    {
        DataTable dtInv = new DataTable();
        try
        {
            dbcommand = db.GetSqlStringCommand(SQL);
            ds = db.ExecuteDataSet(dbcommand);
            if (ds.Tables.Count > 0)
            {
                dtInv = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            dtInv = null;
        }
        return dtInv;
    }
    public void BindDropDownList(DropDownList ddl, string sql, string TextField, string ValueField)
    {
        DataTable dt = new DataTable();
        dt = GetDataTable(sql);
        ddl.DataSource = dt;
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    public int ExecuteQuery(string qur)
    {
        int Retval = 0;
        dbcommand = db.GetSqlStringCommand(qur);
        Retval = db.ExecuteNonQuery(dbcommand);
        return Retval;
    }
    public int ExecuteScalar(string qur)
    {
        int Retval = 0;
        dbcommand = db.GetSqlStringCommand(qur);
        Retval = Convert.ToInt32(db.ExecuteScalar(dbcommand));
        return Retval;
    }
    public int ExecuteIntScalar(string qur)
    {
        int Retval = 0;
        dbcommand = db.GetSqlStringCommand(qur);
        Retval = Convert.ToInt32(db.ExecuteScalar(dbcommand));
        return Retval;
    }
    public string ExecuteStringScalar(string qur)
    {
        string Retval = string.Empty;
        dbcommand = db.GetSqlStringCommand(qur);
        Retval = Convert.ToString(db.ExecuteScalar(dbcommand));
        return Retval;
    }


   
    public DataTable AdminLoginAuthentication()
    {
        DataTable dtUserMaste = new DataTable();
        dbcommand = db.GetStoredProcCommand("ProcMaster_Login", action, loginname, password, IpAddress);
        ds = db.ExecuteDataSet(dbcommand);
        dtUserMaste = ds.Tables[0];
        return dtUserMaste;
    }

    public DataTable ExcelData(string Name, string Extension)
    {
        string connString = "";

        //Connection String to Excel Workbook
        if (Extension.Trim() == ".xls")
        {
            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Name + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        }
        else if (Extension.Trim() == ".xlsx")
        {
            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Name + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        }

        OleDbConnection connExcel = new OleDbConnection(connString);

        OleDbCommand cmdExcel = new OleDbCommand();

        OleDbDataAdapter oda = new OleDbDataAdapter();

        DataTable dt = new DataTable();

        cmdExcel.Connection = connExcel;
        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        return dt;
    }
    public string loginname
    {
        get { return this._loginname; }
        set { this._loginname = value; }
    }
    public string IpAddress
    {
        get { return this._IpAddress; }
        set { this._IpAddress = value; }
    }
    public string password
    {
        get { return this._password; }
        set { this._password = value; }
    }
    public string action
    {
        get { return this._action; }
        set { this._action = value; }
    }

   
}
