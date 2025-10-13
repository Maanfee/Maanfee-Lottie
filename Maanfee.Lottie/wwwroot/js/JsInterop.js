
export async function ensureDependencies() {
    if (typeof window.$ === 'undefined' || typeof window.$.fn.turn === 'undefined') {
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

export async function play(animationInstance) {
    if (animationInstance) {
        animationInstance.play();
    }
}

export async function pause(animationInstance) {
    if (animationInstance) {
        animationInstance.pause();
    }
}

export async function stop(animationInstance) {
    if (animationInstance) {
        animationInstance.stop();
    }
}

export async function setSpeed(animationInstance, speed) {
    if (animationInstance) {
        animationInstance.setSpeed(speed);
    }
}

export async function goToAndPlay(animationInstance, value) {
    if (animationInstance) {
        animationInstance.goToAndPlay(value);
    }
}

export async function destroy(animationInstance) {
    if (animationInstance) {
        animationInstance.destroy();
    }
}