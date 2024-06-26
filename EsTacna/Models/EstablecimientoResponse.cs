﻿using EsTacna.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EsTacna.Models
{
    public class EstablecimientoResponse
    {
        public int establecimiento_id { get; set; }
        public string clasificacionReal { get; set; }
        private readonly HttpClient httpClient;
        //
        private readonly EstablecimientoSaludRepositoryImpl objEstablecimientoRepo = new EstablecimientoSaludRepositoryImpl(new EsTacnaContext());
        //
        public EstablecimientoResponse()
        {
            httpClient = new HttpClient();
        }
        public async Task<EstablecimientoResponse> ObtenerEstablecimiento(int id)
        {
            // URL del endpoint de tu API
            string apiUrl = $"http://localhost:5000/api/establecimiento?id={id}";

            // Realizar la solicitud GET y obtener la respuesta
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            EstablecimientoResponse objEstablecimientoResponse = new EstablecimientoResponse();
            // Verificar si la respuesta es exitosa (código de estado 200)
            if (response.IsSuccessStatusCode)
            {
                // Leer la respuesta como una cadena de texto
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON a un objeto
                var responseData = JsonConvert.DeserializeObject<EstablecimientoResponse>(responseContent);

                // Utilizar los datos obtenidos
                var establecimientoId = responseData.establecimiento_id;
                var clasificacionReal = responseData.clasificacionReal;

                // Resto del código para trabajar con los datos obtenidos

                return responseData; // O devuelve la vista correspondiente
            }
            else
            {
                // Manejar el caso de error en la respuesta

                return objEstablecimientoResponse;
            }
        }

        [HttpGet]
        public async Task<List<EstablecimientoSalud>> GetEstablecimiento(int id)
        {
            // URL del endpoint de tu API
            string apiUrl = $"http://localhost:5000/api/recomendaciones";
            // Realizar la solicitud GET a la API
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            List<EstablecimientoSalud> ListEstablecimiento = new List<EstablecimientoSalud>();
            // Verificar si la solicitud fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Leer la respuesta como una cadena JSON
                string json = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON en un diccionario
                Dictionary<string, List<string>> recomendaciones = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);

                if (recomendaciones.ContainsKey(id.ToString()))
                {
                    List<string> recomendacionesUsuario = recomendaciones[id.ToString()];
                    foreach (var item in recomendacionesUsuario)
                    {

                        var objEstablecimiento = objEstablecimientoRepo.BuscarId(Convert.ToInt32(item));
                        ListEstablecimiento.Add(objEstablecimiento);
                    }
                    // Hacer algo con las recomendaciones del usuario, como pasarlas a la vista
                    return ListEstablecimiento;
                }
                else
                {
                    // Manejar el caso en que no se encuentren recomendaciones para el ID de usuario
                    return ListEstablecimiento;
                }
            }
            else
            {
                // Manejar el caso de error en la solicitud a la API
                return ListEstablecimiento;
            }
        }
    }
}
