using Questao2.DTO;
using Questao2.Models;
using Questao2.Rest;

namespace Questao2.Services;

public class JogoService
{
    private JogosFutebolApiRest _jogoApi;
    public JogoService()
    {
        _jogoApi = new JogosFutebolApiRest();
    }

    public async Task<int> BuscarQuantidadeGolsPorAnoETime(int year, string team1)
    {

        int golsTime1 = BuscaQuantidadeGolsComoTime1Ou2(year, team1, 1);
        int golsTime2 = BuscaQuantidadeGolsComoTime1Ou2(year, team1, 2);
        return golsTime1 + golsTime2;
    }

    private int BuscaQuantidadeGolsComoTime1Ou2(int year, string team1,int teamNumber)
    {
        int quantidadeGols = 0;
        ResponseGenerico<PageModel> response;
   
        int pagina = 1;
        do
        {
            response = _jogoApi.BuscarPartidasPorAnoETime(year, team1, pagina, teamNumber).Result;

            quantidadeGols += response.DadosRetorno.Data.Sum(x => Int32.Parse(x.Team1Goals));

            pagina++;
        }
        while (response.DadosRetorno.TotalPaginas > response.DadosRetorno.Pagina);

        return quantidadeGols;
    }


}
