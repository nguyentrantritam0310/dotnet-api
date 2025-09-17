using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace dotnet_api.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Configure enum conversions
            modelBuilder.Entity<MaterialType>()
                .Property(m => m.MaterialTypeName)
                .HasConversion<string>();

            modelBuilder.Entity<ConstructionStatus>()
                .Property(m => m.Name)
                .HasConversion<string>();

            modelBuilder.Entity<ReportStatusLog>()
                .Property(m => m.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ImportOrder>()
                .Property(m => m.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ImportOrderEmployee>()
                .Property(m => m.Role)
                .HasConversion<string>();

            modelBuilder.Entity<ConstructionType>()
                .Property(m => m.ConstructionTypeName)
                .HasConversion<string>();

            modelBuilder.Entity<ApplicationUser>()
                .Property(m => m.Status)
                .HasConversion<string>();

            // Seed data for Role
            modelBuilder.Entity<Role>().HasData(
                new Role { ID = 1, RoleName = "Nhân viên kỹ thuật" },
                new Role { ID = 2, RoleName = "Chỉ huy công trình" },
                new Role { ID = 3, RoleName = "Giám đốc" },
                new Role { ID = 4, RoleName = "Thợ" }
            );

            // Seed Identity Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "technician", NormalizedName = "TECHNICIAN" },
                new IdentityRole { Id = "2", Name = "manager", NormalizedName = "MANAGER" },
                new IdentityRole { Id = "3", Name = "director", NormalizedName = "DIRECTOR" },
                new IdentityRole { Id = "4", Name = "employee", NormalizedName = "EMPLOYEE" }
            );

            // Seed Application Users
            var hasher = new PasswordHasher<ApplicationUser>();

            // Admin user
            var adminUser = new ApplicationUser
            {
                Id = "admin-id",
                UserName = "giamdoc@company.com",
                NormalizedUserName = "GIAMDOC@COMPANY.COM",
                Email = "giamdoc@company.com",
                NormalizedEmail = "GIAMDOC@COMPANY.COM",
                EmailConfirmed = true,
                FirstName = "Phạm",
                LastName = "Văn Đốc",
                RoleID = 3,
                Phone = "0901234567",
                Status = EmployeeStatusEnum.Active,
                SecurityStamp = Guid.Parse("427bafc5-bab1-42e5-a5d2-5974daf31890").ToString()
            };
            //giamdoc@123
            var password = "giamdoc1@123";
            //adminUser.PasswordHash = "AQAAAAIAAYagAAAAEMdV6GUkfs6qwgt02YnxYDhvTinyv50xpvMUpXyuO9m3sGtqIVUTHtZPUp1rJiRVow==";
            adminUser.PasswordHash = hasher.HashPassword(adminUser, password); ;

            // Add static DateTime values for RefreshTokenExpiryTime
            adminUser.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);

            // Manager users
            var manager1User = new ApplicationUser
            {
                Id = "manager1-id",
                UserName = "chihuy1@company.com",
                NormalizedUserName = "CHIHUY1@COMPANY.COM",
                Email = "chihuy1@company.com",
                NormalizedEmail = "CHIHUY1@COMPANY.COM",
                EmailConfirmed = true,
                FirstName = "Nguyễn",
                LastName = "Chỉ Huy",
                RoleID = 2,
                Phone = "0912345678",
                Status = EmployeeStatusEnum.Active,
                SecurityStamp = Guid.Parse("227cd1d8-ed74-4f96-9851-ee15b48f8cf2").ToString()
            };
            manager1User.PasswordHash = "AQAAAAIAAYagAAAAELES7SRaXmuGGtS6MEV0kzUq5SDOWE6ecydmGrGSbAOdCl60MK87guvf2UERMAi9zg==";

            // Add static DateTime values for RefreshTokenExpiryTime
            manager1User.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);

            var manager2User = new ApplicationUser
            {
                Id = "manager2-id",
                UserName = "chihuy2@company.com",
                NormalizedUserName = "CHIHUY2@COMPANY.COM",
                Email = "chihuy2@company.com",
                NormalizedEmail = "CHIHUY2@COMPANY.COM",
                EmailConfirmed = true,
                FirstName = "Trần",
                LastName = "Công Trình",
                RoleID = 2,
                Phone = "0923456789",
                Status = EmployeeStatusEnum.Active,
                SecurityStamp = Guid.Parse("f6ab69f9-8a13-4414-81bd-77603126dc4d").ToString()
            };

            manager2User.PasswordHash = "AQAAAAIAAYagAAAAELES7SRaXmuGGtS6MEV0kzUq5SDOWE6ecydmGrGSbAOdCl60MK87guvf2UERMAi9zg==";

            // Add static DateTime values for RefreshTokenExpiryTime
            manager2User.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);

            var manager3User = new ApplicationUser
            {
                Id = "manager3-id",
                UserName = "chihuy3@company.com",
                NormalizedUserName = "CHIHUY3@COMPANY.COM",
                Email = "chihuy3@company.com",
                NormalizedEmail = "CHIHUY3@COMPANY.COM",
                EmailConfirmed = true,
                FirstName = "Lê",
                LastName = "Xây Dựng",
                RoleID = 2,
                Phone = "0934567890",
                Status = EmployeeStatusEnum.Active,
                SecurityStamp = Guid.Parse("7a7a5d4b-8ebd-4d56-b8b3-fdf964a80a4e").ToString()
            };
            manager3User.PasswordHash = "AQAAAAIAAYagAAAAELES7SRaXmuGGtS6MEV0kzUq5SDOWE6ecydmGrGSbAOdCl60MK87guvf2UERMAi9zg==";

            // Add static DateTime values for RefreshTokenExpiryTime
            manager3User.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);

            // Technical staff users
            var tech1User = new ApplicationUser
            {
                Id = "tech1-id",
                UserName = "kythuat1@company.com",
                NormalizedUserName = "KYTHUAT1@COMPANY.COM",
                Email = "kythuat1@company.com",
                NormalizedEmail = "KYTHUAT1@COMPANY.COM",
                EmailConfirmed = true,
                FirstName = "Hoàng",
                LastName = "Kỹ Thuật",
                RoleID = 1,
                Phone = "0945678901",
                Status = EmployeeStatusEnum.Active,
                SecurityStamp = Guid.Parse("6ffe7424-9000-41e1-871b-e744671266a6").ToString()
            };
            tech1User.PasswordHash = "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==";

            // Add static DateTime values for RefreshTokenExpiryTime
            tech1User.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);

            var tech2User = new ApplicationUser
            {
                Id = "tech2-id",
                UserName = "kythuat2@company.com",
                NormalizedUserName = "KYTHUAT2@COMPANY.COM",
                Email = "kythuat2@company.com",
                NormalizedEmail = "KYTHUAT2@COMPANY.COM",
                EmailConfirmed = true,
                FirstName = "Phan",
                LastName = "Thiết Kế",
                RoleID = 1,
                Phone = "0956789012",
                Status = EmployeeStatusEnum.Active,
                SecurityStamp = Guid.Parse("18d4d1bc-78ea-445f-a362-d544e9ea1a9d").ToString()
            };
            tech2User.PasswordHash = "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==";

            // Add static DateTime values for RefreshTokenExpiryTime
            tech2User.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);

            var tech3User = new ApplicationUser
            {
                Id = "tech3-id",
                UserName = "kythuat3@company.com",
                NormalizedUserName = "KYTHUAT3@COMPANY.COM",
                Email = "kythuat3@company.com",
                NormalizedEmail = "KYTHUAT3@COMPANY.COM",
                EmailConfirmed = true,
                FirstName = "Vũ",
                LastName = "Vận Hành",
                RoleID = 1,
                Phone = "0967890123",
                Status = EmployeeStatusEnum.Active,
                SecurityStamp = Guid.Parse("5f694007-36b9-4dbe-8a38-196c05e82cb2").ToString()
            };
            tech3User.PasswordHash = "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==";

            // Add static DateTime values for RefreshTokenExpiryTime
            tech3User.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);

            // Worker users
            var workers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "worker1-id",
                    UserName = "tho1@company.com",
                    NormalizedUserName = "THO1@COMPANY.COM",
                    Email = "tho1@company.com",
                    NormalizedEmail = "THO1@COMPANY.COM",
                    EmailConfirmed = true,
                    FirstName = "Đinh",
                    LastName = "Văn Thợ",
                    RoleID = 4,
                    Phone = "0978901234",
                    Status = EmployeeStatusEnum.Active,
                    SecurityStamp = Guid.Parse("2180abeb-5218-4df0-b695-2be63b8b8cd1").ToString()
                },
                new ApplicationUser
                {
                    Id = "worker2-id",
                    UserName = "tho2@company.com",
                    NormalizedUserName = "THO2@COMPANY.COM",
                    Email = "tho2@company.com",
                    NormalizedEmail = "THO2@COMPANY.COM",
                    EmailConfirmed = true,
                    FirstName = "Mai",
                    LastName = "Thị Hàn",
                    RoleID = 4,
                    Phone = "0989012345",
                    Status = EmployeeStatusEnum.Active,
                    SecurityStamp = Guid.Parse("eba6051e-c6ac-4847-baca-1f886c385128").ToString()
                },
                new ApplicationUser
                {
                    Id = "worker3-id",
                    UserName = "tho3@company.com",
                    NormalizedUserName = "THO3@COMPANY.COM",
                    Email = "tho3@company.com",
                    NormalizedEmail = "THO3@COMPANY.COM",
                    EmailConfirmed = true,
                    FirstName = "Lý",
                    LastName = "Văn Xây",
                    RoleID = 4,
                    Phone = "0990123456",
                    Status = EmployeeStatusEnum.Active,
                    SecurityStamp = Guid.Parse("4933e54c-c4fd-4875-9508-edc952ae52e3").ToString()
                },
                new ApplicationUser
                {
                    Id = "worker4-id",
                    UserName = "tho4@company.com",
                    NormalizedUserName = "THO4@COMPANY.COM",
                    Email = "tho4@company.com",
                    NormalizedEmail = "THO4@COMPANY.COM",
                    EmailConfirmed = true,
                    FirstName = "Trịnh",
                    LastName = "Công Mộc",
                    RoleID = 4,
                    Phone = "0911223344",
                    Status = EmployeeStatusEnum.Active,
                    SecurityStamp = Guid.Parse("f9757b33-c60a-4995-b62f-d0c899b42c68").ToString()
                },
                new ApplicationUser
                {
                    Id = "worker5-id",
                    UserName = "tho5@company.com",
                    NormalizedUserName = "THO5@COMPANY.COM",
                    Email = "tho5@company.com",
                    NormalizedEmail = "THO5@COMPANY.COM",
                    EmailConfirmed = true,
                    FirstName = "Võ",
                    LastName = "Thị Sơn",
                    RoleID = 4,
                    Phone = "0922334455",
                    Status = EmployeeStatusEnum.Active,
                    SecurityStamp = Guid.Parse("a1b2c3d4-e5f6-4a5b-8c7d-9e0f1a2b3c4d").ToString()
                }
            };

            // Add static DateTime values for RefreshTokenExpiryTime
            foreach (var worker in workers)
            {
                worker.RefreshTokenExpiryTime = new DateTime(2025, 1, 1);
            }

            // Add all users to the database
            modelBuilder.Entity<ApplicationUser>().HasData(
                adminUser,
                manager1User,
                manager2User,
                manager3User,
                tech1User,
                tech2User,
                tech3User
            );

            modelBuilder.Entity<ApplicationUser>().HasData(workers);

            // Gán Role cho User qua IdentityUserRole
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                // Giám đốc
                new IdentityUserRole<string> { UserId = "admin-id", RoleId = "3" },

                // Chỉ huy công trình
                new IdentityUserRole<string> { UserId = "manager1-id", RoleId = "2" },
                new IdentityUserRole<string> { UserId = "manager2-id", RoleId = "2" },
                new IdentityUserRole<string> { UserId = "manager3-id", RoleId = "2" },

                // Nhân viên kỹ thuật
                new IdentityUserRole<string> { UserId = "tech1-id", RoleId = "1" },
                new IdentityUserRole<string> { UserId = "tech2-id", RoleId = "1" },
                new IdentityUserRole<string> { UserId = "tech3-id", RoleId = "1" },

                // Thợ
                new IdentityUserRole<string> { UserId = "worker1-id", RoleId = "4" },
                new IdentityUserRole<string> { UserId = "worker2-id", RoleId = "4" },
                new IdentityUserRole<string> { UserId = "worker3-id", RoleId = "4" },
                new IdentityUserRole<string> { UserId = "worker4-id", RoleId = "4" },
                new IdentityUserRole<string> { UserId = "worker5-id", RoleId = "4" }


                );

            // Seed data for ConstructionType
            modelBuilder.Entity<ConstructionType>().HasData(
                new ConstructionType { ID = 1, ConstructionTypeName = ConstructionTypeEnum.House },
                new ConstructionType { ID = 2, ConstructionTypeName = ConstructionTypeEnum.RoadBridge },
                new ConstructionType { ID = 3, ConstructionTypeName = ConstructionTypeEnum.Industrial },
                new ConstructionType { ID = 4, ConstructionTypeName = ConstructionTypeEnum.Irrigation }
            );

            // Seed data for MaterialType
            modelBuilder.Entity<MaterialType>().HasData(
                new MaterialType { ID = 1, MaterialTypeName = MaterialTypeEnum.BasicBuildingMaterial },
                new MaterialType { ID = 2, MaterialTypeName = MaterialTypeEnum.FinishingMaterial },
                new MaterialType { ID = 3, MaterialTypeName = MaterialTypeEnum.ElectricalWaterSystem },
                new MaterialType { ID = 4, MaterialTypeName = MaterialTypeEnum.MechanicalStructure },
                new MaterialType { ID = 5, MaterialTypeName = MaterialTypeEnum.SupportingMaterial }
            );

            // Seed data for ConstructionStatus
            modelBuilder.Entity<ConstructionStatus>().HasData(
                new ConstructionStatus { ID = 1, Name = ConstructionStatusEnum.Pending },
                new ConstructionStatus { ID = 2, Name = ConstructionStatusEnum.InProgress },
                new ConstructionStatus { ID = 3, Name = ConstructionStatusEnum.Paused },
                new ConstructionStatus { ID = 4, Name = ConstructionStatusEnum.Completed },
                new ConstructionStatus { ID = 5, Name = ConstructionStatusEnum.Cancelled }
            );

            // Seed data for UnitofMeasurement
            modelBuilder.Entity<UnitofMeasurement>().HasData(
            // Chiều dài
            new UnitofMeasurement { ID = 1, UnitName = "mét", ShortName = "m", Category = "Chiều dài" },
            new UnitofMeasurement { ID = 2, UnitName = "centimet", ShortName = "cm", Category = "Chiều dài" },
            new UnitofMeasurement { ID = 3, UnitName = "kilomet", ShortName = "km", Category = "Chiều dài" },

            // Khối lượng
            new UnitofMeasurement { ID = 4, UnitName = "kilogram", ShortName = "kg", Category = "Khối lượng" },
            new UnitofMeasurement { ID = 5, UnitName = "tấn", ShortName = "t", Category = "Khối lượng" },

            // Thể tích
            new UnitofMeasurement { ID = 6, UnitName = "mét khối", ShortName = "m³", Category = "Thể tích" },
            new UnitofMeasurement { ID = 7, UnitName = "lít", ShortName = "l", Category = "Thể tích" },

            // Diện tích
            new UnitofMeasurement { ID = 8, UnitName = "mét vuông", ShortName = "m²", Category = "Diện tích" },
            new UnitofMeasurement { ID = 9, UnitName = "hecta", ShortName = "ha", Category = "Diện tích" },

            // Vật liệu xây dựng
            new UnitofMeasurement { ID = 10, UnitName = "bao", ShortName = "bao", Category = "Vật liệu" },
            new UnitofMeasurement { ID = 11, UnitName = "thanh", ShortName = "thanh", Category = "Vật liệu" },
            new UnitofMeasurement { ID = 12, UnitName = "viên", ShortName = "viên", Category = "Vật liệu" },
            new UnitofMeasurement { ID = 13, UnitName = "cái", ShortName = "cái", Category = "Vật liệu" },
            new UnitofMeasurement { ID = 14, UnitName = "bộ", ShortName = "bộ", Category = "Vật liệu" } // Thêm đơn vị "bộ"
            );

            // Seed data for Material
            modelBuilder.Entity<Material>().HasData(
                 // 1. Vật liệu xây dựng cơ bản
                 new Material
                 {
                     ID = 1,
                     MaterialName = "Xi măng Portland PC40",
                     UnitOfMeasurementID = 2, // Bao
                     StockQuantity = 500,
                     UnitPrice = 90000,
                     Specification = "Mác PCB40, độ dẻo cao, thời gian đông kết 3-4 giờ",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng cho bê tông móng, cột, dầm"
                 },
                 new Material
                 {
                     ID = 2,
                     MaterialName = "Xi măng trắng",
                     UnitOfMeasurementID = 2, // Bao
                     StockQuantity = 120,
                     UnitPrice = 140000,
                     Specification = "Độ trắng >85%, cường độ 35MPa",
                     Status = "Còn ít",
                     MaterialTypeID = 1,
                     Note = "Dùng cho ốp lát, trang trí"
                 },
                 new Material
                 {
                     ID = 3,
                     MaterialName = "Cát vàng xây dựng",
                     UnitOfMeasurementID = 4, // m3
                     StockQuantity = 300,
                     UnitPrice = 450000,
                     Specification = "Module độ lớn 2.0-3.3, cỡ hạt 0.5-2mm",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Không lẫn tạp chất, độ sạch >95%"
                 },
                 new Material
                 {
                     ID = 4,
                     MaterialName = "Cát mịn xây tô",
                     UnitOfMeasurementID = 4, // m3
                     StockQuantity = 120,
                     UnitPrice = 350000,
                     Specification = "Module độ lớn 1.5-2.0, cỡ hạt 0.15-0.5mm",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng cho công tác xây, trát tường"
                 },
                 new Material
                 {
                     ID = 5,
                     MaterialName = "Đá 1x2",
                     UnitOfMeasurementID = 4, // m3
                     StockQuantity = 300,
                     UnitPrice = 320000,
                     Specification = "Kích thước 10x20mm, cường độ >100MPa",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng cho bê tông móng, cọc"
                 },
                 new Material
                 {
                     ID = 6,
                     MaterialName = "Đá mi sàng",
                     UnitOfMeasurementID = 4, // m3
                     StockQuantity = 200,
                     UnitPrice = 280000,
                     Specification = "Kích thước 0-5mm, độ ẩm <2%",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng làm lớp nền, san lấp"
                 },
                 new Material
                 {
                     ID = 7,
                     MaterialName = "Gạch ống 4 lỗ",
                     UnitOfMeasurementID = 3, // Viên
                     StockQuantity = 50000,
                     UnitPrice = 2000,
                     Specification = "Cường độ M75, độ hút nước <16%",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng xây tường bao"
                 },
                 new Material
                 {
                     ID = 8,
                     MaterialName = "Gạch ba banh",
                     UnitOfMeasurementID = 3, // Viên
                     StockQuantity = 30000,
                     UnitPrice = 8500,
                     Specification = "Cường độ M100, khối lượng 14kg/viên",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng xây tường chịu lực"
                 },
                 new Material
                 {
                     ID = 9,
                     MaterialName = "Thép phi 6",
                     UnitOfMeasurementID = 1, // Kg
                     StockQuantity = 3000,
                     UnitPrice = 25000,
                     Specification = "Giới hạn chảy 295MPa, độ dãn dài >18%",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng làm thép đai, gia cố"
                 },
                 new Material
                 {
                     ID = 10,
                     MaterialName = "Thép phi 14",
                     UnitOfMeasurementID = 1, // Kg
                     StockQuantity = 5000,
                     UnitPrice = 22000,
                     Specification = "Giới hạn chảy 400MPa, chiều dài 11.7m/cây",
                     Status = "Còn hàng",
                     MaterialTypeID = 1,
                     Note = "Dùng làm thép dầm, cột"
                 },
                 new Material
                 {
                     ID = 11,
                     MaterialName = "Bê tông thương phẩm 30MPa",
                     UnitOfMeasurementID = 4, // m3
                     StockQuantity = 0,
                     UnitPrice = 1650000,
                     Specification = "Cấp phối PC40-Đá 1x2-Cát vàng, độ sụt 10±2cm",
                     Status = "Đặt hàng trước 24h",
                     MaterialTypeID = 1,
                     Note = "Dùng cho sàn, dầm, cột"
                 },
                 new Material
                 {
                     ID = 12,
                     MaterialName = "Bê tông thương phẩm 40MPa",
                     UnitOfMeasurementID = 4, // m3
                     StockQuantity = 0,
                     UnitPrice = 1850000,
                     Specification = "Cấp phối PC50-Đá 1x2-Cát vàng, độ sụt 8±2cm",
                     Status = "Đặt hàng trước 24h",
                     MaterialTypeID = 1,
                     Note = "Dùng cho móng nhà cao tầng"
                 },
                 // 2. Vật tư hoàn thiện
                 new Material
                 {
                     ID = 13,
                     MaterialName = "Sơn lót nội thất cao cấp",
                     UnitOfMeasurementID = 9, // Thùng 18L
                     StockQuantity = 50,
                     UnitPrice = 1350000,
                     Specification = "Độ phủ 8-10m²/lít (2 lớp), thời gian khô 2h",
                     Status = "Còn hàng",
                     MaterialTypeID = 2,
                     Note = "Dùng cho bề mặt bê tông, tường gạch"
                 },
                 new Material
                 {
                     ID = 14,
                     MaterialName = "Sơn hoàn thiện ngoại thất",
                     UnitOfMeasurementID = 9, // Thùng 18L
                     StockQuantity = 45,
                     UnitPrice = 1950000,
                     Specification = "Độ phủ 4-5m²/lít, chống UV, chống rêu mốc",
                     Status = "Còn hàng",
                     MaterialTypeID = 2,
                     Note = "Dùng cho sơn ngoại thất"
                 },
                 new Material
                 {
                     ID = 15,
                     MaterialName = "Gạch ceramic 60x60",
                     UnitOfMeasurementID = 11, // m2
                     StockQuantity = 500,
                     UnitPrice = 350000,
                     Specification = "Độ hút nước <3%, độ dày 9mm, chống trơn",
                     Status = "Còn hàng",
                     MaterialTypeID = 2,
                     Note = "Dùng lát sàn phòng khách"
                 },
                 new Material
                 {
                     ID = 16,
                     MaterialName = "Đá granite đen Phú Yên",
                     UnitOfMeasurementID = 11, // m2
                     StockQuantity = 70,
                     UnitPrice = 1400000,
                     Specification = "Độ dày 20mm, khả năng chịu lực >200MPa",
                     Status = "Nhập theo đơn đặt",
                     MaterialTypeID = 2,
                     Note = "Dùng lát mặt tiền, cầu thang"
                 },
                 new Material
                 {
                     ID = 17,
                     MaterialName = "Keo dán gạch cao cấp",
                     UnitOfMeasurementID = 2, // Bao
                     StockQuantity = 200,
                     UnitPrice = 170000,
                     Specification = "Độ bám dính >1.5MPa, thời gian chỉnh sửa 20 phút",
                     Status = "Còn hàng",
                     MaterialTypeID = 2,
                     Note = "Dùng cho gạch lớn >60cm"
                 },
                 new Material
                 {
                     ID = 18,
                     MaterialName = "Keo chà ron",
                     UnitOfMeasurementID = 2, // Bao
                     StockQuantity = 300,
                     UnitPrice = 120000,
                     Specification = "Độ trắng >85%, cường độ 45MPa",
                     Status = "Còn ít",
                     MaterialTypeID = 2,
                     Note = "Dùng chà ron gạch men"
                 },
                 new Material
                 {
                     ID = 19,
                     MaterialName = "Màng chống thấm Sika",
                     UnitOfMeasurementID = 10, // Cuộn
                     StockQuantity = 50,
                     UnitPrice = 1350000,
                     Specification = "Độ dày 3mm, kháng UV 2000h",
                     Status = "Còn hàng",
                     MaterialTypeID = 2,
                     Note = "Dán bằng đèn khò"
                 },
                 new Material
                 {
                     ID = 20,
                     MaterialName = "Sơn chống thấm Kova CT-11A",
                     UnitOfMeasurementID = 9, // Thùng 18L
                     StockQuantity = 35,
                     UnitPrice = 1820000,
                     Specification = "Độ phủ 1.5-2m²/lít, chịu được áp lực nước 3 bar",
                     Status = "Còn hàng",
                     MaterialTypeID = 2,
                     Note = "Phun 2 lớp cách nhau 6h"
                 },
                 // 3. Vật tư điện nước
                 new Material
                 {
                     ID = 21,
                     MaterialName = "Ống PPR 32mm",
                     UnitOfMeasurementID = 5, // Mét
                     StockQuantity = 2000,
                     UnitPrice = 42000,
                     Specification = "Áp lực làm việc 2.0MPa, chịu nhiệt 95°C",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Dùng cho hệ thống nước nóng lạnh"
                 },
                 new Material
                 {
                     ID = 22,
                     MaterialName = "Ống thoát nước PVC 90mm",
                     UnitOfMeasurementID = 5, // Mét
                     StockQuantity = 1500,
                     UnitPrice = 65000,
                     Specification = "Áp lực 10bar, chống tia UV, đường kính ngoài 90mm",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Dùng cho thoát nước thải"
                 },
                 new Material
                 {
                     ID = 23,
                     MaterialName = "Dây điện đơn 2.5mm² - Daphaco",
                     UnitOfMeasurementID = 5, // Mét
                     StockQuantity = 5000,
                     UnitPrice = 8000,
                     Specification = "Tiết diện 2.5mm², vỏ PVC chống cháy",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Dây đơn lõi đồng, dùng cho đèn"
                 },
                 new Material
                 {
                     ID = 24,
                     MaterialName = "Dây điện đơn 2.5mm² - Daphaco (dây nguội)",
                     UnitOfMeasurementID = 5, // Mét
                     StockQuantity = 4000,
                     UnitPrice = 8000,
                     Specification = "Tiết diện 2.5mm², vỏ PVC chống cháy",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Dây đơn lõi đồng, dùng cho ổ cắm"
                 },
                 new Material
                 {
                     ID = 25,
                     MaterialName = "Dây điện đơn 3.5mm² - Daphaco",
                     UnitOfMeasurementID = 5, // Mét
                     StockQuantity = 3000,
                     UnitPrice = 12000,
                     Specification = "Tiết diện 3.5mm², vỏ PVC chống cháy",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Dây đơn lõi đồng, dùng cho điều hòa"
                 },
                 new Material
                 {
                     ID = 26,
                     MaterialName = "Atomat 1 pha 40A",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 150,
                     UnitPrice = 98000,
                     Specification = "Dòng cắt 40A, điện áp 230V, tiêu chuẩn IEC 60898",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Lắp tủ điện chính"
                 },
                 new Material
                 {
                     ID = 27,
                     MaterialName = "Bóng đèn LED bulb 9W",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 300,
                     UnitPrice = 48000,
                     Specification = "Công suất 9W (~60W đèn sợi đốt), nhiệt độ màu 4000K",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Tuổi thọ 25,000 giờ"
                 },
                 new Material
                 {
                     ID = 28,
                     MaterialName = "Đèn LED pha 50W",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 50,
                     UnitPrice = 350000,
                     Specification = "IP67, quang thông 5000lm, góc chiếu 120°",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Chống nước, chống bụi"
                 },
                 new Material
                 {
                     ID = 29,
                     MaterialName = "Máy bơm nước Pentax",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 10,
                     UnitPrice = 3500000,
                     Specification = "Lưu lượng 2.4m³/h, cột áp 32m, điện 1 pha 220V",
                     Status = "Đặt hàng trước",
                     MaterialTypeID = 3,
                     Note = "Bơm tăng áp cho biệt thự"
                 },
                 new Material
                 {
                     ID = 30,
                     MaterialName = "Bình nóng lạnh Ariston",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 15,
                     UnitPrice = 3200000,
                     Specification = "Công suất 2500W, chống giật ELCB, inox 304",
                     Status = "Còn hàng",
                     MaterialTypeID = 3,
                     Note = "Lắp phòng tắm gia đình"
                 },
                 // 4. Vật tư cơ khí, kết cấu
                 new Material
                 {
                     ID = 31,
                     MaterialName = "Cốp pha nhôm định hình",
                     UnitOfMeasurementID = 11, // m2
                     StockQuantity = 2000,
                     UnitPrice = 950000,
                     Specification = "Độ dày 4mm, tải trọng 60kN/m², nhôm hợp kim 6061",
                     Status = "Còn hàng",
                     MaterialTypeID = 4,
                     Note = "Tái sử dụng 200 lần, chống dính"
                 },
                 new Material
                 {
                     ID = 32,
                     MaterialName = "Giàn giáo khung 1.7m x 1.2m",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 250,
                     UnitPrice = 1450000,
                     Specification = "Thép Q235, tải trọng 4.5 tấn/khung, mạ kẽm nhúng nóng",
                     Status = "Còn hàng",
                     MaterialTypeID = 4,
                     Note = "Kèm chéo giằng và mâm đứng"
                 },
                 new Material
                 {
                     ID = 33,
                     MaterialName = "Bulong neo M16x150mm",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 2000,
                     UnitPrice = 18000,
                     Specification = "Cấp bền 8.8, mạ kẽm điện phân, đầu hexagon",
                     Status = "Còn hàng",
                     MaterialTypeID = 4,
                     Note = "Dùng neo cột thép vào móng"
                 },
                 new Material
                 {
                     ID = 34,
                     MaterialName = "Vít khoan đuôi chuồn 12mm",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 15000,
                     UnitPrice = 1200,
                     Specification = "Thép carbon, phủ lớp zinc 15µm, 100 cái/hộp",
                     Status = "Còn hàng",
                     MaterialTypeID = 4,
                     Note = "Bắt tôn vào xà gồ"
                 },
                 new Material
                 {
                     ID = 35,
                     MaterialName = "Kính cường lực 10mm",
                     UnitOfMeasurementID = 11, // m2
                     StockQuantity = 100,
                     UnitPrice = 480000,
                     Specification = "Độ dày 10±0.2mm, chịu lực 90MPa, an toàn khi vỡ",
                     Status = "Cắt theo yêu cầu",
                     MaterialTypeID = 4,
                     Note = "Thời gian gia công 3-5 ngày"
                 },
                 new Material
                 {
                     ID = 36,
                     MaterialName = "Tấm inox 304 đánh bóng",
                     UnitOfMeasurementID = 11, // m2
                     StockQuantity = 45,
                     UnitPrice = 850000,
                     Specification = "Độ dày 1.5mm, kích thước 1.2x2.4m, bề mặt No.4",
                     Status = "Còn hàng",
                     MaterialTypeID = 4,
                     Note = "Dùng làm lan can, bếp công nghiệp"
                 },
                 new Material
                 {
                     ID = 37,
                     MaterialName = "Dây thép buộc 1.1mm",
                     UnitOfMeasurementID = 1, // Kg
                     StockQuantity = 500,
                     UnitPrice = 32000,
                     Specification = "Kẽm mạ đồng, độ bền kéo 400-500N/mm²",
                     Status = "Còn hàng",
                     MaterialTypeID = 4,
                     Note = "Buộc thép, cốt thép"
                 },
                 new Material
                 {
                     ID = 38,
                     MaterialName = "Thanh nhôm hộp 50x100mm",
                     UnitOfMeasurementID = 5, // Mét
                     StockQuantity = 300,
                     UnitPrice = 180000,
                     Specification = "Hợp kim 6063-T5, dài 6m/cây, tải trọng 150kg/m",
                     Status = "Còn hàng",
                     MaterialTypeID = 4,
                     Note = "Dựng vách ngăn, mặt dựng"
                 },
                 // 5. Vật tư phụ, an toàn
                 new Material
                 {
                     ID = 39,
                     MaterialName = "Mũ bảo hộ lao động",
                     UnitOfMeasurementID = 3, // Chiếc
                     StockQuantity = 200,
                     UnitPrice = 65000,
                     Specification = "Nhựa HDPE, chịu lực 5kg, quai cài 4 điểm",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Màu vàng, in logo công ty"
                 },
                 new Material
                 {
                     ID = 40,
                     MaterialName = "Giày bảo hộ lao động",
                     UnitOfMeasurementID = 7, // Đôi
                     StockQuantity = 50,
                     UnitPrice = 320000,
                     Specification = "Mũi thép chịu lực 200J, đế chống trượt SR",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Size 38-44, chống đinh đâm"
                 },
                 new Material
                 {
                     ID = 41,
                     MaterialName = "Keo silicone trung tính",
                     UnitOfMeasurementID = 12, // Tuýp
                     StockQuantity = 200,
                     UnitPrice = 75000,
                     Specification = "Độ giãn dài 400%, lực bám dính 1.5MPa",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Dán khe co giãn, chịu UV tốt"
                 },
                 new Material
                 {
                     ID = 42,
                     MaterialName = "Băng keo hai mặt 50mm",
                     UnitOfMeasurementID = 3, // Cuộn
                     StockQuantity = 100,
                     UnitPrice = 28000,
                     Specification = "Lực kéo 40N/10mm, chịu nhiệt 80°C",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Dán bạt, che phủ bề mặt"
                 },
                 new Material
                 {
                     ID = 43,
                     MaterialName = "Dây thừng PP 12mm",
                     UnitOfMeasurementID = 5, // Mét
                     StockQuantity = 1000,
                     UnitPrice = 9000,
                     Specification = "Chịu lực 150kg, kháng UV, chống mục",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Buộc vật liệu, giàn giáo"
                 },
                 new Material
                 {
                     ID = 44,
                     MaterialName = "Lưới an toàn HDPE",
                     UnitOfMeasurementID = 11, // m2
                     StockQuantity = 1000,
                     UnitPrice = 15000,
                     Specification = "Chỉ PE, mắt lưới 15mm, chống tia UV 95%",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Che chắn an toàn, chống rơi vãi"
                 },
                 new Material
                 {
                     ID = 45,
                     MaterialName = "Thùng rác công trình 120 lít",
                     UnitOfMeasurementID = 3, // Cái
                     StockQuantity = 30,
                     UnitPrice = 550000,
                     Specification = "Nhựa HDPE, bánh xe đẩy 360°, nắp kín",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Màu xanh lá, có nắp đậy"
                 },
                 new Material
                 {
                     ID = 46,
                     MaterialName = "Biển báo an toàn công trình",
                     UnitOfMeasurementID = 3, // Cái
                     StockQuantity = 50,
                     UnitPrice = 120000,
                     Specification = "Nhựa PVC dày 3mm, kích thước 30x40cm",
                     Status = "Còn hàng",
                     MaterialTypeID = 5,
                     Note = "Theo tiêu chuẩn ISO 7010"
                 },
                 new Material // Thêm Que hàn
                 {
                     ID = 47,
                     MaterialName = "Que hàn điện YAWATA-50",
                     UnitOfMeasurementID = 1, // Kg
                     StockQuantity = 500,
                     UnitPrice = 48000,
                     Specification = "Điện cực thép cacbon E6013, Φ3.2mm",
                     Status = "Còn hàng",
                     MaterialTypeID = 5, // Vật tư phụ trợ
                     Note = "Dùng cho hàn kết cấu thép"
                 },
                 new Material // Thêm Răng cào
                 {
                     ID = 48,
                     MaterialName = "Răng cào máy đào",
                     UnitOfMeasurementID = 3, // Cái
                     StockQuantity = 100,
                     UnitPrice = 650000,
                     Specification = "Hợp kim thép chịu mài mòn cao",
                     Status = "Còn hàng",
                     MaterialTypeID = 5, // Vật tư phụ trợ
                     Note = "Phụ tùng cho máy cào bóc"
                 }
             );




            // Seed data for Construction
            modelBuilder.Entity<Construction>().HasData(
                // Các công trình loại Dân dụng (Nhà ở)
                new Construction
                {
                    ID = 1,
                    ConstructionTypeID = 1, // Residential (Nhà ở)
                    ConstructionStatusID = 4, // Completed
                    ConstructionName = "Khu chung cư An Hòa Garden",
                    Location = "Số 10, đường Nguyễn Văn Cừ, thị trấn Tuy Phước, huyện Tuy Phước, Bình Định",
                    TotalArea = 1500.5f,
                    StartDate = new DateTime(2021, 04, 10),
                    ExpectedCompletionDate = new DateTime(2023, 01, 01),
                    ActualCompletionDate = new DateTime(2023, 01, 01),  // Đã hoàn thành
                    DesignBlueprint = "Design_AnHoa.pdf"
                },
                new Construction
                {
                    ID = 2,
                    ConstructionTypeID = 1, // Residential (Nhà ở)
                    ConstructionStatusID = 4, // Completed
                    ConstructionName = "Nhà ở dân dụng Phù Mỹ",
                    Location = "Khu phố 3, thị trấn Phù Mỹ, huyện Phù Mỹ, Bình Định",
                    TotalArea = 2500.0f,
                    StartDate = new DateTime(2021, 02, 20),
                    ExpectedCompletionDate = new DateTime(2023, 06, 15),
                    ActualCompletionDate = new DateTime(2023, 06, 10),  // Đã hoàn thành
                    DesignBlueprint = "Design_PhuyMy.pdf"
                },
                new Construction
                {
                    ID = 3,
                    ConstructionTypeID = 1, // Residential (Nhà ở)
                    ConstructionStatusID = 1, // Pending
                    ConstructionName = "Nhà ở dân dụng An Nhơn",
                    Location = "Số 50, đường Nguyễn Du, thị xã An Nhơn, Bình Định",
                    TotalArea = 1800.0f,
                    StartDate = new DateTime(2021, 05, 01),
                    ExpectedCompletionDate = new DateTime(2023, 03, 01),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    DesignBlueprint = "Design_BinhDinh.pdf"
                },

                // Công trình loại Cầu đường
                new Construction
                {
                    ID = 4,
                    ConstructionTypeID = 2, // RoadBridge
                    ConstructionStatusID = 2, // InProgress
                    ConstructionName = "Cầu An Hòa",
                    Location = "Km 12, Quốc lộ 1A, huyện Tuy Phước, tỉnh Bình Định",
                    TotalArea = 1500.5f,
                    StartDate = new DateTime(2021, 04, 10),
                    ExpectedCompletionDate = new DateTime(2023, 01, 01),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    DesignBlueprint = "Design_AnHoa.pdf"
                },

                new Construction
                {
                    ID = 5,
                    ConstructionTypeID = 2, // RoadBridge
                    ConstructionStatusID = 4, // Completed
                    ConstructionName = "Đường tránh QL1A - Phù Mỹ",
                    Location = "Đoạn từ Km 35 đến Km 50, Quốc lộ 1A, huyện Phù Mỹ, Bình Định",
                    TotalArea = 2500.0f,
                    StartDate = new DateTime(2021, 02, 20),
                    ExpectedCompletionDate = new DateTime(2023, 06, 15),
                    ActualCompletionDate = new DateTime(2023, 06, 10),  // Đã hoàn thành
                    DesignBlueprint = "Design_PhuyMy.pdf"
                },

                // Công trình loại Công nghiệp
                new Construction
                {
                    ID = 6,
                    ConstructionTypeID = 3, // Industrial
                    ConstructionStatusID = 3, // Paused
                    ConstructionName = "Nhà máy sản xuất thép An Phát",
                    Location = "Khu công nghiệp Long Mỹ, xã Long Mỹ, huyện Phù Cát, Bình Định",
                    TotalArea = 8000.0f,
                    StartDate = new DateTime(2020, 07, 15),
                    ExpectedCompletionDate = new DateTime(2022, 05, 20),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    DesignBlueprint = "Design_NhaMayThep.pdf"
                },

                // Công trình loại Thủy lợi
                new Construction
                {
                    ID = 7,
                    ConstructionTypeID = 4, // Irrigation
                    ConstructionStatusID = 5, // Cancelled
                    ConstructionName = "Đập thủy lợi Phú Tài",
                    Location = "Số 20, xã Phú Tài, thành phố Quy Nhơn, Bình Định",
                    TotalArea = 2000.0f,
                    StartDate = new DateTime(2020, 03, 10),
                    ExpectedCompletionDate = new DateTime(2022, 12, 25),
                    ActualCompletionDate = null,  // Đã hủy
                    DesignBlueprint = "Design_ThuyLoiPhuTai.pdf"
                }
            );


            // Seed data for ConstructionItem
            modelBuilder.Entity<ConstructionItem>().HasData(
                // Khu chung cư An Hòa Garden
                new ConstructionItem
                {
                    ID = 1,
                    ConstructionID = 1,
                    ConstructionItemName = "Thi công móng",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 500,
                    StartDate = new DateTime(2021, 04, 12),
                    ExpectedCompletionDate = new DateTime(2021, 05, 15),
                    ActualCompletionDate = new DateTime(2021, 05, 14),
                    ConstructionStatusID = 3, // Completed
                    WorkSubTypeVariantID = 3
                },
                new ConstructionItem
                {
                    ID = 2,
                    ConstructionID = 1,
                    ConstructionItemName = "Thi công khung kết cấu",
                    UnitOfMeasurementID = 1, // mét
                    TotalVolume = 1200,
                    StartDate = new DateTime(2021, 05, 16),
                    ExpectedCompletionDate = new DateTime(2021, 07, 01),
                    ActualCompletionDate = new DateTime(2021, 06, 28),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 8
                },
                new ConstructionItem
                {
                    ID = 3,
                    ConstructionID = 1,
                    ConstructionItemName = "Xây tường bao và ngăn phòng",
                    UnitOfMeasurementID = 12, // viên
                    TotalVolume = 30000,
                    StartDate = new DateTime(2021, 07, 02),
                    ExpectedCompletionDate = new DateTime(2021, 08, 15),
                    ActualCompletionDate = new DateTime(2021, 08, 14),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 13
                },
                new ConstructionItem
                {
                    ID = 4,
                    ConstructionID = 1,
                    ConstructionItemName = "Lắp đặt hệ thống điện nước",
                    UnitOfMeasurementID = 1, // mét
                    TotalVolume = 2500,
                    StartDate = new DateTime(2021, 08, 16),
                    ExpectedCompletionDate = new DateTime(2021, 09, 15),
                    ActualCompletionDate = new DateTime(2021, 09, 12),
                    ConstructionStatusID = 3,

                    WorkSubTypeVariantID = 19
                },
                new ConstructionItem
                {
                    ID = 5,
                    ConstructionID = 1,
                    ConstructionItemName = "Ốp lát nền và tường",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 1800,
                    StartDate = new DateTime(2021, 09, 16),
                    ExpectedCompletionDate = new DateTime(2021, 10, 10),
                    ActualCompletionDate = new DateTime(2021, 10, 09),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 21
                },
                new ConstructionItem
                {
                    ID = 6,
                    ConstructionID = 1,
                    ConstructionItemName = "Sơn tường và chống thấm",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 2200,
                    StartDate = new DateTime(2021, 10, 11),
                    ExpectedCompletionDate = new DateTime(2021, 11, 01),
                    ActualCompletionDate = new DateTime(2021, 10, 30),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 23
                },
                new ConstructionItem
                {
                    ID = 7,
                    ConstructionID = 1,
                    ConstructionItemName = "Lắp cửa và lan can",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 300,
                    StartDate = new DateTime(2021, 11, 02),
                    ExpectedCompletionDate = new DateTime(2021, 11, 20),
                    ActualCompletionDate = new DateTime(2021, 11, 18),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 27
                },
                new ConstructionItem
                {
                    ID = 8,
                    ConstructionID = 1,
                    ConstructionItemName = "Thi công thang máy và cầu thang",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 10,
                    StartDate = new DateTime(2021, 11, 21),
                    ExpectedCompletionDate = new DateTime(2021, 12, 10),
                    ActualCompletionDate = new DateTime(2021, 12, 08),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 31
                },
                new ConstructionItem
                {
                    ID = 9,
                    ConstructionID = 1,
                    ConstructionItemName = "Hoàn thiện nội thất cơ bản",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 150,
                    StartDate = new DateTime(2021, 12, 11),
                    ExpectedCompletionDate = new DateTime(2021, 12, 31),
                    ActualCompletionDate = new DateTime(2021, 12, 30),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 30
                },
                new ConstructionItem
                {
                    ID = 10,
                    ConstructionID = 1,
                    ConstructionItemName = "Cảnh quan và sân vườn",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 1000,
                    StartDate = new DateTime(2022, 01, 01),
                    ExpectedCompletionDate = new DateTime(2022, 01, 20),
                    ActualCompletionDate = new DateTime(2022, 01, 18),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 33
                },
                // "Nhà ở dân dụng Phù Mỹ
                new ConstructionItem
                {
                    ID = 11,
                    ConstructionID = 2,
                    ConstructionItemName = "San lấp mặt bằng",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 1000,
                    StartDate = new DateTime(2021, 02, 22),
                    ExpectedCompletionDate = new DateTime(2021, 03, 10),
                    ActualCompletionDate = new DateTime(2021, 03, 08),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 1
                },
                new ConstructionItem
                {
                    ID = 12,
                    ConstructionID = 2,
                    ConstructionItemName = "Đổ móng bê tông cốt thép",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 800,
                    StartDate = new DateTime(2021, 03, 11),
                    ExpectedCompletionDate = new DateTime(2021, 04, 15),
                    ActualCompletionDate = new DateTime(2021, 04, 13),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 7
                },
                new ConstructionItem
                {
                    ID = 13,
                    ConstructionID = 2,
                    ConstructionItemName = "Dựng cột, dầm, sàn",
                    UnitOfMeasurementID = 1, // mét
                    TotalVolume = 1500,
                    StartDate = new DateTime(2021, 04, 16),
                    ExpectedCompletionDate = new DateTime(2021, 06, 01),
                    ActualCompletionDate = new DateTime(2021, 05, 29),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 9
                },
                new ConstructionItem
                {
                    ID = 14,
                    ConstructionID = 2,
                    ConstructionItemName = "Xây tường gạch",
                    UnitOfMeasurementID = 12, // viên
                    TotalVolume = 25000,
                    StartDate = new DateTime(2021, 06, 02),
                    ExpectedCompletionDate = new DateTime(2021, 07, 20),
                    ActualCompletionDate = new DateTime(2021, 07, 19),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 13
                },
                new ConstructionItem
                {
                    ID = 15,
                    ConstructionID = 2,
                    ConstructionItemName = "Lắp đặt hệ thống điện âm",
                    UnitOfMeasurementID = 1, // mét
                    TotalVolume = 1800,
                    StartDate = new DateTime(2021, 07, 21),
                    ExpectedCompletionDate = new DateTime(2021, 08, 15),
                    ActualCompletionDate = new DateTime(2021, 08, 13),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 17
                },
                new ConstructionItem
                {
                    ID = 16,
                    ConstructionID = 2,
                    ConstructionItemName = "Ốp lát gạch nền",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 2000,
                    StartDate = new DateTime(2021, 08, 16),
                    ExpectedCompletionDate = new DateTime(2021, 09, 10),
                    ActualCompletionDate = new DateTime(2021, 09, 09),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 22
                },
                new ConstructionItem
                {
                    ID = 17,
                    ConstructionID = 2,
                    ConstructionItemName = "Sơn nước nội ngoại thất",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 3500,
                    StartDate = new DateTime(2021, 09, 11),
                    ExpectedCompletionDate = new DateTime(2021, 10, 15),
                    ActualCompletionDate = new DateTime(2021, 10, 12),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 23
                },
                new ConstructionItem
                {
                    ID = 18,
                    ConstructionID = 2,
                    ConstructionItemName = "Lắp đặt hệ thống nước sinh hoạt",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 60,
                    StartDate = new DateTime(2021, 10, 16),
                    ExpectedCompletionDate = new DateTime(2021, 11, 10),
                    ActualCompletionDate = new DateTime(2021, 11, 08),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 20
                },
                new ConstructionItem
                {
                    ID = 19,
                    ConstructionID = 2,
                    ConstructionItemName = "Thi công mái nhà",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 900,
                    StartDate = new DateTime(2021, 11, 11),
                    ExpectedCompletionDate = new DateTime(2021, 12, 05),
                    ActualCompletionDate = new DateTime(2021, 12, 02),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 15
                },
                new ConstructionItem
                {
                    ID = 20,
                    ConstructionID = 2,
                    ConstructionItemName = "Hoàn thiện sân vườn trước nhà",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 700,
                    StartDate = new DateTime(2021, 12, 06),
                    ExpectedCompletionDate = new DateTime(2021, 12, 25),
                    ActualCompletionDate = new DateTime(2021, 12, 23),
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 33
                },
                // Nhà ở dân dụng An Nhơn
                new ConstructionItem
                {
                    ID = 21,
                    ConstructionID = 3,
                    ConstructionItemName = "San lấp mặt bằng",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 1200,
                    StartDate = new DateTime(2021, 05, 02),
                    ExpectedCompletionDate = new DateTime(2021, 05, 25),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 2 // Pending
                },
                new ConstructionItem
                {
                    ID = 22,
                    ConstructionID = 3,
                    ConstructionItemName = "Đổ móng bê tông cốt thép",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 900,
                    StartDate = new DateTime(2021, 05, 26),
                    ExpectedCompletionDate = new DateTime(2021, 07, 05),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 2 // Pending
                },
                new ConstructionItem
                {
                    ID = 23,
                    ConstructionID = 3,
                    ConstructionItemName = "Dựng cột, dầm, sàn",
                    UnitOfMeasurementID = 1, // mét
                    TotalVolume = 1800,
                    StartDate = new DateTime(2021, 07, 06),
                    ExpectedCompletionDate = new DateTime(2021, 08, 15),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 7 // Pending
                },
                new ConstructionItem
                {
                    ID = 24,
                    ConstructionID = 3,
                    ConstructionItemName = "Xây tường gạch",
                    UnitOfMeasurementID = 12, // viên
                    TotalVolume = 30000,
                    StartDate = new DateTime(2021, 08, 16),
                    ExpectedCompletionDate = new DateTime(2021, 10, 01),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 13 // Pending
                },
                new ConstructionItem
                {
                    ID = 25,
                    ConstructionID = 3,
                    ConstructionItemName = "Lắp đặt hệ thống điện âm",
                    UnitOfMeasurementID = 1, // mét
                    TotalVolume = 2000,
                    StartDate = new DateTime(2021, 10, 02),
                    ExpectedCompletionDate = new DateTime(2021, 11, 10),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 17 // Pending
                },
                new ConstructionItem
                {
                    ID = 26,
                    ConstructionID = 3,
                    ConstructionItemName = "Ốp lát gạch nền",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 2500,
                    StartDate = new DateTime(2021, 11, 11),
                    ExpectedCompletionDate = new DateTime(2021, 12, 20),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 22 // Pending
                },
                new ConstructionItem
                {
                    ID = 27,
                    ConstructionID = 3,
                    ConstructionItemName = "Sơn nước nội ngoại thất",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 4000,
                    StartDate = new DateTime(2021, 12, 21),
                    ExpectedCompletionDate = new DateTime(2022, 01, 15),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 24 // Pending
                },
                new ConstructionItem
                {
                    ID = 28,
                    ConstructionID = 3,
                    ConstructionItemName = "Lắp đặt hệ thống nước sinh hoạt",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 70,
                    StartDate = new DateTime(2022, 01, 16),
                    ExpectedCompletionDate = new DateTime(2022, 02, 28),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 30 // Pending
                },
                new ConstructionItem
                {
                    ID = 29,
                    ConstructionID = 3,
                    ConstructionItemName = "Thi công mái nhà",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 1000,
                    StartDate = new DateTime(2022, 03, 01),
                    ExpectedCompletionDate = new DateTime(2022, 04, 10),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 16 // Pending
                },
                new ConstructionItem
                {
                    ID = 30,
                    ConstructionID = 3,
                    ConstructionItemName = "Hoàn thiện sân vườn trước nhà",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 800,
                    StartDate = new DateTime(2022, 04, 11),
                    ExpectedCompletionDate = new DateTime(2022, 05, 05),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 33 // Pending
                },

                // Cầu An Hòa
                new ConstructionItem
                {
                    ID = 31,
                    ConstructionID = 4,
                    ConstructionItemName = "Thi công nền đường",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 2500,
                    StartDate = new DateTime(2021, 04, 12),
                    ExpectedCompletionDate = new DateTime(2021, 06, 30),
                    ActualCompletionDate = new DateTime(2021, 06, 25),  // Đã hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 1 // Completed
                },
                new ConstructionItem
                {
                    ID = 32,
                    ConstructionID = 4,
                    ConstructionItemName = "Lắp đặt móng cầu",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 1200,
                    StartDate = new DateTime(2021, 07, 01),
                    ExpectedCompletionDate = new DateTime(2021, 09, 15),
                    ActualCompletionDate = new DateTime(2021, 09, 10),  // Đã hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 8 // Completed
                },
                new ConstructionItem
                {
                    ID = 33,
                    ConstructionID = 4,
                    ConstructionItemName = "Đổ bê tông cầu",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 1500,
                    StartDate = new DateTime(2021, 09, 16),
                    ExpectedCompletionDate = new DateTime(2021, 11, 30),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 9 // InProgress
                },
                new ConstructionItem
                {
                    ID = 34,
                    ConstructionID = 4,
                    ConstructionItemName = "Lắp đặt cầu giao thông",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 500,
                    StartDate = new DateTime(2021, 12, 01),
                    ExpectedCompletionDate = new DateTime(2022, 02, 28),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 14 // InProgress
                },
                new ConstructionItem
                {
                    ID = 35,
                    ConstructionID = 4,
                    ConstructionItemName = "Lắp đặt hệ thống thoát nước",
                    UnitOfMeasurementID = 6, // mét khối
                    TotalVolume = 800,
                    StartDate = new DateTime(2022, 03, 01),
                    ExpectedCompletionDate = new DateTime(2022, 05, 15),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 19 // InProgress
                },
                new ConstructionItem
                {
                    ID = 36,
                    ConstructionID = 4,
                    ConstructionItemName = "Hoàn thiện mặt cầu",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 1200,
                    StartDate = new DateTime(2022, 05, 16),
                    ExpectedCompletionDate = new DateTime(2022, 07, 30),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 25 // InProgress
                },
                new ConstructionItem
                {
                    ID = 37,
                    ConstructionID = 4,
                    ConstructionItemName = "Thi công bảo trì",
                    UnitOfMeasurementID = 8, // mét vuông
                    TotalVolume = 800,
                    StartDate = new DateTime(2022, 08, 01),
                    ExpectedCompletionDate = new DateTime(2022, 09, 30),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 26 // InProgress
                },
                new ConstructionItem
                {
                    ID = 38,
                    ConstructionID = 4,
                    ConstructionItemName = "Lắp đặt hệ thống chiếu sáng cầu",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 50,
                    StartDate = new DateTime(2022, 10, 01),
                    ExpectedCompletionDate = new DateTime(2022, 11, 15),
                    ActualCompletionDate = null,  // Chưa hoàn thành
                    ConstructionStatusID = 3,
                    WorkSubTypeVariantID = 36 // InProgress
                },
            // Đường tránh QL1A - Phù Mỹ
                new ConstructionItem
                {
                    ID = 39,
                    ConstructionID = 5,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "Thi công nền đường",
                    UnitOfMeasurementID = 6, // m³
                    TotalVolume = 4000f,
                    StartDate = new DateTime(2021, 02, 25),
                    ExpectedCompletionDate = new DateTime(2022, 01, 15),
                    ActualCompletionDate = new DateTime(2022, 01, 10),
                    WorkSubTypeVariantID = 1
                },
                new ConstructionItem
                {
                    ID = 40,
                    ConstructionID = 5,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "Lát mặt đường nhựa",
                    UnitOfMeasurementID = 8, // m²
                    TotalVolume = 5000f,
                    StartDate = new DateTime(2022, 01, 16),
                    ExpectedCompletionDate = new DateTime(2022, 05, 15),
                    ActualCompletionDate = new DateTime(2022, 05, 10),
                    WorkSubTypeVariantID = 8 // InProgress

                },
                new ConstructionItem
                {
                    ID = 41,
                    ConstructionID = 5,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "Thi công rãnh thoát nước",
                    UnitOfMeasurementID = 1, // m
                    TotalVolume = 1500f,
                    StartDate = new DateTime(2022, 05, 16),
                    ExpectedCompletionDate = new DateTime(2022, 08, 15),
                    ActualCompletionDate = new DateTime(2022, 08, 14),
                    WorkSubTypeVariantID = 19 // InProgress

                },
                new ConstructionItem
                {
                    ID = 42,
                    ConstructionID = 5,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "Sơn kẻ vạch đường",
                    UnitOfMeasurementID = 8, // m²
                    TotalVolume = 600f,
                    StartDate = new DateTime(2022, 08, 20),
                    ExpectedCompletionDate = new DateTime(2022, 10, 01),
                    ActualCompletionDate = new DateTime(2022, 09, 29),
                    WorkSubTypeVariantID = 24 // InProgress

                },
                new ConstructionItem
                {
                    ID = 43,
                    ConstructionID = 5,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "Lắp đặt hệ thống đèn đường",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 100f,
                    StartDate = new DateTime(2022, 10, 05),
                    ExpectedCompletionDate = new DateTime(2022, 11, 30),
                    ActualCompletionDate = new DateTime(2022, 11, 25),
                    WorkSubTypeVariantID = 36 // InProgress

                },
                new ConstructionItem
                {
                    ID = 44,
                    ConstructionID = 5,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "Kiểm tra & nghiệm thu",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 1f,
                    StartDate = new DateTime(2022, 12, 01),
                    ExpectedCompletionDate = new DateTime(2023, 06, 10),
                    ActualCompletionDate = new DateTime(2023, 06, 10),
                    WorkSubTypeVariantID = 36 // InProgress

                },
            // Nhà máy sản xuất thép An Phát
                new ConstructionItem
                {
                    ID = 45,
                    ConstructionID = 6,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "San lấp mặt bằng",
                    UnitOfMeasurementID = 6, // m³
                    TotalVolume = 12000f,
                    StartDate = new DateTime(2020, 07, 20),
                    ExpectedCompletionDate = new DateTime(2020, 10, 15),
                    ActualCompletionDate = new DateTime(2020, 10, 10),
                    WorkSubTypeVariantID = 2 // InProgress

                },
                new ConstructionItem
                {
                    ID = 46,
                    ConstructionID = 6,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "Thi công móng",
                    UnitOfMeasurementID = 6, // m³
                    TotalVolume = 5000f,
                    StartDate = new DateTime(2020, 10, 20),
                    ExpectedCompletionDate = new DateTime(2021, 01, 30),
                    ActualCompletionDate = new DateTime(2021, 01, 25),
                    WorkSubTypeVariantID = 7 // InProgress

                },
                new ConstructionItem
                {
                    ID = 47,
                    ConstructionID = 6,
                    ConstructionStatusID = 2, // In Progress
                    ConstructionItemName = "Dựng khung thép nhà xưởng",
                    UnitOfMeasurementID = 7, // tấn
                    TotalVolume = 350f,
                    StartDate = new DateTime(2021, 02, 01),
                    ExpectedCompletionDate = new DateTime(2021, 06, 01),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 9 // InProgress

                },
                new ConstructionItem
                {
                    ID = 48,
                    ConstructionID = 6,
                    ConstructionStatusID = 4, // Paused
                    ConstructionItemName = "Lắp đặt máy móc thiết bị",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 45f,
                    StartDate = new DateTime(2021, 06, 15),
                    ExpectedCompletionDate = new DateTime(2021, 11, 01),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 31 // InProgress

                },
                new ConstructionItem
                {
                    ID = 49,
                    ConstructionID = 6,
                    ConstructionStatusID = 1, // Pending
                    ConstructionItemName = "Xây dựng nhà kho nguyên liệu",
                    UnitOfMeasurementID = 8, // m²
                    TotalVolume = 1800f,
                    StartDate = new DateTime(2022, 01, 01),
                    ExpectedCompletionDate = new DateTime(2022, 03, 15),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 8 // InProgress
                },
                new ConstructionItem
                {
                    ID = 50,
                    ConstructionID = 6,
                    ConstructionStatusID = 1, // Pending
                    ConstructionItemName = "Thi công hệ thống xử lý nước thải",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 2f,
                    StartDate = new DateTime(2022, 03, 20),
                    ExpectedCompletionDate = new DateTime(2022, 05, 15),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 20 // InProgress

                },
            // Đập thủy lợi Phú Tài
             new ConstructionItem
             {
                 ID = 51,
                 ConstructionID = 7,
                 ConstructionStatusID = 3, // Completed
                 ConstructionItemName = "Khảo sát địa chất",
                 UnitOfMeasurementID = 13, // cái
                 TotalVolume = 1f,
                 StartDate = new DateTime(2020, 03, 15),
                 ExpectedCompletionDate = new DateTime(2020, 05, 01),
                 ActualCompletionDate = new DateTime(2020, 04, 28),
                 WorkSubTypeVariantID = 1 // InProgress

             },
                new ConstructionItem
                {
                    ID = 52,
                    ConstructionID = 7,
                    ConstructionStatusID = 3, // Completed
                    ConstructionItemName = "San lấp mặt bằng",
                    UnitOfMeasurementID = 6, // m³
                    TotalVolume = 10000f,
                    StartDate = new DateTime(2020, 05, 10),
                    ExpectedCompletionDate = new DateTime(2020, 08, 01),
                    ActualCompletionDate = new DateTime(2020, 07, 25),
                    WorkSubTypeVariantID = 2 // InProgress
                },
                new ConstructionItem
                {
                    ID = 53,
                    ConstructionID = 7,
                    ConstructionStatusID = 2, // In Progress
                    ConstructionItemName = "Đào hố móng đập",
                    UnitOfMeasurementID = 6, // m³
                    TotalVolume = 7000f,
                    StartDate = new DateTime(2020, 08, 10),
                    ExpectedCompletionDate = new DateTime(2020, 11, 15),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 4 // InProgress

                },
                new ConstructionItem
                {
                    ID = 54,
                    ConstructionID = 7,
                    ConstructionStatusID = 4, // Paused
                    ConstructionItemName = "Lắp cống xả đáy",
                    UnitOfMeasurementID = 13, // cái
                    TotalVolume = 3f,
                    StartDate = new DateTime(2020, 12, 01),
                    ExpectedCompletionDate = new DateTime(2021, 03, 15),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 19 // InProgress

                },
                new ConstructionItem
                {
                    ID = 55,
                    ConstructionID = 7,
                    ConstructionStatusID = 1, // Pending
                    ConstructionItemName = "Xây thân đập",
                    UnitOfMeasurementID = 6, // m³
                    TotalVolume = 15000f,
                    StartDate = new DateTime(2021, 04, 01),
                    ExpectedCompletionDate = new DateTime(2021, 10, 15),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 7 // InProgress

                },
                new ConstructionItem
                {
                    ID = 56,
                    ConstructionID = 7,
                    ConstructionStatusID = 1, // Pending
                    ConstructionItemName = "Làm đường công vụ",
                    UnitOfMeasurementID = 8, // m²
                    TotalVolume = 2500f,
                    StartDate = new DateTime(2021, 10, 20),
                    ExpectedCompletionDate = new DateTime(2021, 12, 25),
                    ActualCompletionDate = null,
                    WorkSubTypeVariantID = 33 // InProgress

                }
            );



            // Seed data for ConstructionPlan
            modelBuilder.Entity<ConstructionPlan>().HasData(
                //Khu chung cư An Hòa Garden
                // Thi công móng (ID = 1)
                new ConstructionPlan { ID = 1, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 1, StartDate = new DateTime(2021, 04, 12), ExpectedCompletionDate = new DateTime(2021, 04, 25), ActualCompletionDate = new DateTime(2021, 04, 24) },
                new ConstructionPlan { ID = 2, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 1, StartDate = new DateTime(2021, 04, 26), ExpectedCompletionDate = new DateTime(2021, 05, 15), ActualCompletionDate = new DateTime(2021, 05, 14) },

                // Thi công khung kết cấu (ID = 2)
                new ConstructionPlan { ID = 3, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 2, StartDate = new DateTime(2021, 05, 16), ExpectedCompletionDate = new DateTime(2021, 06, 10), ActualCompletionDate = new DateTime(2021, 06, 09) },
                new ConstructionPlan { ID = 4, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 2, StartDate = new DateTime(2021, 06, 11), ExpectedCompletionDate = new DateTime(2021, 07, 01), ActualCompletionDate = new DateTime(2021, 06, 28) },

                // Xây tường bao và ngăn phòng (ID = 3)
                new ConstructionPlan { ID = 5, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 3, StartDate = new DateTime(2021, 07, 02), ExpectedCompletionDate = new DateTime(2021, 08, 15), ActualCompletionDate = new DateTime(2021, 08, 14) },

                // Lắp đặt hệ thống điện nước (ID = 4)
                new ConstructionPlan { ID = 6, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 4, StartDate = new DateTime(2021, 08, 16), ExpectedCompletionDate = new DateTime(2021, 08, 30), ActualCompletionDate = new DateTime(2021, 08, 29) },
                new ConstructionPlan { ID = 7, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 4, StartDate = new DateTime(2021, 08, 31), ExpectedCompletionDate = new DateTime(2021, 09, 15), ActualCompletionDate = new DateTime(2021, 09, 12) },

                // Ốp lát nền và tường (ID = 5)
                new ConstructionPlan { ID = 8, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 5, StartDate = new DateTime(2021, 09, 16), ExpectedCompletionDate = new DateTime(2021, 10, 10), ActualCompletionDate = new DateTime(2021, 10, 09) },

                // Sơn tường và chống thấm (ID = 6)
                new ConstructionPlan { ID = 9, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 6, StartDate = new DateTime(2021, 10, 11), ExpectedCompletionDate = new DateTime(2021, 10, 25), ActualCompletionDate = new DateTime(2021, 10, 24) },
                new ConstructionPlan { ID = 10, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 6, StartDate = new DateTime(2021, 10, 26), ExpectedCompletionDate = new DateTime(2021, 11, 01), ActualCompletionDate = new DateTime(2021, 10, 30) },

                // Lắp cửa và lan can (ID = 7)
                new ConstructionPlan { ID = 11, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 7, StartDate = new DateTime(2021, 11, 02), ExpectedCompletionDate = new DateTime(2021, 11, 20), ActualCompletionDate = new DateTime(2021, 11, 18) },

                // Thi công thang máy và cầu thang (ID = 8)
                new ConstructionPlan { ID = 12, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 8, StartDate = new DateTime(2021, 11, 21), ExpectedCompletionDate = new DateTime(2021, 12, 10), ActualCompletionDate = new DateTime(2021, 12, 08) },

                // Hoàn thiện nội thất cơ bản (ID = 9)
                new ConstructionPlan { ID = 13, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 9, StartDate = new DateTime(2021, 12, 11), ExpectedCompletionDate = new DateTime(2021, 12, 31), ActualCompletionDate = new DateTime(2021, 12, 30) },

                // Cảnh quan và sân vườn (ID = 10)
                new ConstructionPlan { ID = 14, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 10, StartDate = new DateTime(2022, 01, 01), ExpectedCompletionDate = new DateTime(2022, 01, 20), ActualCompletionDate = new DateTime(2022, 01, 18) },
                // Nhà ở dân dụng Phù mỹ
                // Hạng mục 11: San lấp mặt bằng
                new ConstructionPlan { ID = 15, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 11, StartDate = new DateTime(2021, 02, 22), ExpectedCompletionDate = new DateTime(2021, 02, 28), ActualCompletionDate = new DateTime(2021, 02, 27) },
                new ConstructionPlan { ID = 16, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 11, StartDate = new DateTime(2021, 03, 01), ExpectedCompletionDate = new DateTime(2021, 03, 10), ActualCompletionDate = new DateTime(2021, 03, 08) },

                // Hạng mục 12: Đổ móng bê tông cốt thép
                new ConstructionPlan { ID = 17, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 12, StartDate = new DateTime(2021, 03, 11), ExpectedCompletionDate = new DateTime(2021, 03, 25), ActualCompletionDate = new DateTime(2021, 03, 24) },
                new ConstructionPlan { ID = 18, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 12, StartDate = new DateTime(2021, 03, 26), ExpectedCompletionDate = new DateTime(2021, 04, 15), ActualCompletionDate = new DateTime(2021, 04, 13) },

                // Hạng mục 13: Dựng cột, dầm, sàn
                new ConstructionPlan { ID = 19, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 13, StartDate = new DateTime(2021, 04, 16), ExpectedCompletionDate = new DateTime(2021, 05, 15), ActualCompletionDate = new DateTime(2021, 05, 14) },
                new ConstructionPlan { ID = 20, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 13, StartDate = new DateTime(2021, 05, 16), ExpectedCompletionDate = new DateTime(2021, 06, 01), ActualCompletionDate = new DateTime(2021, 05, 29) },

                // Hạng mục 14: Xây tường gạch
                new ConstructionPlan { ID = 21, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 14, StartDate = new DateTime(2021, 06, 02), ExpectedCompletionDate = new DateTime(2021, 07, 10), ActualCompletionDate = new DateTime(2021, 07, 09) },
                new ConstructionPlan { ID = 22, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 14, StartDate = new DateTime(2021, 07, 11), ExpectedCompletionDate = new DateTime(2021, 07, 20), ActualCompletionDate = new DateTime(2021, 07, 19) },

                // Hạng mục 15: Lắp đặt hệ thống điện âm
                new ConstructionPlan { ID = 23, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 15, StartDate = new DateTime(2021, 07, 21), ExpectedCompletionDate = new DateTime(2021, 08, 10), ActualCompletionDate = new DateTime(2021, 08, 09) },
                new ConstructionPlan { ID = 24, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 15, StartDate = new DateTime(2021, 08, 11), ExpectedCompletionDate = new DateTime(2021, 08, 15), ActualCompletionDate = new DateTime(2021, 08, 13) },

                // Hạng mục 16: Ốp lát gạch nền
                new ConstructionPlan { ID = 25, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 16, StartDate = new DateTime(2021, 08, 16), ExpectedCompletionDate = new DateTime(2021, 09, 10), ActualCompletionDate = new DateTime(2021, 09, 09) },

                // Hạng mục 17: Sơn nước nội ngoại thất
                new ConstructionPlan { ID = 26, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 17, StartDate = new DateTime(2021, 09, 11), ExpectedCompletionDate = new DateTime(2021, 09, 30), ActualCompletionDate = new DateTime(2021, 09, 29) },
                new ConstructionPlan { ID = 27, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 17, StartDate = new DateTime(2021, 10, 01), ExpectedCompletionDate = new DateTime(2021, 10, 15), ActualCompletionDate = new DateTime(2021, 10, 12) },

                // Hạng mục 18: Lắp đặt hệ thống nước sinh hoạt
                new ConstructionPlan { ID = 28, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 18, StartDate = new DateTime(2021, 10, 16), ExpectedCompletionDate = new DateTime(2021, 11, 10), ActualCompletionDate = new DateTime(2021, 11, 08) },

                // Hạng mục 19: Thi công mái nhà
                new ConstructionPlan { ID = 29, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 19, StartDate = new DateTime(2021, 11, 11), ExpectedCompletionDate = new DateTime(2021, 12, 05), ActualCompletionDate = new DateTime(2021, 12, 02) },

                // Hạng mục 20: Hoàn thiện sân vườn trước nhà
                new ConstructionPlan { ID = 30, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 20, StartDate = new DateTime(2021, 12, 06), ExpectedCompletionDate = new DateTime(2021, 12, 25), ActualCompletionDate = new DateTime(2021, 12, 23) },
                // Nhà ở dân dụng An Nhơn
                // Hạng mục San lấp mặt bằng
                new ConstructionPlan { ID = 31, ConstructionStatusID = 2, EmployeeID = "manager1-id", ConstructionItemID = 21, StartDate = new DateTime(2021, 05, 02), ExpectedCompletionDate = new DateTime(2021, 05, 25), ActualCompletionDate = null },

                // Hạng mục Đổ móng bê tông cốt thép
                new ConstructionPlan { ID = 32, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 22, StartDate = new DateTime(2021, 05, 26), ExpectedCompletionDate = new DateTime(2021, 07, 05), ActualCompletionDate = null },

                // Hạng mục Dựng cột, dầm, sàn
                new ConstructionPlan { ID = 33, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 23, StartDate = new DateTime(2021, 07, 06), ExpectedCompletionDate = new DateTime(2021, 08, 15), ActualCompletionDate = null },

                // Hạng mục Xây tường gạch
                new ConstructionPlan { ID = 34, ConstructionStatusID = 2, EmployeeID = "manager1-id", ConstructionItemID = 24, StartDate = new DateTime(2021, 08, 16), ExpectedCompletionDate = new DateTime(2021, 10, 01), ActualCompletionDate = null },

                // Hạng mục Lắp đặt hệ thống điện âm
                new ConstructionPlan { ID = 35, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 25, StartDate = new DateTime(2021, 10, 02), ExpectedCompletionDate = new DateTime(2021, 11, 10), ActualCompletionDate = null },

                // Hạng mục Ốp lát gạch nền
                new ConstructionPlan { ID = 36, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 26, StartDate = new DateTime(2021, 11, 11), ExpectedCompletionDate = new DateTime(2021, 12, 20), ActualCompletionDate = null },

                // Hạng mục Sơn nước nội ngoại thất
                new ConstructionPlan { ID = 37, ConstructionStatusID = 2, EmployeeID = "manager1-id", ConstructionItemID = 27, StartDate = new DateTime(2021, 12, 21), ExpectedCompletionDate = new DateTime(2022, 01, 15), ActualCompletionDate = null },

                // Hạng mục Lắp đặt hệ thống nước sinh hoạt
                new ConstructionPlan { ID = 38, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 28, StartDate = new DateTime(2022, 01, 16), ExpectedCompletionDate = new DateTime(2022, 02, 28), ActualCompletionDate = null },

                // Hạng mục Thi công mái nhà
                new ConstructionPlan { ID = 39, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 29, StartDate = new DateTime(2022, 03, 01), ExpectedCompletionDate = new DateTime(2022, 04, 10), ActualCompletionDate = null },

                // Hạng mục Hoàn thiện sân vườn trước nhà
                new ConstructionPlan { ID = 40, ConstructionStatusID = 2, EmployeeID = "manager1-id", ConstructionItemID = 30, StartDate = new DateTime(2022, 04, 11), ExpectedCompletionDate = new DateTime(2022, 05, 05), ActualCompletionDate = null },
                // Cầu An Hòa
                // Hạng mục ID = 31: Thi công nền đường (đã hoàn thành)
                new ConstructionPlan { ID = 41, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 31, StartDate = new DateTime(2021, 04, 12), ExpectedCompletionDate = new DateTime(2021, 05, 20), ActualCompletionDate = new DateTime(2021, 05, 18) },
                new ConstructionPlan { ID = 42, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 31, StartDate = new DateTime(2021, 05, 21), ExpectedCompletionDate = new DateTime(2021, 06, 30), ActualCompletionDate = new DateTime(2021, 06, 25) },

                // Hạng mục ID = 32: Lắp đặt móng cầu (đã hoàn thành)
                new ConstructionPlan { ID = 43, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 32, StartDate = new DateTime(2021, 07, 01), ExpectedCompletionDate = new DateTime(2021, 08, 10), ActualCompletionDate = new DateTime(2021, 08, 09) },
                new ConstructionPlan { ID = 44, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 32, StartDate = new DateTime(2021, 08, 11), ExpectedCompletionDate = new DateTime(2021, 09, 15), ActualCompletionDate = new DateTime(2021, 09, 10) },

                // Hạng mục ID = 33: Đổ bê tông cầu (đang thi công)
                new ConstructionPlan { ID = 45, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 33, StartDate = new DateTime(2021, 09, 16), ExpectedCompletionDate = new DateTime(2021, 10, 15), ActualCompletionDate = null },
                new ConstructionPlan { ID = 46, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 33, StartDate = new DateTime(2021, 10, 16), ExpectedCompletionDate = new DateTime(2021, 11, 30), ActualCompletionDate = null },

                // Hạng mục ID = 34: Lắp đặt cầu giao thông
                new ConstructionPlan { ID = 47, ConstructionStatusID = 2, EmployeeID = "manager1-id", ConstructionItemID = 34, StartDate = new DateTime(2021, 12, 01), ExpectedCompletionDate = new DateTime(2022, 01, 15), ActualCompletionDate = null },

                // Hạng mục ID = 35: Lắp đặt hệ thống thoát nước
                new ConstructionPlan { ID = 48, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 35, StartDate = new DateTime(2022, 03, 01), ExpectedCompletionDate = new DateTime(2022, 04, 10), ActualCompletionDate = null },
                new ConstructionPlan { ID = 49, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 35, StartDate = new DateTime(2022, 04, 11), ExpectedCompletionDate = new DateTime(2022, 05, 15), ActualCompletionDate = null },

                // Hạng mục ID = 36: Hoàn thiện mặt cầu
                new ConstructionPlan { ID = 50, ConstructionStatusID = 2, EmployeeID = "manager1-id", ConstructionItemID = 36, StartDate = new DateTime(2022, 05, 16), ExpectedCompletionDate = new DateTime(2022, 06, 20), ActualCompletionDate = null },

                // Hạng mục ID = 37: Thi công bảo trì
                new ConstructionPlan { ID = 51, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 37, StartDate = new DateTime(2022, 08, 01), ExpectedCompletionDate = new DateTime(2022, 09, 30), ActualCompletionDate = null },

                // Hạng mục ID = 38: Lắp đặt hệ thống chiếu sáng cầu
                new ConstructionPlan { ID = 52, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 38, StartDate = new DateTime(2022, 10, 01), ExpectedCompletionDate = new DateTime(2022, 11, 15), ActualCompletionDate = null },
                // Đường tránh QL1A - Phù Mỹ
                new ConstructionPlan { ID = 53, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 39, StartDate = new DateTime(2021, 02, 25), ExpectedCompletionDate = new DateTime(2021, 07, 01), ActualCompletionDate = new DateTime(2021, 07, 01) },
                new ConstructionPlan { ID = 54, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 39, StartDate = new DateTime(2021, 07, 02), ExpectedCompletionDate = new DateTime(2022, 01, 15), ActualCompletionDate = new DateTime(2022, 01, 10) },

                new ConstructionPlan { ID = 55, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 40, StartDate = new DateTime(2022, 01, 16), ExpectedCompletionDate = new DateTime(2022, 03, 15), ActualCompletionDate = new DateTime(2022, 03, 14) },
                new ConstructionPlan { ID = 56, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 40, StartDate = new DateTime(2022, 03, 16), ExpectedCompletionDate = new DateTime(2022, 05, 15), ActualCompletionDate = new DateTime(2022, 05, 10) },

                new ConstructionPlan { ID = 57, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 41, StartDate = new DateTime(2022, 05, 16), ExpectedCompletionDate = new DateTime(2022, 08, 15), ActualCompletionDate = new DateTime(2022, 08, 14) },

                new ConstructionPlan { ID = 58, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 42, StartDate = new DateTime(2022, 08, 20), ExpectedCompletionDate = new DateTime(2022, 09, 15), ActualCompletionDate = new DateTime(2022, 09, 14) },
                new ConstructionPlan { ID = 59, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 42, StartDate = new DateTime(2022, 09, 16), ExpectedCompletionDate = new DateTime(2022, 10, 01), ActualCompletionDate = new DateTime(2022, 09, 29) },

                new ConstructionPlan { ID = 60, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 43, StartDate = new DateTime(2022, 10, 05), ExpectedCompletionDate = new DateTime(2022, 11, 10), ActualCompletionDate = new DateTime(2022, 11, 09) },
                new ConstructionPlan { ID = 61, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 43, StartDate = new DateTime(2022, 11, 11), ExpectedCompletionDate = new DateTime(2022, 11, 30), ActualCompletionDate = new DateTime(2022, 11, 25) },

                new ConstructionPlan { ID = 62, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 44, StartDate = new DateTime(2022, 12, 01), ExpectedCompletionDate = new DateTime(2023, 03, 01), ActualCompletionDate = new DateTime(2023, 03, 01) },
                new ConstructionPlan { ID = 63, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 44, StartDate = new DateTime(2023, 03, 02), ExpectedCompletionDate = new DateTime(2023, 06, 10), ActualCompletionDate = new DateTime(2023, 06, 10) },
                // Nhà máy sản xuất thép An Phát
                // Hạng mục ID = 45 (Completed)
                new ConstructionPlan { ID = 64, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 45, StartDate = new DateTime(2020, 07, 20), ExpectedCompletionDate = new DateTime(2020, 09, 01), ActualCompletionDate = new DateTime(2020, 09, 01) },
                new ConstructionPlan { ID = 65, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 45, StartDate = new DateTime(2020, 09, 02), ExpectedCompletionDate = new DateTime(2020, 10, 10), ActualCompletionDate = new DateTime(2020, 10, 10) },

                // Hạng mục ID = 46 (Completed)
                new ConstructionPlan { ID = 66, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 46, StartDate = new DateTime(2020, 10, 20), ExpectedCompletionDate = new DateTime(2020, 12, 15), ActualCompletionDate = new DateTime(2020, 12, 15) },
                new ConstructionPlan { ID = 67, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 46, StartDate = new DateTime(2020, 12, 16), ExpectedCompletionDate = new DateTime(2021, 01, 25), ActualCompletionDate = new DateTime(2021, 01, 25) },

                // Hạng mục ID = 47 (In Progress)
                new ConstructionPlan { ID = 68, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 47, StartDate = new DateTime(2021, 02, 01), ExpectedCompletionDate = new DateTime(2021, 04, 01), ActualCompletionDate = null },
                new ConstructionPlan { ID = 69, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 47, StartDate = new DateTime(2021, 04, 02), ExpectedCompletionDate = new DateTime(2021, 06, 01), ActualCompletionDate = null },

                // Hạng mục ID = 48 (Paused)
                new ConstructionPlan { ID = 70, ConstructionStatusID = 4, EmployeeID = "manager1-id", ConstructionItemID = 48, StartDate = new DateTime(2021, 06, 15), ExpectedCompletionDate = new DateTime(2021, 09, 15), ActualCompletionDate = null },

                // Hạng mục ID = 49 (Pending)
                new ConstructionPlan { ID = 71, ConstructionStatusID = 1, EmployeeID = "manager2-id", ConstructionItemID = 49, StartDate = new DateTime(2022, 01, 01), ExpectedCompletionDate = new DateTime(2022, 02, 10), ActualCompletionDate = null },

                // Hạng mục ID = 50 (Pending)
                new ConstructionPlan { ID = 72, ConstructionStatusID = 1, EmployeeID = "manager3-id", ConstructionItemID = 50, StartDate = new DateTime(2022, 03, 20), ExpectedCompletionDate = new DateTime(2022, 04, 30), ActualCompletionDate = null },
                // Đập thủy lợi Phú Tài
                // ConstructionItemID = 51, Completed
                new ConstructionPlan { ID = 73, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 51, StartDate = new DateTime(2020, 03, 15), ExpectedCompletionDate = new DateTime(2020, 04, 15), ActualCompletionDate = new DateTime(2020, 04, 14) },
                new ConstructionPlan { ID = 74, ConstructionStatusID = 3, EmployeeID = "manager2-id", ConstructionItemID = 51, StartDate = new DateTime(2020, 04, 16), ExpectedCompletionDate = new DateTime(2020, 05, 01), ActualCompletionDate = new DateTime(2020, 04, 28) },

                // ConstructionItemID = 52, Completed
                new ConstructionPlan { ID = 75, ConstructionStatusID = 3, EmployeeID = "manager3-id", ConstructionItemID = 52, StartDate = new DateTime(2020, 05, 10), ExpectedCompletionDate = new DateTime(2020, 06, 20), ActualCompletionDate = new DateTime(2020, 06, 18) },
                new ConstructionPlan { ID = 76, ConstructionStatusID = 3, EmployeeID = "manager1-id", ConstructionItemID = 52, StartDate = new DateTime(2020, 06, 21), ExpectedCompletionDate = new DateTime(2020, 08, 01), ActualCompletionDate = new DateTime(2020, 07, 25) },

                // ConstructionItemID = 53, In Progress
                new ConstructionPlan { ID = 77, ConstructionStatusID = 2, EmployeeID = "manager2-id", ConstructionItemID = 53, StartDate = new DateTime(2020, 08, 10), ExpectedCompletionDate = new DateTime(2020, 10, 01), ActualCompletionDate = null },
                new ConstructionPlan { ID = 78, ConstructionStatusID = 2, EmployeeID = "manager3-id", ConstructionItemID = 53, StartDate = new DateTime(2020, 10, 02), ExpectedCompletionDate = new DateTime(2020, 11, 15), ActualCompletionDate = null },

                // ConstructionItemID = 54, Paused
                new ConstructionPlan { ID = 79, ConstructionStatusID = 4, EmployeeID = "manager1-id", ConstructionItemID = 54, StartDate = new DateTime(2020, 12, 01), ExpectedCompletionDate = new DateTime(2021, 01, 15), ActualCompletionDate = null },

                // ConstructionItemID = 55, Pending
                new ConstructionPlan { ID = 80, ConstructionStatusID = 1, EmployeeID = "manager2-id", ConstructionItemID = 55, StartDate = new DateTime(2021, 04, 01), ExpectedCompletionDate = new DateTime(2021, 07, 01), ActualCompletionDate = null },
                new ConstructionPlan { ID = 81, ConstructionStatusID = 1, EmployeeID = "manager3-id", ConstructionItemID = 55, StartDate = new DateTime(2021, 07, 02), ExpectedCompletionDate = new DateTime(2021, 10, 15), ActualCompletionDate = null },

                // ConstructionItemID = 56, Pending
                new ConstructionPlan { ID = 82, ConstructionStatusID = 1, EmployeeID = "manager1-id", ConstructionItemID = 56, StartDate = new DateTime(2021, 10, 20), ExpectedCompletionDate = new DateTime(2021, 12, 25), ActualCompletionDate = null }
                );


            // Seed data for ConstructionTask
            modelBuilder.Entity<ConstructionTask>().HasData(
            //  Khu chung cư An Hòa Garden
            // Thi công móng (ID = 1)
            new ConstructionTask { ID = 1, ConstructionPlanID = 1, ConstructionStatusID = 3, Workload = 150.0f },
            new ConstructionTask { ID = 2, ConstructionPlanID = 1, ConstructionStatusID = 3, Workload = 100.0f },
            new ConstructionTask { ID = 3, ConstructionPlanID = 2, ConstructionStatusID = 3, Workload = 150.0f },
            new ConstructionTask { ID = 4, ConstructionPlanID = 2, ConstructionStatusID = 3, Workload = 100.0f },

            // Thi công khung kết cấu (ID = 2)
            new ConstructionTask { ID = 5, ConstructionPlanID = 3, ConstructionStatusID = 3, Workload = 300.0f },
            new ConstructionTask { ID = 6, ConstructionPlanID = 3, ConstructionStatusID = 3, Workload = 300.0f },
            new ConstructionTask { ID = 7, ConstructionPlanID = 4, ConstructionStatusID = 3, Workload = 300.0f },
            new ConstructionTask { ID = 8, ConstructionPlanID = 4, ConstructionStatusID = 3, Workload = 300.0f },

            // Xây tường bao và ngăn phòng (ID = 3)
            new ConstructionTask { ID = 9, ConstructionPlanID = 5, ConstructionStatusID = 3, Workload = 15000.0f },
            new ConstructionTask { ID = 10, ConstructionPlanID = 5, ConstructionStatusID = 3, Workload = 15000.0f },

            // Lắp đặt hệ thống điện nước (ID = 4)
            new ConstructionTask { ID = 11, ConstructionPlanID = 6, ConstructionStatusID = 3, Workload = 1250.0f },
            new ConstructionTask { ID = 12, ConstructionPlanID = 7, ConstructionStatusID = 3, Workload = 1250.0f },

            // Ốp lát nền và tường (ID = 5)
            new ConstructionTask { ID = 13, ConstructionPlanID = 8, ConstructionStatusID = 3, Workload = 900.0f },
            new ConstructionTask { ID = 14, ConstructionPlanID = 8, ConstructionStatusID = 3, Workload = 900.0f },

            // Sơn tường và chống thấm (ID = 6)
            new ConstructionTask { ID = 15, ConstructionPlanID = 9, ConstructionStatusID = 3, Workload = 1100.0f },
            new ConstructionTask { ID = 16, ConstructionPlanID = 9, ConstructionStatusID = 3, Workload = 1100.0f },

            // Lắp cửa và lan can (ID = 7)
            new ConstructionTask { ID = 17, ConstructionPlanID = 11, ConstructionStatusID = 3, Workload = 150.0f },
            new ConstructionTask { ID = 18, ConstructionPlanID = 11, ConstructionStatusID = 3, Workload = 150.0f },

            // Thi công thang máy và cầu thang (ID = 8)
            new ConstructionTask { ID = 19, ConstructionPlanID = 12, ConstructionStatusID = 3, Workload = 5.0f },
            new ConstructionTask { ID = 20, ConstructionPlanID = 12, ConstructionStatusID = 3, Workload = 5.0f },

            // Hoàn thiện nội thất cơ bản (ID = 9)
            new ConstructionTask { ID = 21, ConstructionPlanID = 13, ConstructionStatusID = 3, Workload = 100.0f },
            new ConstructionTask { ID = 22, ConstructionPlanID = 13, ConstructionStatusID = 3, Workload = 50.0f },

            // Cảnh quan và sân vườn (ID = 10)
            new ConstructionTask { ID = 23, ConstructionPlanID = 14, ConstructionStatusID = 3, Workload = 500.0f },
            new ConstructionTask { ID = 24, ConstructionPlanID = 14, ConstructionStatusID = 3, Workload = 500.0f },
            // Nhà ở dân dụng Phù mỹ
            // Hạng mục 11: San lấp mặt bằng
            new ConstructionTask { ID = 25, ConstructionPlanID = 15, ConstructionStatusID = 3, Workload = 600.0f },
            new ConstructionTask { ID = 26, ConstructionPlanID = 15, ConstructionStatusID = 3, Workload = 200.0f },

            new ConstructionTask { ID = 27, ConstructionPlanID = 16, ConstructionStatusID = 3, Workload = 200.0f },

            // Hạng mục 12: Đổ móng bê tông cốt thép
            new ConstructionTask { ID = 28, ConstructionPlanID = 17, ConstructionStatusID = 3, Workload = 400.0f },
            new ConstructionTask { ID = 29, ConstructionPlanID = 17, ConstructionStatusID = 3, Workload = 200.0f },
            new ConstructionTask { ID = 30, ConstructionPlanID = 17, ConstructionStatusID = 3, Workload = 200.0f },

            new ConstructionTask { ID = 31, ConstructionPlanID = 18, ConstructionStatusID = 3, Workload = 500.0f },
            new ConstructionTask { ID = 32, ConstructionPlanID = 18, ConstructionStatusID = 3, Workload = 300.0f },

            // Hạng mục 13: Dựng cột, dầm, sàn
            new ConstructionTask { ID = 33, ConstructionPlanID = 19, ConstructionStatusID = 3, Workload = 200.0f },
            new ConstructionTask { ID = 34, ConstructionPlanID = 19, ConstructionStatusID = 3, Workload = 100.0f },

            new ConstructionTask { ID = 35, ConstructionPlanID = 20, ConstructionStatusID = 3, Workload = 600.0f },
            new ConstructionTask { ID = 36, ConstructionPlanID = 20, ConstructionStatusID = 3, Workload = 600.0f },

            // Hạng mục 14: Xây tường gạch
            new ConstructionTask { ID = 37, ConstructionPlanID = 21, ConstructionStatusID = 3, Workload = 12000.0f },

            new ConstructionTask { ID = 38, ConstructionPlanID = 22, ConstructionStatusID = 3, Workload = 13000.0f },

            // Hạng mục 15: Lắp đặt hệ thống điện âm
            new ConstructionTask { ID = 39, ConstructionPlanID = 23, ConstructionStatusID = 3, Workload = 900.0f },

            new ConstructionTask { ID = 40, ConstructionPlanID = 24, ConstructionStatusID = 3, Workload = 300.0f },
            new ConstructionTask { ID = 41, ConstructionPlanID = 24, ConstructionStatusID = 3, Workload = 600.0f },

            // Hạng mục 16: Ốp lát gạch nền
            new ConstructionTask { ID = 42, ConstructionPlanID = 25, ConstructionStatusID = 3, Workload = 1500.0f },
            new ConstructionTask { ID = 43, ConstructionPlanID = 25, ConstructionStatusID = 3, Workload = 500.0f },

            // Hạng mục 17: Sơn nước nội ngoại thất
            new ConstructionTask { ID = 44, ConstructionPlanID = 26, ConstructionStatusID = 3, Workload = 1500.0f },
            new ConstructionTask { ID = 45, ConstructionPlanID = 26, ConstructionStatusID = 3, Workload = 1000.0f },

            new ConstructionTask { ID = 46, ConstructionPlanID = 27, ConstructionStatusID = 3, Workload = 1000.0f },

            // Hạng mục 18: Lắp đặt hệ thống nước sinh hoạt
            new ConstructionTask { ID = 47, ConstructionPlanID = 28, ConstructionStatusID = 3, Workload = 40.0f },
            new ConstructionTask { ID = 48, ConstructionPlanID = 28, ConstructionStatusID = 3, Workload = 20.0f },

            // Hạng mục 19: Thi công mái nhà
            new ConstructionTask { ID = 49, ConstructionPlanID = 29, ConstructionStatusID = 3, Workload = 400.0f },
            new ConstructionTask { ID = 50, ConstructionPlanID = 29, ConstructionStatusID = 3, Workload = 500.0f },

            // Hạng mục 20: Hoàn thiện sân vườn trước nhà
            new ConstructionTask { ID = 51, ConstructionPlanID = 30, ConstructionStatusID = 3, Workload = 350.0f },
            new ConstructionTask { ID = 52, ConstructionPlanID = 30, ConstructionStatusID = 3, Workload = 350.0f },

            // Nhà ở dân dụng An Nhơn
            // Tasks cho ConstructionPlan ID = 31 (San lấp mặt bằng, Volume = 1200)
            new ConstructionTask { ID = 53, ConstructionPlanID = 31, ConstructionStatusID = 2, Workload = 400f },
            new ConstructionTask { ID = 54, ConstructionPlanID = 31, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 55, ConstructionPlanID = 31, ConstructionStatusID = 2, Workload = 250f },
            new ConstructionTask { ID = 56, ConstructionPlanID = 31, ConstructionStatusID = 2, Workload = 150f },
            new ConstructionTask { ID = 57, ConstructionPlanID = 31, ConstructionStatusID = 2, Workload = 100f },

            // Tasks cho ConstructionPlan ID = 32 (Đổ móng, Volume = 900)
            new ConstructionTask { ID = 58, ConstructionPlanID = 32, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 59, ConstructionPlanID = 32, ConstructionStatusID = 2, Workload = 200f },
            new ConstructionTask { ID = 60, ConstructionPlanID = 32, ConstructionStatusID = 2, Workload = 250f },
            new ConstructionTask { ID = 61, ConstructionPlanID = 32, ConstructionStatusID = 2, Workload = 150f },

            // Tasks cho ConstructionPlan ID = 33 (Dựng cột, Volume = 1800)
            new ConstructionTask { ID = 62, ConstructionPlanID = 33, ConstructionStatusID = 2, Workload = 500f },
            new ConstructionTask { ID = 63, ConstructionPlanID = 33, ConstructionStatusID = 2, Workload = 400f },
            new ConstructionTask { ID = 64, ConstructionPlanID = 33, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 65, ConstructionPlanID = 33, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 66, ConstructionPlanID = 33, ConstructionStatusID = 2, Workload = 300f },

            // Tasks cho ConstructionPlan ID = 34 (Xây tường, Volume = 30000)
            new ConstructionTask { ID = 67, ConstructionPlanID = 34, ConstructionStatusID = 2, Workload = 8000f },
            new ConstructionTask { ID = 68, ConstructionPlanID = 34, ConstructionStatusID = 2, Workload = 7000f },
            new ConstructionTask { ID = 69, ConstructionPlanID = 34, ConstructionStatusID = 2, Workload = 6000f },
            new ConstructionTask { ID = 70, ConstructionPlanID = 34, ConstructionStatusID = 2, Workload = 5000f },
            new ConstructionTask { ID = 71, ConstructionPlanID = 34, ConstructionStatusID = 2, Workload = 4000f },

            // Tasks cho ConstructionPlan ID = 35 (Điện âm, Volume = 2000)
            new ConstructionTask { ID = 72, ConstructionPlanID = 35, ConstructionStatusID = 2, Workload = 500f },
            new ConstructionTask { ID = 73, ConstructionPlanID = 35, ConstructionStatusID = 2, Workload = 400f },
            new ConstructionTask { ID = 74, ConstructionPlanID = 35, ConstructionStatusID = 2, Workload = 400f },
            new ConstructionTask { ID = 75, ConstructionPlanID = 35, ConstructionStatusID = 2, Workload = 400f },
            new ConstructionTask { ID = 76, ConstructionPlanID = 35, ConstructionStatusID = 2, Workload = 300f },

            // Tasks cho ConstructionPlan ID = 36 (Ốp lát gạch nền, Volume = 2500)
            new ConstructionTask { ID = 77, ConstructionPlanID = 36, ConstructionStatusID = 2, Workload = 600f },
            new ConstructionTask { ID = 78, ConstructionPlanID = 36, ConstructionStatusID = 2, Workload = 600f },
            new ConstructionTask { ID = 79, ConstructionPlanID = 36, ConstructionStatusID = 2, Workload = 500f },
            new ConstructionTask { ID = 80, ConstructionPlanID = 36, ConstructionStatusID = 2, Workload = 400f },
            new ConstructionTask { ID = 81, ConstructionPlanID = 36, ConstructionStatusID = 2, Workload = 400f },

            // Tasks cho ConstructionPlan ID = 37 (Sơn nước, Volume = 4000)
            new ConstructionTask { ID = 82, ConstructionPlanID = 37, ConstructionStatusID = 2, Workload = 1000f },
            new ConstructionTask { ID = 83, ConstructionPlanID = 37, ConstructionStatusID = 2, Workload = 1000f },
            new ConstructionTask { ID = 84, ConstructionPlanID = 37, ConstructionStatusID = 2, Workload = 800f },
            new ConstructionTask { ID = 85, ConstructionPlanID = 37, ConstructionStatusID = 2, Workload = 700f },
            new ConstructionTask { ID = 86, ConstructionPlanID = 37, ConstructionStatusID = 2, Workload = 500f },

            // Tasks cho ConstructionPlan ID = 38 (Nước sinh hoạt, Volume = 70)
            new ConstructionTask { ID = 87, ConstructionPlanID = 38, ConstructionStatusID = 2, Workload = 20f },
            new ConstructionTask { ID = 88, ConstructionPlanID = 38, ConstructionStatusID = 2, Workload = 20f },
            new ConstructionTask { ID = 89, ConstructionPlanID = 38, ConstructionStatusID = 2, Workload = 15f },
            new ConstructionTask { ID = 90, ConstructionPlanID = 38, ConstructionStatusID = 2, Workload = 15f },

            // Tasks cho ConstructionPlan ID = 39 (Mái nhà, Volume = 1000)
            new ConstructionTask { ID = 91, ConstructionPlanID = 39, ConstructionStatusID = 2, Workload = 300 },
            new ConstructionTask { ID = 92, ConstructionPlanID = 39, ConstructionStatusID = 2, Workload = 250 },
            new ConstructionTask { ID = 93, ConstructionPlanID = 39, ConstructionStatusID = 2, Workload = 200 },
            new ConstructionTask { ID = 94, ConstructionPlanID = 39, ConstructionStatusID = 2, Workload = 150 },
            new ConstructionTask { ID = 95, ConstructionPlanID = 39, ConstructionStatusID = 2, Workload = 100f },

            // Tasks cho ConstructionPlan ID = 40 (Sân vườn, Volume = 800)
            new ConstructionTask { ID = 96, ConstructionPlanID = 40, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 97, ConstructionPlanID = 40, ConstructionStatusID = 2, Workload = 200f },
            new ConstructionTask { ID = 98, ConstructionPlanID = 40, ConstructionStatusID = 2, Workload = 200f },
            new ConstructionTask { ID = 99, ConstructionPlanID = 40, ConstructionStatusID = 2, Workload = 100f },

            // Cầu An Hòa
            // ConstructionItemID = 31: Thi công nền đường (TotalVolume = 2500)
            // Kế hoạch: 41, 42
            new ConstructionTask { ID = 100, ConstructionPlanID = 41, ConstructionStatusID = 3, Workload = 1300f },
            new ConstructionTask { ID = 101, ConstructionPlanID = 41, ConstructionStatusID = 3, Workload = 200f },
            new ConstructionTask { ID = 102, ConstructionPlanID = 42, ConstructionStatusID = 3, Workload = 600f },
            new ConstructionTask { ID = 103, ConstructionPlanID = 42, ConstructionStatusID = 3, Workload = 400f },

            // ConstructionItemID = 32: Lắp đặt móng cầu (TotalVolume = 1200)
            // Kế hoạch: 43, 44
            new ConstructionTask { ID = 104, ConstructionPlanID = 43, ConstructionStatusID = 3, Workload = 700f },
            new ConstructionTask { ID = 105, ConstructionPlanID = 44, ConstructionStatusID = 3, Workload = 300f },
            new ConstructionTask { ID = 106, ConstructionPlanID = 44, ConstructionStatusID = 3, Workload = 200f },

            // ConstructionItemID = 33: Đổ bê tông cầu (TotalVolume = 1500)
            // Kế hoạch: 45, 46
            new ConstructionTask { ID = 107, ConstructionPlanID = 45, ConstructionStatusID = 2, Workload = 400f },
            new ConstructionTask { ID = 108, ConstructionPlanID = 45, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 109, ConstructionPlanID = 46, ConstructionStatusID = 2, Workload = 500f },
            new ConstructionTask { ID = 110, ConstructionPlanID = 46, ConstructionStatusID = 2, Workload = 300f },

            // ConstructionItemID = 34: Lắp đặt cầu giao thông (TotalVolume = 500)
            // Kế hoạch: 47
            new ConstructionTask { ID = 111, ConstructionPlanID = 47, ConstructionStatusID = 2, Workload = 150f },
            new ConstructionTask { ID = 112, ConstructionPlanID = 47, ConstructionStatusID = 2, Workload = 200f },
            new ConstructionTask { ID = 113, ConstructionPlanID = 47, ConstructionStatusID = 2, Workload = 150f },

            // ConstructionItemID = 35: Lắp đặt hệ thống thoát nước (TotalVolume = 800)
            // Kế hoạch: 48, 49
            new ConstructionTask { ID = 114, ConstructionPlanID = 48, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 115, ConstructionPlanID = 48, ConstructionStatusID = 2, Workload = 150f },
            new ConstructionTask { ID = 116, ConstructionPlanID = 49, ConstructionStatusID = 2, Workload = 250f },
            new ConstructionTask { ID = 117, ConstructionPlanID = 49, ConstructionStatusID = 2, Workload = 100f },

            // ConstructionItemID = 36: Hoàn thiện mặt cầu (TotalVolume = 1200)
            // Kế hoạch: 50
            new ConstructionTask { ID = 118, ConstructionPlanID = 50, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 119, ConstructionPlanID = 50, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 200, ConstructionPlanID = 50, ConstructionStatusID = 2, Workload = 300f },
            new ConstructionTask { ID = 201, ConstructionPlanID = 50, ConstructionStatusID = 2, Workload = 300f },

            // ConstructionItemID = 37: Thi công bảo trì (TotalVolume = 800)
            // Kế hoạch: 51
            new ConstructionTask { ID = 202, ConstructionPlanID = 51, ConstructionStatusID = 2, Workload = 250f },
            new ConstructionTask { ID = 203, ConstructionPlanID = 51, ConstructionStatusID = 2, Workload = 250f },
            new ConstructionTask { ID = 204, ConstructionPlanID = 51, ConstructionStatusID = 2, Workload = 300f },

            // ConstructionItemID = 38: Lắp đặt hệ thống chiếu sáng cầu (TotalVolume = 50)
            // Kế hoạch: 52
            new ConstructionTask { ID = 205, ConstructionPlanID = 52, ConstructionStatusID = 2, Workload = 20f },
            new ConstructionTask { ID = 206, ConstructionPlanID = 52, ConstructionStatusID = 2, Workload = 15f },
            new ConstructionTask { ID = 207, ConstructionPlanID = 52, ConstructionStatusID = 2, Workload = 15f },

            // Đường tránh QL1A - Phù Mỹ
            // ConstructionItem 39 - Tổng Workload = 4000f
            new ConstructionTask { ID = 208, ConstructionPlanID = 53, ConstructionStatusID = 3, Workload = 1800f },
            new ConstructionTask { ID = 209, ConstructionPlanID = 53, ConstructionStatusID = 3, Workload = 200f },
            new ConstructionTask { ID = 210, ConstructionPlanID = 54, ConstructionStatusID = 3, Workload = 1500f },
            new ConstructionTask { ID = 211, ConstructionPlanID = 54, ConstructionStatusID = 3, Workload = 500f },

            // ConstructionItem 40 - Tổng Workload = 5000f
            new ConstructionTask { ID = 212, ConstructionPlanID = 55, ConstructionStatusID = 3, Workload = 2300f },
            new ConstructionTask { ID = 213, ConstructionPlanID = 56, ConstructionStatusID = 3, Workload = 1800f },
            new ConstructionTask { ID = 214, ConstructionPlanID = 56, ConstructionStatusID = 3, Workload = 900f },

            // ConstructionItem 41 - Tổng Workload = 1500f
            new ConstructionTask { ID = 215, ConstructionPlanID = 57, ConstructionStatusID = 3, Workload = 400f },
            new ConstructionTask { ID = 216, ConstructionPlanID = 57, ConstructionStatusID = 3, Workload = 300f },
            new ConstructionTask { ID = 217, ConstructionPlanID = 57, ConstructionStatusID = 3, Workload = 300f },
            new ConstructionTask { ID = 218, ConstructionPlanID = 57, ConstructionStatusID = 3, Workload = 250f },
            new ConstructionTask { ID = 219, ConstructionPlanID = 57, ConstructionStatusID = 3, Workload = 250f },

            // ConstructionItem 42 - Tổng Workload = 600f
            new ConstructionTask { ID = 220, ConstructionPlanID = 58, ConstructionStatusID = 3, Workload = 300f },
            new ConstructionTask { ID = 221, ConstructionPlanID = 59, ConstructionStatusID = 3, Workload = 300f },

            // ConstructionItem 43 - Tổng Workload = 100f
            new ConstructionTask { ID = 222, ConstructionPlanID = 60, ConstructionStatusID = 3, Workload = 60f },
            new ConstructionTask { ID = 223, ConstructionPlanID = 61, ConstructionStatusID = 3, Workload = 40f },

            // ConstructionItem 44 - Tổng Workload = 1f
            new ConstructionTask { ID = 224, ConstructionPlanID = 62, ConstructionStatusID = 3, Workload = 0.5f },
            new ConstructionTask { ID = 225, ConstructionPlanID = 63, ConstructionStatusID = 3, Workload = 0.5f },

            // Nhà máy sản xuất thép An Phát
            // Hạng mục ID = 45 (Completed)
            new ConstructionTask { ID = 226, ConstructionPlanID = 64, ConstructionStatusID = 3, Workload = 6000f },
            new ConstructionTask { ID = 227, ConstructionPlanID = 65, ConstructionStatusID = 3, Workload = 6000f },

            // Hạng mục ID = 46 (Completed)
            new ConstructionTask { ID = 228, ConstructionPlanID = 66, ConstructionStatusID = 3, Workload = 2500f },
            new ConstructionTask { ID = 229, ConstructionPlanID = 67, ConstructionStatusID = 3, Workload = 2500f },

            // Hạng mục ID = 47 (In Progress)
            new ConstructionTask { ID = 230, ConstructionPlanID = 68, ConstructionStatusID = 2, Workload = 175f },
            new ConstructionTask { ID = 231, ConstructionPlanID = 69, ConstructionStatusID = 2, Workload = 175f },

            // Hạng mục ID = 48 (Paused)
            new ConstructionTask { ID = 232, ConstructionPlanID = 70, ConstructionStatusID = 4, Workload = 22.5f },
            new ConstructionTask { ID = 233, ConstructionPlanID = 70, ConstructionStatusID = 4, Workload = 22.5f },

            // Hạng mục ID = 49 (Pending)
            new ConstructionTask { ID = 234, ConstructionPlanID = 71, ConstructionStatusID = 1, Workload = 900f },
            new ConstructionTask { ID = 235, ConstructionPlanID = 71, ConstructionStatusID = 1, Workload = 900f },

            // Hạng mục ID = 50 (Pending)
            new ConstructionTask { ID = 236, ConstructionPlanID = 72, ConstructionStatusID = 1, Workload = 1f },
            new ConstructionTask { ID = 237, ConstructionPlanID = 72, ConstructionStatusID = 1, Workload = 1f },

            // Đập thủy lợi Phú Tài
            // ConstructionItemID = 51, Completed (Khảo sát địa chất)
            new ConstructionTask { ID = 238, ConstructionPlanID = 73, ConstructionStatusID = 3, Workload = 1f },

            // ConstructionItemID = 52, Completed (San lấp mặt bằng)
            new ConstructionTask { ID = 239, ConstructionPlanID = 75, ConstructionStatusID = 3, Workload = 5000f },
            new ConstructionTask { ID = 240, ConstructionPlanID = 76, ConstructionStatusID = 3, Workload = 5000f },

            // ConstructionItemID = 53, In Progress (Đào hố móng đập)
            new ConstructionTask { ID = 241, ConstructionPlanID = 77, ConstructionStatusID = 2, Workload = 3500f },
            new ConstructionTask { ID = 242, ConstructionPlanID = 78, ConstructionStatusID = 2, Workload = 3500f },

            // ConstructionItemID = 54, Paused (Lắp cống xả đáy)
            new ConstructionTask { ID = 243, ConstructionPlanID = 79, ConstructionStatusID = 4, Workload = 3f },

            // ConstructionItemID = 55, Pending (Xây thân đập)
            new ConstructionTask { ID = 244, ConstructionPlanID = 80, ConstructionStatusID = 1, Workload = 7500f },
            new ConstructionTask { ID = 245, ConstructionPlanID = 81, ConstructionStatusID = 1, Workload = 7500f },

            // ConstructionItemID = 56, Pending (Làm đường công vụ)
            new ConstructionTask { ID = 246, ConstructionPlanID = 82, ConstructionStatusID = 1, Workload = 1250f },
            new ConstructionTask { ID = 247, ConstructionPlanID = 82, ConstructionStatusID = 1, Workload = 1250f }

        );

            // Seed data for WorkAttribute
            modelBuilder.Entity<WorkAttribute>().HasData(
    // Thuộc tính cho san lấp mặt bằng
    new WorkAttribute { ID = 1, UnitOfMeasurementID = 6, WorkAttributeName = "Chiều sâu san lấp" }, // mét
    new WorkAttribute { ID = 2, UnitOfMeasurementID = 13, WorkAttributeName = "Vật liệu san lấp: Cát" },
    new WorkAttribute { ID = 3, UnitOfMeasurementID = 13, WorkAttributeName = "Vật liệu san lấp: Đất" },

    // Thuộc tính cho đào đất
    new WorkAttribute { ID = 4, UnitOfMeasurementID = 6, WorkAttributeName = "Chiều sâu đào" },
    new WorkAttribute { ID = 5, UnitOfMeasurementID = 13, WorkAttributeName = "Loại đất" },

    // Thuộc tính cho phá dỡ
    new WorkAttribute { ID = 6, UnitOfMeasurementID = 13, WorkAttributeName = "Loại kết cấu cần phá dỡ" },
    new WorkAttribute { ID = 7, UnitOfMeasurementID = 6, WorkAttributeName = "Độ dày kết cấu" },

    // Thuộc tính cho đổ móng
    new WorkAttribute { ID = 8, UnitOfMeasurementID = 2, WorkAttributeName = "Chiều cao móng" },
    new WorkAttribute { ID = 9, UnitOfMeasurementID = 13, WorkAttributeName = "Loại móng: Băng/Đơn/Bè" },

    // Thuộc tính cho đổ bê tông cột, dầm, sàn
    new WorkAttribute { ID = 10, UnitOfMeasurementID = 13, WorkAttributeName = "Loại bê tông" },
    new WorkAttribute { ID = 11, UnitOfMeasurementID = 6, WorkAttributeName = "Chiều cao cột" },
    new WorkAttribute { ID = 12, UnitOfMeasurementID = 2, WorkAttributeName = "Chiều dày sàn" },

    // Thuộc tính cho xây tường
    new WorkAttribute { ID = 13, UnitOfMeasurementID = 13, WorkAttributeName = "Loại gạch" },
    new WorkAttribute { ID = 14, UnitOfMeasurementID = 2, WorkAttributeName = "Chiều dày tường" },

    // Thuộc tính cho hệ thống điện nước
    new WorkAttribute { ID = 15, UnitOfMeasurementID = 13, WorkAttributeName = "Loại hệ thống: Âm/Nổi" },
    new WorkAttribute { ID = 16, UnitOfMeasurementID = 6, WorkAttributeName = "Chiều dài ống" },

    // Thuộc tính cho ốp lát
    new WorkAttribute { ID = 17, UnitOfMeasurementID = 13, WorkAttributeName = "Loại vật liệu ốp lát" },
    new WorkAttribute { ID = 18, UnitOfMeasurementID = 2, WorkAttributeName = "Kích thước viên gạch" },

    // Thuộc tính cho sơn nước
    new WorkAttribute { ID = 19, UnitOfMeasurementID = 13, WorkAttributeName = "Loại sơn" },
    new WorkAttribute { ID = 20, UnitOfMeasurementID = 6, WorkAttributeName = "Số lớp sơn" },

    // Thuộc tính cho lắp đặt cửa
    new WorkAttribute { ID = 21, UnitOfMeasurementID = 13, WorkAttributeName = "Loại cửa" },
    new WorkAttribute { ID = 22, UnitOfMeasurementID = 2, WorkAttributeName = "Kích thước cửa" },

    // Thuộc tính cho thiết bị vệ sinh
    new WorkAttribute { ID = 23, UnitOfMeasurementID = 13, WorkAttributeName = "Loại thiết bị vệ sinh" },

    // Thuộc tính cho thang máy
    new WorkAttribute { ID = 24, UnitOfMeasurementID = 13, WorkAttributeName = "Loại thang máy" },
    new WorkAttribute { ID = 25, UnitOfMeasurementID = 6, WorkAttributeName = "Số tầng phục vụ" },

    // Thuộc tính cho sân vườn
    new WorkAttribute { ID = 26, UnitOfMeasurementID = 13, WorkAttributeName = "Loại cảnh quan" },
    new WorkAttribute { ID = 27, UnitOfMeasurementID = 6, WorkAttributeName = "Diện tích thi công" },

    // Thuộc tính cho hệ thống chiếu sáng
    new WorkAttribute { ID = 28, UnitOfMeasurementID = 13, WorkAttributeName = "Loại bóng đèn" },
    new WorkAttribute { ID = 29, UnitOfMeasurementID = 6, WorkAttributeName = "Chiều dài dây điện" },

    // Thuộc tính trát tường
    new WorkAttribute { ID = 30, UnitOfMeasurementID = 6, WorkAttributeName = "Chiều cao tường" },
    new WorkAttribute { ID = 31, UnitOfMeasurementID = 2, WorkAttributeName = "Chiều dày lớp trát" }
);


            // Seed data for WorkSubTypeVariant_WorkAttribute
            modelBuilder.Entity<WorkSubTypeVariant_WorkAttribute>().HasData(
                // San lấp bằng cát
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 1, WorkAttributeID = 1, Value = "2" }, // Chiều sâu san lấp
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 1, WorkAttributeID = 2, Value = "Cát" },

                // San lấp bằng đất
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 2, WorkAttributeID = 1, Value = "2" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 2, WorkAttributeID = 3, Value = "Đất" },

                // Đào đất sét
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 3, WorkAttributeID = 4, Value = "3" }, // Chiều sâu đào
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 3, WorkAttributeID = 5, Value = "Đất sét" },

                // Đào đất pha cát
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 4, WorkAttributeID = 4, Value = "3" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 4, WorkAttributeID = 5, Value = "Đất pha cát" },

                // Phá dỡ tường gạch
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 5, WorkAttributeID = 6, Value = "Tường gạch" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 5, WorkAttributeID = 7, Value = "0.2" }, // Độ dày kết cấu

                // Phá dỡ bê tông cốt thép
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 6, WorkAttributeID = 6, Value = "Bê tông cốt thép" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 6, WorkAttributeID = 7, Value = "0.3" },

                // Móng băng
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 7, WorkAttributeID = 8, Value = "1.5" }, // Chiều cao móng
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 7, WorkAttributeID = 9, Value = "Móng băng" },

                // Móng đơn
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 8, WorkAttributeID = 8, Value = "1.2" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 8, WorkAttributeID = 9, Value = "Móng đơn" },

                // Cột vuông
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 9, WorkAttributeID = 10, Value = "Bê tông M250" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 9, WorkAttributeID = 11, Value = "3.0" },

                // Cột tròn
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 10, WorkAttributeID = 10, Value = "Bê tông M300" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 10, WorkAttributeID = 11, Value = "3.0" },

                // Dầm chịu lực chính
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 11, WorkAttributeID = 10, Value = "Bê tông M250" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 11, WorkAttributeID = 12, Value = "0.2" },

                // Sàn không dầm
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 12, WorkAttributeID = 10, Value = "Bê tông nhẹ" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 12, WorkAttributeID = 12, Value = "0.15" },

                // Tường gạch đặc
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 13, WorkAttributeID = 13, Value = "Gạch đặc" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 13, WorkAttributeID = 14, Value = "0.22" },

                // Tường gạch lỗ
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 14, WorkAttributeID = 13, Value = "Gạch lỗ" },
                new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 14, WorkAttributeID = 14, Value = "0.15" },

