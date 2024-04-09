using Questao2.DTO;
using Questao2.Models;
using System.Dynamic;
using System.Text.Json;

namespace Questao2.Rest;

public class JogosFutebolApiRest
{
    public async Task<ResponseGenerico<PageModel>> BuscarPartidasPorAnoETime(int year, string team, int page, int teamNumber = 1)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team{teamNumber}={team}&page={page}");

        var response = new ResponseGenerico<PageModel>();
        try
        {
            using (var client = new HttpClient())
            {
                var responseBrasilApi = client.Send(request);
                var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<PageModel>(contentResp);

                if (responseBrasilApi.IsSuccessStatusCode)
                {
                    response.CodigoHttp = responseBrasilApi.StatusCode;
                    response.DadosRetorno = objResponse;
                }
                else
                {
                    response.CodigoHttp = responseBrasilApi.StatusCode;
                    response.ErroRetorno = JsonSerializer.Deserialize<ExpandoObject>(contentResp);
                }
            }
                //  Block of code to try
        }
        catch (Exception e)
        {
            //  Block of code to handle errors
        }
        return response;
    }
}