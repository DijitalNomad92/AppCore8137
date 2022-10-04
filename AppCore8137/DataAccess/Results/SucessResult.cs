using AppCore8137.DataAccess.Results.Bases;

namespace AppCore8137.DataAccess.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
        }

        public SuccessResult() : base(true, "")
        {

        }
    }
}

/* Örnek kullanım:
SuccessResult result = new SuccessResult("Operation successful.");
Result result = new SuccessResult();
if (result.IsSuccessful) // result.IsSuccessful: true
{ 
    ... (burası çalışacak)
}
else
{
    ...
}
*/


