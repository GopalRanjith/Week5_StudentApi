namespace Week5_StudentApi.Strategies;

public class GpaGradeStrategy : IGradeStrategy
{
    public string Calculate(double score)
    {
        double gpa = score / 25;

        return gpa.ToString("0.0");
    }
}