namespace FacebookSharp.Schemas.Graph
{
    /// <summary>
    /// Picture size
    /// </summary>
    /// <remarks>
    /// usually specified like http://graph.facebook.com/prabirshrestha/picture?type=large 
    /// </remarks>
    public enum PictureSizeType
    {
        /// <summary>
        /// 50x50 pixels
        /// </summary>
        Square,

        /// <summary>
        /// 50 pixels wide, variable height
        /// </summary>
        Small,

        /// <summary>
        /// 200 pixels wide, variable height
        /// </summary>
        Large
    }
}