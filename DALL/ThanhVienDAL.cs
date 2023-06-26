using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThanhVienDAL: DatabaseAccess
    {
        public async Task<DataTable> GetInfoThanhVien()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"SELECT A.*, B.Ten AS TenCha FROM ThanhVien A JOIN ThanhVien B ON A.MaCha = B.MaThanhVien");
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
        public async Task<bool> ThemThanhVien(string HoTen, DateTime NgaySinh, string HotenCha, string HoTenMe, string Doi, byte GioiTinh, string DiaChi)
        {
            byte GioiTinhBit = GioiTinh > 0 ? (byte)0 : (byte)1;

            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"INSERT INTO [ThanhVien] ([Ten],[NgaySinh],[GioiTinh]," +
                    $"[MaCha],[HoTenMe],[Doi],[DiaChi]) " +
                    $"VALUES(@HoTen,@NgaySinh,@GioiTinhBit,@HotenCha,@HoTenMe,@Doi,@DiaChi)");

                sqlCommand.Parameters.AddWithValue("@HoTen", HoTen);
                sqlCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                sqlCommand.Parameters.AddWithValue("@GioiTinhBit", GioiTinhBit);
                sqlCommand.Parameters.AddWithValue("@HotenCha", HotenCha);
                sqlCommand.Parameters.AddWithValue("@HoTenMe", HoTenMe);
                sqlCommand.Parameters.AddWithValue("@Doi", Doi);
                sqlCommand.Parameters.AddWithValue("@DiaChi", DiaChi);



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
        public async Task<bool> ThemCuTo(string HoTen, DateTime NgaySinh, string HotenCha, string HoTenMe, string Doi, byte GioiTinh, 
            string DiaChi, byte TinhTrangHonNhan, string HoTenVC,string HocVan, byte TrangThai, string NoiAnTang, DateTime? NgayMat,
            string NgheNghiep, string ThanhTuu)
        {
            byte GioiTinhBit = GioiTinh > 0 ? (byte)0 : (byte)1;
            byte TinhTrang_HonNhan = TinhTrangHonNhan > 0 ? (byte)1 : (byte)0;
            byte TrangThaiBit = TrangThai > 0 ? (byte)0 : (byte)1;

            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"INSERT INTO [ThanhVien] ([Ten],[NgaySinh],[GioiTinh]," +
                    $"[HoTenCha],[HoTenMe],[Doi],[DiaChi],[TinhTrang_HonNhan],[HoTenVo/Chong],[HocVan],[TrangThai],[NoiAnTang],[Ngay_Mat],[NgheNghiep],[ThanhTuu]) " +
                    $"VALUES(@HoTen,@NgaySinh,@GioiTinhBit,@HotenCha,@HoTenMe,@Doi,@DiaChi,@TinhTrang_HonNhan,@HoTenVC,@HocVan,@TrangThai,@NoiAnTang," +
                    $"@Ngay_Mat,@NgheNghiep,@ThanhTuu)");

                sqlCommand.Parameters.AddWithValue("@HoTen", HoTen);
                sqlCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                sqlCommand.Parameters.AddWithValue("@GioiTinhBit", GioiTinhBit);
                sqlCommand.Parameters.AddWithValue("@HotenCha", HotenCha);
                sqlCommand.Parameters.AddWithValue("@HoTenMe", HoTenMe);
                sqlCommand.Parameters.AddWithValue("@Doi", Doi);
                sqlCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                sqlCommand.Parameters.AddWithValue("@Ngay_Mat", NgayMat);
                sqlCommand.Parameters.AddWithValue("@TrangThai", TrangThaiBit);
                sqlCommand.Parameters.AddWithValue("@NoiAnTang", NoiAnTang);
                sqlCommand.Parameters.AddWithValue("@NgheNghiep", NgheNghiep);
                if (ThanhTuu != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@ThanhTuu", ThanhTuu);
                else
                    sqlCommand.Parameters.AddWithValue("@ThanhTuu", DBNull.Value);


                sqlCommand.Parameters.AddWithValue("@TinhTrang_HonNhan", TinhTrang_HonNhan);
                if (HoTenVC != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@HoTenVC", HoTenVC);
                else
                    sqlCommand.Parameters.AddWithValue("@HoTenVC", DBNull.Value);

                if (HocVan != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@HocVan", HocVan);
                else
                    sqlCommand.Parameters.AddWithValue("@HocVan", DBNull.Value);
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
        public async Task<bool> SuaThanhVien(string MaThanhVien,string HoTen, DateTime NgaySinh, string HotenCha, string HoTenMe, string Doi, 
            byte GioiTinh, string DiaChi, byte TinhTrangHonNhan,string HoTenVC, string SDT, string HocVan, byte TrangThai,string NoiAnTang, 
            DateTime NgayMat,string NgheNghiep,string ThanhTuu)
        {
            byte GioiTinhBit = GioiTinh > 0 ? (byte)0 : (byte)1;
            byte TinhTrang_HonNhan = TinhTrangHonNhan > 0 ? (byte)1 : (byte)0;
            byte TrangThaiBit = TrangThai > 0 ? (byte)0 : (byte)1;



            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"UPDATE[ThanhVien] " +
                                                        "SET [Ten] = @HoTen, " +
                                                        "[NgaySinh] = @NgaySinh, " +
                                                        "[GioiTinh] = @GioiTinhBit, " +
                                                        "[HoTenCha] = @HoTenCha, " +
                                                        "[HoTenMe] = @HoTenMe, " +
                                                        "[Doi] = @Doi, " +
                                                        "[DiaChi] = @DiaChi, " +
                                                        "[TinhTrang_HonNhan] = @TinhTrang_HonNhan," +
                                                        "[HoTenVo/Chong] = @HoTenVC, " +
                                                        "[SDT] = @SDT, " +
                                                        "[HocVan] = @HocVan, " +
                                                        "[TrangThai] = @TrangThai, " +
                                                        "[Ngay_Mat] = @Ngay_Mat, " +
                                                        "[NoiAnTang]= @NoiAnTang, " +
                                                        "[NgheNghiep] = @NgheNghiep," +
                                                        "[ThanhTuu] = @ThanhTuu " +
                                                        "WHERE [MaThanhVien] = @MaThanhVien");
                sqlCommand.Parameters.AddWithValue("@MaThanhVien", MaThanhVien);
                sqlCommand.Parameters.AddWithValue("@HoTen", HoTen);
                sqlCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                sqlCommand.Parameters.AddWithValue("@GioiTinhBit", GioiTinhBit);
                sqlCommand.Parameters.AddWithValue("@HoTenCha", HotenCha);
                sqlCommand.Parameters.AddWithValue("@HoTenMe", HoTenMe);
                sqlCommand.Parameters.AddWithValue("@Doi", Doi);
                sqlCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                sqlCommand.Parameters.AddWithValue("@TrangThai", TrangThaiBit);
                sqlCommand.Parameters.AddWithValue("@NgheNghiep", NgheNghiep);

                if (ThanhTuu != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@ThanhTuu", ThanhTuu);
                else
                    sqlCommand.Parameters.AddWithValue("@ThanhTuu", DBNull.Value);


                sqlCommand.Parameters.AddWithValue("@TinhTrang_HonNhan", TinhTrang_HonNhan);
                
                if(HoTenVC != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@HoTenVC", HoTenVC);
                else
                    sqlCommand.Parameters.AddWithValue("@HoTenVC", DBNull.Value);
                if(SDT != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@SDT", SDT);
                else
                    sqlCommand.Parameters.AddWithValue("@SDT", DBNull.Value);
                if(HocVan != String.Empty) 
                    sqlCommand.Parameters.AddWithValue("@HocVan", HocVan);
                else
                    sqlCommand.Parameters.AddWithValue("@HocVan", DBNull.Value);
                if(NoiAnTang != String.Empty)
                    sqlCommand.Parameters.AddWithValue("@NoiAnTang", NoiAnTang);
                else
                    sqlCommand.Parameters.AddWithValue("@NoiAnTang", DBNull.Value);
                if(TrangThaiBit == 0)
                    sqlCommand.Parameters.AddWithValue("@Ngay_Mat", NgayMat);
                else
                    sqlCommand.Parameters.AddWithValue("@Ngay_Mat", DBNull.Value);

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
        public async Task<SqlDataReader> HienThiThanhVien(string id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();

                await OpenConAsync();

                sqlCommand.CommandText = String.Format($"SELECT A.*, B.Ten AS TenCha FROM ThanhVien A JOIN ThanhVien B ON A.MaCha = B.MaThanhVien " +
                                                    $"Where A.MaThanhVien = @id ");

                sqlCommand.Parameters.AddWithValue("@id", id);

                var row = await sqlCommand.ExecuteReaderAsync();

                if (row.HasRows)
                {
                    // Khi đọc thành công một dòng dữ liệu, trả về đối tượng SqlDataReader
                    return row;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlCommand.Dispose();
            }
            
        }
        public async Task<DataTable> DanhSachCha(string Doi)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"select MaThanhVien,Ten from ThanhVien where Doi = @Doi");
                sqlCommand.Parameters.AddWithValue("@Doi", Doi);

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
        public async Task<DataTable> DanhSachTen()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = Connect();
                await OpenConAsync();

                sqlCommand.CommandText = string.Format($"select MaThanhVien,Ten from ThanhVien");

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
        public async Task<string> TatCaThanhVien()
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = Connect();
                await OpenConAsync();

                myCommand.CommandText = String.Format($"SELECT COUNT(*) AS TotalMembers FROM ThanhVien;");

                var row = await myCommand.ExecuteScalarAsync();
                if (row != null)
                    return row.ToString();
                return null;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnect();
                myCommand.Dispose();
            }
        }

    }
}
