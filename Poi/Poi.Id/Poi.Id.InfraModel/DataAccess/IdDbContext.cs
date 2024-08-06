using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Id.InfraModel.DataAccess.Prj;
using System.Text.Json;

namespace Poi.Id.InfraModel.DataAccess
{
    public class IdDbContext : IdentityDbContext<User, Role, Guid>
    {
        public IdDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<CoQuanDonVi> CoQuanDonVis { get; set; }
        public DbSet<ChiNhanhVanPhong> ChiNhanhVanPhongs { get; set; }
        public DbSet<PhongBanBoPhan> PhongBanBoPhans { get; set; }
        public DbSet<AFeedback> AFeedbacks { get; set; }
        public DbSet<TokenExpired> TokenExpired { get; set; }
        public DbSet<PerEndpoint> PerEndpoint { get; set; }
        public DbSet<PerFunction> PerFunction { get; set; }
        public DbSet<PerGroupFunction> PerGroupFunction { get; set; }
        public DbSet<PerRole> PerRole { get; set; }
        public DbSet<PerScope> PerScope { get; set; }
        public DbSet<PerRoleFunctionScope> PerRoleFunctionScope { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<int>("HoSoNhanSuSeq")
                        .StartsAt(1)
                        .IncrementsBy(1)
                        .HasMin(1)
                        .HasMax(int.MaxValue)
                        .IsCyclic(false);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var converter = new ValueConverter<ThongTinNhanSu, string>(
                v => JsonSerializer.Serialize(v, options),
                v => JsonSerializer.Deserialize<ThongTinNhanSu>(v, options));

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<App>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });


            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<CoQuanDonVi>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<ChiNhanhVanPhong>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PhongBanBoPhan>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PhongBanBoPhan>()
                .HasMany(tn => tn.ThanhVien)
                .WithMany(u => u.ThanhVienPhongBan)
                .UsingEntity(j => j.ToTable("PhongBanThanhVien"));

            modelBuilder.Entity<PhongBanBoPhan>()
                .HasMany(tn => tn.QuanLy)
                .WithMany(u => u.LanhDaoPhongBan)
                .UsingEntity(j => j.ToTable("PhongBanLanhDao"));

            modelBuilder.Entity<HrmHoSoNhanSu>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.ThongTinThem)
                            .HasConversion(converter)
                            .HasColumnType("jsonb");

                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmKhuVucChuyenMon>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmPhanLoaiNhanSu>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmViTriCongViec>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmVaiTro>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmThamSoLuong>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmCongThucLuong>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmTrangThaiChamCong>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmCongKhaiBao>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmChamCongDiemDanh>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
                entity.Property(e => e.TrangThai)
                     .HasConversion(
                         v => v.ToString(), // Convert Enum to string for storage
                         v => (TrangThaiEnum)Enum.Parse(typeof(TrangThaiEnum), v)); // Convert string to Enum for use
            });

            modelBuilder.Entity<HrmDiemDanhHistory>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmGiaiTrinhChamCong>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmNhomChucNang>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<HrmChucNang>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // PRJ data
            modelBuilder.Entity<PrjCongViec>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.TrangThaiChiTiet).HasDefaultValue("READY");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjNhomCongViec>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjDuAnNvChuyenMon>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.IsCaNhan).HasDefaultValue(false);
                entity.Property(e => e.IsClosed).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjDuAnNvChuyenMon>()
                .HasMany(d => d.ThanhVienDuAn)
                .WithMany(u => u.PrjDuAnNvChuyenMon)
                .UsingEntity(j => j.ToTable("PrjDuAnChuyenMonThanhVienDuAn"));

            modelBuilder.Entity<PrjDuAnNvChuyenMon>()
                .HasOne(d => d.QuanLyDuAn)
                .WithMany()
                .HasForeignKey(d => d.QuanLyDuAnId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PrjLinhVuc>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjToNhom>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjToNhom>()
                .HasMany(tn => tn.ThanhVien)
                .WithMany(u => u.ThanhVienToNhom)
                .UsingEntity(j => j.ToTable("PrjToNhomThanhVien"));

            modelBuilder.Entity<PrjToNhom>()
                .HasMany(tn => tn.LanhDao)
                .WithMany(u => u.LanhDaoToNhom)
                .UsingEntity(j => j.ToTable("PrjToNhomLanhDao"));

            modelBuilder.Entity<PrjLoaiCongViec>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjTagCongViec>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjTagComment>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjComment>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjDuAnSetting>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.JsonValue).HasColumnType("jsonb");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PrjCongViec>()
                .HasMany(d => d.NguoiPhoiHop)
                .WithMany()
                .UsingEntity(j => j.ToTable("PrjCongViecNguoiPhoiHop"));

            modelBuilder.Entity<PrjCongViec>()
                .HasMany(d => d.NguoiThucHien)
                .WithMany()
                .UsingEntity(j => j.ToTable("PrjCongViecNguoiThucHien"));

            modelBuilder.Entity<PrjKanban>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.YeuCauXacNhan).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<AFeedback>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<TokenExpired>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PerEndpoint>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PerFunction>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PerFunction>()
                .HasMany(d => d.Endpoints)
                .WithMany()
                .UsingEntity(j => j.ToTable("PerFunctionEndPoint"));

            modelBuilder.Entity<PerGroupFunction>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PerRole>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PerScope>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PerRoleFunctionScope>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PerRoleFunctionScope>()
                .HasKey(rf => new { rf.PerRoleId, rf.PerFunctionId });

            modelBuilder.Entity<PerRoleFunctionScope>()
                .HasOne(rf => rf.Role)
                .WithMany(r => r.PerRoleFunctionScope)
                .HasForeignKey(rf => rf.PerRoleId);

            modelBuilder.Entity<PerRoleFunctionScope>()
                .HasOne(rf => rf.Function)
                .WithMany(f => f.PerRoleFunctionScope)
                .HasForeignKey(rf => rf.PerFunctionId);

            modelBuilder.Entity<PerRoleFunctionScope>()
                .HasOne(rf => rf.Scope)
                .WithMany()
                .HasForeignKey(rf => rf.PerScopeId);

            modelBuilder.Entity<PrjHoatDong>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });
        }
    }
}
