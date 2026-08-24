import { defineConfig } from 'vite';
import { resolve } from 'path';

export default defineConfig({
  build: {
    outDir: resolve(__dirname, 'src/Clight.LogoStudio.Wasm/wwwroot/dist'),
    emptyOutDir: true,
    lib: {
      entry: resolve(__dirname, 'src/frontend/main.js'),
      name: 'ClightStudio',
      fileName: () => 'app.min.js',
      formats: ['iife']
    },
    rollupOptions: {
      output: {
        assetFileNames: (assetInfo) => {
          if (assetInfo.name && assetInfo.name.endsWith('.css')) {
            return 'app.min.css';
          }
          return 'assets/[name]-[hash][extname]';
        }
      }
    }
  }
});
