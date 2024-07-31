using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Shared.Model.Helpers
{
    public static class TrangThaiCongViecHelper
    {
        public static string GetTrangThaiCongViec(int trangThai)
        {
            switch (trangThai)
            {
                case 0:
                    return "Chưa bắt đầu";
                case 1:
                    return "Đang thực hiện";
                case 2:
                    return "Đã hoàn thành";
                case 3:
                    return "Đã xác nhận";
                default:
                    return "Không xác định";
            }
        }

        public const string DefaultTrangThaiKey = "chua-xac-dinh";
        public const string DefaultTrangThaiValue = "Chưa xác định";
        public const string TrangThaiSettingKey = "trangThaiSetting";
        public const string TrangThaiHoanThanh = "hoan-thanh";
    }
}
