using System.Net;
using System.Text.Json;

namespace WinForms;
public static class ErrorApi
{
    public static async Task<string> LeerMensajeAsync(HttpResponseMessage response)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.Forbidden:
                return "No tiene permiso para realizar esta acción.";
            case HttpStatusCode.Unauthorized:
                return "La sesión expiró o no es válida. Cierre sesión y vuelva a ingresar.";
            case HttpStatusCode.NotFound:
                return "El registro ya no existe. Actualice la pantalla.";
        }

        try
        {
            var cuerpo = await response.Content.ReadAsStringAsync();
            using var documento = JsonDocument.Parse(cuerpo);

            if (documento.RootElement.ValueKind == JsonValueKind.Object && documento.RootElement.TryGetProperty("error", out var error) && error.ValueKind == JsonValueKind.String)
            {
                return error.GetString() ?? "Error desconocido.";
            }
        }
        catch (JsonException)
        {
            //mensaje genérico
        }

        return $"El servidor respondió {(int)response.StatusCode} ({response.ReasonPhrase}).";
    }
}
