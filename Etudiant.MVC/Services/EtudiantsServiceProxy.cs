using Etudiants.MVC.Interfaces;
using Etudiants.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace Etudiants.MVC.Services
{
    public class EtudiantsServiceProxy : IEtudiantsService
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<EtudiantsServiceProxy> _logger;
        private const string _etudiantsApiUrl = "api/etudiants/";

        public EtudiantsServiceProxy(HttpClient httpClient, ILogger<EtudiantsServiceProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task Ajouter(Etudiant etudiant)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(etudiant), Encoding.UTF8, "application/json");

            var reponse = await _httpClient.PostAsync(_etudiantsApiUrl, content);

            _ = LogReponseAPI(reponse.Content);
     
        }

        public async Task<IEnumerable<Etudiant>> ObtenirTout()
        {
            var reponse = await _httpClient.GetAsync(_etudiantsApiUrl);

            _ = LogReponseAPI(reponse.Content);

            return await reponse.Content.ReadFromJsonAsync<IEnumerable<Etudiant>>();
        }

        private async Task LogReponseAPI(HttpContent httpContent)
        {
            var contenuReponse = await httpContent.ReadAsStringAsync();

            _logger.LogInformation($"Réponse de l'appel de l'API étudiant : {contenuReponse}");
        }
    }
}
