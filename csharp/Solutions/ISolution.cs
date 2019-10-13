namespace LeetCode
{
    public interface ISolution
    {
        string Name { get; }
        string Link { get; }
    }

    public interface ISolution<TInput, TResult> : ISolution
    {
        (TInput, TResult)[] TestCases { get; }
        TResult Execute(TInput input);
    }
}