// Biến thể cho Xây tường ngăn
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 15, WorkAttributeID = 13, Value = "Thạch cao" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 15, WorkAttributeID = 14, Value = "0.10" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 16, WorkAttributeID = 13, Value = "Gạch nhẹ" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 16, WorkAttributeID = 14, Value = "0.15" },

// Biến thể cho Lắp đặt hệ thống điện
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 17, WorkAttributeID = 15, Value = "Âm tường" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 17, WorkAttributeID = 16, Value = "50" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 18, WorkAttributeID = 15, Value = "Nổi" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 18, WorkAttributeID = 16, Value = "70" },

// Biến thể cho Lắp đặt hệ thống nước
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 19, WorkAttributeID = 15, Value = "PVC" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 19, WorkAttributeID = 16, Value = "25" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 20, WorkAttributeID = 15, Value = "PPR" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 20, WorkAttributeID = 16, Value = "32" },

// Biến thể cho Ốp lát
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 21, WorkAttributeID = 17, Value = "Gạch ceramic" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 21, WorkAttributeID = 18, Value = "30x30" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 22, WorkAttributeID = 17, Value = "Đá tự nhiên" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 22, WorkAttributeID = 18, Value = "40x40" },

// Biến thể cho Sơn nước
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 23, WorkAttributeID = 19, Value = "Sơn nước nội thất" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 23, WorkAttributeID = 20, Value = "2" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 24, WorkAttributeID = 19, Value = "Sơn nước ngoại thất" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 24, WorkAttributeID = 20, Value = "3" },

