
namespace Week5_StudentApi.Strategies;

public class PercentageGradeStrategy : IGradeStrategy
{
    public string Calculate(double score)
    {
        return $"{score}%";
    }
}