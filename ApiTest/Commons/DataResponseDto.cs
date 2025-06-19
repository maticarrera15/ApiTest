namespace ApiTest.Commons
{
    public class DataResponseDto<T>
    {

        public string? Status { get; set; }

        public string? Msg { get; set; }

        public T? Data { get; set; }

        public bool? exist { get; set; }
    }
}
