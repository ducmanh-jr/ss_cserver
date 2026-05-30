# Viet exception, utils va constants

## 1. Tao `UserFriendlyException`

Duong dan:

```text
Exceptions/UserFriendlyException.cs
```

File nay dung de nem loi nghiep vu ma nguoi dung hieu duoc.

Vi sao can file nay:

- De bai yeu cau xu ly ngoai le bang `UserFriendlyException`.
- Khong nen tra loi he thong thô như stack trace cho client.
- Service co the nem loi ro rang: trung ten, khong tim thay doanh nghiep, page sai.

File nay khong nen chua:

- `DbContext`.
- Logic truy van database.
- Message constants dai.

Code:

```csharp
namespace NguyenVanA1234.Exceptions;

public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message)
    {
    }
}
```

## 2. Tao constants message

Duong dan:

```text
Constants/ErrorMessages1234De1.cs
```

File nay dung de gom cac message loi dung lai nhieu lan.

Vi sao can file nay:

- Tranh viet lap string loi o nhieu noi.
- Khi can sua message chi sua mot cho.

File nay khong nen chua:

- Logic `if`.
- Query LINQ.
- Cau hinh EF Core.

Code:

```csharp
namespace NguyenVanA1234.Constants;

public static class ErrorMessages1234De1
{
    public const string EnterpriseNotFound = "Khong tim thay doanh nghiep";
    public const string EnterpriseNameExists = "Ten doanh nghiep da ton tai";
    public const string EnterpriseTaxCodeExists = "Ma so thue da ton tai";
    public const string PageSizeInvalid = "PageSize phai lon hon 0";
    public const string PageIndexInvalid = "PageIndex phai lon hon 0";
}
```

## 3. Tao success messages neu muon

Duong dan:

```text
Constants/SuccessMessages1234De1.cs
```

File nay dung de gom message thanh cong.

Code:

```csharp
namespace NguyenVanA1234.Constants;

public static class SuccessMessages1234De1
{
    public const string CreateEnterpriseSuccess = "Them doanh nghiep thanh cong";
    public const string UpdateEnterpriseSuccess = "Sua doanh nghiep thanh cong";
    public const string DeleteEnterpriseSuccess = "Xoa doanh nghiep thanh cong";
}
```

## 4. Tao util normalize string

Duong dan:

```text
Utils/StringUtils1234De1.cs
```

File nay dung de chua ham xu ly string nho, dung chung.

Vi sao can file nay:

- Khi so sanh keyword, ten, ma so thue, nen trim va xu ly null gon gang.
- Neu de khong can nhieu util, giu file that nho.

File nay khong nen chua:

- Logic database.
- Logic them/sua/xoa doanh nghiep.

Code:

```csharp
namespace NguyenVanA1234.Utils;

public static class StringUtils1234De1
{
    public static string Normalize(string? value)
    {
        return value?.Trim() ?? string.Empty;
    }
}
```

## 5. Co nen tao middleware xu ly exception khong?

De thi yeu cau controller tra `IActionResult` va xu ly bang `UserFriendlyException`. Cach don gian, it rui ro trong phong thi:

- Service nem `UserFriendlyException`.
- Controller bat `UserFriendlyException` va tra `BadRequest`.
- Loi khong mong muon tra `StatusCode(500)`.

Khong nen them middleware phuc tap neu de khong yeu cau, vi de tang rui ro sai va ton thoi gian.
