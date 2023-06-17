using System.Text;

namespace ASPNetCoreIntroductionExercise.Service
{
    public class NumberService
    {
        public static async Task<IEnumerable<int>> Counter(int count)
        {
            var list = new List<int>();
            await Task.Run(() =>
            {
                for (int i = 1; i <= count; i++)
                {
                    list.Add(i);
                }
            });
            return list;
        }
    }
}
