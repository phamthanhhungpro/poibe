﻿using Microsoft.Extensions.DependencyInjection;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Service;
using System.Reflection;

namespace Poi.Hrm.Logic
{
    public static class ServiceRegister
    {
        public static void AddLogic(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IHoSoNhanSuService, HoSoNhanSuService>();
            services.AddScoped<IVaiTroService, VaiTroService>();
            services.AddScoped<IViTriCongViecService, ViTriCongViecService>();
            services.AddScoped<IThamSoLuongService, ThamSoLuongService>();
            services.AddScoped<ICongThucLuongService, CongThucLuongService>();
            services.AddScoped<ITrangThaiChamCongService, TrangThaiChamCongService>();
            services.AddScoped<ICongKhaiBaoService, CongKhaiBaoService>();
            services.AddScoped<IChamCongDiemDanhService, ChamCongDiemDanhService>();
            services.AddScoped<IGiaiTrinhChamCongService, GiaiTrinhChamCongService>();
            services.AddScoped<IFaceRecognitionService, FaceRecognitionService>();
            services.AddScoped<IChucNangService, ChucNangService>();
            services.AddScoped<INhomChucNangService, NhomChucNangService>();
        }
    }
}
