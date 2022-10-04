namespace AppCore8137.DataAccess.Results.Bases
{
    public abstract class Result
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        protected Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}

/* Örnek kullanım:
Result result = new Result()
{ 
    IsSuccessful = true,
    Message = "Operation successful."
};
Result result = new Result(true, "Operation successful.");
*/
