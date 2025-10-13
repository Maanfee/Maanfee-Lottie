using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;

namespace Maanfee.Lottie
{
    public class LottieService : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference _module;
        private IJSObjectReference _animationInstance;
        private DotNetObjectReference<LottieService> _dotNetRef;
        private bool _disposed = false;

        public LottieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task EnsureModuleLoaded()
        {
            if (_module == null)
            {
                _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Maanfee.Lottie/js/JsInterop.js");

                // اطمینان از بارگذاری وابستگی‌ها
                await _module.InvokeVoidAsync("ensureDependencies");
            }
        }

        public async Task InitializeAsync(ElementReference containerElement, string AnimationUrl,
            string Renderer, bool Loop, bool AutoPlay)
        {
            await EnsureModuleLoaded();

            // اگر انیمیشن قبلی وجود دارد، آن را destroy کنید
            if (_animationInstance != null)
            {
                await _module.InvokeVoidAsync("destroy", _animationInstance);
                await _animationInstance.DisposeAsync();
            }

            _dotNetRef = DotNetObjectReference.Create(this);

            _animationInstance = await _module.InvokeAsync<IJSObjectReference>("loadAnimation", containerElement, AnimationUrl, Renderer, Loop, AutoPlay);

            if (OnLoaded.HasDelegate)
            {
                await OnLoaded.InvokeAsync();
            }
        }

        public async Task InitializeWithJsonAsync(ElementReference container, string animationJson,
            string renderer, bool loop, bool autoplay)
        {
            await EnsureModuleLoaded();

            // اگر انیمیشن قبلی وجود دارد، آن را destroy کنید
            if (_animationInstance != null)
            {
                await _module.InvokeVoidAsync("destroy", _animationInstance);
                await _animationInstance.DisposeAsync();
            }

            _dotNetRef = DotNetObjectReference.Create(this);

            _animationInstance = await _module.InvokeAsync<IJSObjectReference>("loadAnimationWithJson", container, animationJson, renderer, loop, autoplay);

            if (OnLoaded.HasDelegate)
            {
                await OnLoaded.InvokeAsync();
            }
        }

        public async Task Play()
        {
            if (_animationInstance != null)
            {
                await _module.InvokeVoidAsync("play", _animationInstance);
            }
        }

        public async Task Pause()
        {
            if (_animationInstance != null)
            {
                await _module.InvokeVoidAsync("pause", _animationInstance);
            }
        }

        public async Task Stop()
        {
            if (_animationInstance != null)
            {
                await _module.InvokeVoidAsync("stop", _animationInstance);
            }
        }

        public async Task SetSpeed(float speed)
        {
            if (_animationInstance != null)
            {
                await _module.InvokeVoidAsync("setSpeed", _animationInstance, speed);
            }
        }

        public async Task GoToAndPlay(int value)
        {
            if (_animationInstance != null)
            {
                await _module.InvokeVoidAsync("goToAndPlay", _animationInstance, value);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                if (_animationInstance != null)
                {
                    await _module.InvokeVoidAsync("destroy", _animationInstance);
                    await _animationInstance.DisposeAsync();
                    _animationInstance = null;
                }

                if (_module != null)
                {
                    await _module.DisposeAsync();
                    _module = null;
                }

                _dotNetRef?.Dispose();
                _dotNetRef = null;

                _disposed = true;
            }
        }

        // ********************************************

        public EventCallback OnLoaded { get; set; }
    }
}