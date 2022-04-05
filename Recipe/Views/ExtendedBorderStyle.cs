namespace Recipe.Views
{
    public enum ExtendedBorderStyle
    {
        /// <summary>
        ///  No border.
        /// </summary>
        None = 0,

        /// <summary>
        ///  A single-line border.
        /// </summary>
        FixedSingle = 1,

        /// <summary>
        ///  A three-dimensional border.
        /// </summary>
        Fixed3D = 2,

        /// <summary>
        ///  An underline border
        /// </summary>
        UnderLined = 3,

        /// <summary>
        /// A drawn line defined by the BorderColor, BorderSize and BorderRadius properties
        /// </summary>
        DrawnBorder = 4,
    }
}