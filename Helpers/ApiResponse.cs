namespace VetApp_BE.Helpers
{
    public class ApiResponse
    {

        public int code { get; set; }
        public bool succeeded { get; set; }
        public string errors { get; set; }
        public string message { get; set; }
        public ApiResponse()
        {
            this.code = 200;
            this.errors = null;
            this.message = "succeeded";
            this.succeeded = true;
        }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T data { get; set; }
    }

    public class ApiResponsePaging<T> : ApiResponse
    {
        public int page_number { get; set; }
        public int page_size { get; set; }
        public int first_page => 1;
        public int total_page => Convert.ToInt32(((double)total_record / (double)page_size));
        public int last_page => Convert.ToInt32(Math.Ceiling(((double)total_record / (double)page_size)));
        public int? next_page => page_number >= 1 && page_number < last_page ? (page_number + 1) : null;
        public int? previous_page => page_number - 1 >= 1 && this.page_number <= last_page ? (page_number - 1) : null;
        public int total_record { get; set; }

        public T data { get; set; }

        public ApiResponsePaging()
        {

            this.page_number = this.page_number;
            this.page_size = this.page_size;
            this.total_record = this.total_record;
            code = 200;
            succeeded = true;
            message = string.Empty;
            errors = string.Empty;

        }


    }

}
