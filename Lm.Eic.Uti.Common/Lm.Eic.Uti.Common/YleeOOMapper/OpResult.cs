namespace Lm.Eic.Uti.Common.YleeOOMapper
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class OpResult
    {
        private string message;

        /// <summary>
        /// 操作结果信息
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
        }

        private bool result = false;

        /// <summary>
        /// 操作结果
        /// </summary>
        public bool Result
        {
            get { return result; }
        }

        /// <summary>
        /// 对象的键值
        /// </summary>
        public decimal Id_Key { get; set; }

        public OpResult(string successMessage, bool result)
        {
            this.result = result;
            this.message = successMessage;
            if (!result)
                this.message = "您的操作失败！";
        }
        public OpResult(string errorMsg)
        {
            this.result = false;
            this.message = errorMsg;
        }
        public OpResult(string successMessage, bool result, decimal idKey)
        {
            this.result = result;
            this.message = successMessage;
            this.Id_Key = idKey;
            if (!result)
                this.message = "您的操作失败！";
        }

        public OpResult(string successMessage, string falseMessage, int record)
        {
            this.result = record > 0;
            this.message = successMessage;
            if (!result)
                this.message = falseMessage;
        }

        public OpResult(string successMessage, string falseMessage, bool result)
        {
            this.result = result;
            this.message = successMessage;
            if (!result)
                this.message = falseMessage;
        }

        public OpResult(string successMessage, string falseMessage, int record, decimal idKey)
        {
            this.result = record > 0;
            this.message = successMessage;
            this.Id_Key = idKey;
            if (!result)
                this.message = falseMessage;
        }

        /// <summary>
        /// 附加对象
        /// </summary>
        public object Attach { get; set; }

        /// <summary>
        /// 设定操作结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OpResult SetResult(string errorMsg)
        {
            return new OpResult(errorMsg);
        }
        /// <summary>
        /// 设定操作结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OpResult SetResult(string successMessage, bool result)
        {
            return new OpResult(successMessage, result);
        }
        public static OpResult SetResult(string successMessage, bool result, decimal idKey = 0)
        {
            return new OpResult(successMessage, result, idKey);
        }

        public static OpResult SetResult(string successMessage, string falseMessage, int record, decimal idKey)
        {
            return new OpResult(successMessage, falseMessage, record, idKey);
        }

        public static OpResult SetResult(string successMessage, string falseMessage, int record)
        {
            return new OpResult(successMessage, falseMessage, record);
        }
        public static OpResult SetResult(string successMessage, string falseMessage, bool result)
        {
            return new OpResult(successMessage, falseMessage, result);
        }
    }

    public  class OpMode
    {
        public const string Add = "add";
        public const string Edit = "edit";
        public const string Delete = "delete";
    }

}