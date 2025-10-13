using System.Reflection;

namespace Maanfee.Lottie
{
    public class AnimationService
    {
        public AnimationService()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        private readonly Assembly _assembly;

        // دریافت لیست تمام انیمیشن‌های موجود
        public List<AnimationInfo> GetAvailableAnimations()
        {
            var resourceNames = _assembly.GetManifestResourceNames();
            var animationResources = resourceNames
                .Where(name => name.Contains("Assets.Animations") && name.EndsWith(".json"))
                .ToList();

            var Animations = new List<AnimationInfo>();
            foreach (var resource in animationResources)
            {
                var fileName = Path.GetFileName(resource.Replace("Maanfee.Lottie.Assets.Animations.", ""));
                var animationName = Path.GetFileNameWithoutExtension(fileName);

                Animations.Add(new AnimationInfo
                {
                    Name = animationName,
                    FullPath = resource,
                    FileName = fileName
                });
            }

            return Animations;
        }

        // خواندن محتوای یک انیمیشن خاص
        public async Task<string> GetAnimationContentAsync(string animationName)
        {
            var resourceName = $"Maanfee.Lottie.Assets.Animations.{animationName}.json";

            using var stream = _assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"Animation {animationName} not found");

            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        // بررسی وجود انیمیشن
        public bool AnimationExists(string animationName)
        {
            var resourceName = $"Maanfee.Lottie.Assets.Animations.{animationName}.json";
            return _assembly.GetManifestResourceNames().Contains(resourceName);
        }
    }
}
