

using Questao2.Rest;
using Questao2.Services;





public class Program
{

    
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        GetTotalScoredGoals(teamName, year);


        teamName = "Chelsea";
        year = 2014;

        GetTotalScoredGoals(teamName, year);

       // 22 32 9
        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async void GetTotalScoredGoals(string team, int year)
    {
        JogoService service = new JogoService();
        int totalGoals = await service.BuscarQuantidadeGolsPorAnoETime(year, team);

        Console.WriteLine("Team " + team + " scored " + totalGoals.ToString() + " goals in " + year);
       
    }

}