using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThanhVien_ThuDAL: DatabaseAccess
    {
        public async Task<DataTable> DanhSachThanhVienThu(string idThu)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT T.MaThanhVien,U.TenThu, T.Ten, Tv.TrangThai, Tv.NgayThu, Tv.SoTien " +
                    $"from ThanhVien_Thu as Tv inner join ThanhVien as T on T.MaThanhVien = Tv.MaThanhVien " +
                    $"inner join Thu as U on U.MaThu = Tv.MaThu " +
                    $"where U.MaThu = @mathu ");

                sqlCommand.Parameters.AddWithValue("@mathu", idThu);
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dataTable);
                return dataTable;
            }
            finally
            {
                CloseConnect();
                sqlCommand.Dispose();
                adapter.Dispose();
            }
        }
    }
}
