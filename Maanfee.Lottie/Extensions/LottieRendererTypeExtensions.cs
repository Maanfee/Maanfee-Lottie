namespace Maanfee.Lottie
{
    internal static class LottieRendererTypeExtensions
    {
        internal static string ToStringValue(this LottieRendererType renderer)
        {
            return renderer switch
            {
                LottieRendererType.SVG => "svg",
                LottieRendererType.Canvas => "canvas",
                _ => "svg"
            };
        }
    }
}
