namespace CMS.Application.Models.Common
{

    public class ResultModel<TResult>
    {
        public ResultModel()
        {
            Errors = new List<ErrorModel>();
        }

        public bool IsSuccess => Errors.Count == 0;

        public IList<ErrorModel> Errors { get; set; }

        public TResult Result { get; set; }

        public static ResultModel<TResult> Success(TResult result) =>
            new ResultModel<TResult> { Result = result };

        public static ResultModel<TResult> Fail(string errorMessage) =>
            new ResultModel<TResult> { Errors = new List<ErrorModel> { new ErrorModel() { Message = errorMessage } } };
        public static ResultModel<TResult> Fail(ErrorModel error) =>
            new ResultModel<TResult> { Errors = new List<ErrorModel> { error } };

        public static ResultModel<TResult> Fail(List<ErrorModel> errors) =>
            new ResultModel<TResult> { Errors = errors };

        public static implicit operator ResultModel<TResult>(TResult result) =>
            Success(result);

        public static implicit operator ResultModel<TResult>(Task<TResult> result) =>
            Success(result.GetAwaiter().GetResult());

        public static implicit operator ResultModel<TResult>(ErrorModel error) =>
            Fail(error);

        public static implicit operator ResultModel<TResult>(List<ErrorModel> errors) =>
            Fail(errors);
    }



    public class ResultModel : ResultModel<object>
    {
        public ResultModel() : base() { }

        public static ResultModel Success(object result = null) =>
            new ResultModel { Result = result };

        public static ResultModel Fail(string errorMessage) =>
            new ResultModel
            {
                Errors = new List<ErrorModel> { new ErrorModel { Message = errorMessage } }
            };

        public static ResultModel Fail(ErrorModel error) =>
            new ResultModel { Errors = new List<ErrorModel> { error } };

        public static ResultModel Fail(List<ErrorModel> errors) =>
            new ResultModel { Errors = errors };
    }
}