// Biến thể cho Trát tường
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 25, WorkAttributeID = 13, Value = "5" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 25, WorkAttributeID = 14, Value = "0.03" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 26, WorkAttributeID = 13, Value = "10" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 26, WorkAttributeID = 14, Value = "0.02" },

// Biến thể cho Lắp đặt cửa
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 27, WorkAttributeID = 21, Value = "Nhôm kính" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 27, WorkAttributeID = 22, Value = "2x2" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 28, WorkAttributeID = 21, Value = "Gỗ công nghiệp" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 28, WorkAttributeID = 22, Value = "2.5x2.5" },

// Biến thể cho Lắp đặt thiết bị vệ sinh
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 29, WorkAttributeID = 23, Value = "Bồn cầu treo tường" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 30, WorkAttributeID = 23, Value = "Chậu rửa đặt bàn" },

// Biến thể cho Lắp đặt thang máy
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 31, WorkAttributeID = 24, Value = "Tải khách" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 31, WorkAttributeID = 25, Value = "12" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 32, WorkAttributeID = 24, Value = "Tải hàng" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 32, WorkAttributeID = 25, Value = "6" },

// Biến thể cho Thi công sân vườn
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 33, WorkAttributeID = 26, Value = "Biệt thự" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 33, WorkAttributeID = 27, Value = "500" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 34, WorkAttributeID = 26, Value = "Công cộng" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 34, WorkAttributeID = 27, Value = "1000" },

