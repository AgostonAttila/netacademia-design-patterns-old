using System;
using System.Data;
using System.IO;

namespace _04Adapter
{
    public class AdatMegjelenito
    {
        private IDbDataAdapter adapter;

        public AdatMegjelenito(IDbDataAdapter adapter)
        {
            this.adapter = adapter;
        }

        public void Megjelenites(TextWriter writer)
        {
            writer.WriteLine("AdatMegjelenito.Megjelenites");

            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach (DataTable table in data.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    writer.Write(column.ColumnName.PadRight(20));
                }
                writer.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString().PadRight(20));
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}