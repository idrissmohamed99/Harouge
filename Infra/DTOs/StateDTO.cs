namespace Infra.DTOs
{

    public enum StateContact
    {
        NatNum = 1,
        NumberAdministrative,
        PassportNumber
    }


    public enum ProductType
    {
        Medicines, //ادوية
        Accessories,// مستلزمات
    }
    public enum GenderState
    {
        Male = 1,
        Female,
    }

    public enum NeedRequestState
    {
        InProccess, // قيد الأدخال
        SendToKidnyUnit, // ارسال الوحدة الكلي
        RejectRequestKidnyUnit,// رفض من قبل وحدة الكلي
        SendToEmdad, // إرسال الي جهاز الامداد
        RejectRequestEmdad, // رفض من قبل جهاز الامداد
        AcceptNeedRequest, // القبول النهائي
        ClosingTheProccess, // صرف النهائي
    }

    public enum NeedRequestType
    {
        ProductFoodMedicines,
        ProductBrand,
        ProductFoodAccessories,
    }

    public enum NeedRequestProductState
    {
        InProccess, // قيد الأدخال
        AcceptProductKidnyUnit, // قبول الوحدة الكلي
        RejectProductKidnyUnit,// رفض من قبل وحدة الكلي
        RejectProductRequestEmdad, // رفض من قبل جهاز الامداد
        AcceptProductNeedRequest, // القبول النهائي
    }

    public enum ExchangePatientTypeState
    {
        ExchangeFoodMedicines = 1,
        ExchangeBrand,
        ExchangeFoodAccessories,

    }

    public enum ExchangeGallerieTypeState
    {
        ExchangeFoodMedicines = 1,
        ExchangeBrand,
        ExchangeFoodAccessories,

    }

    public enum CheckIsAdminState
    {
        IsAdmin = 1,
        IsNotAdmin,
        IsErrorCreateToken
    }

    public enum ModuleUserState
    {

    }


}
