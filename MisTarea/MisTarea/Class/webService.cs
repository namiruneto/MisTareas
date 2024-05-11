using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MisTarea.Class
{
    
    public class webService
    {
        public class Categoria
        {
            public int Id { get; set; }
            public string Category { get; set; }
        }
        public async Task<List<Categoria>> CargarCategorias()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://eqtools.eqtax.com:8585/api/TechnicalTest");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Categoria> categorias = JsonConvert.DeserializeObject<List<Categoria>>(content);

                    return categorias;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
