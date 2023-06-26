using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThuDAL: DatabaseAccess
    {
        public async Task<bool> ThietLapKhoanThu(string TenKhoanThu, DateTime NgayBatDauThu, string HanMuc, byte LoaiThu, string MoTa)
        {
            byte LoaiThuBit = LoaiThu > 0 ? (byte)0 : (byte)1;

            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"INSERT INTO [Thu] ([TenThu],[NgayBatDauThu],[DinhMuc]," +
                    $"[LoaiThu],[MoTa]) " +
                    $"VALUES(@TenKhoanThu,@NgayBatDauThu,@HanMuc,@LoaiThuBit,@MoTa)");

                sqlCommand.Parameters.AddWithValue("@TenKhoanThu", TenKhoanThu);
                sqlCommand.Parameters.AddWithValue("@NgayBatDauThu", NgayBatDauThu);
                sqlCommand.Parameters.AddWithValue("@LoaiThuBit", LoaiThuBit);

                if (HanMuc != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@HanMuc", HanMuc);
                else
                    sqlCommand.Parameters.AddWithValue("@HanMuc", DBNull.Value);
                if (MoTa != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@MoTa", MoTa);
               else 
                    sqlCommand.Parameters.AddWithValue("@MoTa",DBNull.Value);

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
        public async Task<bool> SuaThietLapKhoanThu(string MaThuChi,string TenKhoanThu, DateTime NgayBatDauThu, string HanMuc, byte LoaiThu, string MoTa)
        {
            byte LoaiThuBit = LoaiThu > 0 ? (byte)0 : (byte)1;

            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"UPDATE [Thu] SET [TenThu] = @TenKhoanThu, " +
                    $"[NgayBatDauThu] = @NgayBatDauThu, [DinhMuc] = @HanMuc, " +
                    $"[LoaiThu] = @LoaiThuBit, [MoTa] = @MoTa " +
                    $"WHERE [MaThu] = @MaThuChi");
                sqlCommand.Parameters.AddWithValue("@MaThuChi", MaThuChi);
                sqlCommand.Parameters.AddWithValue("@TenKhoanThu", TenKhoanThu);
                sqlCommand.Parameters.AddWithValue("@NgayBatDauThu", NgayBatDauThu);
                sqlCommand.Parameters.AddWithValue("@HanMuc", HanMuc);
                sqlCommand.Parameters.AddWithValue("@LoaiThuBit", LoaiThuBit);

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
        public async Task<DataTable> DanhSachKhoanThu()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT * from Thu");

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
        public async Task<bool> XoaKhoanThu(string maThu)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"DELETE FROM Thu WHERE MaThu = @MaThu");
                sqlCommand.Parameters.AddWithValue("@MaThu", maThu);

                var rowEffected = await sqlCommand.ExecuteNonQueryAsync();
                return (rowEffected != 0);
            }
            finally
            {
                CloseConnect();
                sqlCommand.Dispose();
            }
        }

        public async Task<SqlDataReader> LayThongTinKhoanThu(string MaThu)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT * from Thu where MaThu = @MaThu");
                sqlCommand.Parameters.AddWithValue("@MaThu", MaThu);

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
                sqlCommand.Dispose();;
            }
        }
    }
}
