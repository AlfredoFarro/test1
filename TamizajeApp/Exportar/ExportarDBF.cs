using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Dbf;

namespace TamizajeApp.Exportar
{
    public partial class ExportarDBF : Form
    {
        public ExportarDBF()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Rechazada");
            dt.PrimaryKey = new DataColumn[] { dt.Columns["Codigo"] };

            DataRow dr = dt.NewRow();
            dr["Codigo"] = "primero";
            dr["Rechazada"] = false;
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["Codigo"] = "segundo";
            dr1["Rechazada"] = true;
            dt.Rows.Add(dr1);

            //ViewState["TablaTarjetas"] = dt;

            dataGridView1.DataSource = dt;
            Write(@"C:\FilesCSV\test.dbf",dt,Encoding.ASCII);
            //dataGridView1.DataBind();
        }
        public static void Write(string fileName, System.Data.DataTable table, Encoding encoding)
        {
            System.IO.Stream stream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);

            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);

            List<DbfFieldDescriptor> columns = MakeDbfFields(table.Columns);

            DbfHeader header = new DbfHeader(table.Rows.Count, table.Columns.Count, GetRecordLength(columns), encoding);

            writer.Write(IRI.Ket.IO.BinaryStream.StructureToByteArray(header));

            foreach (var item in columns)
            {
                writer.Write(IRI.Ket.IO.BinaryStream.StructureToByteArray(item));
            }

            //Terminator
            writer.Write(byte.Parse("0D", System.Globalization.NumberStyles.HexNumber));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                // All dbf field records begin with a deleted flag field. Deleted - 0x2A (asterisk) else 0x20 (space)
                writer.Write(byte.Parse("20", System.Globalization.NumberStyles.HexNumber));

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    byte[] temp = new byte[columns[j].Length];

                    string value = table.Rows[i][j].ToString().Trim();

                    encoding.GetBytes(value, 0, value.Length, temp, 0);

                    writer.Write(temp);
                }
            }

            //End of file
            writer.Write(byte.Parse("1A", System.Globalization.NumberStyles.HexNumber));

            writer.Close();

            stream.Close();
        }

        public static List<DbfFieldDescriptor> MakeDbfFields(System.Data.DataColumnCollection columns)
        {
            List<DbfFieldDescriptor> result = new List<DbfFieldDescriptor>();

            foreach (System.Data.DataColumn item in columns)
            {
                result.Add(new DbfFieldDescriptor(item.ColumnName, 'C', 255, 0));
            }

            return result;
        }
        private static short GetRecordLength(List<DbfFieldDescriptor> columns)
        {
            short result = 0;

            foreach (var item in columns)
            {
                result += item.Length;
            }

            result += 1; //Deletion Flag

            return result;
        }
    }
}