// Biến thể cho Thi công hệ thống chiếu sáng
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 35, WorkAttributeID = 28, Value = "Đèn LED" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 35, WorkAttributeID = 29, Value = "100" },

new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 36, WorkAttributeID = 28, Value = "Đèn Sodium" },
new WorkSubTypeVariant_WorkAttribute { WorkSubTypeVariantID = 36, WorkAttributeID = 29, Value = "150" }
            );
            // Seed data for MaterialNorm
            modelBuilder.Entity<MaterialNorm>().HasData(
               // San lấp mặt bằng - WorkSubTypeVariantID = 1 (San lấp mặt bằng bằng cát)
               new MaterialNorm { MaterialID = 3, WorkSubTypeVariantID = 1, Quantity = 105 }, // Cát vàng (1.05m3/m3 san lấp)
               new MaterialNorm { MaterialID = 6, WorkSubTypeVariantID = 1, Quantity = 10 }, // Đá mi (Vật liệu khác, 10kg/m3)

               // San lấp mặt bằng - WorkSubTypeVariantID = 2 (San lấp mặt bằng bằng đất - Cho ConstructionItem 52)
               new MaterialNorm { MaterialID = 6, WorkSubTypeVariantID = 2, Quantity = 15 }, // Đá mi

               // Đào đất hố móng - WorkSubTypeVariantID = 3 (Cho ConstructionItem 53)
               new MaterialNorm { MaterialID = 48, WorkSubTypeVariantID = 3, Quantity = 1 }, // Răng cào (Phụ tùng máy đào)

               // Đổ móng bê tông - WorkSubTypeVariantID = 7 (Đổ móng băng)
               new MaterialNorm { MaterialID = 1, WorkSubTypeVariantID = 7, Quantity = 350 }, // Xi măng PC40
               new MaterialNorm { MaterialID = 3, WorkSubTypeVariantID = 7, Quantity = 650 }, // Cát vàng
               new MaterialNorm { MaterialID = 5, WorkSubTypeVariantID = 7, Quantity = 950 }, // Đá 1x2
               new MaterialNorm { MaterialID = 9, WorkSubTypeVariantID = 7, Quantity = 80 },  // Thép phi 6
               new MaterialNorm { MaterialID = 37, WorkSubTypeVariantID = 7, Quantity = 5 },  // Dây thép buộc

               // Đổ bê tông cột - WorkSubTypeVariantID = 10 (Đổ bê tông cột tròn)
               new MaterialNorm { MaterialID = 1, WorkSubTypeVariantID = 10, Quantity = 380 }, // Xi măng PC40
               new MaterialNorm { MaterialID = 3, WorkSubTypeVariantID = 10, Quantity = 620 }, // Cát vàng
               new MaterialNorm { MaterialID = 5, WorkSubTypeVariantID = 10, Quantity = 930 }, // Đá 1x2
               new MaterialNorm { MaterialID = 10, WorkSubTypeVariantID = 10, Quantity = 150 }, // Thép phi 14

               // Đổ bê tông sàn - WorkSubTypeVariantID = 13 (Cho ConstructionItem 55: Xây thân đập)
               new MaterialNorm { MaterialID = 1, WorkSubTypeVariantID = 13, Quantity = 340 }, // Xi măng PC40
               new MaterialNorm { MaterialID = 3, WorkSubTypeVariantID = 13, Quantity = 660 }, // Cát vàng
               new MaterialNorm { MaterialID = 5, WorkSubTypeVariantID = 13, Quantity = 940 }, // Đá 1x2
               new MaterialNorm { MaterialID = 10, WorkSubTypeVariantID = 13, Quantity = 100 }, // Thép phi 14

               // Xây tường bao - WorkSubTypeVariantID = 14 (Xây tường bao gạch ống)
               new MaterialNorm { MaterialID = 1, WorkSubTypeVariantID = 14, Quantity = 150 }, // Xi măng PC40
               new MaterialNorm { MaterialID = 4, WorkSubTypeVariantID = 14, Quantity = 300 }, // Cát mịn
               new MaterialNorm { MaterialID = 7, WorkSubTypeVariantID = 14, Quantity = 1000 }, // Gạch ống

               // Xây tường ngăn - WorkSubTypeVariantID = 15 (Xây tường ngăn gạch ba banh)
               new MaterialNorm { MaterialID = 1, WorkSubTypeVariantID = 15, Quantity = 100 }, // Xi măng PC40
               new MaterialNorm { MaterialID = 4, WorkSubTypeVariantID = 15, Quantity = 200 }, // Cát mịn
               new MaterialNorm { MaterialID = 8, WorkSubTypeVariantID = 15, Quantity = 500 }, // Gạch ba banh

               // Lắp đặt hệ thống điện - WorkSubTypeVariantID = 18 (Lắp đặt hệ thống điện âm tường)
               new MaterialNorm { MaterialID = 23, WorkSubTypeVariantID = 18, Quantity = 5 }, // Dây điện 2.5mm

               // Lắp đặt hệ thống nước - WorkSubTypeVariantID = 20 (Lắp đặt đường ống cấp nước)
               new MaterialNorm { MaterialID = 21, WorkSubTypeVariantID = 20, Quantity = 30 }, // Ống PPR

               // Lắp đặt thiết bị thoát nước - WorkSubTypeVariantID = 21 (Cho ConstructionItem 54: Lắp cống xả đáy)
               new MaterialNorm { MaterialID = 22, WorkSubTypeVariantID = 21, Quantity = 10 }, // Ống PVC 90mm
               new MaterialNorm { MaterialID = 1, WorkSubTypeVariantID = 21, Quantity = 50 }, // Xi măng PC40

               // Ốp lát - WorkSubTypeVariantID = 22 (Ốp lát gạch ceramic)
               new MaterialNorm { MaterialID = 15, WorkSubTypeVariantID = 22, Quantity = 400 }, // Gạch ceramic
               new MaterialNorm { MaterialID = 17, WorkSubTypeVariantID = 22, Quantity = 25 }, // Keo dán gạch
               new MaterialNorm { MaterialID = 18, WorkSubTypeVariantID = 22, Quantity = 5 }, // Keo chà ron

               // Sơn nước - WorkSubTypeVariantID = 24 (Sơn nước nội thất)
               new MaterialNorm { MaterialID = 13, WorkSubTypeVariantID = 24, Quantity = 10 }, // Sơn lót
               new MaterialNorm { MaterialID = 14, WorkSubTypeVariantID = 24, Quantity = 15 }, // Sơn hoàn thiện

               // Lắp đặt cửa - WorkSubTypeVariantID = 28 (Lắp cửa kính)
               new MaterialNorm { MaterialID = 35, WorkSubTypeVariantID = 28, Quantity = 20 }, // Kính cường lực
               new MaterialNorm { MaterialID = 38, WorkSubTypeVariantID = 28, Quantity = 8 }, // Thanh nhôm hộp

               // Thi công sân vườn - WorkSubTypeVariantID = 35 (Cho ConstructionItem 56: Làm đường công vụ)
               new MaterialNorm { MaterialID = 3, WorkSubTypeVariantID = 35, Quantity = 200 }, // Cát vàng
               new MaterialNorm { MaterialID = 6, WorkSubTypeVariantID = 35, Quantity = 300 } // Đá mi
           );

            // Seed data for ExportOrder
            modelBuilder.Entity<ExportOrder>().HasData(
                new ExportOrder { ID = 1, EmployeeID = "manager1-id", ConstructionItemID = 1, ExportDate = new DateTime(2023, 01, 01) },
                new ExportOrder { ID = 2, EmployeeID = "manager2-id", ConstructionItemID = 2, ExportDate = new DateTime(2023, 01, 01) }
            );

            // Seed data for Material_ExportOrder
            modelBuilder.Entity<Material_ExportOrder>().HasData(
                new Material_ExportOrder { ExportOrderID = 1, MaterialID = 1, Quantity = 20 },
                new Material_ExportOrder { ExportOrderID = 1, MaterialID = 2, Quantity = 10 },
                new Material_ExportOrder { ExportOrderID = 2, MaterialID = 3, Quantity = 15 }
            );

            // Seed data for ImportOrder
            modelBuilder.Entity<ImportOrder>().HasData(
                new ImportOrder { ID = 1, ImportDate = new DateTime(2024, 01, 01), Status = ImportOrderStatusEnum.Approved },
                new ImportOrder { ID = 2, ImportDate = new DateTime(2024, 01, 05), Status = ImportOrderStatusEnum.Pending }
            );

            // Seed data for ImportOrderEmployee
            modelBuilder.Entity<ImportOrderEmployee>().HasData(
                new ImportOrderEmployee { EmployeeID = "manager3-id", ImportOrderId = 1, Role = ImportOrderRoleEnum.Planner },
                new ImportOrderEmployee { EmployeeID = "manager3-id", ImportOrderId = 2, Role = ImportOrderRoleEnum.Planner }
            );

            // Seed data for Report
            modelBuilder.Entity<Report>().HasData(
                // ===== Sự cố kỹ thuật =====
                new Report { ID = 1, EmployeeID = "manager1-id", ConstructionID = 1, Content = "Báo cáo tiến độ ngày 1", Level = "Cao", ReportDate = new DateTime(2023, 01, 01), ReportType = "Sự cố kĩ thuật" },
                new Report { ID = 2, EmployeeID = "manager2-id", ConstructionID = 2, Content = "Báo cáo tiến độ ngày 2", Level = "Thấp", ReportDate = new DateTime(2023, 01, 01), ReportType = "Sự cố kĩ thuật" },
                new Report { ID = 3, EmployeeID = "manager1-id", ConstructionID = 1, Content = "Hệ thống điện gặp trục trặc", Level = "Cao", ReportDate = new DateTime(2023, 02, 10), ReportType = "Sự cố kĩ thuật" },
                new Report { ID = 4, EmployeeID = "manager2-id", ConstructionID = 3, Content = "Rò rỉ nước tại tầng hầm", Level = "Trung bình", ReportDate = new DateTime(2023, 03, 05), ReportType = "Sự cố kĩ thuật" },
                new Report { ID = 5, EmployeeID = "manager3-id", ConstructionID = 4, Content = "Thiết bị giám sát không hoạt động", Level = "Thấp", ReportDate = new DateTime(2023, 04, 12), ReportType = "Sự cố kĩ thuật" },

                // ===== Sự cố thi công =====
                new Report { ID = 6, EmployeeID = "manager1-id", ConstructionID = 1, Content = "Ngã giàn giáo tại khu A", Level = "Cao", ReportDate = new DateTime(2023, 01, 15), ReportType = "Sự cố thi công" },
                new Report { ID = 7, EmployeeID = "manager2-id", ConstructionID = 2, Content = "Máy xúc bị hỏng giữa ca", Level = "Trung bình", ReportDate = new DateTime(2023, 02, 01), ReportType = "Sự cố thi công" },
                new Report { ID = 8, EmployeeID = "manager3-id", ConstructionID = 3, Content = "Công nhân đình công", Level = "Cao", ReportDate = new DateTime(2023, 03, 20), ReportType = "Sự cố thi công" },
                new Report { ID = 9, EmployeeID = "manager2-id", ConstructionID = 4, Content = "Chậm tiến độ do mưa lớn", Level = "Thấp", ReportDate = new DateTime(2023, 04, 01), ReportType = "Sự cố thi công" },
                new Report { ID = 10, EmployeeID = "manager1-id", ConstructionID = 1, Content = "Vật liệu không đạt chất lượng", Level = "Trung bình", ReportDate = new DateTime(2023, 05, 05), ReportType = "Sự cố thi công" }
            );

            // Seed data for ReportAttachment
            modelBuilder.Entity<ReportAttachment>().HasData(
                new ReportAttachment { ID = 1, ReportID = 1, FilePath = "/uploads/report1.pdf", UploadDate = new DateTime(2023, 01, 01) },
                new ReportAttachment { ID = 2, ReportID = 2, FilePath = "/uploads/report2.pdf", UploadDate = new DateTime(2023, 01, 01) },
                new ReportAttachment { ID = 3, ReportID = 3, FilePath = "/uploads/report3.pdf", UploadDate = new DateTime(2023, 02, 10) },
                new ReportAttachment { ID = 4, ReportID = 4, FilePath = "/uploads/report4.pdf", UploadDate = new DateTime(2023, 03, 05) },
                new ReportAttachment { ID = 5, ReportID = 5, FilePath = "/uploads/report5.pdf", UploadDate = new DateTime(2023, 04, 12) },
                new ReportAttachment { ID = 6, ReportID = 6, FilePath = "/uploads/report6.pdf", UploadDate = new DateTime(2023, 01, 15) },
                new ReportAttachment { ID = 7, ReportID = 7, FilePath = "/uploads/report7.pdf", UploadDate = new DateTime(2023, 02, 01) },
                new ReportAttachment { ID = 8, ReportID = 8, FilePath = "/uploads/report8.pdf", UploadDate = new DateTime(2023, 03, 20) },
                new ReportAttachment { ID = 9, ReportID = 9, FilePath = "/uploads/report9.pdf", UploadDate = new DateTime(2023, 04, 01) },
                new ReportAttachment { ID = 10, ReportID = 10, FilePath = "/uploads/report10.pdf", UploadDate = new DateTime(2023, 05, 05) }
            );

            // Seed data for ReportStatusLog
            modelBuilder.Entity<ReportStatusLog>().HasData(
                new ReportStatusLog { ID = 1, ReportID = 1, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 01, 01) },
                new ReportStatusLog { ID = 2, ReportID = 2, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 01, 01) },
                new ReportStatusLog { ID = 3, ReportID = 3, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 02, 10) },
                new ReportStatusLog { ID = 4, ReportID = 4, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 03, 05) },
                new ReportStatusLog { ID = 5, ReportID = 5, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 04, 12) },
                new ReportStatusLog { ID = 6, ReportID = 6, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 01, 15) },
                new ReportStatusLog { ID = 7, ReportID = 7, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 02, 01) },
                new ReportStatusLog { ID = 8, ReportID = 8, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 03, 20) },
                new ReportStatusLog { ID = 9, ReportID = 9, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 04, 01) },
                new ReportStatusLog { ID = 10, ReportID = 10, Status = ReportStatusEnum.Pending, Note = "Đang chờ phê duyệt", ReportDate = new DateTime(2023, 05, 05) }
            );


            // Seed data for WorkType
            modelBuilder.Entity<WorkType>().HasData(
                new WorkType { ID = 1, WorkTypeName = "Công tác chuẩn bị mặt bằng" },
                new WorkType { ID = 2, WorkTypeName = "Công tác đổ bê tông" },
                new WorkType { ID = 3, WorkTypeName = "Công tác xây" },
                new WorkType { ID = 4, WorkTypeName = "Công tác điện nước" },
                new WorkType { ID = 5, WorkTypeName = "Công tác hoàn thiện" },
                new WorkType { ID = 6, WorkTypeName = "Công tác lắp đặt thiết bị" },
                new WorkType { ID = 7, WorkTypeName = "Công tác cảnh quan" }
            );

            // Seed data for WorkSubType
            modelBuilder.Entity<WorkSubType>().HasData(
                // Công tác chuẩn bị mặt bằng
                new WorkSubType { ID = 1, WorkTypeID = 1, WorkSubTypeName = "San lấp mặt bằng" },
                new WorkSubType { ID = 2, WorkTypeID = 1, WorkSubTypeName = "Đào đất" },
                new WorkSubType { ID = 3, WorkTypeID = 1, WorkSubTypeName = "Phá dỡ công trình cũ" },

                // Công tác đổ bê tông
                new WorkSubType { ID = 4, WorkTypeID = 2, WorkSubTypeName = "Đổ móng bê tông" },
                new WorkSubType { ID = 5, WorkTypeID = 2, WorkSubTypeName = "Đổ bê tông cột" },
                new WorkSubType { ID = 6, WorkTypeID = 2, WorkSubTypeName = "Đổ bê tông dầm sàn" },

                // Công tác xây
                new WorkSubType { ID = 7, WorkTypeID = 3, WorkSubTypeName = "Xây tường bao" },
                new WorkSubType { ID = 8, WorkTypeID = 3, WorkSubTypeName = "Xây tường ngăn" },

                // Công tác điện nước
                new WorkSubType { ID = 9, WorkTypeID = 4, WorkSubTypeName = "Lắp đặt hệ thống điện" },
                new WorkSubType { ID = 10, WorkTypeID = 4, WorkSubTypeName = "Lắp đặt hệ thống nước" },

                // Công tác hoàn thiện
                new WorkSubType { ID = 11, WorkTypeID = 5, WorkSubTypeName = "Ốp lát" },
                new WorkSubType { ID = 12, WorkTypeID = 5, WorkSubTypeName = "Sơn nước" },
                new WorkSubType { ID = 13, WorkTypeID = 5, WorkSubTypeName = "Trát tường" },

                // Công tác lắp đặt thiết bị
                new WorkSubType { ID = 14, WorkTypeID = 6, WorkSubTypeName = "Lắp đặt cửa" },
                new WorkSubType { ID = 15, WorkTypeID = 6, WorkSubTypeName = "Lắp đặt thiết bị vệ sinh" },
                new WorkSubType { ID = 16, WorkTypeID = 6, WorkSubTypeName = "Lắp đặt thang máy" },

                // Công tác cảnh quan
                new WorkSubType { ID = 17, WorkTypeID = 7, WorkSubTypeName = "Thi công sân vườn" },
                new WorkSubType { ID = 18, WorkTypeID = 7, WorkSubTypeName = "Thi công hệ thống chiếu sáng" }
            );

            // Seed data for WorkSubTypeVariant
            modelBuilder.Entity<WorkSubTypeVariant>().HasData(
                // Biến thể cho San lấp mặt bằng
                new WorkSubTypeVariant { ID = 1, WorkSubTypeID = 1, Description = "San lấp bằng cát" },
                new WorkSubTypeVariant { ID = 2, WorkSubTypeID = 1, Description = "San lấp bằng đất" },

                // Biến thể cho Đào đất
                new WorkSubTypeVariant { ID = 3, WorkSubTypeID = 2, Description = "Đào đất sét" },
                new WorkSubTypeVariant { ID = 4, WorkSubTypeID = 2, Description = "Đào đất pha cát" },

                // Biến thể cho Phá dỡ công trình cũ
                new WorkSubTypeVariant { ID = 5, WorkSubTypeID = 3, Description = "Phá dỡ tường gạch" },
                new WorkSubTypeVariant { ID = 6, WorkSubTypeID = 3, Description = "Phá dỡ bê tông cốt thép" },

                // Biến thể cho Đổ móng bê tông
                new WorkSubTypeVariant { ID = 7, WorkSubTypeID = 4, Description = "Móng băng" },
                new WorkSubTypeVariant { ID = 8, WorkSubTypeID = 4, Description = "Móng đơn" },

                // Biến thể cho Đổ bê tông cột
                new WorkSubTypeVariant { ID = 9, WorkSubTypeID = 5, Description = "Cột vuông" },
                new WorkSubTypeVariant { ID = 10, WorkSubTypeID = 5, Description = "Cột tròn" },

                // Biến thể cho Đổ bê tông dầm sàn
                new WorkSubTypeVariant { ID = 11, WorkSubTypeID = 6, Description = "Dầm chịu lực chính" },
                new WorkSubTypeVariant { ID = 12, WorkSubTypeID = 6, Description = "Sàn không dầm" },

                // Biến thể cho Xây tường bao
                new WorkSubTypeVariant { ID = 13, WorkSubTypeID = 7, Description = "Tường gạch đặc" },
                new WorkSubTypeVariant { ID = 14, WorkSubTypeID = 7, Description = "Tường gạch lỗ" },

                // Biến thể cho Xây tường ngăn
                new WorkSubTypeVariant { ID = 15, WorkSubTypeID = 8, Description = "Tường thạch cao" },
                new WorkSubTypeVariant { ID = 16, WorkSubTypeID = 8, Description = "Tường gạch nhẹ" },

                // Biến thể cho Lắp đặt hệ thống điện
                new WorkSubTypeVariant { ID = 17, WorkSubTypeID = 9, Description = "Điện âm tường" },
                new WorkSubTypeVariant { ID = 18, WorkSubTypeID = 9, Description = "Điện nổi" },

                // Biến thể cho Lắp đặt hệ thống nước
                new WorkSubTypeVariant { ID = 19, WorkSubTypeID = 10, Description = "Ống nước PVC" },
                new WorkSubTypeVariant { ID = 20, WorkSubTypeID = 10, Description = "Ống nước PPR" },

                // Biến thể cho Ốp lát
                new WorkSubTypeVariant { ID = 21, WorkSubTypeID = 11, Description = "Ốp lát gạch ceramic" },
                new WorkSubTypeVariant { ID = 22, WorkSubTypeID = 11, Description = "Ốp lát đá tự nhiên" },

                // Biến thể cho Sơn nước
                new WorkSubTypeVariant { ID = 23, WorkSubTypeID = 12, Description = "Sơn nước nội thất" },
                new WorkSubTypeVariant { ID = 24, WorkSubTypeID = 12, Description = "Sơn nước ngoại thất" },

                // Biến thể cho Trát tường
                new WorkSubTypeVariant { ID = 25, WorkSubTypeID = 13, Description = "Trát vữa xi măng cát" },
                new WorkSubTypeVariant { ID = 26, WorkSubTypeID = 13, Description = "Trát vữa xi măng mịn" },

                // Biến thể cho Lắp đặt cửa
                new WorkSubTypeVariant { ID = 27, WorkSubTypeID = 14, Description = "Cửa nhôm kính" },
                new WorkSubTypeVariant { ID = 28, WorkSubTypeID = 14, Description = "Cửa gỗ công nghiệp" },

                // Biến thể cho Lắp đặt thiết bị vệ sinh
                new WorkSubTypeVariant { ID = 29, WorkSubTypeID = 15, Description = "Bồn cầu treo tường" },
                new WorkSubTypeVariant { ID = 30, WorkSubTypeID = 15, Description = "Chậu rửa đặt bàn" },

                // Biến thể cho Lắp đặt thang máy
                new WorkSubTypeVariant { ID = 31, WorkSubTypeID = 16, Description = "Thang máy tải khách" },
                new WorkSubTypeVariant { ID = 32, WorkSubTypeID = 16, Description = "Thang máy tải hàng" },

                // Biến thể cho Thi công sân vườn
                new WorkSubTypeVariant { ID = 33, WorkSubTypeID = 17, Description = "Sân vườn biệt thự" },
                new WorkSubTypeVariant { ID = 34, WorkSubTypeID = 17, Description = "Sân vườn công cộng" },

                // Biến thể cho Thi công hệ thống chiếu sáng
                new WorkSubTypeVariant { ID = 35, WorkSubTypeID = 18, Description = "Chiếu sáng công viên" },
                new WorkSubTypeVariant { ID = 36, WorkSubTypeID = 18, Description = "Chiếu sáng đường phố" }
            );


            // Seed data for ConstructionTemplateItem
            modelBuilder.Entity<ConstructionTemplateItem>().HasData(
                // Template cho Nhà ở (ConstructionTypeID = 2)
                new ConstructionTemplateItem { ID = 1, ConstructionTypeID = 2, WorkSubTypeVarientID = 1, ConstructionTemplateItemName = "Chuẩn bị mặt bằng" },
                new ConstructionTemplateItem { ID = 2, ConstructionTypeID = 2, WorkSubTypeVarientID = 7, ConstructionTemplateItemName = "Thi công móng băng" },
                new ConstructionTemplateItem { ID = 3, ConstructionTypeID = 2, WorkSubTypeVarientID = 9, ConstructionTemplateItemName = "Thi công cột vuông" },
                new ConstructionTemplateItem { ID = 4, ConstructionTypeID = 2, WorkSubTypeVarientID = 11, ConstructionTemplateItemName = "Đổ bê tông dầm chịu lực" },
                new ConstructionTemplateItem { ID = 5, ConstructionTypeID = 2, WorkSubTypeVarientID = 13, ConstructionTemplateItemName = "Xây tường gạch đặc" },
                new ConstructionTemplateItem { ID = 6, ConstructionTypeID = 2, WorkSubTypeVarientID = 17, ConstructionTemplateItemName = "Lắp đặt hệ thống điện âm tường" },
                new ConstructionTemplateItem { ID = 7, ConstructionTypeID = 2, WorkSubTypeVarientID = 19, ConstructionTemplateItemName = "Lắp đặt hệ thống nước PVC" },
                new ConstructionTemplateItem { ID = 8, ConstructionTypeID = 2, WorkSubTypeVarientID = 21, ConstructionTemplateItemName = "Ốp lát gạch ceramic" },
                new ConstructionTemplateItem { ID = 9, ConstructionTypeID = 2, WorkSubTypeVarientID = 23, ConstructionTemplateItemName = "Sơn nước nội thất" },
                new ConstructionTemplateItem { ID = 10, ConstructionTypeID = 2, WorkSubTypeVarientID = 27, ConstructionTemplateItemName = "Lắp đặt cửa nhôm kính" },
                new ConstructionTemplateItem { ID = 11, ConstructionTypeID = 2, WorkSubTypeVarientID = 29, ConstructionTemplateItemName = "Lắp đặt thiết bị vệ sinh" },
                new ConstructionTemplateItem { ID = 12, ConstructionTypeID = 2, WorkSubTypeVarientID = 33, ConstructionTemplateItemName = "Thi công sân vườn biệt thự" },

                // Template cho Cầu đường (ConstructionTypeID = 1)
                new ConstructionTemplateItem { ID = 13, ConstructionTypeID = 1, WorkSubTypeVarientID = 1, ConstructionTemplateItemName = "Giải phóng mặt bằng bằng cát" },
                new ConstructionTemplateItem { ID = 14, ConstructionTypeID = 1, WorkSubTypeVarientID = 3, ConstructionTemplateItemName = "Đào đất sét nền móng" },
                new ConstructionTemplateItem { ID = 15, ConstructionTypeID = 1, WorkSubTypeVarientID = 5, ConstructionTemplateItemName = "Phá dỡ công trình cũ" },
                new ConstructionTemplateItem { ID = 16, ConstructionTypeID = 1, WorkSubTypeVarientID = 7, ConstructionTemplateItemName = "Đổ móng băng cầu" },
                new ConstructionTemplateItem { ID = 17, ConstructionTypeID = 1, WorkSubTypeVarientID = 9, ConstructionTemplateItemName = "Đổ bê tông cột cầu" },
                new ConstructionTemplateItem { ID = 18, ConstructionTypeID = 1, WorkSubTypeVarientID = 11, ConstructionTemplateItemName = "Đổ bê tông dầm cầu" },
                new ConstructionTemplateItem { ID = 19, ConstructionTypeID = 1, WorkSubTypeVarientID = 36, ConstructionTemplateItemName = "Thi công hệ thống chiếu sáng đường" },

                // Template cho Công nghiệp (ConstructionTypeID = 3)
                new ConstructionTemplateItem { ID = 20, ConstructionTypeID = 3, WorkSubTypeVarientID = 1, ConstructionTemplateItemName = "Chuẩn bị mặt bằng nhà máy" },
                new ConstructionTemplateItem { ID = 21, ConstructionTypeID = 3, WorkSubTypeVarientID = 7, ConstructionTemplateItemName = "Đổ móng băng chịu tải" },
                new ConstructionTemplateItem { ID = 22, ConstructionTypeID = 3, WorkSubTypeVarientID = 9, ConstructionTemplateItemName = "Thi công cột vuông nhà xưởng" },
                new ConstructionTemplateItem { ID = 23, ConstructionTypeID = 3, WorkSubTypeVarientID = 11, ConstructionTemplateItemName = "Đổ bê tông dầm chịu lực chính" },
                new ConstructionTemplateItem { ID = 24, ConstructionTypeID = 3, WorkSubTypeVarientID = 13, ConstructionTemplateItemName = "Xây tường gạch đặc nhà máy" },
                new ConstructionTemplateItem { ID = 25, ConstructionTypeID = 3, WorkSubTypeVarientID = 17, ConstructionTemplateItemName = "Lắp hệ thống điện âm" },
                new ConstructionTemplateItem { ID = 26, ConstructionTypeID = 3, WorkSubTypeVarientID = 19, ConstructionTemplateItemName = "Lắp hệ thống nước PVC" },
                new ConstructionTemplateItem { ID = 27, ConstructionTypeID = 3, WorkSubTypeVarientID = 35, ConstructionTemplateItemName = "Thi công chiếu sáng công viên" },

                // Template cho Thủy lợi (ConstructionTypeID = 4)
                new ConstructionTemplateItem { ID = 28, ConstructionTypeID = 4, WorkSubTypeVarientID = 1, ConstructionTemplateItemName = "Chuẩn bị mặt bằng đập/kenh" },
                new ConstructionTemplateItem { ID = 29, ConstructionTypeID = 4, WorkSubTypeVarientID = 3, ConstructionTemplateItemName = "Đào đất kênh/mương" },
                new ConstructionTemplateItem { ID = 30, ConstructionTypeID = 4, WorkSubTypeVarientID = 5, ConstructionTemplateItemName = "Phá dỡ công trình thủy lợi cũ" },
                new ConstructionTemplateItem { ID = 31, ConstructionTypeID = 4, WorkSubTypeVarientID = 7, ConstructionTemplateItemName = "Thi công móng cống đập" },
                new ConstructionTemplateItem { ID = 32, ConstructionTypeID = 4, WorkSubTypeVarientID = 9, ConstructionTemplateItemName = "Đổ cột công trình thủy" },
                new ConstructionTemplateItem { ID = 33, ConstructionTypeID = 4, WorkSubTypeVarientID = 11, ConstructionTemplateItemName = "Đổ dầm sàn kênh" },
                new ConstructionTemplateItem { ID = 34, ConstructionTypeID = 4, WorkSubTypeVarientID = 36, ConstructionTemplateItemName = "Chiếu sáng khu vực công trình" }
            );

            // Seed data for MaterialPlan
            modelBuilder.Entity<MaterialPlan>().HasData(
               // Link ImportOrder 1 to Plan 1 for Material 1 (Xi măng)
               new MaterialPlan { ImportOrderID = 1, ConstructionItemID = 1, MaterialID = 1, ImportQuantity = 50 },
               // Link ImportOrder 1 to Plan 1 for Material 3 (Cát vàng)
               new MaterialPlan { ImportOrderID = 1, ConstructionItemID = 1, MaterialID = 3, ImportQuantity = 100 },
               // Link ImportOrder 2 to Plan 3 for Material 10 (Thép phi 14)
               new MaterialPlan { ImportOrderID = 2, ConstructionItemID = 2, MaterialID = 10, ImportQuantity = 500 },
               // Link ImportOrder 2 to Plan 77 (Dam - Đào móng) for Material 48 (Răng cào) - Assuming Răng cào comes from ImportOrder 2
               new MaterialPlan { ImportOrderID = 2, ConstructionItemID = 2, MaterialID = 48, ImportQuantity = 2, }
           );

            modelBuilder.Entity<WorkShift>().HasData(
                new WorkShift { ID = 1, ShiftName = "Ca Hành Chính" }
                );
            modelBuilder.Entity<ShiftDetail>().HasData(
                new ShiftDetail
                {
                    ID = 1,
                    DayOfWeek = "Thứ 2",
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    BreakStart = new TimeSpan(12, 0, 0),
                    BreakEnd = new TimeSpan(13, 0, 0),
                    WorkShiftID = 1
                },
                new ShiftDetail
                {
                    ID = 2,
                    DayOfWeek = "Thứ 3",
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    BreakStart = new TimeSpan(12, 0, 0),
                    BreakEnd = new TimeSpan(13, 0, 0),
                    WorkShiftID = 1
                },
                 new ShiftDetail
                 {
                     ID = 3,
                     DayOfWeek = "Thứ 4",
                     StartTime = new TimeSpan(8, 0, 0),
                     EndTime = new TimeSpan(17, 0, 0),
                     BreakStart = new TimeSpan(12, 0, 0),
                     BreakEnd = new TimeSpan(13, 0, 0),
                     WorkShiftID = 1
                 },
                 new ShiftDetail
                 {
                     ID = 4,
                     DayOfWeek = "Thứ 5",
                     StartTime = new TimeSpan(8, 0, 0),
                     EndTime = new TimeSpan(17, 0, 0),
                     BreakStart = new TimeSpan(12, 0, 0),
                     BreakEnd = new TimeSpan(13, 0, 0),
                     WorkShiftID = 1
                 },
                 new ShiftDetail
                 {
                     ID = 5,
                     DayOfWeek = "Thứ 6",
                     StartTime = new TimeSpan(8, 0, 0),
                     EndTime = new TimeSpan(17, 0, 0),
                     BreakStart = new TimeSpan(12, 0, 0),
                     BreakEnd = new TimeSpan(13, 0, 0),
                     WorkShiftID = 1
                 },
                 new ShiftDetail
                 {
                     ID = 6,
                     DayOfWeek = "Thứ 7",
                     StartTime = new TimeSpan(8, 0, 0),
                     EndTime = new TimeSpan(12, 0, 0),
                     BreakStart = new TimeSpan(12, 0, 0),
                     BreakEnd = new TimeSpan(12, 0, 0),
                     WorkShiftID = 1
                 }
             );

            modelBuilder.Entity<ShiftAssignment>().HasData(
                new ShiftAssignment
                {
                    ID = 1,
                    EmployeeID = "manager1-id",
                    WorkShiftID = 1,
                    WorkDate = new DateTime(2025, 9, 12)
                },
                new ShiftAssignment
                {
                    ID = 2,
                    EmployeeID = "manager1-id",
                    WorkShiftID = 1,
                    WorkDate = new DateTime(2025, 9, 11)
                },
                 new ShiftAssignment
                 {
                     ID = 3,
                     EmployeeID = "manager1-id",
                     WorkShiftID = 1,
                     WorkDate = new DateTime(2025, 9, 10)
                 }
             );

            modelBuilder.Entity<Attendance>().HasData(
    new Attendance
    {
        ID = 1,
        ShiftAssignmentID = 1,
        CheckIn = new TimeSpan(8, 0, 0),
        CheckOut = new TimeSpan(17, 0, 0),
        ImageCheckIn = "/uploads/attendace/worker1-20240912-checkin.jpg",
        ImageCheckOut = "/uploads/attendace/worker1-20240912-checkin.jpg",
        Status = AttendanceStatusEnum.Present
    }
 );
            modelBuilder.Entity<AttendanceMachine>().HasData(
                new AttendanceMachine
                {
                    ID = 1,
                    AttendanceMachineName = "Máy chấm công Văn Phòng 1",
                    Longitude = "108.234567",
                    Latitude = "16.123456",
                    AllowedRadius = "50" // mét
                },
                new AttendanceMachine
                {
                    ID = 2,
                    AttendanceMachineName = "Máy chấm công Văn Phòng 2",
                    Longitude = "108.235678",
                    Latitude = "16.124567",
                    AllowedRadius = "50"
                },
                new AttendanceMachine
                {
                    ID = 3,
                    AttendanceMachineName = "Máy chấm công Nhà Xưởng",
                    Longitude = "108.236789",
                    Latitude = "16.125678",
                    AllowedRadius = "100"
                }
            );
        }
    }
}