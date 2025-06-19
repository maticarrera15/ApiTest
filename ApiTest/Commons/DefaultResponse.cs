namespace ApiTest.Commons
{
    public class DefaultResponse
    {
        public string? mensaje { get; set; }

        public object? response { get; set; }

        public DefaultResponse(object obj) { this.response = obj; }

        public DefaultResponse(string msg) { this.mensaje = msg; }

        public DefaultResponse(string mensaje, object obj) 
        {
            this.mensaje = mensaje;
            this.response = obj;
        }

    }
}
