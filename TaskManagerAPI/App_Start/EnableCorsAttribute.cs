namespace TaskManagerAPI
{
    internal class EnableCorsAttribute
    {
        private string v;
        private string headers;
        private string methods;

        public EnableCorsAttribute(string v, string headers, string methods)
        {
            this.v = v;
            this.headers = headers;
            this.methods = methods;
        }
    }
}