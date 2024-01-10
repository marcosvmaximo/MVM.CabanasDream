namespace MVM.CabanasDream.Festas.API.Controllers.Common
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }

        public int HttpCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }

        public static BaseResponse<T> SucessResponse(
            T? data,
            int httpCode = 200,
            string message = "Requisição enviada com sucesso.",
            bool success = true)
        {
            return new BaseResponse<T>()
            {
                HttpCode = httpCode,
                Message = message,
                Success = success,
                Data = data
            };
        }

        public static BaseResponse<Dictionary<string, object>> FailureResponse(
            Dictionary<string, object>? errors,
            int httpCode = 400,
            string message = "Ocorreu uma falha ao enviar a requisição.",
            bool success = false)
        {
            return new BaseResponse<Dictionary<string, object>>()
            {
                HttpCode = httpCode,
                Message = message,
                Success = success,
                Data = errors
            };
        }
        
        public static BaseResponse<T> FailureResponse(
            T errors,
            int httpCode = 400,
            string message = "Ocorreu uma falha ao enviar a requisição.",
            bool success = false)
        {
            return new BaseResponse<T>()
            {
                HttpCode = httpCode,
                Message = message,
                Success = success,
                Data = errors
            };
        }
    }
}