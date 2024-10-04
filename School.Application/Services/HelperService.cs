using School.Core.Model;

namespace School.Application.Services;

public static class HelperService
{
    public static Sex ParseToSexFromDb(object obj)
    {
        Sex sex = Sex.Man;
        if (obj is Sex)
        {
            sex = (Sex)Enum.ToObject(typeof(Sex), obj);
        }

        if (obj is int)
        {
            if (Enum.IsDefined(typeof(Sex), obj))
                sex = (Sex)obj;
        }
        return sex;
    }
    
    public static string GetSexText(Sex sex)
    {
        return sex == Sex.Man ? "М" : "Ж";
    }

    public static string GetSexFromDb(object obj)
    {
        return GetSexText(ParseToSexFromDb(obj));
    }
}