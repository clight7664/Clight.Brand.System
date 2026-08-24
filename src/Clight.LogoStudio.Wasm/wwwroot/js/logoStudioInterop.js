// Production JS Interop helper for Clight Logo Studio
window.logoStudio = {
    // Downloads plain text or SVG file directly
    downloadTextFile: function (fileName, content, mimeType) {
        const blob = new Blob([content], { type: mimeType || 'text/plain;charset=utf-8' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = fileName;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
    },

    // Downloads binary array buffer
    downloadBinaryFile: function (fileName, base64Data, mimeType) {
        const byteCharacters = atob(base64Data);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: mimeType || 'application/octet-stream' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = fileName;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
    },

    // High fidelity client-side SVG to PNG rasterizer using HTML5 Canvas
    renderSvgToPngAndDownload: async function (svgString, width, height, fileName, backgroundColor) {
        return new Promise((resolve, reject) => {
            try {
                const canvas = document.createElement('canvas');
                canvas.width = width;
                canvas.height = height;
                const ctx = canvas.getContext('2d');

                const img = new Image();
                const svgBlob = new Blob([svgString], { type: 'image/svg+xml;charset=utf-8' });
                const url = URL.createObjectURL(svgBlob);

                img.onload = () => {
                    if (backgroundColor && backgroundColor !== 'transparent') {
                        ctx.fillStyle = backgroundColor;
                        ctx.fillRect(0, 0, width, height);
                    }
                    ctx.drawImage(img, 0, 0, width, height);
                    URL.revokeObjectURL(url);

                    canvas.toBlob((blob) => {
                        if (!blob) {
                            reject('Failed to convert canvas to blob');
                            return;
                        }
                        const downloadUrl = URL.createObjectURL(blob);
                        const a = document.createElement('a');
                        a.href = downloadUrl;
                        a.download = fileName || `clight-logo-${width}.png`;
                        document.body.appendChild(a);
                        a.click();
                        document.body.removeChild(a);
                        URL.revokeObjectURL(downloadUrl);
                        resolve(true);
                    }, 'image/png');
                };

                img.onerror = (e) => {
                    URL.revokeObjectURL(url);
                    reject('Image failed to load: ' + e);
                };

                img.src = url;
            } catch (err) {
                reject(err);
            }
        });
    },

    // Renders SVG to Base64 PNG data for bundle ZIP compiler
    renderSvgToBase64Png: async function (svgString, width, height, backgroundColor) {
        return new Promise((resolve, reject) => {
            try {
                const canvas = document.createElement('canvas');
                canvas.width = width;
                canvas.height = height;
                const ctx = canvas.getContext('2d');

                const img = new Image();
                const svgBlob = new Blob([svgString], { type: 'image/svg+xml;charset=utf-8' });
                const url = URL.createObjectURL(svgBlob);

                img.onload = () => {
                    if (backgroundColor && backgroundColor !== 'transparent') {
                        ctx.fillStyle = backgroundColor;
                        ctx.fillRect(0, 0, width, height);
                    }
                    ctx.drawImage(img, 0, 0, width, height);
                    URL.revokeObjectURL(url);
                    const dataUrl = canvas.toDataURL('image/png');
                    const base64 = dataUrl.split(',')[1];
                    resolve(base64);
                };

                img.onerror = (e) => {
                    URL.revokeObjectURL(url);
                    reject(e);
                };

                img.src = url;
            } catch (err) {
                reject(err);
            }
        });
    },

    // Generates a complete ZIP archive in-browser and triggers download
    exportAllAssetsAsZip: async function (svgString, fileName) {
        try {
            if (typeof JSZip === 'undefined') {
                console.error('JSZip is not loaded');
                return false;
            }

            const zip = new JSZip();
            const svgFolder = zip.folder("svg");
            const pngFolder = zip.folder("png");
            const webFolder = zip.folder("web");
            const guidelineFolder = zip.folder("brand-guideline");

            // Add SVG files
            svgFolder.file("clight-logo.svg", svgString);

            // Generate PNG sizes
            const sizes = [16, 32, 48, 64, 128, 180, 192, 256, 512, 1024];
            for (const size of sizes) {
                const b64 = await window.logoStudio.renderSvgToBase64Png(svgString, size, size, null);
                pngFolder.file(`clight-logo-${size}.png`, b64, { base64: true });
                
                if (size === 180) webFolder.file("apple-touch-icon.png", b64, { base64: true });
                if (size === 192) webFolder.file("android-chrome-192.png", b64, { base64: true });
                if (size === 512) webFolder.file("android-chrome-512.png", b64, { base64: true });
                if (size === 32) webFolder.file("favicon.png", b64, { base64: true });
            }

            // Web manifest
            const manifest = {
                name: "Clight Brand System",
                short_name: "Clight",
                icons: [
                    { src: "favicon.png", sizes: "32x32", type: "image/png" },
                    { src: "apple-touch-icon.png", sizes: "180x180", type: "image/png" },
                    { src: "android-chrome-192.png", sizes: "192x192", type: "image/png" },
                    { src: "android-chrome-512.png", sizes: "512x512", type: "image/png" }
                ],
                theme_color: "#111111",
                background_color: "#FAF9F6",
                display: "standalone"
            };
            webFolder.file("manifest.json", JSON.stringify(manifest, null, 2));

            // Guidelines
            guidelineFolder.file("README.md", "# Clight Brand System Assets\nGenerated from Clight Logo Studio.\nSymbol: Moon · Orchid · C");

            const content = await zip.generateAsync({ type: "blob" });
            const url = URL.createObjectURL(content);
            const a = document.createElement('a');
            a.href = url;
            a.download = fileName || "Clight.Brand.Assets.zip";
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            URL.revokeObjectURL(url);
            return true;
        } catch (err) {
            console.error('ZIP generation failed:', err);
            return false;
        }
    },

    // Copy string to clipboard
    copyToClipboard: async function (text) {
        if (navigator.clipboard) {
            await navigator.clipboard.writeText(text);
            return true;
        }
        return false;
    },

    // Theme toggle
    setTheme: function (theme) {
        if (theme === 'dark') {
            document.documentElement.classList.add('dark');
        } else {
            document.documentElement.classList.remove('dark');
        }
        localStorage.setItem('clight_theme', theme);
    },

    getTheme: function () {
        return localStorage.getItem('clight_theme') || 'light';
    }
};
