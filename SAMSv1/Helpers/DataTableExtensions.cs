using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SAMSv1.Helpers
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            var table = new DataTable(typeof(T).Name);
            var props = TypeDescriptor.GetProperties(typeof(T));

            foreach (PropertyDescriptor prop in props)
                table.Columns.Add(prop.Name,
                    System.Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (var item in source)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in props)
                    row[prop.Name] = prop.GetValue(item) ?? System.DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}