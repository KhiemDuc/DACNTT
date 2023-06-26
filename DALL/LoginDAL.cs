using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;

namespace DAL
{
    public class LoginDAL : DatabaseAccess
    {
        public async Task<DataTable> LoginAndGetRole(string username, string password)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = Connect();
                await OpenConAsync();

                myCommand.CommandText = String.Format($"Select CV.Ma_ChucVu,T.MaThanhVien,T.Ten " +
                                                    $"from ThanhVien T " +
                                                    $"Inner Join ChucVu CV on CV.Ma_ChucVu = T.MaChucVu " +
                                                    $"Inner join TaiKhoan TK on TK.MaThanhVien = T.MaThanhVien " +
                                                    $"Where TK.User_Name = @user_name and TK.Password = @pass_word ");
                myCommand.Parameters.AddWithValue("@user_name", username);
                myCommand.Parameters.AddWithValue("@pass_word", password);

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
    }
}
