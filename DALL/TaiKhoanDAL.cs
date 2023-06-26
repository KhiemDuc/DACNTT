using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaiKhoanDAL : DatabaseAccess
    {
        public async Task<bool> UpdatePasswordUserAsync(string username, string oldpassword, string newpassword)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = String.Format($"Update [TaiKhoan]  SET  " +
                                                       $"[Password] = @new_password " +
                                                       $"WHERE [User_name] = @user_name and [Password] = @old_password ");

                sqlCommand.Parameters.AddWithValue("@user_name", username);
                sqlCommand.Parameters.AddWithValue("@old_password", oldpassword);
                sqlCommand.Parameters.AddWithValue("@new_password", newpassword);


                var rowAffected = await sqlCommand.ExecuteNonQueryAsync();

                return (rowAffected != 0);
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
        public async Task<bool> UpdateUser(string username, string password, string user_id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = String.Format($"Update [TaiKhoan]  SET  " +
                                                       $"[Password] = @password, " +
                                                       $"[User_Name] = @user_name " +
                                                       $"WHERE MaThanhVien = @user_id");

                sqlCommand.Parameters.AddWithValue("@user_name", username);
                sqlCommand.Parameters.AddWithValue("@password", password);
                sqlCommand.Parameters.AddWithValue("@user_id", user_id);

                var rowAffected = await sqlCommand.ExecuteNonQueryAsync();

                return (rowAffected != 0);
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
        public async Task<bool> PhanQuyen(string chucvu, string user_id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = String.Format($"Update [ThanhVien]  SET  " +
                                                       $"[MaChucVu] = @chucvu " +
                                                       $"WHERE MaThanhVien = @user_id");

                sqlCommand.Parameters.AddWithValue("@chucvu", chucvu);
                sqlCommand.Parameters.AddWithValue("@user_id", user_id);

                var rowAffected = await sqlCommand.ExecuteNonQueryAsync();

                return (rowAffected != 0);
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
        public async Task<DataTable> getInfoAllUserAsync()
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                myCommand.Connection = Connect();
                await OpenConAsync();

                myCommand.CommandText = String.Format($"Select CV.Ten_ChucVu,T.MaThanhVien,TK.User_Name,TK.Password,T.Ten " +
                                                    $"from ThanhVien T " +
                                                    $"Inner Join ChucVu CV on CV.Ma_ChucVu = T.MaChucVu " +
                                                    $"Inner join TaiKhoan TK on TK.MaThanhVien = T.MaThanhVien ");

                sqlDataAdapter.SelectCommand = myCommand;

                sqlDataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                CloseConnect();
                myCommand.Dispose();
                sqlDataAdapter.Dispose();
            }
        }
        public async Task<DataTable> getInfoUserAsync()
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                myCommand.Connection = Connect();
                await OpenConAsync();

                myCommand.CommandText = String.Format($"Select CV.Ten_ChucVu,T.MaThanhVien,TK.User_Name,TK.Password,T.Ten " +
                                                    $"from ThanhVien T " +
                                                    $"Inner Join ChucVu CV on CV.Ma_ChucVu = T.MaChucVu " +
                                                    $"Inner join TaiKhoan TK on TK.MaThanhVien = T.MaThanhVien " +
                                                    $"Where CV.Ma_ChucVu = 'User'");

                sqlDataAdapter.SelectCommand = myCommand;

                sqlDataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                CloseConnect();
                myCommand.Dispose();
                sqlDataAdapter.Dispose();
            }
        }
        public async Task<DataTable> DanhSachChucVu()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"select * from ChucVu");

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
        public async Task<DataTable> ThongTinCaNhan(string user_id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"select * from ThanhVien where MaThanhVien = @user_id");
                sqlCommand.Parameters.AddWithValue("@user_id", user_id);

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
        public async Task<DataTable> LayTaiKhoanMatKhau(string user_id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"select * from TaiKhoan where MaThanhVien = @user_id");
                sqlCommand.Parameters.AddWithValue("@user_id", user_id);

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
