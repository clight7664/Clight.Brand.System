/** @type {import('tailwindcss').Config} */
export default {
  darkMode: 'class',
  content: [
    './src/Clight.LogoStudio.Wasm/**/*.{razor,html,cshtml,cs}',
    './src/frontend/**/*.{js,ts,css}'
  ],
  theme: {
    extend: {
      fontFamily: {
        sans: ['Inter', 'Plus Jakarta Sans', 'system-ui', '-apple-system', 'BlinkMacSystemFont', 'Segoe UI', 'Roboto', 'sans-serif'],
        serif: ['Cormorant Garamond', 'Georgia', 'serif']
      },
      colors: {
        ink: {
          DEFAULT: '#111111',
          50: '#F9F9F9',
          100: '#F2F2F2',
          200: '#E5E5E5',
          800: '#1F1F1F',
          900: '#111111'
        },
        paper: {
          DEFAULT: '#FAF9F6',
          warm: '#F5F2EB',
          dark: '#161616'
        },
        mist: '#E0E0E0',
        deep: '#444444'
      }
    }
  },
  plugins: []
};
