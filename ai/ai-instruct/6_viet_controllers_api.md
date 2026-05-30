# Viet controllers va API

## 1. Vai tro cua controller

Controller dung de:

- Nhan HTTP request.
- Validate `ModelState`.
- Goi service.
- Tra `IActionResult`.
- Chuyen `UserFriendlyException` thanh response de nguoi dung hieu.

Controller khong nen chua:

- Query EF Core truc tiep.
- Logic check trung ten/ma so thue.
- Logic tinh san pham nhap nhieu nhat.

## 2. Tao `EnterprisesController1234De1`

Duong dan:

```text
Controllers/EnterprisesController1234De1.cs
```

File nay lien he voi:

- `IEnterpriseService1234De1`
- DTO trong `Dtos/Enterprises`
- `UserFriendlyException`

Code:

```csharp
using Microsoft.AspNetCore.Mvc;
using NguyenVanA1234.Constants;
using NguyenVanA1234.Dtos.Enterprises;
using NguyenVanA1234.Exceptions;
using NguyenVanA1234.Services.Interfaces;

namespace NguyenVanA1234.Controllers;

[ApiController]
[Route("api/enterprises")]
public class EnterprisesController1234De1 : ControllerBase
{
    private readonly IEnterpriseService1234De1 _enterpriseService;

    public EnterprisesController1234De1(IEnterpriseService1234De1 enterpriseService)
    {
        _enterpriseService = enterpriseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnterpriseDto1234De1 input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _enterpriseService.CreateAsync(input);
            return Ok(new
            {
                Message = SuccessMessages1234De1.CreateEnterpriseSuccess,
                Data = result
            });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEnterpriseDto1234De1 input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _enterpriseService.UpdateAsync(id, input);
            return Ok(new
            {
                Message = SuccessMessages1234De1.UpdateEnterpriseSuccess,
                Data = result
            });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _enterpriseService.DeleteAsync(id);
            return Ok(new { Message = SuccessMessages1234De1.DeleteEnterpriseSuccess });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] FilterEnterpriseDto1234De1 input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _enterpriseService.GetListAsync(input);
            return Ok(result);
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("{enterpriseId:int}/top-products")]
    public async Task<IActionResult> GetTopProducts(int enterpriseId)
    {
        try
        {
            var result = await _enterpriseService.GetTopProductsAsync(enterpriseId);
            return Ok(result);
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}
```

## 3. Giai thich route

```csharp
[Route("api/enterprises")]
```

Tat ca API trong controller se bat dau bang `/api/enterprises`.

```csharp
[HttpPost]
```

Dung cho them moi.

```csharp
[HttpPut("{id:int}")]
```

Dung cho sua theo id.

```csharp
[HttpDelete("{id:int}")]
```

Dung cho xoa theo id.

```csharp
[HttpGet]
```

Dung cho danh sach doanh nghiep.

```csharp
[HttpGet("{enterpriseId:int}/top-products")]
```

Dung cho API san pham nhap nhieu nhat cua mot doanh nghiep.

## 4. Vi sao controller tra `IActionResult`

`IActionResult` giup controller linh hoat tra:

- `Ok(...)` khi thanh cong.
- `BadRequest(...)` khi loi validate hoac loi nghiep vu.
- `NotFound(...)` neu muon tach loi khong tim thay.
- `StatusCode(500, ...)` neu can bat loi he thong.

De thi yeu cau controller tra ve `IActionResult`, nen khong nen tra truc tiep DTO.

## 5. Co can try/catch khong?

Nen co try/catch don gian cho `UserFriendlyException`:

```csharp
catch (UserFriendlyException ex)
{
    return BadRequest(new { ex.Message });
}
```

Khong nen catch rong roi tra thanh cong. Loi nghiep vu phai tra loi ro rang.

## 6. Dang ky service trong `Program.cs`

Neu quen dong nay, API se loi DI:

```csharp
builder.Services.AddScoped<IEnterpriseService1234De1, EnterpriseService1234De1>();
```

Dong nay noi ASP.NET Core rang khi controller can interface, hay tao instance cua service implement.
