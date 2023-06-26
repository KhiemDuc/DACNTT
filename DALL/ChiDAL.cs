using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChiDAL: DatabaseAccess
    {
        public async Task<bool> ThietLapKhoanChi(string TenKhoanChi, DateTime NgayChi, string SoTien, string MoTa)
        {

            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"INSERT INTO [Chi] ([TenKhoanChi],[SoTien]," +
                    $"[NgayChi],[MoTa]) " +
                    $"VALUES(@TenKhoanChi,@SoTien,@NgayChi,@MoTa)");

                sqlCommand.Parameters.AddWithValue("@TenKhoanChi", TenKhoanChi);
                sqlCommand.Parameters.AddWithValue("@NgayChi", NgayChi);
                sqlCommand.Parameters.AddWithValue("@SoTien", SoTien);

                if (MoTa != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@MoTa", MoTa);
                else
                    sqlCommand.Parameters.AddWithValue("@MoTa", DBNull.Value);

                var rowEfected = await sqlCommand.ExecuteNonQueryAsync();
                return (rowEfected != 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnect();
                sqlCommand.Dispose();

            }

        }
        public async Task<bool> SuaThietLapKhoanChi(string MaChi, string TenKhoanChi, DateTime NgayBatDauThu, string HanMuc, string MoTa)
        {

            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"UPDATE [Chi] SET [TenKhoanChi] = @TenKhoanChi, " +
                    $"[NgayChi] = @NgayBatDauThu, [SoTien] = @HanMuc, " +
                    $"[MoTa] = @MoTa " +
                    $"WHERE [MaChi] = @MaChi");
                sqlCommand.Parameters.AddWithValue("@MaChi", MaChi);
                sqlCommand.Parameters.AddWithValue("@TenKhoanChi", TenKhoanChi);
                sqlCommand.Parameters.AddWithValue("@NgayBatDauThu", NgayBatDauThu);
                sqlCommand.Parameters.AddWithValue("@HanMuc", HanMuc);
  

                if (MoTa != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@MoTa", MoTa);
                else
                    sqlCommand.Parameters.AddWithValue("@MoTa", DBNull.Value);

                var rowEfected = await sqlCommand.ExecuteNonQueryAsync();
                return (rowEfected != 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnect();
                sqlCommand.Dispose();

            }

        }
        public async Task<DataTable> DanhSachKhoanChi()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT * from GiaoDich_ChiTieu");

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
        public async Task<DataTable> DanhSachSuKien()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT * from GiaoDich_ChiTieu as G inner join ThanhVien as TV " +
                                                        $"on G.MaThanhVien = TV.MaThanhVien where MaChi = 2");

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
        public async Task<DataTable> DanhSachLoaiChi()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT MaChi,LoaiChi from Chi");

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
        public async Task<SqlDataReader> LayThongTinKhoanChi(string MaChi)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT * from GiaoDich_ChiTieu where MaChi = @MaChi");
                sqlCommand.Parameters.AddWithValue("@MaChi", MaChi);

                var row = await sqlCommand.ExecuteReaderAsync();

                if (row.HasRows)
                {
                    // Khi đọc thành công một dòng dữ liệu, trả về đối tượng SqlDataReader
                    return row;
                }
                return null;
            }
            finally
            {
                sqlCommand.Dispose(); ;
            }
        }

    }
}
