using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using dotnet_api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.Json;

namespace dotnet_api.Services
{
    public class ConstructionService : IConstructionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private const string UPLOAD_DIRECTORY = "uploads/blueprints";

        public ConstructionService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<ConstructionDTO> CreateConstructionAsync(ConstructionCreateDTO constructionDTO)
        {
            try
            {
                Console.WriteLine($"Creating construction with data: {JsonSerializer.Serialize(constructionDTO)}");

                // Validate input data
                if (constructionDTO == null)
                    throw new ArgumentException("Dữ liệu công trình không hợp lệ");

                if (string.IsNullOrWhiteSpace(constructionDTO.ConstructionName))
                    throw new ArgumentException("Tên công trình không được để trống");

                if (string.IsNullOrWhiteSpace(constructionDTO.Location))
                    throw new ArgumentException("Địa điểm không được để trống");

                if (constructionDTO.TotalArea <= 0)
                    throw new ArgumentException("Diện tích phải lớn hơn 0");

                if (constructionDTO.ConstructionTypeID <= 0)
                    throw new ArgumentException("Loại công trình không hợp lệ");

                if (constructionDTO.StartDate >= constructionDTO.ExpectedCompletionDate)
                    throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu");

                // Validate ConstructionTypeID and ConstructionStatusID
                try
                {
                    Console.WriteLine($"Validating ConstructionTypeID={constructionDTO.ConstructionTypeID}...");
                    var constructionType = await _context.ConstructionTypes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(ct => ct.ID == constructionDTO.ConstructionTypeID);
                    if (constructionType == null)
                        throw new ArgumentException($"Không tìm thấy loại công trình với ID={constructionDTO.ConstructionTypeID}");

                    Console.WriteLine($"Validating ConstructionStatusID={constructionDTO.ConstructionStatusID}...");
                    var constructionStatus = await _context.ConstructionStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cs => cs.ID == constructionDTO.ConstructionStatusID);
                    if (constructionStatus == null)
                        throw new ArgumentException($"Không tìm thấy trạng thái công trình với ID={constructionDTO.ConstructionStatusID}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Validation error: {ex.Message}");
                    throw;
                }

                // Create new construction entity
                var construction = new Construction
                {
                    ConstructionTypeID = constructionDTO.ConstructionTypeID,
                    ConstructionStatusID = constructionDTO.ConstructionStatusID,
                    ConstructionName = constructionDTO.ConstructionName,
                    Location = constructionDTO.Location,
                    TotalArea = constructionDTO.TotalArea,
                    StartDate = constructionDTO.StartDate,
                    ExpectedCompletionDate = constructionDTO.ExpectedCompletionDate
                };

                // Handle file upload if provided
                if (constructionDTO.DesignBlueprint != null && constructionDTO.DesignBlueprint.Length > 0)
                {
                    try
                    {
                        // Validate file
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var extension = Path.GetExtension(constructionDTO.DesignBlueprint.FileName).ToLowerInvariant();
                        if (!allowedExtensions.Contains(extension))
                            throw new ArgumentException($"File không hợp lệ. Chỉ chấp nhận các định dạng: {string.Join(", ", allowedExtensions)}");

                        if (constructionDTO.DesignBlueprint.Length > 5 * 1024 * 1024) // 5MB limit
                            throw new ArgumentException("File không được vượt quá 5MB");

                        // Save file first
                        Console.WriteLine($"Processing file upload: {constructionDTO.DesignBlueprint.FileName}");
                        var filePath = await SaveDesignBlueprintAsync(constructionDTO.DesignBlueprint);
                        construction.DesignBlueprint = filePath;
                        Console.WriteLine($"File saved to: {filePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"File upload error: {ex.Message}");
                        throw new ApplicationException($"Lỗi khi lưu file thiết kế: {ex.Message}", ex);
                    }
                }

                try
                {
                    // Add and save construction
                    _context.Constructions.Add(construction);
                    Console.WriteLine("Attempting to save construction to database...");
                await _context.SaveChangesAsync();
                    Console.WriteLine($"Construction saved successfully with ID: {construction.ID}");

                    // Fetch the created construction with related data
            var createdConstruction = await _context.Constructions
                        .AsNoTracking()
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .FirstOrDefaultAsync(c => c.ID == construction.ID);

            if (createdConstruction == null)
                    {
                        throw new Exception("Không tìm thấy công trình sau khi tạo");
                    }

                    // Map to DTO
                    var result = new ConstructionDTO
                    {
                        ID = createdConstruction.ID,
                        ConstructionTypeID = createdConstruction.ConstructionTypeID,
                        ConstructionStatusID = createdConstruction.ConstructionStatusID,
                        ConstructionName = createdConstruction.ConstructionName,
                        Location = createdConstruction.Location,
                        TotalArea = createdConstruction.TotalArea,
                        StartDate = createdConstruction.StartDate,
                        ExpectedCompletionDate = createdConstruction.ExpectedCompletionDate,
                        DesignBlueprint = createdConstruction.DesignBlueprint,
                        StatusName = EnumHelper.GetDisplayName(createdConstruction.ConstructionStatus.Name),
                        ConstructionTypeName = EnumHelper.GetDisplayName(createdConstruction.ConstructionType.ConstructionTypeName)
                    };

                    return result;
                }
                catch (DbUpdateException dbEx)
                {
                    Console.WriteLine($"Database error: {dbEx.Message}");
                    if (dbEx.InnerException != null)
                    {
                        Console.WriteLine($"Inner database error: {dbEx.InnerException.Message}");
                        Console.WriteLine($"Stack trace: {dbEx.InnerException.StackTrace}");
                }
                    throw new ApplicationException("Lỗi khi lưu dữ liệu công trình vào database. Vui lòng kiểm tra lại thông tin.", dbEx);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateConstructionAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<ConstructionDTO> GetConstructionByIdAsync(int id)
        {
            var construction = await _context.Constructions
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(i => i.UnitOfMeasurement)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(i => i.WorkSubTypeVariant)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(i => i.ConstructionStatus)
                .FirstOrDefaultAsync(c => c.ID == id);

            return construction == null ? null : _mapper.Map<ConstructionDTO>(construction);
        }

        public async Task<IEnumerable<ConstructionDTO>> GetAllConstructionsAsync()
        {
            var constructions = await _context.Constructions
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(i => i.UnitOfMeasurement)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(i => i.WorkSubTypeVariant)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(i => i.ConstructionStatus)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionDTO>>(constructions);
        }

        public async Task<ConstructionDTO> UpdateConstructionAsync(ConstructionUpdateDTO constructionDTO)
        {
            var existingConstruction = await _context.Constructions
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .FirstOrDefaultAsync(c => c.ID == constructionDTO.ID);

            if (existingConstruction == null)
            {
                throw new Exception("Không tìm thấy công trình để cập nhật");
            }

            // Update basic properties
            existingConstruction.ConstructionTypeID = constructionDTO.ConstructionTypeID;
            existingConstruction.ConstructionName = constructionDTO.ConstructionName;
            existingConstruction.Location = constructionDTO.Location;
            existingConstruction.TotalArea = constructionDTO.TotalArea;
            existingConstruction.StartDate = constructionDTO.StartDate;
            existingConstruction.ExpectedCompletionDate = constructionDTO.ExpectedCompletionDate;

            // Handle file upload if provided
            if (constructionDTO.DesignBlueprint != null && constructionDTO.DesignBlueprint.Length > 0)
            {
                try
                {
                    // Delete old file if exists
                    if (!string.IsNullOrEmpty(existingConstruction.DesignBlueprint))
                    {
                        await DeleteDesignBlueprintAsync(existingConstruction.DesignBlueprint);
                    }

                    // Save new file
                    var filePath = await SaveDesignBlueprintAsync(constructionDTO.DesignBlueprint);
                    existingConstruction.DesignBlueprint = filePath;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Lỗi khi cập nhật file thiết kế: {ex.Message}", ex);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<ConstructionDTO>(existingConstruction);
            }
            catch (DbUpdateException dbEx)
            {
                throw new ApplicationException("Lỗi khi cập nhật dữ liệu công trình. Vui lòng kiểm tra lại thông tin.", dbEx);
            }
        }

        public async Task<ConstructionDTO> UpdateConstructionStatusAsync(int id, int status)
        {
            var existingConstruction = await _context.Constructions
                .FirstOrDefaultAsync(c => c.ID == id);

            if (existingConstruction == null)
            {
                throw new Exception("Không tìm thấy công trình để cập nhật trạng thái");
            }

            existingConstruction.ConstructionStatusID = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionDTO>(existingConstruction);
        }

        public async Task<string> SaveDesignBlueprintAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("No file uploaded");

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), UPLOAD_DIRECTORY);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Validate file extension
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    throw new ArgumentException($"File type not supported. Allowed types: {string.Join(", ", allowedExtensions)}");
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Path.Combine(UPLOAD_DIRECTORY, fileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error saving file: {ex.Message}", ex);
            }
        }

        public async Task DeleteDesignBlueprintAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return;

                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath.TrimStart('/'));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting file: {ex.Message}", ex);
            }
        }
    }
}
