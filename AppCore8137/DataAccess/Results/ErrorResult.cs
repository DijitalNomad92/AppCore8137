using AppCore8137.DataAccess.Results.Bases;

namespace AppCore8137.DataAccess.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false, "")
        {

        }
    }
}

/* Örnek kullanım:
Result result = new ErrorResult("Record exists in database!");
if (result.IsSuccessful) // result.IsSuccessful: false
{ 
    ...
}
else 
{ 
    ... (burası çalışacak)
}
*/
