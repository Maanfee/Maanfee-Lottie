using Maanfee.JsServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Maanfee.Lottie
{
    public class LottieService : JsService, IAsyncDisposable
    {
        public LottieService(IJSRuntime JSRuntime) : base(JSRuntime, "Maanfee.Lottie")
        {
        }

        private IJSObjectReference _AnimationInstance;
        protected DotNetObjectReference<LottieService> _DotNetRef;

        public async Task InitializeAsync(ElementReference containerElement, string AnimationUrl,
            string Renderer, bool Loop, bool AutoPlay)
        {
            await EnsureModuleLoaded();

            // اگر انیمیشن قبلی وجود دارد، آن را destroy کنید
            if (_AnimationInstance != null)
            {
                await _Module.InvokeVoidAsync("destroy", _AnimationInstance);
                await _AnimationInstance.DisposeAsync();
            }

            _DotNetRef = DotNetObjectReference.Create(this);

            _AnimationInstance = await _Module.InvokeAsync<IJSObjectReference>("loadAnimation", containerElement, AnimationUrl, Renderer, Loop, AutoPlay);

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
            if (_AnimationInstance != null)
            {
                await _Module.InvokeVoidAsync("destroy", _AnimationInstance);
                await _AnimationInstance.DisposeAsync();
            }

            _DotNetRef = DotNetObjectReference.Create(this);

            _AnimationInstance = await _Module.InvokeAsync<IJSObjectReference>("loadAnimationWithJson", container, animationJson, renderer, loop, autoplay);

            if (OnLoaded.HasDelegate)
            {
                await OnLoaded.InvokeAsync();
            }
        }

        public async Task Play()
        {
            if (_AnimationInstance != null)
            {
                await _Module.InvokeVoidAsync("play", _AnimationInstance);
            }
        }

        public async Task Pause()
        {
            if (_AnimationInstance != null)
            {
                await _Module.InvokeVoidAsync("pause", _AnimationInstance);
            }
        }

        public async Task Stop()
        {
            if (_AnimationInstance != null)
            {
                await _Module.InvokeVoidAsync("stop", _AnimationInstance);
            }
        }

        public async Task SetSpeed(float speed)
        {
            if (_AnimationInstance != null)
            {
                await _Module.InvokeVoidAsync("setSpeed", _AnimationInstance, speed);
            }
        }

        public async Task GoToAndPlay(int value)
        {
            if (_AnimationInstance != null)
            {
                await _Module.InvokeVoidAsync("goToAndPlay", _AnimationInstance, value);
            }
        }

        public new async ValueTask DisposeAsync()
        {
            if (!IsDisposed)
            {
                if (_AnimationInstance != null)
                {
                    await _Module.InvokeVoidAsync("destroy", _AnimationInstance);
                    await _AnimationInstance.DisposeAsync();
                    _AnimationInstance = null;
                }

                _DotNetRef?.Dispose();
                _DotNetRef = null;

                await base.DisposeAsync();
            }
        }

        // ********************************************

        public EventCallback OnLoaded { get; set; }
    }
}