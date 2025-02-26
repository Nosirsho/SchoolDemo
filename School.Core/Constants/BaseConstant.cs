namespace School.Core.Constants;

public static class BaseConstant
{
    public class SysSettingType
    {
        public static readonly Guid String = new Guid("c6bc2289-451d-408b-b97f-35fa42974c05");
        public static readonly Guid Integer = new Guid("09166a2f-2541-4787-b6ba-0bd45941127d");
        public static readonly Guid Boolean = new Guid("b7bc1d4e-c906-4f32-83d9-76c016f82de6");
        public static readonly Guid DateTime = new Guid("6be94388-5f91-4634-b749-70259437bab3");
        public static readonly Guid Guid = new Guid("bcd76746-9bf8-4012-93bc-a4d1392c60cf");
    }
    public static Guid GetByName(string name)
    {
        switch (name.ToLower())
        {
            case "string":
                return SysSettingType.String;
            case "integer":
                return SysSettingType.Integer;
            case "boolean":
                return SysSettingType.Boolean;
            case "datetime":
                return SysSettingType.DateTime;
            case "guid":
                return SysSettingType.Guid;
            default:
                throw new ArgumentException($"Invalid SysSettingType name: {name}");
        }
    }
}