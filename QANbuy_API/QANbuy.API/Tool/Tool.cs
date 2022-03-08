namespace QANbuy.API.Tool
{
    public static class Tool
    {
        public static string byteToStr(byte[] goodPic)
        {
            if (goodPic == null)
            {
                return "";
            }
            else
            {
                return System.Text.Encoding.Default.GetString(goodPic);
            }
        }
    }
}
