
export async function ensureDependencies() {
    if (typeof window.lottie === 'undefined') {
        await LoadScript('_content/Maanfee.Lottie/js/lottie.js');
    }
}

function LoadStyle(src) {
    return new Promise((resolve, reject) => {
        const Link = document.createElement('link');
        Link.rel = "stylesheet";
        Link.href = src;
        Link.onload = resolve;
        Link.onerror = reject;
        document.head.appendChild(Link);
    });
}

function LoadScript(src) {
    return new Promise((resolve, reject) => {
        const script = document.createElement('script');
        script.src = src;
        script.onload = resolve;
        script.onerror = reject;
        document.head.appendChild(script);
    });
}

// *****************************************************

export async function loadAnimation(container, animationPath, renderer = 'svg', loop = true, autoplay = true) {

    return lottie.loadAnimation({
        container: container,
        renderer: renderer,
        loop: loop,
        autoplay: autoplay,
        path: animationPath,
        rendererSettings: {
            progressiveLoad: true,
            hideOnTransparent: true
        }
    });
}

export async function loadAnimationWithJson(container, animationJson, renderer = 'svg', loop = true, autoplay = true) {

    const animationData = JSON.parse(animationJson);

    return lottie.loadAnimation({
        container: container,
        renderer: renderer,
        loop: loop,
        autoplay: autoplay,
        animationData: animationData,
        rendererSettings: {
            progressiveLoad: true,
            hideOnTransparent: true
        }
    });
}

export function play(animationInstance) {
    animationInstance?.play();
}

export function pause(animationInstance) {
    animationInstance?.pause();
}

export function stop(animationInstance) {
    animationInstance?.stop();
}

export function setSpeed(animationInstance, speed) {
    animationInstance?.setSpeed(speed);
}

export function goToAndStop(animationInstance, value) {
    animationInstance?.goToAndStop(value, true);
}

export function goToAndPlay(animationInstance, value) {
    animationInstance?.goToAndPlay(value, true);
}

export async function setLoop(animationInstance, loop) {
    if (animationInstance) {
        animationInstance.loop = loop;
    }
}

export function destroy(animationInstance) {
    animationInstance?.destroy();
}

// *****************************************************

export function registerEventListeners(animationInstance, dotNetRef) {
    animationInstance.addEventListener('enterFrame', function () {
        const state = {
            currentFrame: Math.floor(animationInstance.currentFrame),
            totalFrames: Math.floor(animationInstance.totalFrames),
            isPlaying: !animationInstance.isPaused
        };
        dotNetRef.invokeMethodAsync('OnAnimationUpdate', state.currentFrame, state.totalFrames, state.isPlaying);
    });

    animationInstance.addEventListener('complete', function () {
        dotNetRef.invokeMethodAsync('OnAnimationComplete');
    });
}

export function getAnimationState(animationInstance) {
    if (!animationInstance)
        return null;

    return {
        currentFrame: Math.floor(animationInstance.currentFrame),
        totalFrames: Math.floor(animationInstance.totalFrames),
        isPlaying: !animationInstance.isPaused
    };
}
