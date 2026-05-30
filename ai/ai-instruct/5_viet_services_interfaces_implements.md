# Viet services interfaces va implements

## 1. Vai tro cua service

Service la noi chua logic nghiep vu.

Service can lam:

- Them doanh nghiep.
- Sua doanh nghiep.
- Xoa doanh nghiep.
- Check trung ten doanh nghiep.
- Check trung ma so thue.
- Lay danh sach co phan trang va keyword.
- Lay danh sach san pham nhap nhieu nhat cua mot doanh nghiep.

Service khong nen lam:

- Nhan HTTP request truc tiep.
- Tra `IActionResult`.
- Chua route API.

## 2. Tao interface service

Duong dan:

```text
Services/Interfaces/IEnterpriseService1234De1.cs
```

File nay dung de khai bao cac ham ma controller can goi.

Vi sao can file nay:

- Dung Dependency Injection.
- Controller phu thuoc vao abstraction thay vi class cu the.

Code:

```csharp
using NguyenVanA1234.Dtos.Common;
using NguyenVanA1234.Dtos.Enterprises;
using NguyenVanA1234.Dtos.Products;

namespace NguyenVanA1234.Services.Interfaces;

public interface IEnterpriseService1234De1
{
    Task<EnterpriseDto1234De1> CreateAsync(CreateEnterpriseDto1234De1 input);

    Task<EnterpriseDto1234De1> UpdateAsync(int id, UpdateEnterpriseDto1234De1 input);

    Task DeleteAsync(int id);

    Task<PagedResultDto1234De1<EnterpriseDto1234De1>> GetListAsync(FilterEnterpriseDto1234De1 input);

    Task<List<TopProductDto1234De1>> GetTopProductsAsync(int enterpriseId);
}
```

## 3. Tao implement service

Duong dan:

```text
Services/Implements/EnterpriseService1234De1.cs
```

File nay dung de viet logic that.

Vi sao can file nay:

- Tach controller khoi logic database.
- Tap trung validate nghiep vu vao mot noi.

File nay khong nen chua:

- Route API.
- `IActionResult`.
- Swagger attribute.

Code:

