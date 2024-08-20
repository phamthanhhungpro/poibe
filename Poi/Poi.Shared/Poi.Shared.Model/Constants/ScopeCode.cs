using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Shared.Model.Constants
{
    public static class ScopeCode
    {
        public const string DANHSACH_CONGVIEC_ALL = "ALL";
        public const string DANHSACH_CONGVIEC_DUAN = "ALL-DU-AN";
        public const string DANHSACH_CONGVIEC_RELATED = "RELATED";

        public const string THEM_CONGVIEC_KCANXDINH_DUAN = "NO-NEED-DU-AN";
        public const string THEM_CONGVIEC_AUTODUYET = "AUTO-APPROVE";

        public const string DUYET_CONGVIEC_ALL = "ALL";
        public const string DUYET_CONGVIEC_DUAN = "ALL-DU-AN";

        public const string XOA_CONGVIEC_ALL = "ALL";
        public const string XOA_CONGVIEC_DUAN = "ALL-DU-AN";
        public const string XOA_CONGVIEC_CREATED = "ONLY-CREATED";
        public const string XOA_CONGVIEC_NOALLOW = "NO-ALLOW";

        public const string GIAHAN_CONGVIEC_ALLOW = "ALLOW-GIA-HAN";
        public const string GIAHAN_CONGVIEC_AUTO = "AUTO-APPROVE";

        public const string DANHGIA_CONGVIEC_ALL = "ALL";
        public const string DANHGIA_CONGVIEC_DUAN = "ALL-DU-AN";
        public const string DANHGIA_CONGVIEC_LEADER = "ONLY-LEADER";

        public const string DANHSACH_DUAN_ALL = "ALL";
        public const string DANHSACH_DUAN_DUAN = "ALL-DU-AN";
        public const string DANHSACH_DUAN_RELATED = "RELATED";

        public const string XOA_DUAN_ALL = "ALL";
        public const string XOA_DUAN_DUAN = "ALL-DU-AN";
        public const string XOA_DUAN_CREATED = "ONLY-CREATED";

        public const string HOATDONG_DUAN_ALL = "ALL";
        public const string HOATDONG_DUAN_RELATED = "RELATED";

        public const string EDIT_DUAN_ALL = "ALL";
        public const string EDIT_DUAN_PHONGBAN = "ALL-PHONGBAN";

        public const string LSCC_PHONGBAN = "LSCC-PHONGBAN";
        public const string LSCC_ALL= "LSCC-ALL";
        public const string XNCC_PHONGBAN = "XNCC-PHONGBAN";
        public const string XNCC_ALL = "XNCC-ALL";


    }
}