```csharp
using Microsoft.EntityFrameworkCore;
using NguyenVanA1234.Constants;
using NguyenVanA1234.DbContexts;
using NguyenVanA1234.Dtos.Common;
using NguyenVanA1234.Dtos.Enterprises;
using NguyenVanA1234.Dtos.Products;
using NguyenVanA1234.Entities;
using NguyenVanA1234.Exceptions;
using NguyenVanA1234.Services.Interfaces;

namespace NguyenVanA1234.Services.Implements;

public class EnterpriseService1234De1 : IEnterpriseService1234De1
{
    private readonly AppDbContext1234De1 _dbContext;

    public EnterpriseService1234De1(AppDbContext1234De1 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EnterpriseDto1234De1> CreateAsync(CreateEnterpriseDto1234De1 input)
    {
        await ValidateEnterpriseUniqueAsync(input.Name, input.TaxCode);

        var enterprise = new Enterprise1234De1
        {
            Name = input.Name,
            TaxCode = input.TaxCode,
            Address = input.Address
        };

        _dbContext.Enterprises.Add(enterprise);
        await _dbContext.SaveChangesAsync();

        return MapToEnterpriseDto(enterprise);
    }

    public async Task<EnterpriseDto1234De1> UpdateAsync(int id, UpdateEnterpriseDto1234De1 input)
    {
        var enterprise = await _dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
        if (enterprise == null)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);
        }

        await ValidateEnterpriseUniqueAsync(input.Name, input.TaxCode, id);

        enterprise.Name = input.Name;
        enterprise.TaxCode = input.TaxCode;
        enterprise.Address = input.Address;

        await _dbContext.SaveChangesAsync();

        return MapToEnterpriseDto(enterprise);
    }

    public async Task DeleteAsync(int id)
    {
        var enterprise = await _dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
        if (enterprise == null)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);
        }

        _dbContext.Enterprises.Remove(enterprise);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PagedResultDto1234De1<EnterpriseDto1234De1>> GetListAsync(FilterEnterpriseDto1234De1 input)
    {
        if (input.PageSize <= 0)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.PageSizeInvalid);
        }

        if (input.PageIndex <= 0)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.PageIndexInvalid);
        }

        var query = _dbContext.Enterprises.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim();
            query = query.Where(e => e.Name.Contains(keyword) || e.TaxCode.Contains(keyword));
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderBy(e => e.Id)
            .Skip((input.PageIndex - 1) * input.PageSize)
            .Take(input.PageSize)
            .Select(e => new EnterpriseDto1234De1
            {
                Id = e.Id,
                Name = e.Name,
                TaxCode = e.TaxCode,
                Address = e.Address
            })
            .ToListAsync();

        return new PagedResultDto1234De1<EnterpriseDto1234De1>
        {
            TotalItems = totalItems,
            PageSize = input.PageSize,
            PageIndex = input.PageIndex,
            Items = items
        };
    }

    public async Task<List<TopProductDto1234De1>> GetTopProductsAsync(int enterpriseId)
    {
        var enterpriseExists = await _dbContext.Enterprises.AnyAsync(e => e.Id == enterpriseId);
        if (!enterpriseExists)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);
        }

        var maxQuantity = await _dbContext.EnterpriseProducts
            .Where(ep => ep.EnterpriseId == enterpriseId)
            .MaxAsync(ep => (int?)ep.Quantity);

        if (maxQuantity == null)
        {
            return new List<TopProductDto1234De1>();
        }

        return await _dbContext.EnterpriseProducts
            .AsNoTracking()
            .Where(ep => ep.EnterpriseId == enterpriseId && ep.Quantity == maxQuantity)
            .Select(ep => new TopProductDto1234De1
            {
                Name = ep.Product.Name,
                Code = ep.Product.Code
            })
            .ToListAsync();
    }

    private async Task ValidateEnterpriseUniqueAsync(string name, string taxCode, int? currentId = null)
    {
        var nameExists = await _dbContext.Enterprises.AnyAsync(e =>
            e.Name == name && (!currentId.HasValue || e.Id != currentId.Value));

        if (nameExists)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNameExists);
        }

        var taxCodeExists = await _dbContext.Enterprises.AnyAsync(e =>
            e.TaxCode == taxCode && (!currentId.HasValue || e.Id != currentId.Value));

        if (taxCodeExists)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseTaxCodeExists);
        }
    }

    private static EnterpriseDto1234De1 MapToEnterpriseDto(Enterprise1234De1 enterprise)
    {
        return new EnterpriseDto1234De1
        {
            Id = enterprise.Id,
            Name = enterprise.Name,
            TaxCode = enterprise.TaxCode,
            Address = enterprise.Address
        };
    }
}
```

## 4. Giai thich logic them doanh nghiep

Truoc khi them:

- Check trung ten.
- Check trung ma so thue.

Sau do:

- Tao entity.
- Add vao DbContext.
- `SaveChangesAsync`.
- Map sang DTO de tra ve.

Khong nen:

- Cho phep client gui `Id`.
- Bo qua check trung va chi dua vao loi database.

## 5. Giai thich logic sua doanh nghiep

Khi sua:

- Tim doanh nghiep theo `id`.
- Neu khong co thi nem `UserFriendlyException`.
- Check trung ten/ma so thue, nhung bo qua chinh ban ghi dang sua.
- Cap nhat field.
- Save.

Dong quan trong:

```csharp
e.Name == name && e.Id != currentId.Value
```

Neu khong bo qua id hien tai, khi sua ma giu nguyen ten cu se bi bao trung sai.

## 6. Giai thich phan trang va keyword

Phan trang dung:

```csharp
Skip((PageIndex - 1) * PageSize).Take(PageSize)
```

`PageIndex` bat dau tu 1 vi de nguoi dung de hieu:

- Trang 1: bo qua 0 dong.
- Trang 2: bo qua `PageSize` dong.

Keyword dung:

```csharp
Name.Contains(keyword) || TaxCode.Contains(keyword)
```

Day la loc gan dung theo de bai.

## 7. Giai thich san pham nhap nhieu nhat

Can lay san pham co `Quantity` lon nhat cua mot doanh nghiep.

Neu co nhieu san pham cung so luong lon nhat, tra tat ca:

```text
Doanh nghiep 1:
- SP001: 50
- SP002: 50
- SP003: 20

Ket qua: SP001 va SP002
```

Dau ra chi gom `Name`, `Code` theo dung de.
